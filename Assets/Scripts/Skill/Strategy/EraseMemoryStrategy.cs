using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EraseMemoryStrategy : SkillStrategy
{
    private Skill skill;

    private CharacterController entityController;

    public override void Use(ICaster caster,object[] objects)
    {
        entityController = objects[0] as CharacterController;

        var monsters =  entityController.App.GameModel.RuntimeDataModel.ReturnDatas<Monster> ();

        entityController.GetController<DataController>().OnDataSpawn += setSkill;

        foreach (Monster monster in monsters)
        {
            SpawnEntity(monster);

           // skill.Use(monster.Transform);
        }
    }
    
    private void SpawnEntity(Entity entity)
    {
        if (entity is Monster)
        {
            var monster = entity as Monster;

           // entityController.App.GameController.GetController<SkillController>().Spawn("Skill", 30001);

            skill.Transform.parent = monster.HeadBarTransform;
            skill.Transform.localPosition = Vector3.zero;
            monster.Collider.ObserveEveryValueChanged(x => x.bounds.size).Subscribe(_ => ch(skill, monster));

        //    monster.actorDeath += skill.OnDestroy;
        }
    }

    public void setSkill(IData data)
    {
        if (data is Skill)
        {
            skill = data as Skill;
        }

        if (data is Monster)
        {
            var monster = data as Monster;

                 
                SpawnEntity(monster);

          //  skill.Use(monster.Transform);
        }
    }

    public void ch(Skill skill, Monster monster)
    {
        Debug.Log(monster.Collider.bounds.size);
      //  skill.Transform.localScale = skill.Transform.localScale + monster.Collider.bounds.size;
    }
}
