using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider2 : MonoBehaviour
{
    [SerializeField] private SkillObject skillObject;

    private List<CharacterObject> hitedOtherCharacterObjs = new List<CharacterObject>();

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

            for (int i = 0; i < skill.LayerName.Length; i++)
            {
                if (skill.LayerName[i] == LayerMask.LayerToName(other.gameObject.layer))
                {
                    if (skill.SpawnCount > 0 && hitedOtherCharacterObjs.Count > skill.SpawnCount) return;

                    hitedOtherCharacterObjs.Add(otherCharacterObj);

                    CoroutineHelper.StartCoroutine(skill.SkillStrategy.OnTrigger(otherCharacterObj.Entity, skill));
                }
            }
        }
    }
}
