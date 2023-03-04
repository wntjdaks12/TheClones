using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class HTTPCloneGacha : MonoBehaviour
{
    public void GetRequestAsync(Action callback = null)
    {
        StartCoroutine(GetRequest(callback));
    }

    private IEnumerator GetRequest(Action callback)
    {
        var url = "http://qqqq8692.dothome.co.kr/CloneGacha.php";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var dataStr = www.downloadHandler.text;
                    Debug.Log(dataStr);
                    var clonInfo = JsonUtility.FromJson<ClonInfo>(dataStr);
                    Debug.Log(clonInfo.clonId);
                    Debug.Log(clonInfo.skillId.Length);
                    var playerManager = GameManager.Instance.GetManager<PlayerManager>();

                    playerManager.PlayerInfo.cloneInofs.Add(clonInfo);
                  //  playerManager.PlayerInfo.cloneInfosRP.Add(clonInfo);

                    CFirebase.WriteData<PlayerInfo>(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

                    callback?.Invoke();
                    break;
            }
        }
    }
}
