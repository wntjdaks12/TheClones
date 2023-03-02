using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySimpleSpoilsBagSlot : InventorySimpleSlot, IPollingScrollview
{
    public void Init(int index)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var itemId = playerManager.PlayerInfo.itemInfos[index].itemId;

        var imageInfo = App.GameModel.PresetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), itemId);

        base.Init(index, assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(imageInfo.Icon));
    }
}
