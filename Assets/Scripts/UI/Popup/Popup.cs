using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : GameView
{
    public GameObject popup;

    public void OnShow()
    {
        popup.SetActive(true);
    }

    public void OnHide()
    {
        popup.SetActive(false);
    }
}
