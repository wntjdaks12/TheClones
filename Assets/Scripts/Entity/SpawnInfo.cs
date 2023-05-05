using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : Data
{
    public List<MonsterInfo> monsterInfos { get; set; }
}
public class MonsterInfo
{
    public uint Id { get; set; }
    public int MaxNumberOfSpawn { get; set; }
    public float SpawnSpeed { get; set; }
}