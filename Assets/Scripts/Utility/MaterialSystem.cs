using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSystem : MonoBehaviour
{
    [SerializeField] private Material[] materials;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;

    }

    public void RevertMaterial()
    {
        meshRenderer.sharedMaterial = materials[0];
    }

    public void ChangeMaterial()
    {
        meshRenderer.sharedMaterial = materials[1];
    }
}
