using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class DropItemObject : EntityObject
{
    [SerializeField] [Range(0f, 10f)] private float speed = 1f;
    [SerializeField] [Range(0f, 10f)] private float length = 1f;

    private float runningTime = 0f;
    private Vector3 pos;

    [SerializeField] private MeshRenderer meshRenderer;
    public MeshRenderer MeshRenderer { get => meshRenderer; }

    public void Start()
    {
        pos = transform.position; pos.y = pos.y + length;

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                runningTime += Time.deltaTime * speed;

                var yPos = Mathf.Sin(runningTime) * length;
                
                transform.position = new Vector3(pos.x, pos.y + yPos, pos.z);
            });
    }

    public void Init(Data data)
    {
        if(data is Entity) base.Init(data as Entity);

        if (data is ImageInfo)
        {
            var imageInfo = data as ImageInfo;

            var assetBundleManager = GameManager.Instance.GetManager<AssetBundleManager>();

            var texture = assetBundleManager.AssetBundleInfo.texture.LoadAsset<Texture>(imageInfo.Id.ToString());

            Entity.originalMaterials[0].SetTexture("_MainTex", texture);
        }
    }
}
