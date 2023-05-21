using Assets.Scripts.Enums;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class StateMachine : MonoBehaviour
    {
        

        // Use this for initialization
        void Start()
        {
            _state = State.Running;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Properties

        private State _state;
        public State State 
        { 
            get => _state;
            set => _state = value;
        }

        #endregion
    }
}