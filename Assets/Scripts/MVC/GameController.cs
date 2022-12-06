using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : Element
{
    public T GetController<T>() where T : GameController
    {
        return gameObject.GetComponent<T>();
    }

    public void Start()
    {
       // App.GameModel.Inventory.OnSlotAdd += App.GameModel.UserDataModel.AddData;
    }
}
