using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerId;
}


public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public Player Player { get => player; set => player = value; }

    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance ??=  GameObject.FindObjectOfType<PlayerManager>(); }
}
