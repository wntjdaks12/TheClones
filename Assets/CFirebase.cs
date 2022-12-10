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

    private void ReadUserData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    for (int i = 0; i < snapshot.ChildrenCount; i++)
                        Debug.Log(snapshot.Child(i.ToString()).Child("username").Value);

                }
            });
    }

    public void WriteUserData<T>(string id, T data)
    {
        var json = JsonUtility.ToJson(data);

        reference.Child("Player").Child(id).SetRawJsonValueAsync(json);
    }

    public void ReadUserData(string id, Action<DataSnapshot> callback)
    {
        reference.Child("Player").Child(id).GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("데이터 베이스 로드 실패");
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("데이터 베이스 로드 성공");

                    var data = task.Result;

                    callback?.Invoke(data);
                }
            });
    }
}