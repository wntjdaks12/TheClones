using UnityEngine;

public class GameApplication : Element
{
    public GameModel GameModel 
    {
        get => gameModel ??= GameObject.FindObjectOfType<GameModel>();
    }
    private GameModel gameModel;

    public GameView GameView 
    {
        get => gameVIew ??= GameObject.FindObjectOfType<GameView>();
    }
    private GameView gameVIew;

    public GameController GameController
    {
        get =>gameController ??= GameObject.FindObjectOfType<GameController>();
    }
    private GameController gameController;


    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }

}