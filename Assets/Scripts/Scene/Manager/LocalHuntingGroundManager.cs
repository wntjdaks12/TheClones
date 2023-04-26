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

        var stage = App.GameModel.PresetDataModel.ReturnDatas<Stage>()
            .Where(x => x.Id == map.StageIds[mapInfo.stageIndex]).FirstOrDefault();

        //nameTMP.text = stage.name;
    }

    public void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red,  10000);

        var ground = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 10000).Where(x => x.collider.GetComponent<GroundObject>()).Select(x => x.collider.GetComponent<GroundObject>().Entity as Ground).FirstOrDefault();

        if (ground != null)
        {
            Debug.Log("asd");
            var stage = App.GameModel.PresetDataModel.ReturnDatas<Stage>()
                .Where(x => x.Id == ground.stageId).FirstOrDefault();

            nameTMP.text = stage.name;
        }
    }
}
