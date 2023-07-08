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
    public event Action<Actor> actorIdle;
    public event Action<Actor> actorMove;

    public enum StateTypes { Idle, Move, Death, Hit }
    public StateTypes StateType { get; set; }

    public uint AbilityOwnerInstanceId { get => InstanceId; }
    public Ability Ability { get; set; } = new Ability();

    public Transform HeadBarTransform { get; set; }
    public Transform NameBarTransform { get; set; }

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
            var totalMoveSpeed = this is Clon ? moveSpeed + GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.GetStatInfo(Id).statDatas.GetStat(Stat.StatType.MoveSpeed) : moveSpeed;
            return totalMoveSpeed + totalMoveSpeed * (moveSpeedIncRate - moveSpeedDecRate)*0.01f;
        }
    }

    public override void Init(Transform transform, Collider collider, Rigidbody rigidbody)
    {
        base.Init(transform, collider, rigidbody);

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
        //OnRemoveData();
    }

    public void OnActorMove()
    {
        actorMove?.Invoke(this);
    }

    public void OnActorIdle()
    {
        actorIdle.Invoke(this);
    }
}
