using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSystem : MonoBehaviour
{
    [SerializeField] private Material[] materials;

    public Material[] ChangedMaterials { get => materials;}
}
