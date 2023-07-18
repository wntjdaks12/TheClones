using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContents : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    public void DeactiveAll()
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void ActiveAll()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
