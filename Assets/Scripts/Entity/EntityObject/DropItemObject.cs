using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using UnityEngine.EventSystems;

public class DropItemObject : EntityObject
{
    [SerializeField] [Range(0f, 10f)] private float speed = 1f;
    [SerializeField] [Range(0f, 10f)] private float length = 1f;

    private float runningTime = 0f;
    private Vector3 originalPosition;

    [SerializeField] private MeshRenderer meshRenderer;
    public MeshRenderer MeshRenderer { get => meshRenderer; }

    private Raycaster raycaster;

    private void Awake()
    {
        raycaster = new Raycaster();
    }

    public void Start()
    {
        originalPosition = transform.position; originalPosition.y = originalPosition.y + length;
        
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                runningTime += Time.deltaTime * speed;

                var yPos = Mathf.Sin(runningTime) * length;
                
                transform.position = new Vector3(transform.position.x, originalPosition.y + yPos, transform.position.z);
            });

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButton(0))
            .Subscribe(_ => 
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    var hits = raycaster.ReturnScreenToWorldHits<DropItemObject>();

                    if (hits.Length > 0)
                    {
                        var hitObject = hits[0].transform.gameObject;

                        if (hitObject == gameObject)
                        {
                            Entity.OnRemoveData();

                            if (Entity is DropItem)
                            {
                                var dropItem = Entity as DropItem;
                                dropItem.Pickup();

                                GameManager.Instance.HTTPController.GetController<HTTPDropItem>().GetRequestAsync();
                            }
                        }
                    }
                }
            });
    }

    public void Init(Data data)
    {
        runningTime = 0;

        if (data is Entity) base.Init(data as Entity);

        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        var texture = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Texture>(data.Id.ToString());

        Entity.originalMaterials[0].SetTexture("_MainTex", texture);
    }
}
