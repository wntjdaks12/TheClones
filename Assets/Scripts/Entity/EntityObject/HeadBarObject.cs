using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadBarObject : EntityObject
{
    [SerializeField] private Image hp, lerpHp;

    [Header("���� Hp �ӵ�")]
    public int lerpSpeed;

    public override void Init(Entity entity)
    {
        base.Init(entity);

        setA();
    }

    private void LateUpdate()
    {
        setA();
    }

    private void setA()
    {
        if (Entity is HeadBar)
        {
            var headBar = Entity as HeadBar;

            if (headBar.Subject.Entity is Actor)
            {
                var actor = headBar.Subject.Entity as Actor;

                hp.fillAmount = actor.CurrentHp / actor.MaxHp;
                lerpHp.fillAmount = Mathf.Lerp(lerpHp.fillAmount, hp.fillAmount, Time.deltaTime * lerpSpeed);

                transform.position = Camera.main.WorldToScreenPoint(actor.HeadBarTransform.position);
            }
        }
    }
}
