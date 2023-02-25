using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MonsterController : GameView
{
    [System.Serializable]
    public class Target
    {
        public uint id;
        public Vector3 spawnPosition;
    }

    [SerializeField] private List<Target> targets;

    public IEnumerator Start()
    {
        var entityController = App.GameController.GetController<CharacterController>();

        while (true)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                entityController.Spawn("Monster", targets[i].id, targets[i].spawnPosition);
            }

            yield return new WaitForSeconds(15);
        }
    }
}
