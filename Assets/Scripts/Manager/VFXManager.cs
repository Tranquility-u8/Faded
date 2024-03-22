using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class VFXManager : MonoBehaviour
{
    public Material postProcessingMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (postProcessingMaterial != null)
        {
            Graphics.Blit(src, dest, postProcessingMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}
