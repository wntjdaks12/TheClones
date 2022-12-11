using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClonStorePopup : Popup
{
    [Header("BUTTON")]
    [SerializeField] private Button ExitBtn;

    public void Init()
    {
        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        OnShow();
    }
}
