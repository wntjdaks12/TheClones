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

    public List<GoodsInfo> goodsInfos = new List<GoodsInfo>();

    public List<StatData> runeInfos = new List<StatData>();

    public List<StatInfo> statInfos = new List<StatInfo>();

    public StatData GetRuneInfo(uint id)
    {
        return runeInfos.Where(x => x.Id == id).FirstOrDefault();
    }

    public StatInfo GetStatInfo(uint id)
    {
        return statInfos.Where(x => x.statDatas.Id == id).FirstOrDefault();
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

    public ItemInfo GetItem(uint id)
    {
        return itemInfos.Where(x => x.itemId == id).FirstOrDefault();
    }

    public void SetDropItem(uint id, int value)
    {
        if (200 <= id && id <= 250)
        {
            SetGoods(id, value);
        }
        else
        {
            SetItem(id, value);
        }
    }

    public void SetItem(uint id, int value)
    {
        if (itemInfos.Any(x => x.itemId == id))
            itemInfos.Find(x => x.itemId == id).count += value;
        else
            itemInfos.Add(new ItemInfo() {itemId = id, count = value});
    }

    public void SetGoods(uint id, int value)
    {
        if (goodsInfos.Any(x => x.id == id))
            goodsInfos.Find(x => x.id == id).count += value;
        else
            goodsInfos.Add(new GoodsInfo() { id = id, count = value });
    }

    public GoodsInfo GetGoods(uint id)
    {
        return goodsInfos.Where(x => x.id == id).FirstOrDefault();
    }

    public void OnRemove(ItemInfo itemInfo)
    {
        itemInfos.Remove(itemInfo);
    }
}

[Serializable]
public class StatInfo
{
    public ReactiveProperty<int> holdingPoint = new ReactiveProperty<int>(0);
    public StatData statDatas = new StatData();
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
public class GoodsInfo
{
    public uint id;
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