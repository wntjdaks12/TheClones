using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBarController : GameController
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

    public void Spawn(string tableId, uint id, Vector3 position, Transform parent, ICaster caster)
    {
        var headBar = dataController.AddData(tableId, id) as HeadBar;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        //var headBarObject = PoolObjectContainer.CreatePoolableObject<HeadBarObject>($"{prefabInfo.Path}/{prefabInfo.PrefabId}");
        var headBarObject = PoolObjectContainer.CreatePoolableObject<HeadBarObject>(prefabInfo.PrefabId.ToString());

        headBar.OnDataRemove += RemoveEntity;
        headBarObject.gameObject.SetActive(true);

        var actor = caster as Actor;

        headBar.HeadBarTransform = actor.HeadBarTransform;

        headBar.Caster = actor;
        headBar.Subject = actor;

        headBar.Init(headBarObject.transform, transform.GetComponent<Collider>());
        headBarObject.Init(headBar);

        actor.actorDeath += headBar.OnRemove;

        runtimeDataModel.AddData($"{tableId}Object", headBar.InstanceId, headBarObject);

        headBarObject.transform.position = position;
        headBarObject.transform.SetParent(parent, false);
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
