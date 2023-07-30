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

    public void Spawn(string tableId, uint id, ISpell spell)
    {
        var skill = dataController.AddData(tableId, id) as DevSkill;

        switch (skill.TargetType)
        {
            case DevSkill.TargetTypes.Single:

                var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

                var skillObject = PoolObjectContainer.CreatePoolableObject<SkillObject>(prefabInfo.PrefabId.ToString());

                skill.OnDataRemove += RemoveEntity;
                skillObject.gameObject.SetActive(true);

                skill.Caster = spell.Caster;
                skill.Subjects = spell.Subjects;

                if (skill is IAbility) GetController<AbilityController>().AddStat(skill, skill.Id);

                skill.Init(skillObject.transform, transform.GetComponent<Collider>());
                skillObject.Init(App, skill);

                runtimeDataModel.AddData($"{tableId}Object", skill.InstanceId, skillObject);

                switch (skill.SpawnType)
                {
                    case DevSkill.SpawnTypes.Caster:
                        skillObject.transform.position = spell.Caster.Transform.position;
                        skillObject.transform.rotation = spell.Caster.Transform.localRotation;

                        break;
                    case DevSkill.SpawnTypes.Subject:
                        skillObject.transform.position = spell.Subjects[0].Transform.position;
                        skillObject.transform.rotation = spell.Subjects[0].Transform.localRotation;
                        break;
                }

                break;

            case DevSkill.TargetTypes.Many: break;


         /*   case DevSkill.TargetTypess.Many:

                var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

                foreach (var subject in spell.Subjects)
                {
                    
                }

                var skillObject = PoolObjectContainer.CreatePoolableObject<SkillObject>(prefabInfo.PrefabId.ToString());

                skill.OnDataRemove += RemoveEntity;
                skillObject.gameObject.SetActive(true);

                skill.Caster = spell.Caster;
                skill.Subjects = spell.Subjects;

                if (skill is IAbility) GetController<AbilityController>().AddStat(skill, skill.Id);

                skill.Init(skillObject.transform, transform.GetComponent<Collider>());
                skillObject.Init(App, skill);

                runtimeDataModel.AddData($"{tableId}Object", skill.InstanceId, skillObject);

                skillObject.transform.position = position;

                StartCoroutine(skill.SkillStrategy.OnTrigger(skill, skill)); // 일단 추가

                break;*/
        }



        /*var skill = dataController.AddData(tableId, id) as DevSkill;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        var skillObject = PoolObjectContainer.CreatePoolableObject<SkillObject>(prefabInfo.PrefabId.ToString());

        skill.OnDataRemove += RemoveEntity;
        skillObject.gameObject.SetActive(true);

        skill.Caster = spell.Caster;
        skill.Subjects = spell.Subjects;

        if (skill is IAbility) GetController<AbilityController>().AddStat(skill, skill.Id);

        skill.Init(skillObject.transform, transform.GetComponent<Collider>());
        skillObject.Init(App, skill);

        runtimeDataModel.AddData($"{tableId}Object", skill.InstanceId, skillObject);

        skillObject.transform.position = position;

        StartCoroutine(skill.SkillStrategy.OnTrigger(skill, skill)); // 일단 추가
        */
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
