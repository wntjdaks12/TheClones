using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : EntityObject
{
    public virtual void Init(object obj, DevSkill skill)
    {
        base.Init(skill);

        var ai = GetComponent<EntityAI>();

        if (ai != null) ai.Entity = skill;

        if (skill.Lifetime != 0) StartCoroutine(skill.StartLifeTime());
    }
}
