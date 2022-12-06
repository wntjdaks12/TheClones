using UnityEngine;

public class SkillController : GameController
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

    public void Spawn(string tableId, uint id, Vector3 position, ISpell spell)
    {
        var skill = dataController.AddData(tableId, id) as Skill;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        var skillObject = PoolObjectContainer.CreatePoolableObject<SkillObject>($"{prefabInfo.Path}/{prefabInfo.PrefabId}");

        skill.OnDataRemove += RemoveEntity;
        skillObject.gameObject.SetActive(true);

        skill.Caster = spell.Caster;
        skill.Subject = spell.Subject;

        if (skill is IAbility) GetController<AbilityController>().AddStat(skill, skill.Id);

        skill.Init(skillObject.transform, transform.GetComponent<Collider>());
        skillObject.Init(App, skill);

        runtimeDataModel.AddData($"{tableId}Object", skill.InstanceId, skillObject);

        skillObject.transform.position = position;
    }
    public void RemoveEntity(IData data)
    {
        var gameRuntimeDataModel = App.GameModel.RuntimeDataModel;

        var skillObjectTableName = $"{data.TableModel.TableName}Object";
        var skillObject = gameRuntimeDataModel.ReturnData<SkillObject>(skillObjectTableName, data.InstanceId);
        skillObject.OnRemoveEntity();

        gameRuntimeDataModel.RemoveData(data.TableModel.TableName, data.InstanceId);
        gameRuntimeDataModel.RemoveData(skillObjectTableName, data.InstanceId);
    }
}
