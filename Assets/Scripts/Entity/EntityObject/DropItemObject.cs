using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;
using UnityEngine.EventSystems;
using DG.Tweening;

public class DropItemObject : ActorObject
{
    [SerializeField] private MeshRenderer meshRenderer;
    public MeshRenderer MeshRenderer { get => meshRenderer; }

    private Raycaster raycaster;

    private Sequence sequence;

    private void Awake()
    {
        raycaster = new Raycaster();
    }

    public void Init(Data data, object app)
    {
        if (data is Entity) base.Init(data as Entity);

        var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

        var texture = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Texture>(data.Id.ToString());

        Entity.originalMaterials[meshRenderer][0].SetTexture("_MainTex", texture);

        var App = app as GameApplication;
        App.GameController.GetController<NameBarController>().Spawn("NameBar", 251, Entity.Transform.position, GameObject.Find("WorldCanvas").transform, Entity);

        var otherObjs = Physics.OverlapSphere(transform.position, 100f, LayerMask.GetMask(new string[] { "Player" }));

        if (otherObjs.Length > 0)
        {
            var orderByOtherObjs = otherObjs.OrderBy(otherObj => Vector3.Distance(transform.position, otherObj.transform.position)).ToArray();

            var sequence = DOTween.Sequence()
                        .SetAutoKill(false)
                        .Append(transform.DOMove(orderByOtherObjs[0].transform.position, 0.4f)).OnComplete(() =>
                        {
                            Entity.OnRemoveData();

                            if (Entity is DropItem)
                            {
                                var dropItem = Entity as DropItem;
                                dropItem.Pickup();

                                var App = app as GameApplication;
                                App.GameController.GetController<ParticleController>().Spawn(nameof(Particle), 60003, transform.position);

                                GameManager.Instance.HTTPController.GetController<HTTPDropItem>().GetRequestAsync();
                            }
                        });
        }
    }

    public Collider[] ReturnVisibleObjects(Transform transform)
    {
        throw new System.NotImplementedException();
    }
}
