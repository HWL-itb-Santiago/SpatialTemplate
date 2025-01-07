using System.Collections;
using UnityEngine;
using SpatialSys.UnitySDK;

public class GrabObject : MonoBehaviour
{
    private IAvatar avatar = null;
    private float zOffset;
    private bool isGrabbing = false;
    private Vector3 refVelocity;

    [SerializeField]
    private float smoothDamp = 0.1f;

    private void Start()
    {
        refVelocity = Vector3.zero;
        if (ControllerManager.avatar != null && avatar == null)
        {
            avatar = ControllerManager.avatar;
        }
    }

    private void Update()
    {
        if (ControllerManager.avatar != null && avatar == null)
        {
            avatar = ControllerManager.avatar;
        }
    }

    public void OnGrabObject()
    {
        if (!isGrabbing)
        {
            isGrabbing = true;

            // Calcular el offset inicial de profundidad desde la cámara
            Vector3 mouseWorldPosition = SpatialBridge.cameraService.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            zOffset = Vector3.Distance(transform.position, SpatialBridge.cameraService.position);

            StartCoroutine(GrabCoroutine());
        }
    }

    private IEnumerator GrabCoroutine()
    {
        while (isGrabbing)
        {
            Vector3 newTransform;

            if (ControllerManager.isXR)
            {
                // Obtener la posición del hueso de la mano derecha en VR
                Transform bone = avatar.GetAvatarBoneTransform(HumanBodyBones.RightHand);
                newTransform = bone.position;

                transform.SetParent(bone);
            }
            else
            {
                // Calcular posición en World Space basada en el mouse y zOffset
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zOffset);
                Vector3 targetPosition = SpatialBridge.cameraService.ScreenToWorldPoint(mousePosition);

                // Suavizar la posición del objeto
                newTransform = Vector3.SmoothDamp(transform.position, targetPosition, ref refVelocity, smoothDamp);
            }

            // Mover el objeto a la nueva posición
            transform.position = newTransform;

            // Desactivar colisiones mientras se mueve
            gameObject.GetComponent<BoxCollider>().isTrigger = true;

            yield return null;
        }
    }

    public void OnDropObject()
    {
        if (isGrabbing)
        {
            isGrabbing = false;
            StopCoroutine(GrabCoroutine());

            if (ControllerManager.isXR)
            {
                transform.SetParent(null);
            }

            // Restaurar las colisiones
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
