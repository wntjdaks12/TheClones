using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HttpItem : MonoBehaviour
{
    public void GetRequestAsync(ClonInfo cloneInfo, Stat.StatType statType, uint id, Action callback = null)
    {
        StartCoroutine(GetRequest(cloneInfo, statType, id, callback));
    }

    private IEnumerator GetRequest(ClonInfo cloneInfo, Stat.StatType statType, uint id, Action callback)
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        var itemInfo = playerManager.PlayerInfo.GetItem(id);

        if (itemInfo != null)
        {
            --itemInfo.count;

            if (itemInfo.count == 0) playerManager.PlayerInfo.OnRemove(itemInfo);

            playerManager.PlayerInfo.GetRuneInfo(cloneInfo.clonId).SetStat(statType, 1);

            CFirebase.WriteData(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

            callback?.Invoke();
        }

        yield return null;

    }
}
