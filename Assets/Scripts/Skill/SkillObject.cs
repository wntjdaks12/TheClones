using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : EntityObject
{
    [SerializeField] private MaterialSystem materialSystem;

    public void Init(object obj, Skill skill)
    {
        base.Init(skill);

        if (skill.Subject.Entity is Actor)
        {
            var actor = skill.Subject.Entity as Actor;

            StartCoroutine(skill.StartDamage(actor, obj));
        }

        var ai = GetComponent<EntityAI>();

        if (ai != null) ai.Entity = skill;

        if (skill.Lifetime != 0) StartCoroutine(skill.StartLifeTime());

        StartCoroutine(skill.BlendMaterialAsync(skill.Subject.Entity.MeshRenderer, 0f, 0.15f, materialSystem.ChangedMaterials));
    }
}
