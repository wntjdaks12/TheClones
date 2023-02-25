using UnityEngine;
using Newtonsoft.Json;

public class Character : Actor, ISight
{
    public float VisibleDistance { get;  set; }
    public string[] VisibleLayerName { get;  set; }

    public uint[] skillId { get; set; }

    public override void Init(Transform transform, Collider collider)
    {
        base.Init(transform, collider);
        Debug.Log("asdda ");
        Caster = this;
        //Subject = new Subject(this);
    }
    public Collider[] ReturnVisibleObjects(Transform transform)
    {
        if (VisibleLayerName == null)
            return new Collider[0];

        return Physics.OverlapSphere(transform.position, VisibleDistance, LayerMask.GetMask(VisibleLayerName));
    }
}
