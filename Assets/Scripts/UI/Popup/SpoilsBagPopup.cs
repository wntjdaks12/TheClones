using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpoilsBagPopup : Popup
{
    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI TitleTMP;

    [Header("��ư"), SerializeField] private Button exitBtn;

    [SerializeField] private PoolingScrollview poolingScrollview;

    public override void Init()
    {
        OnShow();

        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(OnHide);

        var itemInfos = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.itemInfos;

        // Ǯ�� ��ũ�� �� �ʱ�ȭ
        poolingScrollview.Init(itemInfos.Count);
    }
}
