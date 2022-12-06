using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    private CharacterManager monsterManager;

    private void Awake()
    {
        monsterManager = CharacterManager.Instance;
    }

    private void Start()
    {
        monsterManager.Characters.Add(this);
    }
}
