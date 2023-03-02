using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpoilsBagPopup : Popup
{
    [Header("텍스트"), SerializeField] private TextMeshProUGUI TitleTMP;

    [Header("버튼"), SerializeField] private Button exitBtn;

    [SerializeField] private PoolingScrollview poolingScrollview;

    public override void Init()
    {
        OnShow();

        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(OnHide);

        var itemInfos = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.itemInfos;

        // 풀링 스크롤 뷰 초기화
        poolingScrollview.Init(itemInfos.Count);
    }
}
