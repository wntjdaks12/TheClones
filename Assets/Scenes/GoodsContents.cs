using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsContents : MonoBehaviour
{
    [SerializeField] private List<GoodsSlot> goodsSlots;

    private void Start()
    {
        goodsSlots.ForEach(x => x.Init());
    }

    private void Update()
    {
        goodsSlots.ForEach(x => x.DataInit());
    }
}
