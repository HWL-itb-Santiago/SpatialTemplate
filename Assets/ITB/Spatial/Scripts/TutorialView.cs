using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class TutorialView : MonoBehaviour
{
    private IAvatar avatar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerManager.Instance != null && ControllerManager.Instance.avatar != null)
            avatar = ControllerManager.Instance.avatar;
        if (avatar != null)
            LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        gameObject.transform.LookAt(avatar.position);
    }
}
