using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : EntityObject
{
    [SerializeField] private uint id;

    void Start()
    {
        GameObject.FindObjectOfType<GameApplication>().GameController.GetComponent<StaticEntityController>().Spawn("Ground", id, this);
    }
}
