using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevSkill : Entity, IAbility
{
    public DevISkillStrategy SkillStrategy { get; set; }

    public Ability Ability { get; set; }

    public uint AbilityOwnerInstanceId { get => InstanceId; }

    public StrategyTypes StrategyType { get; set; }

    public enum StrategyTypes
    {
        Attack = 0,
        Heal = 1,
        Buff = 2,
        Debuff = 3
    }

    public float Damage
    {
        get
        {
            var attackPower = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetStatInfo(Caster.Id).statDatas.GetStat(Stat.StatType.AttackPower);
            var damage = Ability.OnReturnValue(this, Stat.SkillStatType.Damage);
            // �鵵 �߰� 
            var totalDamage = attackPower + damage;

            return totalDamage;
        }
    }

    public int AttackCount
    {
        get 
        {
            var attackPower = Ability.OnReturnValue(this, Stat.SkillStatType.AttackCount);

            return (int)attackPower;
        }
    }

    public float DamageOverTime
    {
        get
        {
            return Ability.OnReturnValue(this, Stat.SkillStatType.DamageOverTime);
        }
    }

    public override void Init(Transform transform, Collider collider)
    {
        base.Init(transform, collider);

        switch (StrategyType)
        {
            case StrategyTypes.Attack: SkillStrategy = new DevAttackSkill(); break;
            case StrategyTypes.Heal: SkillStrategy = new DevHealSkill(); break;
            case StrategyTypes.Buff: SkillStrategy = new DevBuffSkill(); break;
            case StrategyTypes.Debuff: SkillStrategy = new DevDebuffSkill(); break;
        }
    }
}