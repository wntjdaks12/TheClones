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
            Debug.LogError("번들 로드 실패");
            return;
        }
        else
        {
            Debug.Log("번들 로드 성공");
        }

       // localAssetBundle.Unload(false);
    }
}
