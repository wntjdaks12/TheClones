using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RunePageContents : MonoBehaviour
{
    [SerializeField] private List<RuneSlot> runeSlots;

    public void Init(ClonInfo cloneInfo)
    {
        runeSlots.ForEach(x => x.Init(cloneInfo));
    }
}
