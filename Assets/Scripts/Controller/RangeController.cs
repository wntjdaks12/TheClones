using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeController : GameController
{
    GameDataContainerModel presetDataModel;
    GameDataContainerModel runtimeDataModel;
    DataController dataController;

    protected override void Awake()
    {
        dataController = GetController<DataController>();
        presetDataModel = App.GameModel.PresetDataModel;
        runtimeDataModel = App.GameModel.RuntimeDataModel;
    }


    public void Spawn(string tableId, uint id, Vector3 position, ICaster caster)
    {
        var range = dataController.AddData(tableId, id) as Range;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        var rangeObject = PoolObjectContainer.CreatePoolableObject<RangeObject>($"{prefabInfo.Path}/{prefabInfo.PrefabId}");

        range.OnDataRemove += RemoveEntity;
        rangeObject.gameObject.SetActive(true);

        var character = caster.Caster.Entity as Character;

        range.Radius = character.VisibleDistance;
        range.Caster = caster.Caster;

        range.Init(rangeObject.transform, rangeObject.GetComponent<Collider>());
        rangeObject.Init(range);
        
        runtimeDataModel.AddData($"{tableId}Object", range.InstanceId, rangeObject);

        rangeObject.transform.position = position;

        if (range.Lifetime != 0)
            StartCoroutine(range.StartLifeTime());
    }
    public void RemoveEntity(IData data)
    {
        var gameRuntimeDataModel = App.GameModel.RuntimeDataModel;

        var entityObjectTableName = $"{data.TableModel.TableName}Object";
        var entityObject = gameRuntimeDataModel.ReturnData<EntityObject>(entityObjectTableName, data.InstanceId);
        entityObject.OnRemoveEntity();

        gameRuntimeDataModel.RemoveData(data.TableModel.TableName, data.InstanceId);
        gameRuntimeDataModel.RemoveData(entityObjectTableName, data.InstanceId);
    }
}
