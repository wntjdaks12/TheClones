using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSettingPopup : Popup
{
    [Header("��ư"), SerializeField] private Button ExitBtn;

    [Header("������"), SerializeField] private CloneSettingDetailContents detailContents;
    [SerializeField] private PoolingScrollview poolingScrollview;

    public override void Init()
    {
        OnShow();

        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        var cloneInfos = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo.cloneInofs;

        // Ǯ�� ��ũ�� �� �ʱ�ȭ �� ������ Ŭ�� �ݹ� �޼ҵ� �߰�
        poolingScrollview.Init(cloneInfos.Count, (index) => DetailContentsInif(cloneInfos, index));

        if (cloneInfos.Count > 0) DetailContentsInif(cloneInfos, 0); // �˾� �ʱ�ȭ �� ù��° Ŭ������ �ʱ�ȭ
    }

    /// <summary>
    /// ������ �������� �ʱ�ȭ ��ŵ�ϴ�.
    /// </summary>
    /// <param name="cloneInfos">Ŭ�� ���� ����Ʈ</param>
    /// <param name="index">�ε���</param>
    private void DetailContentsInif(List<ClonInfo> cloneInfos, int index)
    {
        var presetDataModel = App.GameModel.PresetDataModel;

        var clone = presetDataModel.ReturnData<Clon>(nameof(Clon), cloneInfos[index].clonId);

        detailContents.Init(clone);
    }
}
