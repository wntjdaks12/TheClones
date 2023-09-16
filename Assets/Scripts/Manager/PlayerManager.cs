using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : GameManager
{
    [SerializeField] private PlayerInfo playerInfo;
    public PlayerInfo PlayerInfo { get => playerInfo; set => playerInfo = value; }

    // ���߿� ����
    public void Start()
    {
        // ������ 
        playerInfo.itemInfos.Add(new ItemInfo { itemId = 120201, count = 3});
        //��������
        playerInfo.mapInfo = new MapInfo { mapId = 70001, stageInfo = new StageInfo {id = 130001, currentNumberOfMonsterHunts = new ReactiveProperty<int>(0), maxNumberOfMonsterHunts = new ReactiveProperty<int>(5) } };
    }
}
