using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class HTTPCloneGacha : MonoBehaviour
{
    public void GetRequestAsync(Action callback)
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
                    Debug.Log(www.downloadHandler.text);
                    callback?.Invoke();
                    break;
            }
        }
    }
}
