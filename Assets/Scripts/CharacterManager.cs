using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager
{
    private static CharacterManager instance = new CharacterManager();

    private List<Actor> characters = new List<Actor>();

    public static CharacterManager Instance { get => instance; }

    public List<Actor> Characters { get => characters; set => characters = value; }
}
