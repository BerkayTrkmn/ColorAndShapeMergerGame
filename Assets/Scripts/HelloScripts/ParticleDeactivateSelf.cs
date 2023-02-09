using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelloScripts
{
    public class ParticleDeactivateSelf : MonoBehaviour
    {
        ParticleSystem particle;

        void Start()
        {
            particle = GetComponent<ParticleSystem>();

        }


        void Update()
        {
            if (!particle.IsAlive()) gameObject.SetActive(false);
        }
    }
}