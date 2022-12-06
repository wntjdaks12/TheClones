using UnityEngine;

public class Element : MonoBehaviour
{
    private GameApplication app;
    public GameApplication App 
    {
        get =>app ??= GameObject.FindObjectOfType<GameApplication>();
    }

    public static void Notify()
    { 
    }
    protected virtual void Awake()
    {
    }
}
