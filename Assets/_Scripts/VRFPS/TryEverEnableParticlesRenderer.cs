using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryEverEnableParticlesRenderer : MonoBehaviour
{
    void Update()
    {
        GetComponent<ParticleSystemRenderer>().enabled = true;
    }
}
