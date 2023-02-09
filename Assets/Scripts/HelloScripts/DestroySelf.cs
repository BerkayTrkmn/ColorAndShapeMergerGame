using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HelloScripts
{
    public delegate void CreateBeforeDestroy(GameObject create,Transform createdTransform);

    public class DestroySelf : MonoBehaviour
    {
        public float countdown = 1;
        float timer;
        public GameObject spawnBeforeDestroy;
        [HideInInspector] public CreateBeforeDestroy cbd;

        bool isCreated = false;
        private void Start()
        {
            timer = countdown;
        }
        void LateUpdate()
        {
            timer -= Time.deltaTime;
          
                if (timer < 0f )
            {
              // Debug.Log(gameObject.name + " " + cbd);
               // Debug.Log(gameObject.name + " " + spawnBeforeDestroy);
                if (cbd != null && spawnBeforeDestroy != null && !isCreated)
                {
                    cbd.Invoke(spawnBeforeDestroy, transform);
                    isCreated = true;
                }
                StartCoroutine(WaitAFrame());

            }
        }
        public void ResetTimer()
        {
            timer = countdown;
        }

        IEnumerator WaitAFrame()
        {
            yield return new WaitForEndOfFrame();
                 Destroy(gameObject);
        }
    }
}