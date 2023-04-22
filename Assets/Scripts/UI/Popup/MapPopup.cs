using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MapPopup : Popup
{
    [Header("이미지"), SerializeField] private Image img;

    [Header("버튼"), SerializeField] private Button exitBtn;

    [Header("텍스트"), SerializeField] private TextMeshProUGUI nameTMP;

    [SerializeField] private List<MapPointSlot> mapPointSlots;

    [SerializeField] private RectTransform myLocationRectTransform;

    public override void Init()
    {
        OnShow();

        exitBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(OnHide);

        var mapInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.mapInfo;

        nameTMP.text = App.GameModel.PresetDataModel.ReturnDatas<Map>().Where(x => x.SceneId == mapInfo.mapId).FirstOrDefault().Name;

        mapPointSlots.ForEach(x => x.Init());

        var mapPointSlot = mapPointSlots
            .Where(x => x.StageIndex == mapInfo.stageIndex).FirstOrDefault();

        myLocationRectTransform.anchoredPosition = mapPointSlot.rectTransform.anchoredPosition;
    }
}
