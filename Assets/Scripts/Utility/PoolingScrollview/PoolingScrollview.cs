using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

// 풀링 적용한 스크롤뷰
public class PoolingScrollview : MonoBehaviour
{
    private HorizontalLayoutGroup horizontalLayoutGroup;
    private GridLayoutGroup gridLayoutGroup;

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

    private bool isBool;

    public enum Type {Horizontal, Grid};
    private Type type;

    private void Awake()
    {
        if (parent.GetComponent<HorizontalLayoutGroup>())
        {
            type = Type.Horizontal;
        }
        else if (parent.GetComponent<GridLayoutGroup>())
        {
            type = Type.Grid;
        }

        if (type == Type.Horizontal)
        {
            horizontalLayoutGroup = parent.GetComponent<HorizontalLayoutGroup>();

            padding = horizontalLayoutGroup.padding.left;
        }
        else if (type == Type.Grid)
        {
            gridLayoutGroup = parent.GetComponent<GridLayoutGroup>();

            padding = gridLayoutGroup.padding.left;
        }

        rectTs = GetComponent<RectTransform>();
        itemRectTs = item.GetComponent<RectTransform>();
        parentRectTs = parent.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (type == Type.Horizontal)
        {
            CheckScrolling();
        }
        else if (type == Type.Grid)
        {
            CheckScrolling2();
        }
    }

    // 스크롤링에 관해서 체크합니다.
    private void CheckScrolling()
    {
        // 렉트랜스폼 x값
        var x = (int)parentRectTs.anchoredPosition.x;
        x = x > 0 ? 0 : Mathf.Abs(x);

        var index = (int)((x + itemRectTs.rect.width + padding) / (itemRectTs.rect.width + padding)); // 풀링에 관한 인덱스

        var curIndex = this.index; // 현재 풀링 인덱스

        if (this.index != index)
        {
            isBool = true;
            this.index = index;

            var count = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding)); // 풀링을 사용하기 위한 아이템 최대 개수

            if (index <= maxCount - count) // 인덱스에 따라 패딩 추가
            {
                if (type == Type.Horizontal)
                {
                    var pad = horizontalLayoutGroup.padding;

                    horizontalLayoutGroup.padding = new RectOffset((int)(itemRectTs.rect.width * (index - 1)) + padding * index, pad.right, pad.top, pad.bottom);
                }
                else if (type == Type.Grid)
                {
                    var pad = gridLayoutGroup.padding;

                    gridLayoutGroup.padding = new RectOffset((int)(itemRectTs.rect.width * (index - 1)) + padding * index, pad.right, pad.top, pad.bottom);
                }
            }
            else
            {
                if (maxCount >= count) // 아이템 최대 개수가 최대 개수를 넘어갈 시 현재 인덱스 유지
                {
                    this.index = curIndex;
                }
            }

            InitData();

            updatingPoolingEvent?.Invoke();
        }
    }

    // 스크롤링에 관해서 체크합니다.
    private void CheckScrolling2()
    {
        // 렉트랜스폼 x값
        var y = (int)parentRectTs.anchoredPosition.y;
        y = y < 0 ? 0 : Mathf.Abs(y);

        var index = (int)((y + itemRectTs.rect.height + padding) / (itemRectTs.rect.height + padding)); // 풀링에 관한 인덱스

        var curIndex = this.index; // 현재 풀링 인덱스

        if (this.index != index)
        {
            isBool = true;
            this.index = index;

            var count = (int)(rectTs.rect.height / (itemRectTs.rect.height + padding)); // 풀링을 사용하기 위한 아이템 최대 개수

            if (index <= (maxCount / 4) - count) // 인덱스에 따라 패딩 추가
            {
                if (type == Type.Horizontal)
                {
                    var pad = horizontalLayoutGroup.padding;

                    horizontalLayoutGroup.padding = new RectOffset((int)(itemRectTs.rect.width * (index - 1)) + padding * index, pad.right, pad.top, pad.bottom);
                }
                else if (type == Type.Grid)
                {
                    var pad = gridLayoutGroup.padding;

                    gridLayoutGroup.padding = new RectOffset(pad.left, pad.right, (int)(itemRectTs.rect.height * (index - 1)) + padding * index, pad.bottom);
                }
            }
            else
            {
                if (maxCount >= count) // 아이템 최대 개수가 최대 개수를 넘어갈 시 현재 인덱스 유지
                {
                    this.index = curIndex;
                }
            }

            InitData();

            updatingPoolingEvent?.Invoke();
        }
    }

    public void Init(int maxItemCount, Action<int> itemClickCallBack = null)
    {
        this.maxCount = maxItemCount;
        
        var currentItemCount = parent.childCount; // 현재 아이템 개수
        var updatedItemCount = GetItemCount(maxCount); // 업데이트된 아이템 개수

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

                if (isBool) InitData(itemClickCallBack);
            }
        }
    }

    // 아이템 데이터들을 초기화합니다.
    private void InitData(Action<int> itemClickCallBack = null)
    {
        if (maxCount - GetItemCount(maxCount) >= index - 1)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                var item = parent.GetChild(i).GetComponent<IPollingScrollview>();

                if (item != null)
                {
                    item.Init(i + index - 1);

                    item.ClickEvent += itemClickCallBack;
                }
            }
        }
    }

    /// <summary>
    /// 풀링을 사용하기 위한 아이템 최대 개수를 가져옵니다.
    /// </summary>
    /// <param name="maxItemCount">아이템 최대 개수</param>
    private int GetItemCount(int maxItemCount)
    {
        if (type == Type.Horizontal)
        {
            // 풀링을 사용하기 위한 아이템 최대 개수
            var itemCount = (int)(rectTs.rect.width / (itemRectTs.rect.width + padding)); 
            itemCount = maxItemCount <= itemCount ? maxItemCount : itemCount + 1;

            return itemCount;
        }
        else if (type == Type.Grid)
        {
            // 풀링을 사용하기 위한 아이템 최대 개수
            var itemColCount = (int)((rectTs.rect.width) / (itemRectTs.rect.width + padding));
            var itemRowCount = (int)((rectTs.rect.height) / (itemRectTs.rect.height + padding));
            var itemCount = itemColCount * itemRowCount;

            itemCount = maxItemCount <= itemCount ? maxItemCount : itemCount + 4;

            return itemCount;
        }

        return 0;
    }
}
