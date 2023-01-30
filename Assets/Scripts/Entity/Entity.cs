using System.Collections;
using UnityEngine; 
using System;
using System.Linq;
using Newtonsoft.Json;
//씬에 생성되는 개체
public class Entity:Data
{
    public string Name { get; private set; }

    [JsonProperty]
    public float Lifetime { get; set; }

    public Transform Transform { get; private set; }
    public Collider Collider { get; private set; }
    public MeshRenderer MeshRenderer { get; private set; }
    public Material[] originalMaterials { get; private set; }

    public virtual void Init(Transform transform, Collider collider, MeshRenderer meshRenderer = null)
    {
        Transform = transform;
        Collider = collider;
        MeshRenderer = meshRenderer;
        originalMaterials = originalMaterials ?? meshRenderer?.sharedMaterials;
    }
    public IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(Lifetime);

        OnRemoveData();
    }

    public IEnumerator BlendMaterialAsync(MeshRenderer meshRenderer, float blendingDelay, float blendingTime, Material[] materials)
    {
        yield return new WaitForSeconds(blendingDelay);

        BlendAddMaterial(meshRenderer, materials);

        yield return new WaitForSeconds(blendingTime);

        BlendRemoveMaterial(meshRenderer, materials);
    }

    public void BlendAddMaterial(MeshRenderer meshRenderer, Material[] materials)
    {
        meshRenderer.AddMaterial(materials);
    }

    public void BlendRemoveMaterial(MeshRenderer meshRenderer, Material[] materials)
    {
        meshRenderer.RemoveMaterial(materials);
    }
}
