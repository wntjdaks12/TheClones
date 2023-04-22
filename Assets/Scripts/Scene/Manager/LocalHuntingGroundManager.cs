using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class LocalHuntingGroundManager : GameView
{
    [SerializeField] private TextMeshProUGUI nameTMP;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        var mapInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo;

        var map = App.GameModel.PresetDataModel.ReturnDatas<Map>()
            .Where(x => mapInfo.mapId == x.SceneId).FirstOrDefault();

        nameTMP.text = map.Stage[mapInfo.stageIndex].Name;
    }
}
