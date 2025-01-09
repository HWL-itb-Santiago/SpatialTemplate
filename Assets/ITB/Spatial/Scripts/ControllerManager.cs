using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class ControllerManager : MonoBehaviour
{
    static public ControllerManager Instance;
    [SerializeField]
    private LayerMask interactableLayer;

    [SerializeField]
    public float hoverIntesity;

    private GameObject lastHighlightedObject = null;

    public IAvatar avatar;

    [SerializeField]
    private SpatialInteractable interactable;

    public bool isXR = false;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SpatialBridge.actorService.localActor.avatar.isBodyLoaded)
        {
            avatar = SpatialBridge.actorService.localActor.avatar;
        }
    }

    private void FixedUpdate()
    {
        if (avatar != null)
        {
            RayCastHit();
        }
        SpatialBridge.coreGUIService.DisplayToastMessage(Input.mousePresent.ToString());
    }
    private void RayCastHit()
    {
        Ray rayHand;
        RaycastHit hitHand;
        if (SpatialBridge.cameraService.xrCameraMode == XRCameraMode.FirstPerson && Input.mousePresent == false)
        {
            isXR = true;
            Transform rightHandTransform = avatar.GetAvatarBoneTransform(HumanBodyBones.RightHand);

            rayHand = new Ray(rightHandTransform.position, rightHandTransform.up);
        }

        else
        {
            rayHand = SpatialBridge.cameraService.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(rayHand, out hitHand, 10f, interactableLayer))
        {
            if (lastHighlightedObject != hitHand.collider.gameObject)
            {
                InteractableObject lastObject = lastHighlightedObject ? lastHighlightedObject.GetComponent<InteractableObject>() : null;
                //HoverObject lastHover = lastHighlightedObject ? lastHighlightedObject.GetComponent<HoverObject>() : null;
                InteractableObject hoverObject;
                if (lastObject != null)
                {
                    lastObject.newInteractState = false;
                    //lastHover.OffHit();
                    //interactable = lastHighlightedObject.GetComponent<SpatialInteractable>();
                    //interactable.enabled = false;
                }
                hoverObject = hitHand.collider.gameObject.GetComponent<InteractableObject>();

                if (hoverObject != null)
                {
                    hoverObject.newInteractState = true;
                    //hoverObject.HitRayCast(hoverIntesity);
                    lastHighlightedObject = hitHand.collider.gameObject;

                    //interactable = lastHighlightedObject.GetComponent<SpatialInteractable>();
                    //interactable.enabled = true;

                    SpatialBridge.inputService.PlayVibration(0.15f, 0.5f, 0.05f);
                }

            }
        }
        else
        {
            if (lastHighlightedObject != null)
            {
                InteractableObject lastObject = lastHighlightedObject.GetComponent<InteractableObject>();
                if (lastObject != null)
                {
                    lastObject.newInteractState = false;
                    //lastObject.OffHit();
                    //if (interactable != null)
                    //    interactable.enabled = false;
                }
                lastHighlightedObject = null;
                //interactable = null;
            }
        }
    }
}