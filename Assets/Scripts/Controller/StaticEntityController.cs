using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEntityController : GameController
{
    GameDataContainerModel runtimeDataModel;
    DataController dataController;

    protected override void Awake()
    {
        dataController = GetController<DataController>();
        runtimeDataModel = App.GameModel.RuntimeDataModel;
    }

    public void Spawn(string tableId, uint id, EntityObject entityObject)
    {
        var ground = dataController.AddData(tableId, id) as Ground;
        Debug.Log(ground);
        ground.Init(entityObject.transform, entityObject.GetComponent<Collider>());
        entityObject.Init(ground);

        runtimeDataModel.AddData($"{tableId}Object", ground.InstanceId, entityObject);
    }
}
