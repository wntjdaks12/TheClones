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

    [Header("≈ÿΩ∫∆Æ"), SerializeField] private TextMeshProUGUI nameLeghtTMP;
    [SerializeField] private TextMeshProUGUI nameTMP;

    public void Init()
    {
        var map = App.GameModel.PresetDataModel.ReturnDatas<Map>()
            .Where(x => GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo.mapId == x.SceneId).FirstOrDefault();

        nameLeghtTMP.text = map.Stage[stageIndex].Name;
        nameTMP.text = nameLeghtTMP.text;

        rectTransform = transform.GetComponent<RectTransform>();
    }
}
