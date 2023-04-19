using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class LocalHuntingManager : GameView
{
    [SerializeField] private TextMeshProUGUI nameTMP;

    private void Start()
    {
        Init();    
    }

    public void Init()
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        var presetDataModel = App.GameModel.PresetDataModel;

        var map = presetDataModel.ReturnDatas<Map>().Where(x => SceneManager.GetActiveScene().name == x.SceneId.ToString()).FirstOrDefault();

        nameTMP.text = map.Name.ToString();
    }
}
