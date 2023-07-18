using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorDeathState : IActorState
{
    public static ActorDeathState Instance { get; private set; } = new ActorDeathState();

    public void OnDeath(ActorObject actorObject)
    {
    }

    public void OnHit(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Hit;

        actorObject.setState(ActorHitState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetTrigger("OnHit");
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
