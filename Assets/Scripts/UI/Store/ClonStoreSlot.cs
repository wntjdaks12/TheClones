using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonStoreSlot : MonoBehaviour
{
    public void OnPurchaseComplete()
    {
        Debug.Log("인앱 구매 성공");
    }

    public void OnPurchaseFail()
    {
        Debug.Log("인앱 구매 실패");
    }
}
