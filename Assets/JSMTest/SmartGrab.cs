using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartGrab : MonoBehaviour
{
    public GameObject slot;
    private RectTransform slotRectTs;

    private RectTransform rectTs;

    [SerializeField] private Transform parent;
    private RectTransform parentRectTs;

    private HorizontalLayoutGroup horizontalLayoutGroup;

    private List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        slotRectTs = slot.GetComponent<RectTransform>();

        rectTs = GetComponent<RectTransform>();

        parentRectTs = parent.GetComponent<RectTransform>();

        horizontalLayoutGroup = parent.GetComponent<HorizontalLayoutGroup>();
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        var x = (int)parentRectTs.anchoredPosition.x;
        x = x > 0 ? 0 : Mathf.Abs(x);

        var index = (int)((x + slotRectTs.rect.width) / slotRectTs.rect.width);

        if (index < 10 - 5)
        {
            var padding = horizontalLayoutGroup.padding;

            horizontalLayoutGroup.padding = new RectOffset((int)(slotRectTs.rect.width * (index - 1)) + 20 * index, padding.right, padding.top, padding.bottom);
        }
        /*
        var val = (int)(slotRectTs.rect.width * index);
        var val2 = Mathf.Abs((int)parentRectTs.anchoredPosition.x);
        Debug.Log(index);
        if (val2 >= 300)
            index = (int)val2 / val + 1;
        else
            index = 1;

        var padding = horizontalLayoutGroup.padding;
        horizontalLayoutGroup.padding = new RectOffset(val + 20 * index - 320, padding.right, padding.top, padding.bottom);*/
    }

    public void Init()
    {
        parentRectTs.sizeDelta = new Vector2(10 * (slotRectTs.rect.width + 20) + horizontalLayoutGroup.padding.right, 0);

        for (int i = 0; i < 7; i++)
        {
            slots.Add(Instantiate(slot, parent));
        }
    }
}
