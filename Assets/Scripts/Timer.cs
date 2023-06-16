using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {
        #region Fields

        private float internalTargetTime;

        #endregion

        #region Properties

        public float TargetTime = 1.0f;
        public event EventHandler TimerEnded;

        #endregion

        #region Monobehaviour

        private void Start()
        {
            internalTargetTime = TargetTime;
        }

        void Update()
        {
            internalTargetTime -= Time.deltaTime;

            if (internalTargetTime <= 0.0f)
            {
                TargetTimeReached();
            }

        }

        #endregion

        #region Private Methods

        private void TargetTimeReached()
        {
            TimerEnded.Invoke(null, EventArgs.Empty);
            Destroy(this);
        }

        #endregion

        #region Public Methods

        public void Restart(float time = 0f)
        {
            if ( time <= 0) 
            {
                internalTargetTime = TargetTime;
            }
            else
            {
                internalTargetTime = time;
            }
            
        }

        #endregion
    }
}
