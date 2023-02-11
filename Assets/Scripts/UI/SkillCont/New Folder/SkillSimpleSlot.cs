using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSimpleSlot : MonoBehaviour
{
    [SerializeField] private Image skillIconImage;

    [SerializeField] private Button button;

    public void Init(uint skillId)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => UISystem.TooltipBox("asd", transform.root));

        ShowData(skillId);

        OnShow();
    }

    private void ShowData(uint skillId)
    {
        var assetManager = GameManager.Instance.GetManager<AssetBundleManager>();

        skillIconImage.sprite = assetManager.AssetBundleInfo.texture.LoadAsset<Sprite>(skillId.ToString());
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}