using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIdleState : IActorState
{
    public static ActorIdleState Instance { get; private set; } = new ActorIdleState();

    public void OnDeath(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Death;

        actorObject.setState(ActorDeathState.Instance);

        if (actorObject.Animator != null) actorObject.Animator.SetTrigger("OnDead");
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
    }

    public void OnMove(ActorObject actorObject)
    {
        var actor = actorObject.Entity as Actor;
        actor.StateType = Actor.StateTypes.Move;

        actorObject.setState(ActorMoveState.Instance);

        if(actorObject.Animator != null) actorObject.Animator.SetBool("IsMove", true);
    }
}
