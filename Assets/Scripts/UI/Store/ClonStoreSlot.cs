using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class ClonStoreSlot : MonoBehaviour
{
    [SerializeField] private IAPButton iapButton;

    public void Init()
    {
        //iapButton.productId = "clon"
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
