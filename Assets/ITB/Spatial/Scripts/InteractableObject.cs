using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    private bool InteractState = false;
    public bool newInteractState = false;

    [SerializeField]
    private SpatialInteractable OnAction = null;

    [SerializeField]
    private SpatialInteractable OnRelease = null;

    [SerializeField]
    private HoverObject objectToHover = null;

    [SerializeField]
    private GrabObject objectToGrab = null;
    // Start is called before the first frame update
    void Start()
    {
        if (OnRelease != null)
            OnRelease.enabled = false;
        if (OnAction != null)
            OnAction.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (InteractState != newInteractState)
            Interactable();
    }
    public void Interactable()
    {
        if (objectToHover != null)
            objectToHover.ChangeStateObject(newInteractState);
        if (OnAction != null)
            OnAction.enabled = newInteractState;
        if (OnRelease != null && objectToGrab != null)
            OnRelease.enabled = objectToGrab.isGrabbing;
        InteractState = newInteractState;
        Debug.Log(InteractState);
    }
}
