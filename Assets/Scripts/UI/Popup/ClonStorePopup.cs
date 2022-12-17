using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClonStorePopup : Popup
{
    [SerializeField] private PoolingScrollview poolingScrollview;

    [Header("BUTTON")]
    [SerializeField] private Button ExitBtn;

    public void Init()
    {
        OnShow();

        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        var iaps = App.GameModel.PresetDataModel.ReturnDatas<IAP>();
        poolingScrollview.Init(iaps.Length);
    }
}
