using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillContentsShowState : SkillContentsState
{
    private static SkillContentsShowState showState = new SkillContentsShowState();

    public static SkillContentsShowState getInstance()
    {
        return showState;
    }

    public void OnHide(SkillContents skillContents)
    {
        skillContents.setState(SkillContentsHideState.getInstance());

        var localScale = skillContents.transform.localScale; localScale.y = -1;
        skillContents.transform.DOScale(localScale, 0.3f).SetEase(Ease.OutBack);

        PlayerPrefs.SetInt("SkillContentsState", 0);
    }

    public void OnShow(SkillContents skillContents)
    {
        var localScale = skillContents.transform.localScale; localScale.y = 1;
        skillContents.transform.DOScale(localScale, 0.3f).SetEase(Ease.OutBack);

        PlayerPrefs.SetInt("SkillContentsState", 1);
    }
}
