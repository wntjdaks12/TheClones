using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemController : GameController
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
        Debug.Log(" ������ ���� :" + tableId + id);
        var dropItem = dataController.AddData(tableId, id) as DropItem;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        var entityObject = PoolObjectContainer.CreatePoolableObject<EntityObject>(prefabInfo.PrefabId.ToString());

        entityObject.OnDataRemove += RemoveEntity;
        entityObject.gameObject.SetActive(true);

        dropItem.Caster = caster as Entity;

        dropItem.Init(entityObject.transform, entityObject.GetComponent<Collider>());
        entityObject.Init(dropItem);

        runtimeDataModel.AddData($"{tableId}Object", dropItem.InstanceId, entityObject);

        entityObject.transform.position = position;
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