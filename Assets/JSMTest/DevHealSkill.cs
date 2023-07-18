using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevHealSkill : DevISkillStrategy
{
    public IEnumerator OnTrigger(ISubject subject, DevSkill skill)
    {
        foreach (var other in subject.Subjects)
        {
        }

        yield return null;
    }
}
