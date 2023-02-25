using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    private static TooltipBox tooltipBox;

    public static void TooltipBox(string message, Transform parent)
    {
        if(tooltipBox == null) tooltipBox = Instantiate(Resources.Load<TooltipBox>("TooltipBox"), parent);

        tooltipBox.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;

        tooltipBox.Init(message);
    }
}
