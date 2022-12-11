using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class CFirebase
{
    private DatabaseReference reference;

    public CFirebase()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// 데이터를 저장합니다
    /// </summary>
    /// <typeparam name="T">제네릭</typeparam>
    /// <param name="playerId">플레이어 Id</param>
    /// <param name="data">제네릭 데이터</param>
    public void WriteData<T>(string playerId, T data)
    {
        var json = JsonUtility.ToJson(data);

        reference.Child(typeof(T).ToString()).Child(playerId).SetRawJsonValueAsync(json);

        Debug.Log("데이터 베이스 저장 성공");
    }

    /// <summary>
    /// 데이터를 읽습니다.
    /// </summary>
    /// <typeparam name="T">제네릭</typeparam>
    /// <param name="playerId">플레이어 Id</param>
    /// <param name="callback">제네릭 콜백</param>
    public void ReadData<T>(string playerId, Action<T> callback)
    {
        reference.Child(typeof(T).ToString()).Child(playerId).GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("데이터 베이스 읽기 실패");
                }
                else if (task.IsCompleted)
                {
                    var ClassObjectName = JsonUtility.FromJson<T>(task.Result.GetRawJsonValue());

                    callback?.Invoke(ClassObjectName);

                    Debug.Log("데이터 베이스 읽기 성공");
                }
            });
    }
}