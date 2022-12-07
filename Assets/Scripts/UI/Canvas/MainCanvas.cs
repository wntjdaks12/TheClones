using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainScene
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private Button googleLoginBtn;

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

        private void SceneMove(bool isState)
        {
            if(isState) SceneManager.LoadScene("LobbyScene");
        }
    }
}