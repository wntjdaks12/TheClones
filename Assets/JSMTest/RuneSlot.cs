using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RuneSlot : MonoBehaviour
{
    [Header("≈ÿΩ∫∆Æ"), SerializeField] private TextMeshProUGUI valueTMP;

    public int num;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
       // var playerManager = GameManager.Instance.GetManager<PlayerManager>();

  //      valueTMP.text = playerManager.PlayerInfo.runeStatData.GetStat((Stat.StatType)num).ToString();
    }
}
