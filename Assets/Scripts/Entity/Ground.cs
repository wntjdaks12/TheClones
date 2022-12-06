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
        materialSystem.ChangeMaterial();
    }

    public void OffPoint()
    {
        materialSystem.RevertMaterial();
    }
}
