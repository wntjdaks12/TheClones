using System.Collections.Generic;

[System.Serializable]
public class DropItemInfo
{
    public uint ItemId { get; set; }
    public List<DropItemType> dropItemTypes { get; set; }
}

[System.Serializable]
public class DropItemType
{
    public enum DropItemInfoType
    {
        Probability = 0
    }

    public DropItemInfoType dropItemInfoType { get; set; }
    public float Value { get; set; }
}

