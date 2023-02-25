using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    public void Init(string message)
    {
        messageText.text = message;
    }
}
