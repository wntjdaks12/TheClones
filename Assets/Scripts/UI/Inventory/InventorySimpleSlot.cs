using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySimpleSlot : GameView
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Button slotButton;

    [SerializeField] private CanvasGroup canvasGroup;

    public Action<int> ClickEvent { get; set; }

    public void Init(int index, Sprite sprite)
    {
        slotButton.onClick.RemoveAllListeners();
        slotButton.onClick.AddListener(() => 
        {
            canvasGroup.alpha = 0.5f;

            ClickEvent?.Invoke(index);
        });

        ShowData(sprite);
    }

    private void ShowData(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
}
