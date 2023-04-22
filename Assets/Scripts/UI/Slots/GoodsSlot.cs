using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoodsSlot : GameView
{
    public enum Types { BasicGold = 200 }
    public Types type;

    [Header("�̹���"), SerializeField] private Image IconImage;
    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI NumberTMP;
    [Header("��ư"), SerializeField] private Button iconButton;

    public void Init()
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        IconImage.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(App.GameModel.PresetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), (uint)type).Icon);

        var goodsInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetGoods((uint)type);
        var tooltip = App.GameModel.PresetDataModel.ReturnData<Tooltip>(nameof(Tooltip), (uint)type);

        NumberTMP.text = goodsInfo ==  null ? "0" : goodsInfo.count.ToString();

        iconButton.onClick.RemoveAllListeners();
        iconButton.onClick.AddListener(() => UISystem.TooltipBox(tooltip.description, transform.root));
    }
}
