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

                    var clonInfo = JsonUtility.FromJson<ClonInfo>(dataStr);

                    var playerManager = GameManager.Instance.GetManager<PlayerManager>();

                    playerManager.PlayerInfo.cloneInofs.Add(clonInfo);
                    playerManager.PlayerInfo.cloneInfosRP.Add(clonInfo);

                    var statInfo = new StatInfo();
                    statInfo.holdingPoint.Value = 10;
                    statInfo.statDatas.Id = clonInfo.clonId;
                    statInfo.statDatas.Stats = new List<Stat>();

                    for (int i = 0; i < Enum.GetValues(typeof(Stat.StatType)).Length; i++)
                    {
                        statInfo.statDatas.Stats.Add(new Stat((Stat.StatType)i, 0));
                    }

                    playerManager.PlayerInfo.statInfos.Add(statInfo);

                    CFirebase.WriteData<PlayerInfo>(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

                    callback?.Invoke();
                    break;
            }
        }
    }
}

/*
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

                    var clonInfo = JsonUtility.FromJson<ClonInfo>(dataStr);

                    var playerManager = GameManager.Instance.GetManager<PlayerManager>();

                    playerManager.PlayerInfo.cloneInofs.Add(clonInfo);
                    playerManager.PlayerInfo.cloneInfosRP.Add(clonInfo);

                    var statInfo = new StatInfo();
                    statInfo.holdingPoint.Value = 10;
                    statInfo.statDatas.Id = clonInfo.clonId;
                    statInfo.statDatas.Stats = new List<Stat>();

                    for (int i = 0; i < Enum.GetValues(typeof(Stat.StatType)).Length; i++)
                    {
                        statInfo.statDatas.Stats.Add(new Stat((Stat.StatType)i, 0));
                    }

                    playerManager.PlayerInfo.statInfos.Add(statInfo);

                    var runeInfo = new StatData();
                    runeInfo.Id = clonInfo.clonId;
                    runeInfo.Stats = new List<Stat>();

                    for (int i = 0; i < Enum.GetValues(typeof(Stat.StatType)).Length; i++)
                    {
                        runeInfo.Stats.Add(new Stat((Stat.StatType)i, 0));
                    }

                    playerManager.PlayerInfo.runeInfos.Add(runeInfo);

                    CFirebase.WriteData<PlayerInfo>(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

                    callback?.Invoke();
                    break;
            }
        }
    }
}

*/