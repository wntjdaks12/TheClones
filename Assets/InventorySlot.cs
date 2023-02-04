using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : GameView, IPollingScrollview
{
    [SerializeField] private Image itemImage;  

    public void Init(int index)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var presetDataModel = App.GameModel.PresetDataModel;

        var cloneId = playerManager.PlayerInfo.cloneInofs[0].clonId;

        var imageInfo = presetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), cloneId);

        itemImage.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(imageInfo.Icon);
    }
}
