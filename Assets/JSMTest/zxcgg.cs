using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class zxcgg : MonoBehaviour
{
    public string bundleName;

    public static AssetBundle localAssetBundle;

    private void Start()
    {
        LoadFromLocal();
    }

    public void LoadFromLocal()
    {
        localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));

        if (localAssetBundle == null)
        {
            Debug.LogError("���� �ε� ����");
            return;
        }
        else
        {
            Debug.Log("���� �ε� ����");
        }

       // localAssetBundle.Unload(false);
    }
}
