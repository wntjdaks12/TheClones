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

    [Header("컨텐츠"), SerializeField] private List<Image> skillSlotImgs = new List<Image>();
    [SerializeField] private List<Image> skillImgs = new List<Image>();

    public void Init(Clon clone)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        cloneNameTMP.text = clone.nameKr;
        cloneSkillTitle.text = "고유 스킬";

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