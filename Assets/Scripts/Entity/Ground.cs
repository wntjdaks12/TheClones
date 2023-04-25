using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : EntityObject
{
    private void Start()
    {
        GameObject.FindObjectOfType<GameApplication>().GameController.GetComponent<StaticEntityController>().Spawn("StaticEntity", 80002, this);
    }

    public void OnPoint()
    {
        Entity.BlendAddMaterial(Entity.MeshRenderer, materialSystem.ChangedMaterials);
    }

    public void OffPoint()
    {
        Entity.BlendRemoveMaterial(Entity.MeshRenderer, materialSystem.ChangedMaterials);
    }
}
