using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillContentsHideState : SkillContentsState
{
    private static SkillContentsHideState hideState = new SkillContentsHideState();

    public static SkillContentsHideState getInstance()
    {
        return hideState;
    }

    public void OnHide(SkillContents skillContents)
    {
        var localScale = skillContents.transform.localScale; localScale.y = -1;
        skillContents.transform.DOScale(localScale, 0.3f).SetEase(Ease.OutBack);

        PlayerPrefs.SetInt("SkillContentsState", 0);
    }

    public void OnShow(SkillContents skillContents)
    {
        skillContents.setState(SkillContentsShowState.getInstance());

        var localScale = skillContents.transform.localScale; localScale.y = 1;
        skillContents.transform.DOScale(localScale, 0.3f).SetEase(Ease.OutBack);

        PlayerPrefs.SetInt("SkillContentsState", 1);
    }
}
