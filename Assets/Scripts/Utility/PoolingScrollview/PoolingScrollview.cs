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

    private bool isBool;

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
        CheckScrolling();
    }

    // ��ũ�Ѹ��� ���ؼ� üũ�մϴ�.
    private void CheckScrolling()
    {
        // ��Ʈ������ x��
        var x = (int)parentRectTs.anchoredPosition.x;
        x = x > 0 ? 0 : Mathf.Abs(x);

        var index = (int)((x + itemRectTs.rect.width + padding) / (itemRectTs.rect.width + padding)); // Ǯ���� ���� �ε���

        var curIndex = this.index; // ���� Ǯ�� �ε���

        if (this.index != index)
        {
            isBool = true;
            this.index = index;

            var count = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding)); // Ǯ���� ����ϱ� ���� ������ �ִ� ����

            if (index <= maxCount - count) // �ε����� ���� �е� �߰�
            {
                var pad = horizontalLayoutGroup.padding;

                horizontalLayoutGroup.padding = new RectOffset((int)(itemRectTs.rect.width * (index - 1)) + padding * index, pad.right, pad.top, pad.bottom);
            }
            else
            {
                if (maxCount >= count) // ������ �ִ� ������ �ִ� ������ �Ѿ �� ���� �ε��� ����
                {
                    this.index = curIndex;
                }
            }

            InitData();

            updatingPoolingEvent?.Invoke();
        }
    }

    public void Init(int maxItemCount)
    {
        this.maxCount = maxItemCount;

        var currentItemCount = parent.childCount; // ���� ������ ����
        var updatedItemCount = GetItemCount(maxCount); // ������Ʈ�� ������ ����

        if (maxItemCount > 0)
        {
            if (updatedItemCount != 0)
            {
                if (currentItemCount > updatedItemCount)
                {
                    updatedItemCount = currentItemCount - updatedItemCount;

                    for (int i = 0; i < updatedItemCount; i++)
                    {
                        Destroy(parent.GetChild(i).gameObject);
                    }
                }
                else if (currentItemCount < updatedItemCount)
                {
                    updatedItemCount = updatedItemCount - currentItemCount;

                    for (int i = 0; i < updatedItemCount; i++)
                    {
                        Instantiate(item, parent);
                    }
                }

                if (isBool) InitData();
            }
        }
    }

    // ������ �����͵��� �ʱ�ȭ�մϴ�.
    private void InitData()
    {
        if (maxCount - GetItemCount(maxCount) >= index - 1)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                var item = parent.GetChild(i).GetComponent<IPollingScrollview>();

                if (item != null)
                {
                    item.Init(i + index - 1);
                }
            }
        }
    }

    /// <summary>
    /// Ǯ���� ����ϱ� ���� ������ �ִ� ������ �����ɴϴ�.
    /// </summary>
    /// <param name="maxItemCount">������ �ִ� ����</param>
    private int GetItemCount(int maxItemCount)
    {
        // Ǯ���� ����ϱ� ���� ������ �ִ� ����
        var itemCount = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding));
        itemCount = maxItemCount <= itemCount ? maxItemCount : itemCount + 1;

        return itemCount;
    }
}
