using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorObject : EntityObject
{
    public Animator Animator { get => animator; }
    [SerializeField] private Animator animator;

    public Transform NameBarTransform { get => nameBarTransform; }
    [SerializeField] private Transform nameBarTransform;

    private IActorState state;

    public override void Init(Entity entity)
    {
        var actor = entity as Actor;

        base.Init(entity);

        actor.NameBarTransform = NameBarTransform;

        setState(ActorIdleState.Instance);
    }

    public void OnIdle()
    {
        state.OnIdle(this);
    }

    public void OnActorHit()
    {
        state.OnHit(this);
    }

    public void OnActorDeath()
    {
        state.OnDeath(this);

        StartCoroutine(OnActorDeathAsync());
    }

    public void OnActorMove()
    {
        state.OnMove(this);
    }

    public void setState(IActorState state)
    {
        this.state = state;
    }

    public IEnumerator OnActorDeathAsync()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Entity.OnRemoveData();
    }
}   
