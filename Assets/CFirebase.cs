using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

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
        reference.Child("Player").Child(id).SetValueAsync(data);
    }
}