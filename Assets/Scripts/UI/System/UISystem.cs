using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    public static void Tooltip(string message, Transform position)
    {
        var tooltip = Instantiate(Resources.Load<Tooltip>("Tooltip"), position);

        tooltip.Init(message);
    }
}
     