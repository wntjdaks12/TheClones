using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;

//피해를 입는 모든 오브젝트
public class Actor : Entity, IAbility
{
    public event Action<Actor, float> actorHit;
    public event Action<Actor> actorDeath;

    public uint AbilityOwnerInstanceId { get => InstanceId; }
    public Ability Ability { get; set; } = new Ability();

    public Transform HeadBarTransform { get; set; }

    public float CurrentHp { get; protected set; }

    public float MaxHp
    {
        get
        {
            return Ability.OnReturnValue(this, Stat.StatType.MaxHp);
        }
    }

    public float MoveSpeed
    {
        get
        {
            var moveSpeed= Ability.OnReturnValue(this, Stat.StatType.MoveSpeed); 
            var moveSpeedIncRate = Ability.OnReturnValue(this, Stat.StatType.MoveSpeedIncRate);
            var moveSpeedDecRate = Ability.OnReturnValue(this, Stat.StatType.MoveSpeedDecRate);
            var totalMoveSpeed = moveSpeed;
            return totalMoveSpeed + totalMoveSpeed * (moveSpeedIncRate - moveSpeedDecRate)*0.01f;
        }
    }

    public override void Init(Transform transform, Collider collider, MeshRenderer meshRenderer)
    {
        base.Init(transform, collider, meshRenderer);

        Subject = this;

        CurrentHp = MaxHp;
    }

    public void OnActorHit(float damage)
    {
        if (CurrentHp <= damage)
            OnActorDeath();
        else
            actorHit?.Invoke(this, damage);
     
        CurrentHp -= damage;
    }
    public void OnActorDeath()
    {
        actorDeath?.Invoke(this);
        OnRemoveData();
    }
}
