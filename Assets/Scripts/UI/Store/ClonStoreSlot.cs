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
        Debug.Log("인앱 구매 성공");
    }

    public void OnPurchaseFail()
    {
        Debug.Log("인앱 구매 실패");
    }
}
