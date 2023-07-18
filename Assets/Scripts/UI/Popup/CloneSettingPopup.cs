using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSettingPopup : Popup
{
    [Header("버튼"), SerializeField] private Button ExitBtn;
    [SerializeField] private Button RuneBtn;
    [SerializeField] private Button BattleBtn;

    [Header("컨텐츠"), SerializeField] private CloneSettingDetailContents detailContents;
    [SerializeField] private StatContents statContents;
    [SerializeField] private PoolingScrollview poolingScrollview;
    [SerializeField] private GameObject Content1, Content2;
    [SerializeField] private List<GoodsSlot> goodsSlots;

    private ClonInfo cloneInfo;

    private Raycaster raycaster = new Raycaster();

    public override void Init()
    {
        OnShow();

        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        RuneBtn.onClick.RemoveAllListeners();
        RuneBtn.onClick.AddListener(() => ReturnPopup<RunePopup>().Init(cloneInfo));

        BattleBtn.onClick.RemoveAllListeners();
        BattleBtn.onClick.AddListener(() =>
        {
            var tempEntityCont = App.GameController.GetComponent<ParticleController>();
            var entityCont = App.GameController.GetComponent<CharacterController>();
            tempEntityCont.Spawn("Particle", 60002, Camera.main.transform.position + Camera.main.transform.forward * 10);
            entityCont.Spawn("Clon", cloneInfo.clonId, Camera.main.transform.position + Camera.main.transform.forward * 10, cloneInfo.skillId);

            OnHide();
        });

        var cloneInfos = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.cloneInofs;

        // 풀링 스크롤 뷰 초기화 및 아이템 클릭 콜백 메소드 추가
        poolingScrollview.Init(cloneInfos.Count, (index) =>
        {
            cloneInfo = cloneInfos[index];

            DetailContentsInit(cloneInfos, index);

            statContents.Init(cloneInfo.clonId);
        });

        if (cloneInfos.Count > 0)
        {
            cloneInfo = cloneInfos[0];

            Content1.SetActive(true);
            Content2.SetActive(false);

            DetailContentsInit(cloneInfos, 0); // 팝업 초기화 시 첫번째 클론으로 초기화

            statContents.Init(cloneInfo.clonId); // 스텟 컨텐츠 초기화
        }
        else
        {
            Content1.SetActive(false);
            Content2.SetActive(true);
        }

        goodsSlots.ForEach(x => x.Init());
    }

    /// <summary>
    /// 디테일 컨텐츠를 초기화 시킵니다.
    /// </summary>
    /// <param name="cloneInfos">클론 종류 리스트</param>
    /// <param name="index">인덱스</param>
    private void DetailContentsInit(List<ClonInfo> cloneInfos, int index)
    {
        var presetDataModel = App.GameModel.PresetDataModel;

        var clone = presetDataModel.ReturnData<Clon>(nameof(Clon), cloneInfos[index].clonId);
        clone.skillId = cloneInfos[index].skillId;

        detailContents.Init(clone);
    }
}
