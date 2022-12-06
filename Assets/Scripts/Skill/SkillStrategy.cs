using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class  SkillStrategy
{
    public abstract void Use(ICaster caster,object[] objects);
}
