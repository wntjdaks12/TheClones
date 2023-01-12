using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : GameManager
{
    private AssetBundleInfo assetBundleInfo;
    public AssetBundleInfo AssetBundleInfo { get { return assetBundleInfo ??= new AssetBundleInfo(); } set => assetBundleInfo = value; }
}
