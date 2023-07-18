using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : Data
{
    public List<SpawnObjectInfo> SpawnObjectInfos { get; set; }
}
[SerializeField]
public class SpawnObjectInfo
{
    public uint Id { get; set; }
    public int MaxNumberOfSpawn { get; set; }
    public int CurNumberOfSpawn { get; set; }
}