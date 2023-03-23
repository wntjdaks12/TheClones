using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RuneSlot : MonoBehaviour
{
    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI valueTMP;

    [Header("�̹���"), SerializeField] private Image iconImage;

    [Header("��ư"), SerializeField] private Button increaseButton;

    public Stat.StatType statType;

    public void Init()
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        valueTMP.text = playerManager.PlayerInfo.runeInfo.GetStat(statType).ToString();

        iconImage.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(Stat.GetId(statType).ToString());

        increaseButton.onClick.RemoveAllListeners();
        increaseButton.onClick.AddListener(() =>
        {
            GameManager.Instance.HTTPController.GetController<HttpItem>().GetRequestAsync(statType, 120201, Popup.ReturnPopup<RunePopup>().Init);
        });
    }
}
