using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunePopup : Popup
{
    [Header("��ư"), SerializeField] private Button ExitBtn;

    [Header("������"), SerializeField] private RuneFragmentContents runeFragmentContents;
    [SerializeField] private RunePageContents runePageContents;

    public void Init(ClonInfo cloneInfo)
    {
        OnShow();

        ExitBtn.onClick.RemoveAllListeners();
        ExitBtn.onClick.AddListener(OnHide);

        runeFragmentContents.Init();
        runePageContents.Init(cloneInfo);
    }
}
