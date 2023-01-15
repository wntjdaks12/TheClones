using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UniRx;

public class HTTPAssetBundle : MonoBehaviour
{
    public ReactiveProperty<float> progressRP = new ReactiveProperty<float>();
    public ReactiveProperty<string> nameRP = new ReactiveProperty<string>();

    public void GetRequestAsync(Action callback = null)
    {
        StartCoroutine(GetRequestAssetBundles(callback));
    }

    private IEnumerator GetRequestAssetBundles(Action callback)
    {
        yield return StartCoroutine(GetRequestTextures());
        yield return StartCoroutine(GetRequestMaterials());
        yield return StartCoroutine(GetRequestPrefabs());

        callback?.Invoke();
        Debug.Log("���� ���� �ε� ����");
    }

    private IEnumerator GetRequestTextures()
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/textureassetbundle";

        nameRP.Value = "�ؽ�ó ���� ���� �ε�";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return DownloadProgress(www);

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
                    assetBundleManager.AssetBundleInfo.texture = DownloadHandlerAssetBundle.GetContent(www);

                    break;
            }
        }
    }

    private IEnumerator GetRequestPrefabs()
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/prefabassetbundle";

        nameRP.Value = "������ ���� ���� �ε�";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return DownloadProgress(www);

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
                    assetBundleManager.AssetBundleInfo.prefab = DownloadHandlerAssetBundle.GetContent(www);

                    break;
            }
        }
    }

    private IEnumerator GetRequestMaterials()
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/materialassetbundle";

        nameRP.Value = "��Ƽ���� ���� ���� �ε�";

        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return DownloadProgress(www);

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError: break;
                case UnityWebRequest.Result.DataProcessingError: break;
                case UnityWebRequest.Result.ProtocolError: break;
                case UnityWebRequest.Result.Success:

                    var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
                    assetBundleManager.AssetBundleInfo.material = DownloadHandlerAssetBundle.GetContent(www);

                    break;
            }
        }
    }

    private IEnumerator DownloadProgress(UnityWebRequest www)
    {
        var operation = www.SendWebRequest();

        while (!operation.isDone)
        {
            progressRP.Value = www.downloadProgress;
         
            yield return null;
        }
        Debug.Log("Done");
    }
}
