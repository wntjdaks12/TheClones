using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class CharacterController : GameController, ISpawner
{
    GameDataContainerModel presetDataModel;
    GameDataContainerModel runtimeDataModel;
    DataController dataController;

    public List<Spawner> Spawners { get; set; } = new List<Spawner>();

    protected override void Awake()
    {
        dataController = GetController<DataController>();
        presetDataModel = App.GameModel.PresetDataModel;
        runtimeDataModel = App.GameModel.RuntimeDataModel;
    }

    public void Spawn(string tableId, uint id, Vector3 position, uint[] skillId = null)
    {
        var character = dataController.AddData(tableId, id) as Character;

        var prefabInfo = (EntityPrefabInfo)presetDataModel.ReturnData<EntityPrefabInfo>(nameof(EntityPrefabInfo), id).Clone();

        //var characterObject = PoolObjectContainer.CreatePoolableObject<CharacterObject>($"{prefabInfo.Path}/{prefabInfo.PrefabId}");
        var characterObject = PoolObjectContainer.CreatePoolableObject<CharacterObject>(prefabInfo.PrefabId.ToString());

        var a = Spawners[Spawners.Count - 1].stageId;

        character.OnDataRemove += (data) =>
        {
            RemoveEntity(data);

            if (character is Monster) // 나중에 분리
                --Spawners.Where(x => x.stageId == a).Select(x => x.SpawnObjectInfo).FirstOrDefault().CurNumberOfSpawn;
        };


        characterObject.gameObject.SetActive(true);

        if (character is IAbility) GetController<AbilityController>().AddStat(character, character.Id);

        character.skillId = skillId;

        character.Init(characterObject.transform, characterObject.GetComponent<Collider>(), characterObject.GetComponent<Rigidbody>());
        characterObject.Init(character);

        if (character is Monster)
        {
            var dropItemData = App.GameModel.PresetDataModel.ReturnData<DropItemData>(nameof(DropItemData), id);

            var dropItemIds = new List<uint>();

            dropItemData.DropItemInfos.ForEach(x =>
            {
                var probability = x.dropItemTypes.Where(x => x.dropItemInfoType == DropItemType.DropItemInfoType.Probability).Select(x => x.Value).Sum();

                var randVal = UnityEngine.Random.Range(0, 101);

                if (randVal <= probability)
                {
                    dropItemIds.Add(x.ItemId);
                }
            });

            dropItemData.DropGoodsInfos.ForEach(x =>
            {
                var probability = x.dropItemTypes.Where(x => x.dropItemInfoType == DropItemType.DropItemInfoType.Probability).Select(x => x.Value).Sum();

                var randVal = UnityEngine.Random.Range(0, 101);

                if (randVal <= probability)
                {
                    dropItemIds.Add(x.ItemId);
                }
            });

            character.actorDeath += (actor) =>
            {
                for (int i = 0; i < dropItemIds.Count; i++)
                {
                    var pos = actor.Transform.position;
                    pos.x = pos.x + i - (dropItemIds.Count * 0.5f) + 0.5f;

                    App.GameController.GetController<DropItemController>().Spawn(nameof(DropItem), dropItemIds[i], pos, actor);
                }
            };
        }

        character.actorHit += (actor, damage) =>
        {
            characterObject.OnActorHit();
        };

        character.actorDeath += (actor) =>
        {
            characterObject.OnActorDeath();
        };

        character.actorIdle += (actor) =>
        {
            characterObject.OnIdle();
        };

        character.actorMove += (actor) =>
        {
            characterObject.OnActorMove();
        };

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