using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill30001 : SkillObject
{
    public override void Init(object obj, Skill skill)
    {
        base.Init(obj, skill);

        StartCoroutine(skill.BlendMaterialAsync(skill.Subject.Entity.MeshRenderer, 0f, 0.15f, materialSystem.ChangedMaterials));
    }
}
