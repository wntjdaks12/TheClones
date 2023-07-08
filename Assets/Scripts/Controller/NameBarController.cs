using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBarController : GameController
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
        var nameBar = dataController.AddData(tableId, id) as NameBar;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        var nameBarObject = PoolObjectContainer.CreatePoolableObject<NameBarObject>(prefabInfo.PrefabId.ToString());

        nameBar.OnDataRemove += RemoveEntity;
        nameBarObject.gameObject.SetActive(true);

        var actor = caster as Actor;
        if (actor.Transform.GetComponent<CharacterAI>() != null)
        {
            actor.Transform.GetComponent<CharacterAI>().traceStopEvent += () =>
                {
                    nameBar.OnRemove(nameBar);

                    nameBar.OnDataRemove -= RemoveEntity;
                };
        }
        nameBar.NameBarTransform = actor.NameBarTransform;

        nameBar.Caster = actor;
        nameBar.Subject = actor;

        nameBar.Init(nameBarObject.transform, transform.GetComponent<Collider>());
        nameBarObject.Init(nameBar);

        actor.OnDataRemove += nameBar.OnRemove;

        runtimeDataModel.AddData($"{tableId}Object", nameBar.InstanceId, nameBarObject);

        nameBarObject.transform.position = position;
        nameBarObject.transform.SetParent(parent, false);
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
