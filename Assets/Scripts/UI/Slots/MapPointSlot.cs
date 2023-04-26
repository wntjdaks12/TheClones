using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class MapPointSlot : GameView
{
    [SerializeField] private int stageIndex;
    public int StageIndex { get => stageIndex; }

    public RectTransform rectTransform { get; private set; }

    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI nameLeghtTMP;
    [SerializeField] private TextMeshProUGUI nameTMP;

    public void Init()
    {
        var map = App.GameModel.PresetDataModel.ReturnDatas<Map>()
            .Where(x => GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo.mapId == x.SceneId).FirstOrDefault();

        var stage = App.GameModel.PresetDataModel.ReturnDatas<Stage>()
            .Where(x => x.Id == map.StageIds[stageIndex]).FirstOrDefault();

        nameLeghtTMP.text = stage.name;
        nameTMP.text = nameLeghtTMP.text;

        rectTransform = transform.GetComponent<RectTransform>();
    }
}
