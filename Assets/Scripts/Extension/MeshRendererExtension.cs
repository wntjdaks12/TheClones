using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class MeshRendererExtension
{
    public static void AddMaterial(this MeshRenderer meshRenderer, Material[] materials)
    {
        var mat = new Material[meshRenderer.materials.Length + materials.Length];

        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            mat[i] = meshRenderer.sharedMaterials[i];
        }

        for (int i = materials.Length; i > 0; i--)
        {
            mat[mat.Length - i] = materials[materials.Length - i];
        }

        meshRenderer.sharedMaterials = mat;
    }

     public static void RemoveMaterial(this MeshRenderer meshRenderer, Material[] materials)
    {
        meshRenderer.sharedMaterials = meshRenderer.sharedMaterials.Except(materials).ToArray();
    }
}
