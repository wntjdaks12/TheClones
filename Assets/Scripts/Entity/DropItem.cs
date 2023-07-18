using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DropItem : Actor
{
    public void Pickup()
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        playerManager.PlayerInfo.SetDropItem(Id, 1);
    }
}
