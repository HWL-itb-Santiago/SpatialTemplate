using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class ControllerManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask interactableLayer;

    [SerializeField]
    private float hoverIntesity;

    private GameObject lastHighlightedObject = null;

    private HoverObject hoverObject;

    static public IAvatar avatar;

    private SpatialInteractable interactable;

    static public RaycastHit hitHand;

    static public bool isXR = false;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SpatialBridge.actorService.localActor.avatar.isBodyLoaded)
        {
            avatar = SpatialBridge.actorService.localActor.avatar;
            SpatialBridge.cameraService.forceFirstPerson = true;
        }
        if (avatar != null)
        {
            RayCastHit();
        }
    }

    private void RayCastHit()
    {
        Ray rayHand;
        if (Input.mousePresent)
        {
            rayHand = SpatialBridge.cameraService.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            isXR = true;
            Transform rightHandTransform = avatar.GetAvatarBoneTransform(HumanBodyBones.RightHand);
            // Vector inicial que apunta hacia arriba
            Vector3 originalVector = rightHandTransform.up;

            // Rotación de 15 grados alrededor del eje Z
            Quaternion rotation = Quaternion.Euler(0, 0, 2);

            // Aplica la rotación al vector
            Vector3 rotatedVector = rotation * originalVector;

            rayHand = new Ray(rightHandTransform.position, rotatedVector);
        }


        if (Physics.Raycast(rayHand, out hitHand, Mathf.Infinity, interactableLayer))
        {
            if (lastHighlightedObject != hitHand.collider.gameObject)
            {
                HoverObject lastHover = lastHighlightedObject ? lastHighlightedObject.GetComponent<HoverObject>() : null;
                if (lastHover != null)
                {
                    lastHover.OffHit();
                    interactable = lastHighlightedObject.GetComponent<SpatialInteractable>();
                    interactable.enabled = false;
                }
                hoverObject = hitHand.collider.gameObject.GetComponent<HoverObject>();

                if (hoverObject != null)
                {
                    hoverObject.HitRayCast(hoverIntesity);
                    lastHighlightedObject = hitHand.collider.gameObject;

                    interactable = lastHighlightedObject.GetComponent<SpatialInteractable>();
                    interactable.enabled = true;

                    SpatialBridge.inputService.PlayVibration(0.2f, 0.1f, 0.12f);
                }

            }
        }
        else
        {
            if (lastHighlightedObject != null)
            {
                HoverObject lastObject = lastHighlightedObject.GetComponent<HoverObject>();
                if (lastObject != null)
                {
                    lastObject.OffHit();
                    if (interactable != null)
                        interactable.enabled = false;
                }
                lastHighlightedObject = null;
                interactable = null;
            }
        }
    }
}