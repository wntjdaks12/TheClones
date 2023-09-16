using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using TMPro;
using UniRx;

[Serializable]
public class BossSpawnEvent : UnityEvent<uint> { }

public class BossStateContents : GameView
{
    public BossSpawnEvent bossSpawnEvent;

    [SerializeField] private Image banImg;
    [SerializeField] private Image monsterFillImg;

    [SerializeField] private Button slotBtn;

    [SerializeField] private TextMeshProUGUI monsterCountTMP;

    private bool isActive;

    private StageInfo stageInfo;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stageInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo.stageInfo;
        stageInfo.currentNumberOfMonsterHunts.Subscribe(_ => UpdateCont());

        slotBtn.onClick.RemoveAllListeners();
        slotBtn.onClick.AddListener(SpawnBoss);

        isActive = false;
        OnDeactive();
    }

    // 활성화 시킵니다.
    private void OnActive()
    {
        banImg.gameObject.SetActive(false);

        slotBtn.image.color = new Color(1, 1, 1, 1);

        slotBtn.interactable = true;

        isActive = true;
    }

    // 비활성화 시킵니다.
    private void OnDeactive()
    {
        banImg.gameObject.SetActive(true);

        slotBtn.image.color = new Color(103 / 255f, 103 / 255f, 103 / 255f, 181 / 255f);

        slotBtn.interactable = false;

        isActive = false;
    }

    public void UpdateCont()
    {
        if (CheckActive())
        {
            if (!isActive) OnActive();
        }
        else
        {
            if (isActive) OnDeactive();
        }

        monsterCountTMP.text = stageInfo.currentNumberOfMonsterHunts.Value + " / " + stageInfo.maxNumberOfMonsterHunts.Value;
        monsterFillImg.fillAmount = (float)stageInfo.currentNumberOfMonsterHunts.Value / stageInfo.maxNumberOfMonsterHunts.Value;
    }

    public bool CheckActive()
    {
        if (stageInfo.currentNumberOfMonsterHunts.Value >= stageInfo.maxNumberOfMonsterHunts.Value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 보스를 소환합니다.
    public void SpawnBoss()
    {
        var mapInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo;

        var map = App.GameModel.PresetDataModel.ReturnData<Map>(nameof(Map), mapInfo.mapId);

        // 해당 스테이지의 스폰할 오브젝트들을 스폰합니다.
        for (int i = 0; i < map.StageIds.Count; i++)
        {
            var spawnInfo = App.GameModel.PresetDataModel.ReturnData<SpawnInfo>(nameof(SpawnInfo), map.StageIds[i]);

            for (int j = 0; j < spawnInfo.SpawnObjectInfos.Count; j++) // 스폰 오브젝트들을 스폰합니다   .
            {
                if (spawnInfo.SpawnObjectInfos[j].SpawnMethodType == 1)
                {
                    var spawner = new Spawner();
                    spawner.SpawnObjectInfo = spawnInfo.SpawnObjectInfos[j];

                    var ground = App.GameModel.RuntimeDataModel.ReturnDatas<Ground>(nameof(Ground)).Where(x => x.stageId == 130002).FirstOrDefault();
                    var pos = ground.Transform.position; pos.y = -2;

                    App.GameController.GetController<CharacterController>().Spawn("Monster", spawner.SpawnObjectInfo.Id, pos, null);

                    bossSpawnEvent?.Invoke(spawner.SpawnObjectInfo.Id);
                }
            }
        }
    }
}
