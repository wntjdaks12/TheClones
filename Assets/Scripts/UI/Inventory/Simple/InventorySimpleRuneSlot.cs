using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySimpleRuneSlot : InventorySimpleSlot, IPollingScrollview
{
    [SerializeField] private TextMeshProUGUI countTMP;

    public void Init(int index)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var itemInfo = playerManager.PlayerInfo.itemInfos[index];

        var imageInfo = App.GameModel.PresetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), itemInfo.itemId);
        var tooltip = App.GameModel.PresetDataModel.ReturnData<Tooltip>(nameof(Tooltip), itemInfo.itemId);

        countTMP.text = itemInfo.count.ToString();

        Init(index, assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(imageInfo.Icon));

        slotButton.onClick.AddListener(() => UISystem.TooltipBox(tooltip.description, transform.root));
    }
}
