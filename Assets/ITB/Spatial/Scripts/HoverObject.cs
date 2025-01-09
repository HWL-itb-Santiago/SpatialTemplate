using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;
using UnityEngine.Rendering;

public class HoverObject : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;

    [SerializeField]
    private MeshRenderer render;

    [SerializeField]
    private Animator animator;
    private void Start()
    {
    }

    private IEnumerator ApplyHighlight(float hoverIntensity)
    {
        animator.SetTrigger("OnHover");
        yield return null;
        //// Cambiar el material, color o activar un shader para resaltar el objeto.
        //if (render)
        //{
        //    Material material;
        //    render.enabled = true;

        //    material = render.material;
        //    if (material != null)
        //    {
        //        material.EnableKeyword("_EMISSION");
        //        material.SetColor("_EmissionColor", hoverColor * hoverIntensity); // Ajusta la intensidad con el multiplicador.
        //        Debug.Log(material);
        //        SpatialBridge.coreGUIService.DisplayToastMessage("¡El objeto tiene un Renderer!");
        //    }
        //}
        //else
        //{
        //    SpatialBridge.coreGUIService.DisplayToastMessage("¡El objeto no tiene un Renderer!");
        //}
        //yield return null;
    }

    private IEnumerator ResetHighlight()
    {
        animator.SetTrigger("OffHover");
        yield return null;
        //// Restaurar el material o color original.
        //if (render)
        //{
        //    render.enabled = false;
        //    render.material.DisableKeyword("_EMISSION");
        //}
        //yield return null;
    }

    public void ChangeStateObject(bool state)
    {
        float hoverIntensity = ControllerManager.Instance.hoverIntesity;
        if (state == true)
            StartCoroutine(ApplyHighlight(hoverIntensity));
        else
            StartCoroutine(ResetHighlight());
    }

    //public void OffHit()
    //{
    //    StartCoroutine(ResetHighlight());
    //}
}
