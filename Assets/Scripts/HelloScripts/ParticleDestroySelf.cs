using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelloScripts
{
    public class ParticleDestroySelf : MonoBehaviour
    {
        ParticleSystem particle;

        void Start()
        {
            particle = GetComponent<ParticleSystem>();

        }


        void Update()
        {
            if (!particle.IsAlive()) Destroy(gameObject);
        }
    }
}