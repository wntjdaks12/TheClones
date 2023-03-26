using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GameManager
{
    [SerializeField] private PlayerInfo playerInfo;
    public PlayerInfo PlayerInfo { get => playerInfo; set => playerInfo = value; }


    // 나중에 삭제
    public void Start()
    {
        // 아이템 
        playerInfo.itemInfos.Add(new ItemInfo { itemId = 120201, count = 3});

        //스텟 
        var statData = new StatData();
        statData.Id = 90001;
        statData.Stats = new List<Stat>();

        for (int i = 0; i < System.Enum.GetValues(typeof(Stat.StatType)).Length; i++)
        {
            statData.Stats.Add(new Stat((Stat.StatType)i, 0));   
        }

        playerInfo.statInfo.Add(new StatInfo { holdingPoint = 10, statData = statData });
    }
}
