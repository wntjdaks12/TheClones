using System.Collections.Generic;

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
        DamageOverTimeCount = 2
    }

    public StatType statType { get; set; }
    public SkillStatType skillStatType { get; set; }

    public float Value { get; set; }
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