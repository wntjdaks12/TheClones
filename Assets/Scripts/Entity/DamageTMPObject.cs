using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTMPObject : EntityObject
{
    [SerializeField] private TextMeshProUGUI damageTMP;

    public void Init(DamageTMP damageTMP)
    {
        base.Init(damageTMP);

        var ai = GetComponent<EntityAI>();

        if (ai != null) ai.Entity = damageTMP;

        if (damageTMP.Lifetime != 0) StartCoroutine(damageTMP.StartLifeTime());

        ShowData();
    }

    public void ShowData()
    {
        var damageTMP = Entity as DamageTMP;
       // Debug.Log(damageTMP.);
        if (damageTMP.Caster is Skill)
        {
            var skill = damageTMP.Caster as Skill;

            this.damageTMP.text = skill.Damage.ToString();
        }
    }
}
