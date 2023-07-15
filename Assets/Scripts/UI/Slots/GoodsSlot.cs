using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsSlot : GameView
{
    public enum Types { BasicGold = 200 }
    public Types type;

    [Header("이미지"), SerializeField] private Image IconImage;
    [Header("텍스트"), SerializeField] private TextMeshProUGUI NumberTMP;
    [Header("버튼"), SerializeField] private Button iconButton;

    public void Init()
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        IconImage.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(App.GameModel.PresetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), (uint)type).Icon);

        var tooltip = App.GameModel.PresetDataModel.ReturnData<Tooltip>(nameof(Tooltip), (uint)type);

        iconButton.onClick.RemoveAllListeners();
        iconButton.onClick.AddListener(() => UISystem.TooltipBox(tooltip.description, transform.root));

        DataInit();
    }

    public void DataInit()
    {
        var goodsInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetGoods((uint)type);

        NumberTMP.text = goodsInfo == null ? "0" : goodsInfo.count.ToString();
    }
}
