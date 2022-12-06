using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class MeshRendererExtension
{
    public static void AddMaterial(this MeshRenderer meshRenderer, Material material)
    {
        var mat = new Material[meshRenderer.materials.Length + 1];

        for (int i = 0; i < meshRenderer.materials.Length; i++)
        {
            mat[i] = meshRenderer.materials[i];
        }

        mat[mat.Length - 1] = material;

        meshRenderer.materials = mat;
    }

     public static void RemoveMaterial(this MeshRenderer meshRenderer, Material material)
    {
        meshRenderer.materials = meshRenderer.materials.Where(x => x != material).ToArray();
    }
}
