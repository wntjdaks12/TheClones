using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPAssetBundle : MonoBehaviour
{
    public void GetRequestAsync(Action callback = null)
    {
        StartCoroutine(GetRequest(callback));
    }

    private IEnumerator GetRequest(Action callback)
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/textureassetbundle";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

                    assetBundleManager.AssetBundle = DownloadHandlerAssetBundle.GetContent(www);

                    callback?.Invoke();
                    break;
            }
        }
    }
}
