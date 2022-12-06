using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMeshRender : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    public void Start()
    {
        if (ps != null)
        {
            var a = ps.shape;
            a.meshRenderer = transform.GetComponentInParent<SkillObject>().transform.parent.GetComponent<MeshRenderer>();
        }
    }
}
