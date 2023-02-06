using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CloneSettingDetailContents : GameView
{
    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI cloneNameTMP;
    [SerializeField] private TextMeshProUGUI cloneSkillTitle;

    [SerializeField] private Image attributeIconImg;

    public void Init(Clon clone)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        cloneNameTMP.text = clone.nameKr;
        cloneSkillTitle.text = "���� ��ų";

        attributeIconImg.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(clone.attributeId.ToString());
    }
}
 