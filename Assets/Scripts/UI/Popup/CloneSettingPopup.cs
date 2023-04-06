using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSettingPopup : Popup
{
    [Header("��ư"), SerializeField] private Button ExitBtn;
    [SerializeField] private Button RuneBtn;

    [Header("������"), SerializeField] private CloneSettingDetailContents detailContents;
    [SerializeField] private StatContents statContents;
    [SerializeField] private PoolingScrollview poolingScrollview;
    [SerializeField] private GameObject Content1, Content2;
    [SerializeField] private List<GoodsSlot> goodsSlots;

    private ClonInfo cloneInfo;

    public override void Init()
    {
        OnShow();

        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        RuneBtn.onClick.RemoveAllListeners();
        RuneBtn.onClick.AddListener(() => ReturnPopup<RunePopup>().Init(cloneInfo));

        var cloneInfos = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.cloneInofs;

        // Ǯ�� ��ũ�� �� �ʱ�ȭ �� ������ Ŭ�� �ݹ� �޼ҵ� �߰�
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

            DetailContentsInit(cloneInfos, 0); // �˾� �ʱ�ȭ �� ù��° Ŭ������ �ʱ�ȭ

            statContents.Init(cloneInfo.clonId); // ���� ������ �ʱ�ȭ
        }
        else
        {
            Content1.SetActive(false);
            Content2.SetActive(true);
        }

        goodsSlots.ForEach(x => x.Init());
    }

    /// <summary>
    /// ������ �������� �ʱ�ȭ ��ŵ�ϴ�.
    /// </summary>
    /// <param name="cloneInfos">Ŭ�� ���� ����Ʈ</param>
    /// <param name="index">�ε���</param>
    private void DetailContentsInit(List<ClonInfo> cloneInfos, int index)
    {
        var presetDataModel = App.GameModel.PresetDataModel;

        var clone = presetDataModel.ReturnData<Clon>(nameof(Clon), cloneInfos[index].clonId);
        clone.skillId = cloneInfos[index].skillId;

        detailContents.Init(clone);
    }
}
