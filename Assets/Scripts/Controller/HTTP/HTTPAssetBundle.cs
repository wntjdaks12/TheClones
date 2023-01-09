using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPAssetBundle : MonoBehaviour
{
    public void GetRequestAsync(Action callback = null)
    {
        StartCoroutine(GetRequestAssetBundles(callback));
    }

    private IEnumerator GetRequestAssetBundles(Action callback)
    {
        yield return StartCoroutine(GetRequestTextures(callback));
        yield return StartCoroutine(GetRequestMaterials(callback));
        yield return StartCoroutine(GetRequestPrefabs(callback));
        yield return StartCoroutine(GetRequestGarbages(callback));

        Debug.Log("���� ���� �ε� ����");
    }

    private IEnumerator GetRequestTextures(Action callback)
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/textureassetbundle";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return StartCoroutine(DownloadProgress(www.SendWebRequest()));

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

                    assetBundleManager.AssetBundleInfo.texture = DownloadHandlerAssetBundle.GetContent(www);

                    callback?.Invoke();
                    break;
            }
        }
    }

    private IEnumerator GetRequestPrefabs(Action callback)
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/prefabassetbundle";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return StartCoroutine(DownloadProgress(www.SendWebRequest()));

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

                    assetBundleManager.AssetBundleInfo.prefab = DownloadHandlerAssetBundle.GetContent(www);

                    callback?.Invoke();
                    break;
            }
        }
    }

    private IEnumerator GetRequestMaterials(Action callback)
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/materialassetbundle";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return StartCoroutine(DownloadProgress(www.SendWebRequest()));

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

                    assetBundleManager.AssetBundleInfo.material = DownloadHandlerAssetBundle.GetContent(www);

                    callback?.Invoke();
                    break;
            }
        }
    }

    private IEnumerator GetRequestGarbages(Action callback)
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/testassetbundle";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return StartCoroutine(DownloadProgress(www.SendWebRequest()));

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    callback?.Invoke();
                    break;
            }
        }
    }

    IEnumerator DownloadProgress(UnityWebRequestAsyncOperation operation)
    {
        while (!operation.isDone)
        {
            //progressBar.color = Color.red;
            //downloadDataProgress = operation.progress * 100;
            //progressBar.fillAmount = downloadDataProgress / 100;
            //print("Download: " + downloadDataProgress);
            Debug.Log(operation.progress * 100);
            yield return null;
        }
        Debug.Log("Done");
    }
}
