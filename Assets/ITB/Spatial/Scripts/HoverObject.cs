using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class HoverObject : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;

    private Renderer render;

    public bool isEnabled = true;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void ApplyHighlight(float hoverIntensity)
    {
        // Cambiar el material, color o activar un shader para resaltar el objeto.
        if (render)
        {
            render.material.EnableKeyword("_EMISSION");
            render.material.SetColor("_EmissionColor", hoverColor * hoverIntensity); // Ajusta la intensidad con el multiplicador.
        }
        else
        {
            SpatialBridge.coreGUIService.DisplayToastMessage("¡El objeto no tiene un Renderer!");
        }
    }

    private void ResetHighlight()
    {
        // Restaurar el material o color original.
        if (render)
        {
            render.material.DisableKeyword("_EMISSION");
        }
    }

    public void HitRayCast(float hoverIntensity)
    {
        ApplyHighlight(hoverIntensity);
    }

    public void OffHit()
    {
        ResetHighlight();
    }

    public void EnableToInteracte()
    {
        isEnabled = true;
    }

    public void DisableTointeract()
    {
        isEnabled = false;
    }
}
