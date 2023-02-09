using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelloScripts
{
    public class EnemyForward : MonoBehaviour
    {
        public float speed;
        [HideInInspector]
        public float changingSpeed;

        private void Awake()
        {
            changingSpeed = speed;
        }

        void Update()
        {
            transform.position += Vector3.back * Time.deltaTime * changingSpeed;
        }
    }
}