using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatContents : GameView
{
    [SerializeField] private List<StatSlot> statSlots;

    [Header("ÅØ½ºÆ®"), SerializeField] private TextMeshProUGUI holdingStatPointTMP;

    private uint cloneId;

    public void Init(uint cloneId)
    {
        this.cloneId = cloneId;

        statSlots.ForEach(x => x.Init(cloneId));

        ShowData();
    }

    private void ShowData()
    {
        holdingStatPointTMP.text = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetStatInfo(cloneId).holdingPoint.ToString();
    }
}
