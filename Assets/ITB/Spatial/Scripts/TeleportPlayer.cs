using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    private IAvatar avatar = null;

    [SerializeField]
    private Transform sitDown = null;

    [SerializeField]
    private Transform goOut = null;

    [SerializeField]
    private Transform teleportTo = null;

    //[SerializeField]
    //private GameObject target;

    //[SerializeField]
    //private Transform targetSit;

    //private Vector3 targetPosition;
    //private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (teleportTo == null)
            teleportTo = sitDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerManager.Instance.avatar != null && avatar == null)
        {
            avatar = ControllerManager.Instance.avatar;
        }
    }

    public void teleportIn()
    {
        StartCoroutine(Teleport(teleportTo, sitDown));
    }

    public void teleportOut()
    {
        StartCoroutine(Teleport(goOut));
    }
    private IEnumerator Teleport(Transform targetPosition = null, Transform targetSit = null)
    {
        Quaternion targetRotation = targetPosition.rotation;
        //float time = 0f;
        //float duration = 0.2f; // Duración del teletransporte.

        //Vector3 startPosition = avatar.position;

        //while (time <= duration)
        //{
        //    // Calcular interpolación lineal basada en el tiempo.
        //    float t = time / duration;
        //    Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);

        //    // Aplicar nueva posición al avatar.
        //    avatar.SetPositionRotation(newPosition, targetRotation);

        //    time += Time.deltaTime;
        //    yield return null;
        //}

        // Asegurar la posición final exacta.
        if (targetSit != null)
        {
            avatar.Sit(targetSit);
        }
        else
        {
            avatar.SetPositionRotation(targetPosition.position, targetRotation);
        }
        yield return null;
    }
}
 