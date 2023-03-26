using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Linq;

[Serializable]
public class PlayerInfo
{
    public string playerId;

    public List<ClonInfo> cloneInofs = new List<ClonInfo>();
    [HideInInspector]
    public ReactiveCollection<ClonInfo> cloneInfosRP = new ReactiveCollection<ClonInfo>();

    public List<ItemInfo> itemInfos = new List<ItemInfo>();
    [HideInInspector]
    public ReactiveCollection<ItemInfo> itemInfosRP = new ReactiveCollection<ItemInfo>();

    public StatData runeInfo = new StatData();

    public List<StatInfo> statInfo = new List<StatInfo>();

    public StatInfo GetStatInfo(uint id)
    {
        return statInfo.Where(x => x.statData.Id == id).FirstOrDefault();
    }

    public enum ItemTypes { Consumable }
    public ItemInfo[] GetItem(ItemTypes itemTypes)
    {
        switch (itemTypes)
        {
            case ItemTypes.Consumable: return itemInfos.Where(x => 120200 < x.itemId && x.itemId < 120300).ToArray();
            default: return null;
        }
    }

    public ItemInfo GetItem(int id)
    {
        return itemInfos.Where(x => x.itemId == id).FirstOrDefault();
    }

    public void OnRemove(ItemInfo itemInfo)
    {
        itemInfos.Remove(itemInfo);
    }
}

[Serializable]
public class StatInfo
{
    public int holdingPoint;
    public StatData statData;
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