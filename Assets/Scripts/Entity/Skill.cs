using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skill : Entity, IAbility
{
    public event Action<Skill> destroy;

    public Ability Ability { get; set; }

    public uint AbilityOwnerInstanceId { get => InstanceId; }

    public float Damage
    {
        get
        {
            var attackPower = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetStatInfo(Caster.Id).statDatas.GetStat(Stat.StatType.AttackPower);
            var damage = Ability.OnReturnValue(this, Stat.SkillStatType.Damage);
            // ·éµµ Ãß°¡
            var totalDamage = attackPower + damage;

            return totalDamage;
        }
    }

    public float DamageOverTime
    {
        get
        {
            return Ability.OnReturnValue(this, Stat.SkillStatType.DamageOverTime);
        }
    }

    public float DamageOverTimeCount
    {
        get
        {
            return Ability.OnReturnValue(this, Stat.SkillStatType.DamageOverTimeCount);
        }
    }
}