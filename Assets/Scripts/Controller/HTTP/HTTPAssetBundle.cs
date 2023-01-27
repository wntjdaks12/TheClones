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
        yield return StartCoroutine(GetRequestFonts());
        yield return StartCoroutine(GetRequestMaterials());
        yield return StartCoroutine(GetRequestPrefabs());

        callback?.Invoke();
        Debug.Log("���� ���� �ε� ����");
    }

    // ��Ƽ���� ���̴��� �����մϴ�.
    private void FindMaterialShader()
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var materials = assetBundleManager.AssetBundleInfo.material.LoadAllAssets<Material>();

        foreach (Material m in materials)
        {
            var shaderName = m.shader.name;
            var newShader = Shader.Find(shaderName);
            if (newShader != null)
            {
                m.shader = newShader;
            }
            else
            {
                Debug.LogWarning("unable to refresh shader: " + shaderName + " in material " + m.name);
            }
        }
    }

    // ��Ʈ ���̴��� �����մϴ�.
    private void FindFontShader()
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var materials = assetBundleManager.AssetBundleInfo.font.LoadAllAssets<Material>();

        foreach (Material m in materials)
        {
            var shaderName = m.shader.name;
            var newShader = Shader.Find(shaderName);
            if (newShader != null)
            {
                m.shader = newShader;
            }
            else
            {
                Debug.LogWarning("unable to refresh shader: " + shaderName + " in material " + m.name);
            }
        }
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

        FindFontShader();
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

                    FindMaterialShader(); // ���� ����� ���� ���� ���̴��� �ѹ� �� ã��

                    break;
            }
        }
    }

    private IEnumerator GetRequestFonts()
    {
        var url = "http://qqqq8692.dothome.co.kr/AssetBundles/Android/fontassetbundle";

        nameRP.Value = "��Ʈ ���� ���� �ε�";

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
                    assetBundleManager.AssetBundleInfo.font = DownloadHandlerAssetBundle.GetContent(www);

                    FindFontShader(); // ���� ����� ���� ���� ���̴��� �ѹ� �� ã��

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
