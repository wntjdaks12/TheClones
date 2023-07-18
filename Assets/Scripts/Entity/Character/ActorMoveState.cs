using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMoveState : IActorState
{
    public static ActorMoveState Instance { get; private set; } = new ActorMoveState();

    public void OnDeath(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Death;

        actorObject.setState(ActorDeathState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetTrigger("OnDead");
        if (actorObject.Animator != null) actorObject.Animator.SetBool("IsMove", false);
    }

    public void OnHit(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Hit;

        actorObject.setState(ActorHitState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetTrigger("OnHit");
        if (actorObject.Animator != null) actorObject.Animator.SetBool("IsMove", false);
    }

    public void OnIdle(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Idle;

        actorObject.setState(ActorIdleState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetBool("IsMove", false);
    }

    public void OnMove(ActorObject actorObject)
    {
    }
}
