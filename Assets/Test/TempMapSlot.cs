using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TempMapSlot : GameView
{
    [SerializeField] private TextMeshProUGUI nameTMP;

    [SerializeField] private Button slotBtn;

    private Map map;

    public void Init(int index)
    {
        map = App.GameModel.PresetDataModel.ReturnDatas<Map>(nameof(Map))[index];

        slotBtn.onClick.RemoveAllListeners();
        slotBtn.onClick.AddListener(() => SceneManager.LoadScene(map.SceneId.ToString()));

        ShowData();
    }

    public void ShowData()
    {
        nameTMP.text = map.Name + "(" + map.Id + ")";
    }
}
