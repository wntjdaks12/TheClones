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

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder()
            .RequestIdToken()
            .RequestEmail()
            .Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        // ���� �÷��� ���� Ȱ��ȭ

        auth = FirebaseAuth.DefaultInstance; // Firebase �׼���
    }


    public void TryGoogleLogin(Action<bool, string> output)
    {
        if (!Social.localUser.authenticated) // �α��� �Ǿ� ���� �ʴٸ�
        {
            Social.localUser.Authenticate(success => // �α��� �õ�
            {
                if (success)
                {
                    Debug.Log("���� �α��� ����");
                    output?.Invoke(false, "���� �α��� ����");
                    StartCoroutine(TryFirebaseLogin(output)); // Firebase Login �õ�
                }
                else
                {
                    Debug.Log("���� �α��� ����");
                    output?.Invoke(false, "���� �α��� ����");
                }
            });
        }
    }


    public void TryGoogleLogout()
    {
        if (Social.localUser.authenticated) // �α��� �Ǿ� �ִٸ�
        {
            PlayGamesPlatform.Instance.SignOut(); // Google �α׾ƿ�
            auth.SignOut(); // Firebase �α׾ƿ�
        }
    }


    IEnumerator TryFirebaseLogin(Action<bool, string> callback)
    {
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
                PlayerManager.Instance.Player = new Player { playerId = user.UserId };

                firebase.WriteUserData<Player>(PlayerManager.Instance.Player.playerId, PlayerManager.Instance.Player);
            }

            Debug.Log("���̾� ���̽� �α��� ����");
            callback?.Invoke(true, "���̾� ���̽� �α��� ����");
        });

    }
}