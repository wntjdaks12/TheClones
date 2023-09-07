using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class SpawnSystem : GameView, ISpawner
{
    public List<Spawner> Spawners { get; set; } = new List<Spawner>();

    private void Start()
    {
        StartCoroutine(SpawnAsync());
    }

    public IEnumerator SpawnAsync()
    {
        var characterController = App.GameController.GetController<CharacterController>();

        var mapInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo;

        var map = App.GameModel.PresetDataModel.ReturnData<Map>(nameof(Map), mapInfo.mapId);

        // 해당 스테이지의 스폰할 오브젝트들을 스폰합니다.
        for (int i = 0; i < map.StageIds.Count; i++)
        {
            var spawnInfo = App.GameModel.PresetDataModel.ReturnData<SpawnInfo>(nameof(SpawnInfo), map.StageIds[i]);

            for (int j = 0; j < spawnInfo.SpawnObjectInfos.Count; j++) // 스폰 오브젝트들을 스폰합니다   .
            {
                var spawner = new Spawner();
                spawner.SpawnObjectInfo = spawnInfo.SpawnObjectInfos[j];

                Spawners.Add(spawner);

                for (int k = 0; k < spawner.SpawnObjectInfo.MaxNumberOfSpawn; k++) // 최대 개수만큼 스폰 합니다.
                {
                    spawner.stageId = spawnInfo.Id;
                    ++spawner.SpawnObjectInfo.CurNumberOfSpawn;

                    var ground = App.GameModel.RuntimeDataModel.ReturnDatas<Ground>(nameof(Ground)).Where(x => x.stageId == spawner.stageId).FirstOrDefault();
                    var pos = ground.Transform.position; pos.y = -2;

                    characterController.Spawn("Monster", spawner.SpawnObjectInfo.Id, pos, spawner);
                }
            }
        }


        while (true) // 3초마다 해당 스테이지의 스폰할 오브젝트들을 체크하고 스폰 합니다.
        {
            yield return new WaitForSeconds(3f);

            for (int i = 0; i < map.StageIds.Count; i++)
            {
                var spawners = Spawners.Where(x => x.stageId == map.StageIds[i]).ToList(); // 해당 스테이지의 스포너들을 가져옵니다

                spawners.ForEach(spawner =>
                {
                    var spawnCount = spawner.SpawnObjectInfo.MaxNumberOfSpawn - spawner.SpawnObjectInfo.CurNumberOfSpawn; // 스폰할 오브젝트 개수

                    if (spawnCount > 0)
                    {
                        for (int j = 0; j < spawnCount; j++)
                        {
                            ++spawner.SpawnObjectInfo.CurNumberOfSpawn;

                            var ground = App.GameModel.RuntimeDataModel.ReturnDatas<Ground>(nameof(Ground)).Where(x => x.stageId == spawner.stageId).FirstOrDefault();
                            var pos = ground.Transform.position; pos.y = 0;
                            App.GameController.GetController<CharacterController>().Spawn("Monster", spawner.SpawnObjectInfo.Id, pos, spawner);
                        }
                    }
                });
            }
        }
    }
}

public interface ISpawner
{
    public List<Spawner> Spawners { get; set; }
}

public class Spawner
{
    public uint stageId;

    public SpawnObjectInfo SpawnObjectInfo { get; set; }
}