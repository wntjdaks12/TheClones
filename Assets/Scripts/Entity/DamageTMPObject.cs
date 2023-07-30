using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTMPObject : EntityObject
{
    [SerializeField] private TextMeshProUGUI damageTMP;

    public void Init(DamageTMP damageTMP, float value)
    {
        base.Init(damageTMP);

        var ai = GetComponent<EntityAI>();

        if (ai != null) ai.Entity = damageTMP;

        if (damageTMP.Lifetime != 0) StartCoroutine(damageTMP.StartLifeTime());

        ShowData(value);
    }

    public void ShowData(float value)
    {
        this.damageTMP.text = value.ToString();
    }
}
