using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public Stat(StatType statType,float value)
    {
        this.statType = statType;
        this.Value = value;
    }

    public enum StatType
    {
        MaxHp = 0,
        MaxHpIncRate = 1,
        MaxHpDecRate = 6,
        AttackPower = 2,
        AttackPowerIncRate = 3,
        AttackPowerDecRate = 7,
        MoveSpeed = 4,
        MoveSpeedIncRate = 5,
        MoveSpeedDecRate = 8,
    }
    public enum SkillStatType
    {
        Damage = 0,
        DamageOverTime = 1,
        DamageOverTimeCount = 2,
        AttackCount = 3,
        HealCount = 4,
        SubDamage = 5,
        SubDamageOverTime = 6,
        SubDamageOverTimeCount = 7
    }

    public static uint GetId(StatType statType)
    {
        switch (statType)
        {
            case StatType.MaxHp: return 101;
            case StatType.MaxHpIncRate: return 102;
            case StatType.AttackPower: return 104;
            case StatType.AttackPowerIncRate: return 105;
            case StatType.MoveSpeed: return 107;
            case StatType.MoveSpeedIncRate:return 108;
            case StatType.MaxHpDecRate: return 103;
            case StatType.AttackPowerDecRate: return 106;
            case StatType.MoveSpeedDecRate: return 109;
            default:return 0;
        }
    }

    [field: SerializeField] public StatType statType { get; set; }
    public SkillStatType skillStatType { get; set; }

    [field: SerializeField] public float Value { get; set; }
    public uint TargetDataId { get; set; }
}

//public class Stat:Data
//{
//    public Dictionary<StatElement.StatType , StatElement> stats;
//    public StatElement GetValue(StatElement.StatType statType)
//    {
//        return stats[statType];
//    }
//}