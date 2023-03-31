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
            return Ability.OnReturnValue(this, Stat.SkillStatType.Damage);
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

    public IEnumerator StartDamage(Actor actor, object obj)
    {
        if (obj is GameApplication)
        {
            var app = obj as GameApplication;

            var wait = new WaitForSeconds(DamageOverTime);

            var damageOverTimeCount = 0;

            while (damageOverTimeCount <= DamageOverTimeCount)
            {
                app.GameController.GetComponent<DamageTMPController>().Spawn("DamageTMP", 40001, Subject.Transform.position, GameObject.Find("DamagePopupCanvas").transform, this);

                if (actor.CurrentHp > 0)
                {
                    actor.OnActorHit(Damage);
                }
                else
                {
                    yield return null;
                }

                damageOverTimeCount++;

                yield return wait;
            }
        }
    }
}