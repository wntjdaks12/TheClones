using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class LocalHuntingGroundManager : GameView
{
    [SerializeField] private TextMeshProUGUI nameTMP;

    private Stage stage;

    private IEnumerator typingAsync, alphaAsync;

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
            .Where(x => x.Id == mapInfo.stageId).FirstOrDefault();

        this.stage = stage;

        setFont();
    }

    public void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red,  10000);

        var ground = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 10000).Where(x => x.collider.GetComponent<GroundObject>()).Select(x => x.collider.GetComponent<GroundObject>().Entity as Ground).FirstOrDefault();

        if (ground != null)
        {
            var stage = App.GameModel.PresetDataModel.ReturnDatas<Stage>()
                .Where(x => x.Id == ground.stageId).FirstOrDefault();
            
            if (this.stage != stage)
            {
                this.stage = stage;

                var mapInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo;
                mapInfo.stageId = stage.Id;

                setFont();
            }
        }
    }

    private void setFont()
    {
        if (typingAsync != null) StopCoroutine(typingAsync);
        if (alphaAsync != null) StopCoroutine(alphaAsync);

        typingAsync = nameTMP.TypingAsync(stage.name, 0.05f);
        alphaAsync = nameTMP.AlphaAsync(0.01f);

        StartCoroutine(typingAsync);
        StartCoroutine(alphaAsync);
    }
}
