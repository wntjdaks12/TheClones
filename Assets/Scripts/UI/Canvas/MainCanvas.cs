using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace MainScene
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private Button googleLoginBtn;

        [Header("Temp Auth Checking Text")]
        [SerializeField] private TextMeshProUGUI authText;

        [SerializeField] private FirebaseGoogleAuth firebaseGoogleAuth;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            googleLoginBtn.onClick.RemoveAllListeners();
            googleLoginBtn.onClick.AddListener(() => firebaseGoogleAuth.TryGoogleLogin(SceneMove));
        }

        private void SceneMove(bool isState, string print)
        {
            authText.text = print;

            if (isState) SceneManager.LoadScene("LobbyScene");
        }
    }
}