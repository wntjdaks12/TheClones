using System.Linq;
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

    public void Spawn(string tableId, uint id, Entity caster, Vector3 position, Quaternion rotation, Entity parent = null)
    {
        var skill = dataController.AddData(tableId, id) as DevSkill;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        var skillObject = PoolObjectContainer.CreatePoolableObject<SkillObject>(prefabInfo.PrefabId.ToString());

        skill.OnDataRemove += RemoveEntity;
        skillObject.gameObject.SetActive(true);

        skill.Caster = caster;
        skill.Subjects = Physics.OverlapSphere(transform.position, skill.Range, LayerMask.GetMask(skill.LayerName)).Select(x => x.GetComponent<EntityObject>().Entity).ToArray();

        if (skill is IAbility) GetController<AbilityController>().AddStat(skill, skill.Id);

        skill.Init(skillObject.transform, transform.GetComponent<Collider>());
        skillObject.Init(App, skill);

        runtimeDataModel.AddData($"{tableId}Object", skill.InstanceId, skillObject);

        if (parent != null)
        {
            skillObject.transform.parent = parent.Transform;
            parent.OnDataRemove += (data) =>
            {
                skillObject.transform.parent = null;

                skillObject.OnRemoveEntity();
            };
        }


        skillObject.transform.position = position;
        skillObject.transform.rotation = rotation;
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
