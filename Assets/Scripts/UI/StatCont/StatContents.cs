using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class StatContents : GameView
{
    [SerializeField] private List<StatSlot> statSlots;

    [Header("≈ÿΩ∫∆Æ"), SerializeField] private TextMeshProUGUI holdingStatPointTMP;

    private StatInfo statInfo;

    public void Init(uint cloneId)
    {
        statInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetStatInfo(cloneId);

        statSlots.ForEach(x => x.Init(cloneId));

        ShowData();

        statInfo.holdingPoint.Subscribe(x => ShowData());
    }

    private void ShowData()
    {
        holdingStatPointTMP.text = statInfo.holdingPoint.ToString();
    }
}
