using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public string playerId;

    public List<ClonInfo> cloneInofs = new List<ClonInfo>();
    [HideInInspector]
    public ReactiveCollection<ClonInfo> cloneInfosRP = new ReactiveCollection<ClonInfo>();
}

[Serializable]
public class ClonInfo
{
    public uint clonId;
    public uint skillId;
}


[Serializable]
public class AssetBundleInfo
{
    public AssetBundle texture;
    public AssetBundle prefab;
    public AssetBundle material;
}
