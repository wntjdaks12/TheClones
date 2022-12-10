﻿using UnityEngine;

public class EntityAI : GameView
{
    public Entity Entity { get; set; }
}
public class ActorAI: EntityAI
{
    public void Init(Actor actor)
    {
        Entity = actor;

        App.GameController.GetController<HeadBarController>().Spawn("HeadBar", 110001, Entity.Transform.position, GameObject.Find("WorldCanvas").transform, actor);
    }
}
