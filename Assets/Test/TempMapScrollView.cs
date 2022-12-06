using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMapScrollView : GameView
{
    [SerializeField] private TempMapSlot tempMapSlot;

    [SerializeField] private Transform parent;

    void Start()
    {
        var maps = App.GameModel.PresetDataModel.ReturnDatas<Map>(nameof(Map));

        for (int i = 0; i < maps.Length; i++)
        {
            var slotObj = Instantiate(tempMapSlot, parent);

            slotObj.GetComponent<TempMapSlot>().Init(i);
        }
    }
}
