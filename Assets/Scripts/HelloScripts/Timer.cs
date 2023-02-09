using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HelloScripts
{
    public class Timer : MonoBehaviour
    {
        public bool isTimerActive = false;
        public float time;
        private float startingTime;
        public UnityEvent onTimerOver;
        void Start()
        {
            startingTime = time;
        }

        void Update()
        {
            if (isTimerActive && time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (isTimerActive && time <= 0)
            {
                onTimerOver.Invoke();
                time = startingTime;
            }
        }
    }

}