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
#if ANDROID
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
#elif ANDROID
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
        Temp(callback);
        TempRead(callback);
        yield return null;
#elif ANDROID
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
                Temp(callback);
            }
        });
#endif
    }

    public void TempRead(Action<bool, string> callback)
    {
        var playerManager = GameManager.Instance.GetManager<PlayerManager>();

        firebase.ReadUserData("dWXMvdfmp5Oi3niMs0upJ2OtujL2", datas =>
        {
            var player = new PlayerInfo();

            foreach (var data in datas.Children)
            {
                if (data.Key == "clonInfos")
                {
                    var values = data.Value as List<object>;

                    foreach (var value in values)
                    {
                        var values2 = (IDictionary)value;

                        player.clonInfos.Add(new ClonInfo { clonId = uint.Parse(values2["clonId"].ToString()), skillId = uint.Parse(values2["skillId"].ToString()) });
                    }
                }

                if (data.Key == "playerId")
                {
                    player.playerId = data.Value.ToString();
                }
            }

            GameManager.Instance.GetManager<PlayerManager>().PlayerInfo = player;
        });

        callback?.Invoke(true, "성공");
    }

    public void Temp(Action<bool, string> callback)
    {
        Debug.Log("파이어 베이스 로그인 성공");

        var playerManager = GameManager.Instance.GetManager<PlayerManager>();
        
        var clonInfos = new List<ClonInfo>();
        clonInfos.Add(new ClonInfo { clonId = 90001, skillId = 30004 });
        clonInfos.Add(new ClonInfo { clonId = 90002, skillId = 30001 });

        playerManager.PlayerInfo = new PlayerInfo { playerId = "dWXMvdfmp5Oi3niMs0upJ2OtujL2", clonInfos = clonInfos };

        firebase.WriteUserData<PlayerInfo>(playerManager.PlayerInfo.playerId, playerManager.PlayerInfo);

        callback?.Invoke(true, "성공");
    }
}