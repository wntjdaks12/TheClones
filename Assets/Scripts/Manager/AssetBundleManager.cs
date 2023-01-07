using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : GameManager
{
    [SerializeField] private AssetBundle assetBundle;
    public AssetBundle AssetBundle { get => assetBundle; set => assetBundle = value; }
}
