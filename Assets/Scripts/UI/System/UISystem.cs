using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    private static Tooltip tooltip;

    public static void Tooltip(string message, Transform parent)
    {
        if(tooltip == null) tooltip = Instantiate(Resources.Load<Tooltip>("Tooltip"), parent);

        tooltip.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;

        tooltip.Init(message);
    }
}
