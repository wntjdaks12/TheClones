using System.Collections;
using UnityEngine; 
using System;
using System.Linq;
using Newtonsoft.Json;
//씬에 생성되는 개체
public class Entity:Data
{
    public string Name { get; private set; }

    [JsonProperty]
    public float Lifetime { get; set; }

    public Transform Transform { get; private set; }
    public Collider Collider { get; private set; }
    public virtual void Init(Transform transform, Collider collider)
    {
        Transform = transform;
        Collider = collider;
    }
    public IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(Lifetime);

        OnRemoveData();
    }
}
