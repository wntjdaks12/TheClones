using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunePopup : Popup
{
    [Header("¹öÆ°"), SerializeField] private Button ExitBtn;

    [Header("ÄÁÅÙÃ÷"), SerializeField] private RuneFragmentContents runeFragmentContents;

    public override void Init()
    {
        OnShow();

        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        runeFragmentContents.Init();
    }
}
