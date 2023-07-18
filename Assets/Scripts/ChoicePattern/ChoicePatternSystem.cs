using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePatternSystem
{
    private static GameObject choicePatternObj;

    public static void OnCreate(Actor target)
    {
        if (choicePatternObj != null) return;

        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        var obj = assetBundleManager.AssetBundleInfo.prefab.LoadAsset<GameObject>("ChoicePattern");
        choicePatternObj = MonoBehaviour.Instantiate(obj, GameObject.Find("DynamicCanvas").transform);

        var choicePattern = choicePatternObj.GetComponent<ChoicePattern>();
        choicePattern.Target = target;
    }
}
    