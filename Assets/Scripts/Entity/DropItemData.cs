using System.Collections.Generic;

[System.Serializable]
public class DropItemData : Data
{
    public int Gold { get; set; }
    public List<DropItem> DropItems { get; set; }
}



