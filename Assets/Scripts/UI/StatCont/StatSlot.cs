using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UniRx;

public class StatSlot : GameView
{
    [Header("텍스트"), SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI numberTMP;

    [Header("버튼"), SerializeField] private Button upgradeButton;

    [SerializeField] private Stat.StatType statType;

    public void Init(uint cloneId)
    {
        var id = Stat.GetId(statType);

        nameTMP.text = App.GameModel.PresetDataModel.ReturnData<Name>(nameof(Name), id).LanguageKR;
        numberTMP.text = App.GameModel.PresetDataModel.ReturnData<StatData>(nameof(StatData), cloneId).GetStat(statType) +
            "(" + GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetStatInfo(cloneId).statDatas.GetTotalStatValue(statType) + ")";

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() =>
        {
            var playerInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo;
            var statInfo = playerInfo.GetStatInfo(cloneId);

            if (statInfo.holdingPoint.Value > 0)
            {
                --statInfo.holdingPoint.Value;

                playerInfo.GetStatInfo(cloneId).statDatas.SetStat(statType, 1);

                Init(cloneId);

                CFirebase.WriteData(playerInfo.playerId, playerInfo);
            }
        });
    }
}
