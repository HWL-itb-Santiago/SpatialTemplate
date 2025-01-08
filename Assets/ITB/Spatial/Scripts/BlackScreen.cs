using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;
public class BlackScreen : MonoBehaviour
{
    private IAvatar avatar = null;

    private Vector3 lastAvatarPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerManager.avatar != null && avatar == null)
        {
            avatar = ControllerManager.avatar;
            lastAvatarPosition = avatar.position;
        }
    }

    private void FixedUpdate()
    {

        if (avatar != null && avatar.position != lastAvatarPosition)
        {
            OnAvatarMove();
        }
    }
    private void OnAvatarMove()
    {
        lastAvatarPosition = avatar.position;
        StartCoroutine(nameof(EnabledMesh));
    }

    private IEnumerator EnabledMesh()
    {
        float duration = 0.35f;
        gameObject.GetComponent<Canvas>().enabled = true;
        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
