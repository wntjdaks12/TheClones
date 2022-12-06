using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillBtn : GameView, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // 스킬 아이콘
    [SerializeField] private Image iconImg;

    //임시로 아이디값 받아옴 -나중에 수정
    public uint num;

    private Transform canvasParent, originalParent;

    private Raycaster raycaster;

    public new void Awake()
    {
        canvasParent = GetComponentInParent<Canvas>().transform;
        originalParent = transform.parent;

        raycaster = new Raycaster();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var imageInfo = App.GameModel.PresetDataModel.ReturnData<ImageInfo>("ImageInfo", num);

        iconImg.sprite = Resources.Load<Sprite>(imageInfo.Path + num);
    }

    private void Use()
    {
        var objects = new object[1] {App.GameController.GetComponent<CharacterController>() };
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var huntingPoints = App.GameModel.RuntimeDataModel.ReturnDatas<IHuntingPoint>();

        foreach (IHuntingPoint huntingPoint in huntingPoints)
        {
            huntingPoint.OnPoint();
        }

        transform.parent = canvasParent;
        transform.position = Input.mousePosition;
    }
                                                          
    public void OnPointerUp(PointerEventData eventData)
    {
        var huntingPoints = App.GameModel.RuntimeDataModel.ReturnDatas<IHuntingPoint>();

        foreach (IHuntingPoint huntingPoint in huntingPoints)
        {
            huntingPoint.OffPoint();
        }

        transform.parent = originalParent;

       // CheckUse();
    }

    private void CheckUse()
    {
        var hits = raycaster.ReturnScreenToWorldHits<IHuntingPoint>();

        if (hits.Length > 0)
        {
            App.GameController.GetComponent<CharacterController>().Spawn("Monster", num, hits[0].point);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
}
