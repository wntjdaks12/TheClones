using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterController : GameController
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
    
    // 몬스터 컨트롤러 따로 만듬
    /*
    public IEnumerator Start()
    {
        Spawn("Hero", 111101);

        while (true)
        {
            Spawn("Monster", 111001);
            Spawn("Monster", 111002);
            yield return new WaitForSeconds(15);
        }
    }*/

    // 생성 시 위치 값 추가
    // 스킬 id임시로 추가
    public void Spawn(string tableId, uint id, Vector3 position, uint skillId = 0)
    {
        var character = dataController.AddData(tableId,id) as Character;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        //var characterObject = PoolObjectContainer.CreatePoolableObject<CharacterObject>($"{prefabInfo.Path}/{prefabInfo.PrefabId}");
        var characterObject = PoolObjectContainer.CreatePoolableObject<CharacterObject>(prefabInfo.PrefabId.ToString());

        character.OnDataRemove += RemoveEntity; 
        characterObject.gameObject.SetActive(true);

        if (character is IAbility) GetController<AbilityController>().AddStat(character, character.Id);

        character.skillId = skillId;

        character.Init(characterObject.transform, characterObject.GetComponent<Collider>(), characterObject.GetComponent<MeshRenderer>());
        characterObject.Init(character); 

        runtimeDataModel.AddData($"{tableId}Object", character.InstanceId, characterObject);

        characterObject.transform.position = position;
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