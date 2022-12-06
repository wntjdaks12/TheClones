using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackSpaceDoor : SkillObject
{
    public void asd()
    {
        RenderPipeline.ChangeRenderPipeline(RenderPipeline.RendererDataType.CrackSpaceRenderer);
    }
}
