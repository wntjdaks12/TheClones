using UnityEngine;

public class DamageTMPController : GameController
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
        var damageTMP = dataController.AddData(tableId, id) as DamageTMP;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        //var damageTMPObject = PoolObjectContainer.CreatePoolableObject<DamageTMPObject>($"{prefabInfo.Path}/{prefabInfo.PrefabId}");
        var damageTMPObject = PoolObjectContainer.CreatePoolableObject<DamageTMPObject>(prefabInfo.PrefabId.ToString());

        damageTMP.OnDataRemove += RemoveEntity;
        damageTMPObject.gameObject.SetActive(true);

        damageTMP.Caster = caster;

        damageTMP.Init(damageTMPObject.transform, transform.GetComponent<Collider>());
        damageTMPObject.Init(damageTMP);

        damageTMPObject.transform.position = position;
        damageTMPObject.transform.parent = parent;

        runtimeDataModel.AddData($"{tableId}Object", damageTMP.InstanceId, damageTMPObject);
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
