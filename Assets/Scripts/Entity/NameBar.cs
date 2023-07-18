using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBar : Entity
{
    public Transform NameBarTransform { get; set; }

    public void OnRemove(IData data)
    {
        OnRemoveData();
    }
}
