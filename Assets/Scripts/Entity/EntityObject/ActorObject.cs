using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorObject : EntityObject
{
    public Animator Animator { get => animator; }
    [SerializeField] private Animator animator;

    public void OnActorHit(Actor actor, float damage)
    {
        Animator.SetTrigger("OnHit");
    }
    public void OnActorDeath(Actor actor)
    {
        Animator.SetBool("IsDead", true);
        OnRemoveEntity();
    }
}
