using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSettingPopup : Popup
{
    [Header("¹öÆ°"), SerializeField] private Button ExitBtn;

    [Header("ÄÁÅÙÃ÷"), SerializeField] private CloneSettingDetailContents detailContents;
    [SerializeField] private PoolingScrollview poolingScrollview;

    public override void Init()
    {
        OnShow();

        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        detailContents.Init();

        var playerManager = GameManager.Instance.GetManager<PlayerManager>();
        poolingScrollview.Init(playerManager.PlayerInfo.cloneInofs.Count);
    }
}
