using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBar : Entity, ISubject
{
    public Transform HeadBarTransform { get; set; }

    public Subject Subject { get; set; }

    public void OnRemove(Actor actor)
    {
        OnRemoveData();
    }
}
