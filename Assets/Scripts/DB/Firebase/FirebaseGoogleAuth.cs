using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Firebase.Auth;
using System;

public class FirebaseGoogleAuth : MonoBehaviour
{
    private FirebaseAuth auth;

    private CFirebase firebase;

    private void Awake()
    {
        firebase = new CFirebase();
    }

    private void Start()
    {
#if UNITY_ANDROID
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        // 구글 플레이 게임 활성화

        auth = FirebaseAuth.DefaultInstance; // Firebase 액세스
#endif
    }

    public void TryGoogleLogin(Action<bool, string> callback)
    {
        callback?.Invoke(false, "구글 로그인...");
#if UNITY_EDITOR_WIN
        Debug.Log("구글 로그인 패스");

        StartCoroutine(TryFirebaseLogin(callback)); // Firebase Login 시도
#elif UNITY_ANDROID
        if (!Social.localUser.authenticated) // 로그인 되어 있지 않다면
        {
            Social.localUser.Authenticate(success => // 로그인 시도
            {
                if (success)
                {
                    Debug.Log("구글 로그인 성공");

                    StartCoroutine(TryFirebaseLogin(callback)); // Firebase Login 시도
                }
                else
                {
                    Debug.Log("구글 로그인 실패");
                }
            });
        }
#endif
    }

    public void TryGoogleLogout()
    {
        if (Social.localUser.authenticated) // 로그인 되어 있다면
        {
            PlayGamesPlatform.Instance.SignOut(); // Google 로그아웃
            auth.SignOut(); // Firebase 로그아웃
        }
    }


    private IEnumerator TryFirebaseLogin(Action<bool, string> callback)
    {
        callback?.Invoke(false, "파이어 베이스 로그인...");
#if UNITY_EDITOR_WIN
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        playerManager.PlayerInfo.playerId = "dWXMvdfmp5Oi3niMs0upJ2OtujL2";

        //CFirebase.WriteData<PlayerInfo>(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

        CFirebase.ReadData<PlayerInfo>(playerManager.PlayerInfo.playerId, data =>
        {
            GameManager.Instance.GetManager<PlayerManager>().PlayerInfo = data;
        });

        callback?.Invoke(true, "성공");

        yield return null;
#elif UNITY_ANDROID
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken()))
        {
            Debug.LogError("Token Error");
            callback?.Invoke(false, "Token Error");

            yield return null;
        }

        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                callback?.Invoke(false, "SignInWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                callback?.Invoke(false, "SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser user = auth.CurrentUser;

            if (user != null)
            {
                var playerManager = GameManager.Instance.GetManager<PlayerManager>();

                playerManager.PlayerInfo.playerId = user.UserId;

                CFirebase.WriteData<PlayerInfo>(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

                CFirebase.ReadData<PlayerInfo>(playerManager.PlayerInfo.playerId, data =>
                {
                    GameManager.Instance.GetManager<PlayerManager>().PlayerInfo = data;
                });

                callback?.Invoke(true, "성공");
            }
        });
#endif
    }
}