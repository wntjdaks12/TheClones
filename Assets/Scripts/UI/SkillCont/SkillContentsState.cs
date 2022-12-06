using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SkillContentsState
{
    public void OnShow(SkillContents skillContents);
    public void OnHide(SkillContents skillContents);
}
