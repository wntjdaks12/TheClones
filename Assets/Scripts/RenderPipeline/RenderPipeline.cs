using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RenderPipeline
{
    public enum RendererDataType
    {
        ForwardRenderer = 0,
        CrackSpaceRenderer = 1
    }

    public static void ChangeRenderPipeline(RendererDataType rendererDataType)
    {
        UniversalRenderPipelineAsset urpAsset = Resources.Load<UniversalRenderPipelineAsset>("UniversalRP-HighQuality");
        Type typ = typeof(UniversalRenderPipelineAsset);
        FieldInfo type = typ.GetField("m_DefaultRendererIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        int currentRendererIndex = (int)type.GetValue(urpAsset);
        type.SetValue(urpAsset, (int)rendererDataType);
    }
}
