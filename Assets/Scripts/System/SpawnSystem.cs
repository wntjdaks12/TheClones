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

        // �ش� ���������� ������ ������Ʈ���� �����մϴ�.
        for (int i = 0; i < map.StageIds.Count; i++)
        {
            var spawnInfo = App.GameModel.PresetDataModel.ReturnData<SpawnInfo>(nameof(SpawnInfo), map.StageIds[i]);

            for (int j = 0; j < spawnInfo.SpawnObjectInfos.Count; j++) // ���� ������Ʈ���� �����մϴ�   .
            {
                var spawner = new Spawner();
                spawner.SpawnObjectInfo = spawnInfo.SpawnObjectInfos[j];

                Spawners.Add(spawner);

                for (int k = 0; k < spawner.SpawnObjectInfo.MaxNumberOfSpawn; k++) // �ִ� ������ŭ ���� �մϴ�.
                {
                    spawner.stageId = spawnInfo.Id;
                    ++spawner.SpawnObjectInfo.CurNumberOfSpawn;

                    var ground = App.GameModel.RuntimeDataModel.ReturnDatas<Ground>(nameof(Ground)).Where(x => x.stageId == spawner.stageId).FirstOrDefault();
                    var pos = ground.Transform.position; pos.y = -2;

                    characterController.Spawn("Monster", spawner.SpawnObjectInfo.Id, pos, spawner);
                }
            }
        }


        while (true) // 3�ʸ��� �ش� ���������� ������ ������Ʈ���� üũ�ϰ� ���� �մϴ�.
        {
            yield return new WaitForSeconds(3f);

            for (int i = 0; i < map.StageIds.Count; i++)
            {
                var spawners = Spawners.Where(x => x.stageId == map.StageIds[i]).ToList(); // �ش� ���������� �����ʵ��� �����ɴϴ�

                spawners.ForEach(spawner =>
                {
                    var spawnCount = spawner.SpawnObjectInfo.MaxNumberOfSpawn - spawner.SpawnObjectInfo.CurNumberOfSpawn; // ������ ������Ʈ ����

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