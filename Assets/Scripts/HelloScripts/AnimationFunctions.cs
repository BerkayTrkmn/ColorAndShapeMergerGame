using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HelloScripts
{
    public class AnimationFunctions : MonoBehaviour
    {
        Action deathAction;
        Action attackAction;

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public void SetDeathAction(Action _deathAction)
        {
            deathAction = _deathAction;
        }

        public void InvokeDeathAction()
        {
            deathAction?.Invoke();
        }
    }
}