using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAI : EntityAI
{
    public void Update()
    {
        var skill = Entity as Skill;

        transform.position = skill.Subject.Entity.Transform.position;
    }
}
