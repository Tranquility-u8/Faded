using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class CustomRenderFeature : ScriptableRendererFeature
{
    [SerializeField]
    private Material effectMaterial;

    private CustomRenderPass customRenderPass;

    public override void Create()
    {
        customRenderPass = new CustomRenderPass();
        customRenderPass.SetMaterial(effectMaterial);
    }
    public override void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
    {
        customRenderPass.Setup(renderer.cameraColorTarget);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (effectMaterial == null)
        {
            Debug.LogWarning("Effect Material has not been assigned in CustomRenderFeature.");
            return;
        }

        //customRenderPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(customRenderPass);
    }
}