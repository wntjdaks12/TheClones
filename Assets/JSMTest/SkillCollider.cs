using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    [SerializeField] private SkillObject skillObject;

    private List<CharacterObject> hitedOtherCharacterObjs = new List<CharacterObject>();

    private Coroutine triggerAsync;

    private void OnEnable()
    {
        hitedOtherCharacterObjs.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        var otherCharacterObj = other.GetComponent<CharacterObject>();

        if (otherCharacterObj != null)
        {
            var skill = skillObject.Entity as DevSkill;

            var otherCharacter = otherCharacterObj.Entity as Character;

            if (otherCharacter.StateType == Actor.StateTypes.Death) return;
            Debug.Log("a");
            for (int i = 0; i < skill.LayerName.Length; i++)
            {
                Debug.Log("b");
                if (skill.LayerName[i] == LayerMask.LayerToName(other.gameObject.layer))
                {
                    Debug.Log("c");
                    if (skill.SpawnCount > 0 && hitedOtherCharacterObjs.Count > skill.SpawnCount) return;

                    hitedOtherCharacterObjs.Add(otherCharacterObj);

                    triggerAsync = CoroutineHelper.StartCoroutine(skill.SkillStrategy.OnTrigger(otherCharacterObj.Entity, skill));

                    otherCharacterObj.Entity.OnDataRemove += (data) => CoroutineHelper.StopCoroutine(triggerAsync);
                    Debug.Log("d");
                    if (skill.Id == 30003)
                    {
                        GameObject.Find("App").GetComponent<GameApplication>().GameController.GetComponent<ParticleController>().Spawn(nameof(Particle), 60004, Vector3.zero, otherCharacterObj.Entity);
                    }
                }
            }
        }
    }
}
