using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAI : EntityAI
{
    public void Update()
    {
        var skill = Entity as DevSkill;

        transform.position = skill.Subjects[0].Transform.position;
    }
}
