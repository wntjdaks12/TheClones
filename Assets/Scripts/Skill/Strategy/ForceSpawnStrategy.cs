using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSpawnStrategy : SkillStrategy
{
    private Monster monster;

    private CharacterController entityController;

    public override void Use(ICaster caster,object[] objects)
    {
        // юс╫ц
        var rand = Random.Range(0, 2);
        var a = new uint[2] { 111001, 111002 };

        entityController =  objects[0] as CharacterController;

        entityController.GetController<DataController>().OnDataSpawn += setMonster;

        entityController.Spawn("Monster", a[rand], Vector3.zero);

        AddMaterial();
    }

    public void AddMaterial()
    {
      //  monster.HeadBarTransform.GetComponent<MeshRenderer>().AddMaterial(Resources.Load<Material>("Materials/Skills/30002/Skill_30002_RimLight"));
    }

    public void ReMoveMaterial()
    {
      //  monster.HeadBarTransform.GetComponent<MeshRenderer>().RemoveMaterial(Resources.Load<Material>("Materials/Skills/30002/Skill_30002_RimLight"));
    }

    public void setMonster(IData data)
    {
        monster = data as Monster;

        entityController.GetController<DataController>().OnDataSpawn -= setMonster;
    }
}
