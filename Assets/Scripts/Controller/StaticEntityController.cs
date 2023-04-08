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
        var staticEntity = dataController.AddData(tableId, id) as StaticEntity;

        staticEntity.Init(entityObject.transform, entityObject.GetComponent<Collider>());
        entityObject.Init(staticEntity);

        runtimeDataModel.AddData($"{tableId}Object", staticEntity.InstanceId, entityObject);
    }
}
