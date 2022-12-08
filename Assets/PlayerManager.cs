using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerId;
}


public class PlayerManager : GameManager
{
    [SerializeField] private Player player;
    public Player Player { get => player; set => player = value; }
}
