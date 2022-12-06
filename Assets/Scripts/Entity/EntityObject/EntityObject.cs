using System;
public class EntityObject : PoolableObject
{
    public Action<EntityObject> clickedObject;

    public Entity Entity { get; set; }

    public virtual void Init(Entity entity)
    {
        this.Entity = entity;
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
