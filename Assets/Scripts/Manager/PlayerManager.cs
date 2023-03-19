using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GameManager
{
    [SerializeField] private PlayerInfo playerInfo;
    public PlayerInfo PlayerInfo { get => playerInfo; set => playerInfo = value; }


    // ���߿� ����
    public void Start()
    {
        playerInfo.itemInfos.Add(new ItemInfo { itemId = 120201, count = 1});
    }
}
