using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ClonSlot : GameView, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPollingScrollview
{
    public ClonInfo CloneInfo { get; set; }

    [SerializeField] private Image iconImg;

    private Transform canvasParent, originalParent;

    private Raycaster raycaster;

    public Action<int> ClickEvent { get; set; }


    private new void Awake()
    {
        base.Awake();

        canvasParent = GetComponentInParent<Canvas>().transform;
        originalParent = transform.parent;

        raycaster = new Raycaster();
    }

    public void Init(int index)
    {
        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();
        var playerInfo = GameManager.Instance.GetManager<PlayerManager>().PlayerInfo;

        if (playerInfo.cloneInofs.Count <= index) return;

        CloneInfo = playerInfo.cloneInofs[index];

        iconImg.sprite = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Sprite>(CloneInfo.clonId.ToString());
    }

    public void OnPointerDown(PointerEventData eventData)
    {    /*
        var huntingPoints = App.GameModel.RuntimeDataModel.ReturnDatas<IHuntingPoint>();

        foreach (IHuntingPoint huntingPoint in huntingPoints)
        {
            huntingPoint.OnPoint();
        }

        transform.parent = canvasParent;
        transform.position = Input.mousePosition;*/
    }

    public void OnPointerUp(PointerEventData eventData)
    {/*
        var huntingPoints = App.GameModel.RuntimeDataModel.ReturnDatas<IHuntingPoint>();

        foreach (IHuntingPoint huntingPoint in huntingPoints)
        {
            huntingPoint.OffPoint();
        }

        transform.parent = originalParent;

        Generate();*/
    }

        public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    /*
    private void Generate()
    {
        var hits = raycaster.ReturnScreenToWorldHits<IHuntingPoint>();
        Debug.Log(hits.Length);
        if (hits.Length > 0)
        {
            var tempEntityCont = App.GameController.GetComponent<ParticleController>();
            var entityCont = App.GameController.GetComponent<CharacterController>();
            tempEntityCont.Spawn("Particle", 60002, hits[0].point);
            entityCont.Spawn("Clon", CloneInfo.clonId, hits[0].point, CloneInfo.skillId);
        }
    }*/
}
