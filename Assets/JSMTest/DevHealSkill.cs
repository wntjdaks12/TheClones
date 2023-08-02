using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevHealSkill : DevISkillStrategy
{
    public IEnumerator OnTrigger(ISubject subject, DevSkill skill)
    {
        Debug.Log("x!!!!!!!!!!");
        foreach (var other in subject.Subjects)
        {
            if (other is Actor)
            {
                var otherActor = other as Actor;

                var damagePos = other.Transform.position;

                otherActor.OnActorHeal(skill.Damage * skill.AttackCount);

                for (int i = 0; i < skill.AttackCount; i++)
                {
                    GameObject.Find("App").GetComponent<GameApplication>().GameController.GetComponent<DamageTMPController>().Spawn("DamageTMP", 40001, damagePos, GameObject.Find("DamagePopupCanvas").transform, skill.Damage);

                    damagePos.y++;
                    Debug.Log("xcvxcvxcvxcva");
                    yield return new WaitForSeconds(skill.DamageOverTime);
                }
            }
        }
    }
}
