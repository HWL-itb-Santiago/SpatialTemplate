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
    private SpatialInteractable OnAction;

    [SerializeField]
    private SpatialInteractable OnRelease;

    [SerializeField]
    private HoverObject objectToHover;

    [SerializeField]
    private GrabObject objectToGrab;
    // Start is called before the first frame update
    void Start()
    {
        OnRelease.enabled = false;
        OnAction.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractState != newInteractState)
            Interactable();
    }

    public void Interactable()
    {
        objectToHover?.ChangeStateObject(newInteractState);
        OnAction.enabled = newInteractState;
        OnRelease.enabled = objectToGrab.isGrabbing;
        InteractState = newInteractState;
    }
}
