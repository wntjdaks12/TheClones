using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class AbilityController: GameController
{
    GameDataContainerModel gameRuntimeDataModel;
    GameDataContainerModel gamePresetData; // 추가

    public Ability ability;

    protected override void Awake() 
    {
        gameRuntimeDataModel = App.GameModel.RuntimeDataModel;
        gamePresetData = App.GameModel.PresetDataModel;

        GetController<DataController>().OnDataSpawn += Init;
    }

    public void Init(IData data)
    {
        if (data is IAbility)
        {
            var ability = data as IAbility;

            ability.Ability = new Ability();
            ability.Ability.OnValueReturn += GetTotalValue;
            ability.Ability.OnValueReturn2 += GetTotalValue;
        }
    }

    // 스텟 데이터 추가
    public void AddStat(IAbility ability, uint id)
    {
        var stats = gamePresetData.ReturnData<StatData>(nameof(StatData), id).Stats;
        var skillStats = gamePresetData.ReturnData<StatData>(nameof(StatData), id).SkillStats;

        if (stats != null)
        {
            foreach (var stat in stats)
            {
                ability.Ability.Stats.Add(stat);
            }
        }

        if (skillStats != null)
        {
            foreach (var stat in skillStats)
            {
                ability.Ability.Stats.Add(stat);
            }
        }
    }

    public void AddStat(IAbility ability, Stat stat)
    {
        stat.TargetDataId = ability.AbilityOwnerInstanceId;
        ability.Ability.Stats.Add(stat);
    }

    public void RemoveStat(IAbility ability, Stat stat)
    {
        ability.Ability.Stats.Remove(stat);
    }
    public  void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            var monsters = gameRuntimeDataModel.ReturnDatas<Monster>(nameof(Monster));

            foreach (var monster in monsters)
            {
                Stat stat = new Stat(Stat.StatType.MoveSpeed, 2);
                AddStat(monster, stat);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            var monsters = gameRuntimeDataModel.ReturnDatas<Monster>(nameof(Monster));

            foreach (var monster in monsters)
            {
                Stat stat = new Stat(Stat.StatType.MoveSpeedIncRate, 100);
                AddStat(monster, stat);
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("2");

            var monsters = gameRuntimeDataModel.ReturnDatas<Monster>(nameof(Monster));

            foreach (var monster in monsters)
            {
                Stat stat = new Stat(Stat.StatType.MoveSpeedDecRate, 50);
                AddStat(monster, stat);
            }
        }
    }
    public float GetTotalValue(IAbility ability, Stat.StatType statType)
    {
        var vaules = ability.Ability.Stats
            .Where(x=>x.statType== statType)
            .Select(x => (float)x.Value);
        return vaules.Count() == 0 ? 0.0f : vaules.Sum();
    }
    public float GetTotalValue(IAbility ability, Stat.SkillStatType skillStatType)
    {
        var vaules = ability.Ability.Stats
            .Where(x => x.skillStatType == skillStatType)
            .Select(x => (float)x.Value);
        return vaules.Count() == 0 ? 0.0f : vaules.Sum();
    }
}

public interface IAbility
{
    Ability Ability { get; set; }

    uint AbilityOwnerInstanceId { get;}
}
public class Ability
{
    public event Func<IAbility, Stat.StatType, float> OnValueReturn;
    public event Func<IAbility, Stat.SkillStatType, float> OnValueReturn2;
    public event Action<IAbility, Stat> OnStatRemove;
    public List<Stat> Stats { get; set; } = new List<Stat>();
    public float OnReturnValue(IAbility ability,Stat.StatType statType)
    {
        return (float)OnValueReturn?.Invoke(ability, statType);
    }

    public float OnReturnValue(IAbility ability, Stat.SkillStatType skillStatType)
    {
        return (float)OnValueReturn2?.Invoke(ability, skillStatType);
    }
}