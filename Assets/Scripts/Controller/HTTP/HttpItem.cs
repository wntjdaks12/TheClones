using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HttpItem : MonoBehaviour
{
    public void GetRequestAsync(Stat.StatType statType, int id, Action callback = null)
    {
        StartCoroutine(GetRequest(statType, id, callback));
    }

    private IEnumerator GetRequest(Stat.StatType statType, int id, Action callback)
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var itemInfo = playerManager.PlayerInfo.GetItem(id);

        if (itemInfo != null)
        {
            --itemInfo.count;

            if (itemInfo.count == 0) playerManager.PlayerInfo.OnRemove(itemInfo);

            playerManager.PlayerInfo.runeInfo.SetStat(statType, 1);

            CFirebase.WriteData(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

            callback?.Invoke();
        }

        yield return null;

    }
}
