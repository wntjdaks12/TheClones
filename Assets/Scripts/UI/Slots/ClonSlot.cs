using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClonSlot : GameView, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public ClonInfo CloneInfo { get; set; }

    [SerializeField] private Image iconImg;

    private Transform canvasParent, originalParent;

    private Raycaster raycaster;

    private new void Awake()
    {
        base.Awake();

        canvasParent = GetComponentInParent<Canvas>().transform;
        originalParent = transform.parent;

        raycaster = new Raycaster();
    }

    public void Init(ClonInfo cloneInfo)
    {
        CloneInfo = cloneInfo;

        var imageInfo = App.GameModel.PresetDataModel.ReturnData<ImageInfo>("ImageInfo", cloneInfo.clonId);

        iconImg.sprite = Resources.Load<Sprite>(imageInfo.Path + cloneInfo.clonId);
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

        Generate();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    private void Generate()
    {
        var hits = raycaster.ReturnScreenToWorldHits<IHuntingPoint>();

        if (hits.Length > 0)
        {
            var tempEntityCont = App.GameController.GetComponent<ParticleController>();
            var entityCont = App.GameController.GetComponent<CharacterController>();
            tempEntityCont.Spawn("Particle", 60002, hits[0].point);
            entityCont.Spawn("Clon", CloneInfo.clonId, hits[0].point, CloneInfo.skillId);
        }
    }
}
