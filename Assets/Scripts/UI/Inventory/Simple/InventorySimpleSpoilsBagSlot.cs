using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySimpleSpoilsBagSlot : InventorySimpleSlot, IPollingScrollview
{
    [SerializeField] private TextMeshProUGUI countTMP;

    public void Init(int index)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var itemInfo = playerManager.PlayerInfo.itemInfos[index];

        var imageInfo = App.GameModel.PresetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), itemInfo.itemId);

        countTMP.text = itemInfo.count.ToString();

        base.Init(index, assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(imageInfo.Icon));
    }
}
