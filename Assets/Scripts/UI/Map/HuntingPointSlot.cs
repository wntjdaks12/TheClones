using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntingPointSlot : MapView
{
    [SerializeField] private Toggle toggle;

    private void Start()
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(_ => MapModel.CurrentIndex = transform.GetSiblingIndex());
    }
}
