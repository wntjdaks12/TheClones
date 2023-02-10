using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    public static void Tooltip(string message, Vector3 position)
    {
        var tooltip = Instantiate(Resources.Load<Tooltip>("Tooltip"), position, Quaternion.identity);

        tooltip.Init(message);
    }
}
     