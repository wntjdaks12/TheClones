using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class MapPointSlot : GameView
{
    [SerializeField] private uint stageId;
    public uint StageId { get => stageId; }

    public RectTransform rectTransform { get; private set; }

    [Header("�ؽ�Ʈ"), SerializeField] private TextMeshProUGUI nameLeghtTMP;
    [SerializeField] private TextMeshProUGUI nameTMP;

    public void Init()
    {
        var stage = App.GameModel.PresetDataModel.ReturnDatas<Stage>()
            .Where(x => x.Id == stageId).FirstOrDefault();

        nameLeghtTMP.text = stage.name;
        nameTMP.text = nameLeghtTMP.text;

        rectTransform = transform.GetComponent<RectTransform>();
    }
}
