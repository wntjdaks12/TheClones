using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatSlot : GameView
{
    [Header("≈ÿΩ∫∆Æ"), SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI numberTMP;

    [SerializeField] private Stat.StatType statType;

    public void Init(uint cloneId)
    {
        var id = Stat.GetId(statType);

        nameTMP.text = App.GameModel.PresetDataModel.ReturnData<Name>(nameof(Name), id).LanguageKR;
        numberTMP.text = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetStatInfo(cloneId).statData.GetTotalStatValue(statType).ToString();
    }
}
