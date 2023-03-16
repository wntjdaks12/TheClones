using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class HTTPDropItem : MonoBehaviour
{
    public void GetRequestAsync(Action callback = null)
    {
        StartCoroutine(GetRequest(callback));
    }

    private IEnumerator GetRequest(Action callback)
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        CFirebase.WriteData(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

        callback?.Invoke();

        yield return null;

    }
}
