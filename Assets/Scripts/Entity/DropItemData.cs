using System.Collections.Generic;

[System.Serializable]
public class DropItemData : Data
{
    public List<DropItemInfo> DropGoodsInfos { get; set; }
    public List<DropItemInfo> DropItemInfos { get; set; }
}