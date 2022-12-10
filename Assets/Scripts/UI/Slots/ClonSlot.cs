using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClonSlot : GameView, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public uint ClonId { get; set; }
    public uint SkillId { get; set; }
   
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

    public void Init(uint clonId, uint skillId)
    {
        ClonId = clonId;
        SkillId = skillId;

        var imageInfo = App.GameModel.PresetDataModel.ReturnData<ImageInfo>("ImageInfo", ClonId);

        iconImg.sprite = Resources.Load<Sprite>(imageInfo.Path + ClonId);
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
            entityCont.Spawn("Clon", ClonId, hits[0].point, SkillId);
        }
    }
}
