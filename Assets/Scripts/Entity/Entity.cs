using System.Collections;
using UnityEngine; 
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
//씬에 생성되는 개체
public class Entity : Data, ISpell
{
    public string Name { get; private set; }

    [JsonProperty]
    public float Lifetime { get; set; }

    public Transform Transform { get; private set; }
    public Collider Collider { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public List<MeshRenderer> MeshRenderer { get; private set; }
    public Dictionary<MeshRenderer, Material[]> originalMaterials;

    public Entity Caster { get; set; }
    public Entity Subject { get; set; }

    public virtual void Init(Transform transform, Collider collider)
    {
        Transform = transform;
        Collider = collider;
        MeshRenderer = new List<MeshRenderer>();
        ChangeLayersRecursively(transform);
        if (originalMaterials == null)
        {
            originalMaterials = new Dictionary<MeshRenderer, Material[]>();

            for (int i = 0; i < MeshRenderer.Count; i++)
            {
                originalMaterials.Add(MeshRenderer[i], MeshRenderer[i].materials);
            }
        }
    }

    public virtual void Init(Transform transform, Collider collider, Rigidbody rigidbody)
    {
        Transform = transform;
        Collider = collider;
        MeshRenderer = new List<MeshRenderer>();
        ChangeLayersRecursively(transform);
        if (originalMaterials == null)
        {
            originalMaterials = new Dictionary<MeshRenderer, Material[]>();

            for (int i = 0; i < MeshRenderer.Count; i++)
            {
                originalMaterials.Add(MeshRenderer[i], MeshRenderer[i].materials);
            }
        }
        Rigidbody = rigidbody;
    }

    public void ChangeLayersRecursively(Transform _transform)
    {
        var meshRenderer = _transform.GetComponent<MeshRenderer>();

        if (meshRenderer != null) MeshRenderer.Add(meshRenderer);

        foreach (Transform child in _transform)
        {
            ChangeLayersRecursively(child);
        }
    }

    public IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(Lifetime);

        OnRemoveData();
    }

    public IEnumerator BlendMaterialAsync(List<MeshRenderer> meshRenderer, float blendingDelay, float blendingTime, Material[] materials)
    {
        yield return new WaitForSeconds(blendingDelay);

        BlendAddMaterial(meshRenderer, materials);

        yield return new WaitForSeconds(blendingTime);

        BlendRemoveMaterial(meshRenderer, materials);
    }

    public void BlendAddMaterial(List<MeshRenderer> meshRenderer, Material[] materials)
    {
        meshRenderer.ForEach(x => x.AddMaterial(materials));
    }

    public void BlendRemoveMaterial(List<MeshRenderer> meshRenderer, Material[] materials)
    {
        meshRenderer.ForEach(x => x.RemoveMaterial(materials));
    }
}
