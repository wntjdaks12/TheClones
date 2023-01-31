using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : EntityObject
{
    public virtual void Init(object obj, Skill skill)
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
    }
}
