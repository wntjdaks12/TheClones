using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ParticleController : GameController
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

    public void Spawn(string tableId, uint id, Vector3 position)
    {
        var particle = dataController.AddData(tableId, id) as Particle;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        //var particleObject = PoolObjectContainer.CreatePoolableObject<ParticleObject>($"{prefabInfo.Path}/{prefabInfo.PrefabId}");
        var particleObject = PoolObjectContainer.CreatePoolableObject<ParticleObject>(prefabInfo.PrefabId.ToString());

        particle.OnDataRemove += RemoveEntity;
        particleObject.gameObject.SetActive(true);

        particle.Init(particleObject.transform, particleObject.GetComponent<Collider>());
        particleObject.Init(particle);

        runtimeDataModel.AddData($"{tableId}Object", particle.InstanceId, particleObject);

        particleObject.transform.position = position;
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
