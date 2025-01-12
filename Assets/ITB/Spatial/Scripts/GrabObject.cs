using System.Collections;
using UnityEngine;
using SpatialSys.UnitySDK;
using UnityEngine.XR;
using TMPro;

public class GrabObject : MonoBehaviour
{
    private IAvatar avatar = null;

    Rigidbody rb;
    public bool isGrabbing = false;
    private float zOffset;
    private Vector3 refVelocity;

    [SerializeField]
    private float smoothDamp;

    [SerializeField]
    private float xrOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        refVelocity = Vector3.zero;
        if (ControllerManager.Instance.avatar != null && avatar == null)
        {
            avatar = ControllerManager.Instance.avatar;
        }
    }

    private void Update()
    {
        if (ControllerManager.Instance.avatar != null && avatar == null)
        {
            avatar = ControllerManager.Instance.avatar;
        }
    }

    public void OnGrabObject()
    {
        if (!isGrabbing)
        {
            isGrabbing = true;
            // Calcular el offset inicial de profundidad desde la cámara
            //Vector3 mouseWorldPosition = SpatialBridge.cameraService.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            zOffset = Vector3.Distance(transform.position, SpatialBridge.cameraService.position);

            StartCoroutine(GrabCoroutine());
        }
    }

    private IEnumerator GrabCoroutine()
    {
        while (isGrabbing)
        {
            Vector3 newTransform;
            Vector3 targetPosition;

            if (ControllerManager.Instance.isXR)
            {
                Transform bone = avatar.GetAvatarBoneTransform(HumanBodyBones.RightHand);
                // Obtener la posición del hueso de la mano derecha en VR
                targetPosition = bone.position + (bone.up * xrOffset);
                newTransform = Vector3.SmoothDamp(transform.position, targetPosition, ref refVelocity, smoothDamp);

                transform.SetParent(bone);
            }
            else
            {
                // Calcular posición en World Space basada en el mouse y zOffset
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zOffset);
                targetPosition = SpatialBridge.cameraService.ScreenToWorldPoint(mousePosition);

                // Suavizar la posición del objeto
                newTransform = Vector3.SmoothDamp(transform.position, targetPosition, ref refVelocity, smoothDamp);
            }

            // Mover el objeto a la nueva posición
            transform.position = newTransform;

            //Desactivar colisiones mientras se mueve
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            if (rb != null)
            {
                rb.useGravity = false;
            }


            yield return null;
        }
    }

    public void OnDropObject()
    {
        if (isGrabbing)
        {
            isGrabbing = false;
            StopCoroutine(GrabCoroutine());

            if (ControllerManager.Instance.isXR)
            {
                transform.SetParent(null);
            }

            //Restaurar las colisiones
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            if (rb != null)
            {
                rb.useGravity = true;
            }
        }
    }
}
