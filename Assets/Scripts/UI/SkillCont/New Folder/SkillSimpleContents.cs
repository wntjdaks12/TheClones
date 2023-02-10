using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSimpleContents : MonoBehaviour
{
    [SerializeField] private List<SkillSimpleSlot> skillSimpleSlots;

    public void Init(Clon clone)
    {
        for (int i = 0; i < skillSimpleSlots.Count; i++)
        {
            skillSimpleSlots[i].OnHide();
        }

        for (int i = 0; i < clone.skillId.Length; i++)
        {
            skillSimpleSlots[i].Init(clone.skillId[i]);
        }
    }
}
