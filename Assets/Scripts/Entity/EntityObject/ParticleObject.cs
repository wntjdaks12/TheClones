using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : EntityObject
{
    public void Init(Particle particle)
    {
        base.Init(particle);

        var ai = GetComponent<EntityAI>();

        if (ai != null) ai.Entity = particle;

        if (particle.Lifetime != 0) StartCoroutine(particle.StartLifeTime());
    }
}
