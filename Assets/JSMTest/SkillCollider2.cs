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

            var character = skill.Caster as Character;

            for (int i = 0; i < character.VisibleLayerName.Length; i++)
            {
                if (character.VisibleLayerName[i] == LayerMask.LayerToName(other.gameObject.layer))
                {
                    if (hitedOtherCharacterObjs.Count > 1) return;

                    hitedOtherCharacterObjs.Add(otherCharacterObj);

                    CoroutineHelper.StartCoroutine(skill.SkillStrategy.OnTrigger(otherCharacterObj.Entity, skill));
                }
            }
        }
    }
}
