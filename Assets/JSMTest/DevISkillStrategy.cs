using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DevISkillStrategy
{
    public IEnumerator OnTrigger(ISubject subject, DevSkill skill);
}
