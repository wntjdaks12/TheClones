using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// 풀링 적용한 스크롤뷰
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

    // 풀링 업데이트 시
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

            var count = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding)); // 풀링을 사용하기 위한 아이템 최대 개수

            if (index <= maxCount - count) // 인덱스에 따라 패딩 추가
            {
                var pad = horizontalLayoutGroup.padding;

                horizontalLayoutGroup.padding = new RectOffset((int)(itemRectTs.rect.width * (index - 1)) + padding * index, pad.right, pad.top, pad.bottom);
            }

            updatingPoolingEvent?.Invoke();
        }
    }

    /// <summary>
    /// 초기화
    /// </summary>
    /// <param name="maxItemCount">아이템 최대 개수</param>
    public void Init(int maxItemCount)
    {
        this.maxCount = maxItemCount;

        // 풀링을 사용하기 위한 아이템 최대 개수
        var itemCount = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding)); 
        itemCount = maxItemCount <= itemCount ? maxItemCount - 1 : itemCount;

        parentRectTs.sizeDelta = new Vector2(maxItemCount * (itemRectTs.rect.width + padding) + horizontalLayoutGroup.padding.right, 0); // 풀링을 사용하기 위한 최대 넓이

        for (int i = 0; i <= itemCount; i++) // 최대 개수만큼 아이템 생성
        {
            Instantiate(item, parent);
        }
    }
}
