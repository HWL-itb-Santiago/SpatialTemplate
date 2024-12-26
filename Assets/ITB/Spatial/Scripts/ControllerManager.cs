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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RayCastHit();
        if (SpatialBridge.actorService.localActor.avatar.isBodyLoaded)
        {
            SpatialBridge.cameraService.forceFirstPerson = true;
        }
    }

    private void RayCastHit()
    {
        Ray ray = SpatialBridge.cameraService.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactableLayer))
        {
            //SpatialBridge.coreGUIService.DisplayToastMessage("¡HIT!");
            hoverObject = hit.collider.gameObject.GetComponent<HoverObject>();

            if (lastHighlightedObject != hit.collider.gameObject && hoverObject != null && hoverObject.isEnabled)
            {
                hoverObject.HitRayCast(hoverIntesity);
                lastHighlightedObject = hit.collider.gameObject;
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
                }
                lastHighlightedObject = null;
            }
        }
    }
}
