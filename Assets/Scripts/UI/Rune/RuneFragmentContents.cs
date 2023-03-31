using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneFragmentContents : MonoBehaviour
{
    [Header("CONTENTS")]
    [SerializeField] private PoolingScrollview poolingScrollview;

    public void Init()
    {
        var palyerManager = GameManager.Instance.GetManager<PlayerManager>();

        poolingScrollview.Init(palyerManager.PlayerInfo.GetItem(PlayerInfo.ItemTypes.Consumable).Length);
    }
}
