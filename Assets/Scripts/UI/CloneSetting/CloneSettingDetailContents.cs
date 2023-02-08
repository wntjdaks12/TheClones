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

    [Header("������"), SerializeField] private List<Image> skillSlotImgs = new List<Image>();
    [SerializeField] private List<Image> skillImgs = new List<Image>();

    public void Init(Clon clone)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        cloneNameTMP.text = clone.nameKr;
        cloneSkillTitle.text = "���� ��ų";

        attributeIconImg.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(clone.attributeId.ToString());

        asd(clone);
    }

    public void asd(Clon clone)
    {
        var assetManager = GameManager.Instance.GetManager<AssetBundleManager>();

        for (int i = 0; i < skillSlotImgs.Count; i++)
        {
            skillSlotImgs[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < clone.skillId.Length; i++)
        {
            skillSlotImgs[i].gameObject.SetActive(true);

            skillImgs[i].sprite = assetManager.AssetBundleInfo.texture.LoadAsset<Sprite>(clone.skillId[i].ToString());
        }
    }
}