using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySimpleCloneSlot : InventorySimpleSlot, IPollingScrollview
{
    public void Init(int index)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var cloneId = playerManager.PlayerInfo.cloneInofs[index].clonId;

        var imageInfo = App.GameModel.PresetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), cloneId);

        Init(index, assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(imageInfo.Icon));
    }
}
