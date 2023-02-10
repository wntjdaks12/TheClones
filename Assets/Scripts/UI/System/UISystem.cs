using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public static void Tooltip(string message, RectTransform rectTransform)
    {
        var tooltip = Instantiate(Resources.Load<Tooltip>("Tooltip"), rectTransform.root);

        tooltip.Init(message);
    }
}