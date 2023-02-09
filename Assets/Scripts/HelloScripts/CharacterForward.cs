using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelloScripts
{
    public enum CharacterDirection
    {
        Forward,
        Up,
        Right
    }
    public class CharacterForward : MonoBehaviour
    {
        [HideInInspector]public float startingSpeed;
        public float changingSpeed;
        public CharacterDirection direction = CharacterDirection.Forward;

        private void Start()
        {
            startingSpeed = changingSpeed;
        }

        void Update()
        {
            if (direction == CharacterDirection.Forward)
                transform.position += Vector3.forward * Time.deltaTime * changingSpeed;
            else if (direction == CharacterDirection.Up)
                transform.position += Vector3.up * Time.deltaTime * changingSpeed;
            else if (direction == CharacterDirection.Right)
                transform.position += Vector3.right * Time.deltaTime * changingSpeed;
        }
    }
}