using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHitState : IActorState
{
    public static ActorHitState Instance { get; private set; } = new ActorHitState();

    public void OnDeath(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Death;

        actorObject.setState(ActorDeathState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetTrigger("OnDead");
    }

    public void OnHit(ActorObject actorObject)
    {
    }

    public void OnIdle(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Idle;

        actorObject.setState(ActorIdleState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetTrigger("OnIdle");

    }

    public void OnMove(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Move;

        actorObject.setState(ActorMoveState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetBool("IsMove", true);
    }
}
