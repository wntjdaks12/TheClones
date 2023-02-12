using System.Collections.Generic;

[System.Serializable]
public class DropItem
{
    public uint ItemId { get; set; }
    public List<DropItemInfo> dropItemInfos { get; set; }
}

public class DropItemInfo
{
    public enum DropItemInfoType
    {
        Probability = 0
    }

    public DropItemInfoType dropItemInfoType { get; set; }
    public float Value { get; set; }
}

