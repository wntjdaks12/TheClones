using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CloneSettingDetailContents : GameView
{
    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI cloneNameTMP;
    [SerializeField] private TextMeshProUGUI cloneSkillTitle;

    [Header("�̹���"), SerializeField] private Image attributeIconImg;

    [Header("������"), SerializeField] private SkillSimpleContents skillSimpleContents;

    public void Init(Clon clone)
    {
        ShowData(clone);

        skillSimpleContents.Init(clone);
    }

    private void ShowData(Clon clone)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        cloneNameTMP.text = App.GameModel.PresetDataModel.ReturnData<Name>(nameof(Name), clone.Id).LanguageKR;
        cloneSkillTitle.text = "���� ��ų";

        attributeIconImg.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(clone.attributeId.ToString());
    }
}