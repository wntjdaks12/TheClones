using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySimpleSlot : GameView, IPollingScrollview
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Button slotButton;

    [SerializeField] private CanvasGroup canvasGroup;

    public Action<int> ClickEvent { get; set; }

    public void Init(int index)
    {
        slotButton.onClick.RemoveAllListeners();
        slotButton.onClick.AddListener(() => 
        {
            canvasGroup.alpha = 0.5f;

            ClickEvent?.Invoke(index);
        });

        ShowData(index);
    }

    private void ShowData(int index)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var presetDataModel = App.GameModel.PresetDataModel;

        var cloneId = playerManager.PlayerInfo.cloneInofs[index].clonId;

        var imageInfo = presetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), cloneId);

        itemImage.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(imageInfo.Icon);
    }
}
