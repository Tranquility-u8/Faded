using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomRenderPass : ScriptableRenderPass
{
    [SerializeField] private Material effectMaterial = null;
    private RenderTargetIdentifier source;
    private RenderTargetHandle tempTexture;

    public CustomRenderPass()
    {
        tempTexture.Init("_TempTexture");
    }

    public void SetMaterial(Material material)
    {
        this.effectMaterial = material;
    }

    public void Setup(RenderTargetIdentifier source)
    {
        this.source = source;
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (effectMaterial == null)
        {
            Debug.LogError("Effect Material has not been set.");
            return;
        }

        CommandBuffer cmd = CommandBufferPool.Get("CustomRenderPass");

        RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
        opaqueDesc.depthBufferBits = 0;

        cmd.GetTemporaryRT(tempTexture.id, opaqueDesc, FilterMode.Bilinear);
        Blit(cmd, source, tempTexture.Identifier(), effectMaterial);
        Blit(cmd, tempTexture.Identifier(), source);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}