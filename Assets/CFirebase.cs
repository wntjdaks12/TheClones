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

    public void WriteUserData<T>(string playerId, T data)
    {
        var json = JsonUtility.ToJson(data);

        reference.Child(typeof(T).ToString()).Child(playerId).SetRawJsonValueAsync(json);
    }

    public void ReadUserData<T>(string id, Action<T> callback)
    {
        reference.Child(typeof(T).ToString()).Child(id).GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("������ ���̽� �ε� ����");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("������ ���̽� �ε� ����");

                    var ClassObjectName = JsonUtility.FromJson<T>(task.Result.GetRawJsonValue());

                    callback?.Invoke(ClassObjectName);
                }
            });
    }
}