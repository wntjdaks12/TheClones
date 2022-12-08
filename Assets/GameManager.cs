using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get => instance ??= GameObject.FindObjectOfType<GameManager>();
    }

    public T GetManager<T>() where T : GameManager
    {
        return gameObject.GetComponent<T>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
