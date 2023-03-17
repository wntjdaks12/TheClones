using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Popup : GameView
{
    public GameObject popup;

    private static List<Popup> popups;

    protected void Awake()
    {
        popups ??= new List<Popup>();

        popups.Add(this);
    }

    public void OnShow()
    {
        popup.SetActive(true);
    }

    public void OnHide()
    {
        popup.SetActive(false);
    }

    public virtual void Init()
    {
    }

    public T ReturnPopup<T>() where T : Popup
    {
        return popups.Select(x => (T)x).FirstOrDefault();
    }
}
