using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

            var character = skill.Caster as Character;
            var otherCharacter = otherCharacterObj.Entity as Character;

            if (otherCharacter.StateType == Actor.StateTypes.Death) return;

            for (int i = 0; i < character.VisibleLayerName.Length; i++)
            {
                if (character.VisibleLayerName[i] == LayerMask.LayerToName(other.gameObject.layer))
                {
                    if (hitedOtherCharacterObjs.Any(x => x == otherCharacterObj)) return;

                    hitedOtherCharacterObjs.Add(otherCharacterObj);

                    triggerAsync = CoroutineHelper.StartCoroutine(skill.SkillStrategy.OnTrigger(otherCharacterObj.Entity, skill));

                    otherCharacterObj.Entity.OnDataRemove += (data) => CoroutineHelper.StopCoroutine(triggerAsync);

                    GameObject.Find("App").GetComponent<GameApplication>().GameController.GetComponent<ParticleController>().Spawn(nameof(Particle), 60004, Vector3.zero, otherCharacterObj.Entity);
                }
            }
        }
    }
}
