using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button exitButton;

    public void Init(string message)
    {
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => Destroy(this));

        messageText.text = message;
    }
}
