using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UniRx;

public class AssetBundlesContents : MonoBehaviour
{
    [Header("PARSING TEXT")]
    [SerializeField] private new TextMeshProUGUI name;

    [Header("IMAGE")]
    [SerializeField] private Image prograss;

    private void Start()
    {
        var httpAssetBundle = GameManager.Instance.HTTPController.GetController<HTTPAssetBundle>();

        httpAssetBundle.progressRP.Subscribe(val => prograss.fillAmount = val);
        httpAssetBundle.nameRP.Subscribe(val => name.text = val);

        httpAssetBundle.GetRequestAsync(SceneMove);
    }

    private void SceneMove()
    {
        SceneManager.LoadScene("MainScene");
    }
}
