using UnityEngine;

public class CharacterObject : ActorObject
{
    public Transform HeadBarTransform { get => headBarTransform; }
    [SerializeField] private Transform headBarTransform;

    public void Init(Character character)
    {
        base.Init(character);
        Debug.Log("zxczxc!!!! ");
        character.HeadBarTransform = HeadBarTransform;

        var ai = GetComponent<CharacterAI>();
        var actor = character as Actor;

        if (ai != null) ai.Init(character);

        if (character.Lifetime != 0) StartCoroutine(character.StartLifeTime());
    }
}
