using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RuneSlot : MonoBehaviour
{
    [Header("텍스트"), SerializeField] private TextMeshProUGUI valueTMP;

    [Header("이미지"), SerializeField] private Image iconImage;

    [Header("버튼"), SerializeField] private Button increaseButton;

    public Stat.StatType statType;

    public void Init(ClonInfo cloneInfo)
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        valueTMP.text = playerManager.PlayerInfo.GetRuneInfo(cloneInfo.clonId).GetStat(statType).ToString();

        iconImage.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(Stat.GetId(statType).ToString());

        CheckButtonInteractable(Temp());

        increaseButton.onClick.RemoveAllListeners();
        increaseButton.onClick.AddListener(() =>
        {
            GameManager.Instance.HTTPController.GetController<HttpItem>().GetRequestAsync(cloneInfo, statType, Temp(), () => Popup.ReturnPopup<RunePopup>().Init(cloneInfo));
        });
    }

    private void CheckButtonInteractable(uint id)
    {
        var itemInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetItem(id);

        increaseButton.interactable = itemInfo != null ? true : false;
    }

    public uint Temp()
    {
        switch (statType)
        {
            case Stat.StatType.MaxHp: return 120201;
        }

        return 0;
    }
}