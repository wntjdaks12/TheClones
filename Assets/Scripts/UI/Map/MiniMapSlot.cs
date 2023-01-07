using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using TMPro;
using System.Linq;

public class MiniMapSlot : MapView
{
    [SerializeField] private new TextMeshProUGUI name;
    [SerializeField] private Image img;

    private void Start()
    {
        this.ObserveEveryValueChanged(_ => MapModel.CurrentIndex).Subscribe(_ => ShowData());
    }

    private void ShowData()
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        var presetDataModel = App.GameModel.PresetDataModel;

        var map = presetDataModel.ReturnDatas<Map>()
            .Where(x => SceneManager.GetActiveScene().name == x.SceneId.ToString())
            .ToArray()[MapModel.CurrentIndex + 1];

        var imageInfo = presetDataModel.ReturnData<ImageInfo>(nameof(ImageInfo), map.Id);
        Debug.Log(imageInfo.Id.ToString());
        img.sprite = assetBundleManager.AssetBundle.LoadAsset<Sprite>(imageInfo.Id.ToString());
        name.text = map.Name.ToString();
    }
}
