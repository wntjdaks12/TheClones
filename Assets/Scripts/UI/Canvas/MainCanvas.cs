using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainScene
{
    public class MainCanvas : MonoBehaviour
    {
        private Raycaster raycaster;

        [SerializeField] private GraphicRaycaster graphicRaycaster;

        private void Awake()
        {
            raycaster = new Raycaster();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var hits = raycaster.ReturnUIRaycastResultList(graphicRaycaster);

                if (hits.Count > 0) SceneManager.LoadScene("LobbyScene");
            }
        }
    }
}