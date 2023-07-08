using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] private ActorObject actorObject;

    public void OnDeath()
    {

    }

    public void OnHit()
    {
    }

    public void OnIdle()
    {
        actorObject.OnIdle();
    }

    public void OnMove()
    {
    }
}
