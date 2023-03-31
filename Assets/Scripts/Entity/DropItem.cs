using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DropItem : Entity
{
    public void Pickup()
    {
        var itemInfo = new ItemInfo { itemId = Id, count = 1 };

        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        for (int i = 0; i < playerManager.PlayerInfo.itemInfos.Count; i++)
        {
            if (playerManager.PlayerInfo.itemInfos[i].itemId == itemInfo.itemId)
            {
                playerManager.PlayerInfo.itemInfos[i].count += itemInfo.count;
                //playerManager.PlayerInfo.itemInfosRP[i].count += itemInfo.count;

                return;
            }
        }

        playerManager.PlayerInfo.itemInfos.Add(itemInfo);
    }
}
