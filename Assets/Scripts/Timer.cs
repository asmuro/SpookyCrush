using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {

        public float TargetTime = 1.0f;
        public event EventHandler TimerEnded;

        void Update()
        {
            TargetTime -= Time.deltaTime;

            if (TargetTime <= 0.0f)
            {
                TargetTimeReached();
            }

        }

        private void TargetTimeReached()
        {
            TimerEnded.Invoke(null, EventArgs.Empty);
            Destroy(this);
        }
    }
}
