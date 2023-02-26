using System;
using UnityEngine;

public class EntityObject : PoolableObject
{
    public Action<EntityObject> clickedObject;

    public Entity Entity { get; set; }

    [SerializeField] protected MaterialSystem materialSystem;

    public virtual void Init(Entity entity)
    {
        this.Entity = entity;

        if (Entity.Lifetime != 0) StartCoroutine(Entity.StartLifeTime());
    }
    public void OnRemoveEntity()
    {
        ReturnPoolableObject();
    }
    public void OnClickObject()
    {
        
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
