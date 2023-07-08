using UnityEngine;

public class CharacterObject : ActorObject
{
    public Transform HeadBarTransform { get => headBarTransform; }
    [SerializeField] private Transform headBarTransform;

    public override void Init(Entity entity)
    {
        var character = entity as Character;

        base.Init(character);

        character.HeadBarTransform = HeadBarTransform;

        var ai = GetComponent<CharacterAI>();

        if (character is Clon)
        {
            if (ai != null) ai.Init2(character);
        }
        else
        {
            if (ai != null) ai.Init(character);
        }
    }
}
