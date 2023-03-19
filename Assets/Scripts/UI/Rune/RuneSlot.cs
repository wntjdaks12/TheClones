using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RuneSlot : MonoBehaviour
{
    [Header("텍스트"), SerializeField] private TextMeshProUGUI valueTMP;

    [Header("이미지"), SerializeField] private Image iconImage;

    public Stat.StatType statType;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        valueTMP.text = playerManager.PlayerInfo.runeInfo.GetStat(statType).ToString();

        iconImage.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(Stat.GetId(statType).ToString());
    }
}
