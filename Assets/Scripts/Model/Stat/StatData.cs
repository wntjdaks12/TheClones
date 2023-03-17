using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[System.Serializable]
public class StatData:Data
{
    [field: SerializeField] public List<Stat> Stats { get; set; }
    [field: SerializeField] public List<Stat> SkillStats { get; set; }

    public  float GetTotalStatValue(Stat.StatType statType)
    {
        var resStatValues = Stats.Where(x => x.statType == statType)
                            .Select(x => x.Value).ToList();

        return resStatValues.Sum();
    }
    public float GetStat(Stat.StatType statType)
    {
        var resStatValues = Stats.Where(x => x.statType == statType)
                            .Select(x => x.Value).ToList();

        return resStatValues.Sum();
    }

    public float GetTotalSkillStatValue(Stat.SkillStatType skillStatType)
    {
        var resStatValues = SkillStats.Where(x => x.skillStatType == skillStatType)
                            .Select(x => x.Value).ToList();

        return resStatValues.Sum();
    }
}
