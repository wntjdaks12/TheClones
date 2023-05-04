using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : Data
{
    public uint StageId { get; set; }
    public List<MonsterInfo> monsterInfos { get; set; }

    public class MonsterInfo
    {
        public int MaxNumberOfSpawn { get; set; }
        public float SpawnSpeed { get; set; }
    }
}