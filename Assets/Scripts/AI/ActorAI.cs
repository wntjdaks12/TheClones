using UnityEngine;

public class EntityAI : GameView
{
    public Entity Entity { get; set; }
}
public class ActorAI: EntityAI
{
    public virtual void Init(Actor actor)
    {
        Entity = actor;
    }
}
