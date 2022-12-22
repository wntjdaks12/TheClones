using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class CFirebase
{
    private static DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

    /// <summary>
    /// �����͸� �����մϴ�
    /// </summary>
    /// <typeparam name="T">���׸�</typeparam>
    /// <param name="playerId">�÷��̾� Id</param>
    /// <param name="data">���׸� ������</param>
    public static void WriteData<T>(string playerId, T data)
    {
        var json = JsonUtility.ToJson(data);

        reference.Child(typeof(T).ToString()).Child(playerId).SetRawJsonValueAsync(json);

        Debug.Log("������ ���̽� ���� ����");
    }

    /// <summary>
    /// �����͸� �н��ϴ�.
    /// </summary>
    /// <typeparam name="T">���׸�</typeparam>
    /// <param name="playerId">�÷��̾� Id</param>
    /// <param name="callback">���׸� �ݹ�</param>
    public static void ReadData<T>(string playerId, Action<T> callback)
    {
        reference.Child(typeof(T).ToString()).Child(playerId).GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("������ ���̽� �б� ����");
                }
                else if (task.IsCompleted)
                {
                    var ClassObjectName = JsonUtility.FromJson<T>(task.Result.GetRawJsonValue());

                    callback?.Invoke(ClassObjectName);

                    Debug.Log("������ ���̽� �б� ����");
                }
            });
    }
}