using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class ClonStoreSlot : GameView, IPollingScrollview
{
    [SerializeField] private IAPButton iapButton;
    [SerializeField] private TextMeshProUGUI name;

    private int index;

    public new void Awake()
    {
        base.Awake();

        var iaps = App.GameModel.PresetDataModel.ReturnDatas<IAP>();

        foreach (var data in iaps)
        {
            Debug.Log(data);
        }
        iapButton.productId = "clon_gacha_001";
    }

    public void Init(int index)
    {
        this.index = index;

        ShowData();
    }

    private void ShowData()
    {
        var iap = App.GameModel.PresetDataModel.ReturnDatas<IAP>()[index];

        iapButton.productId = iap.ProductID;
        name.text = iap.Name;
    }

    public void OnPurchaseComplete()
    {
        Debug.Log("�ξ� ���� ����");
    }

    public void OnPurchaseFail()
    {
        Debug.Log("�ξ� ���� ����");
    }
}
