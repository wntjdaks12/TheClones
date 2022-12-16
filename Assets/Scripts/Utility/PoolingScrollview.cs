using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Ǯ�� ������ ��ũ�Ѻ�
public class PoolingScrollview : MonoBehaviour
{
    private HorizontalLayoutGroup horizontalLayoutGroup;

    private RectTransform rectTs;

    [Header("ITEM")]
    public GameObject item;
    private RectTransform itemRectTs;

    [SerializeField] private Transform parent;
    private RectTransform parentRectTs;

    private int padding;

    private int maxCount;

    // Ǯ�� ������Ʈ ��
    private UnityEvent updatingPoolingEvent = new UnityEvent();
    public UnityEvent UpdatingPoolingEvent { get => updatingPoolingEvent; }

    private int index;

    private void Awake()
    {
        horizontalLayoutGroup = parent.GetComponent<HorizontalLayoutGroup>();

        rectTs = GetComponent<RectTransform>();
        itemRectTs = item.GetComponent<RectTransform>();
        parentRectTs = parent.GetComponent<RectTransform>();

        padding = horizontalLayoutGroup.padding.left;
    }

    private void Update()
    {
        var x = (int)parentRectTs.anchoredPosition.x;
        x = x > 0 ? 0 : Mathf.Abs(x);

        var index = (int)((x + itemRectTs.rect.width + padding) / (itemRectTs.rect.width + padding));

        if (this.index != index)
        {
            this.index = index;

            var count = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding)); // Ǯ���� ����ϱ� ���� ������ �ִ� ����

            if (index <= maxCount - count) // �ε����� ���� �е� �߰�
            {
                var pad = horizontalLayoutGroup.padding;

                horizontalLayoutGroup.padding = new RectOffset((int)(itemRectTs.rect.width * (index - 1)) + padding * index, pad.right, pad.top, pad.bottom);
            }

            updatingPoolingEvent?.Invoke();
        }
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    /// <param name="maxItemCount">������ �ִ� ����</param>
    public void Init(int maxItemCount)
    {
        this.maxCount = maxItemCount;

        // Ǯ���� ����ϱ� ���� ������ �ִ� ����
        var itemCount = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding)); 
        itemCount = maxItemCount <= itemCount ? maxItemCount - 1 : itemCount;

        parentRectTs.sizeDelta = new Vector2(maxItemCount * (itemRectTs.rect.width + padding) + horizontalLayoutGroup.padding.right, 0); // Ǯ���� ����ϱ� ���� �ִ� ����

        for (int i = 0; i <= itemCount; i++) // �ִ� ������ŭ ������ ����
        {
            Instantiate(item, parent);
        }
    }
}
