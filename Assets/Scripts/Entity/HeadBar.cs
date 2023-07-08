using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBar : Entity
{
    public Transform HeadBarTransform { get; set; }

    public void OnRemove(IData data)
    {
        Debug.Log("!!!!!!!!!!!!");
        OnRemoveData();
    }
}
