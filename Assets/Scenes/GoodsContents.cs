using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsContents : MonoBehaviour
{
    [SerializeField] private List<GoodsSlot> goodsSlots;

    public void Start()
    {
        goodsSlots.ForEach(x => x.Init());
    }
}
