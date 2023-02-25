using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CloneSettingDetailContents : GameView
{
    [Header("텍스트"), SerializeField] private TextMeshProUGUI cloneNameTMP;
    [SerializeField] private TextMeshProUGUI cloneSkillTitle;

    [Header("이미지"), SerializeField] private Image attributeIconImg;

    [Header("컨텐츠"), SerializeField] private SkillSimpleContents skillSimpleContents;

    public void Init(Clon clone)
    {
        ShowData(clone);

        skillSimpleContents.Init(clone);
    }

    private void ShowData(Clon clone)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        cloneNameTMP.text = clone.nameKr;
        cloneSkillTitle.text = "고유 스킬";

        attributeIconImg.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(clone.attributeId.ToString());
    }
}