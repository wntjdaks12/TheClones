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
        // ���� �÷��� ���� Ȱ��ȭ

        auth = FirebaseAuth.DefaultInstance; // Firebase �׼���
#endif
    }

    public void TryGoogleLogin(Action<bool, string> callback)
    {
        callback?.Invoke(false, "���� �α���...");
#if UNITY_EDITOR_WIN
        Debug.Log("���� �α��� �н�");

        StartCoroutine(TryFirebaseLogin(callback)); // Firebase Login �õ�
#elif UNITY_ANDROID
        if (!Social.localUser.authenticated) // �α��� �Ǿ� ���� �ʴٸ�
        {
            Social.localUser.Authenticate(success => // �α��� �õ�
            {
                if (success)
                {
                    Debug.Log("���� �α��� ����");

                    StartCoroutine(TryFirebaseLogin(callback)); // Firebase Login �õ�
                }
                else
                {
                    Debug.Log("���� �α��� ����");
                }
            });
        }
#endif
    }

    public void TryGoogleLogout()
    {
        if (Social.localUser.authenticated) // �α��� �Ǿ� �ִٸ�
        {
            PlayGamesPlatform.Instance.SignOut(); // Google �α׾ƿ�
            auth.SignOut(); // Firebase �α׾ƿ�
        }
    }


    private IEnumerator TryFirebaseLogin(Action<bool, string> callback)
    {
        callback?.Invoke(false, "���̾� ���̽� �α���...");
#if UNITY_EDITOR_WIN
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        playerManager.PlayerInfo.playerId = "dWXMvdfmp5Oi3niMs0upJ2OtujL2";

        //CFirebase.WriteData<PlayerInfo>(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

        CFirebase.ReadData<PlayerInfo>(playerManager.PlayerInfo.playerId, data =>
        {
            GameManager.Instance.GetManager<PlayerManager>().PlayerInfo = data;
        });

        callback?.Invoke(true, "����");

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

                callback?.Invoke(true, "����");
            }
        });
#endif
    }
}