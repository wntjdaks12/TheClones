using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBar : Entity
{
    public Transform HeadBarTransform { get; set; }

    public void OnRemove(Actor actor)
    {
        OnRemoveData();
    }
}
