using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public string playerId;

    public List<ClonInfo> cloneInofs = new List<ClonInfo>();
   // [HideInInspector]
   //public ReactiveCollection<ClonInfo> cloneInfosRP = new ReactiveCollection<ClonInfo>();

    public List<ItemInfo> itemInfos = new List<ItemInfo>();
   // [HideInInspector]
  //  public ReactiveCollection<ItemInfo> itemInfosRP = new ReactiveCollection<ItemInfo>();
}

[Serializable]
public class ClonInfo
{
    public uint clonId;
    public uint[] skillId;
}

[Serializable]
public class ItemInfo
{
    public uint itemId;
    public int count;
}

[Serializable]
public class AssetBundleInfo
{
    public AssetBundle texture;
    public AssetBundle prefab;
    public AssetBundle material;
    public AssetBundle font;
}
