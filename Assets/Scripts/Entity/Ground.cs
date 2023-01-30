using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : EntityObject, IHuntingPoint
{
    [SerializeField] private MaterialSystem materialSystem;

    private void Start()
    {
        GameObject.FindObjectOfType<GameApplication>().GameController.GetComponent<StaticEntityController>().Spawn("StaticEntity", 80001, this);
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
