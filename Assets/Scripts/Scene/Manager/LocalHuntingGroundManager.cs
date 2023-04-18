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
        var presetDataModel = App.GameModel.PresetDataModel;

        var map = presetDataModel.ReturnDatas<Map>()
            .Where(x => SceneManager.GetActiveScene().name == x.SceneId.ToString()).FirstOrDefault();

        nameTMP.text = map.Name;
    }
}
