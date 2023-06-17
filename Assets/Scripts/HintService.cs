using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class HintService : MonoBehaviour, IHintService
    {
        #region Properties

        public ParticleSystem HintFX;
        public float StartAfterSeconds;
        public float DurationSeconds;
        public bool IsActive = true;

        #endregion

        #region Fields

        private float startAfterSeconds;
        private float durationSeconds;
        private bool hasStarted;
        private bool isHinting;
        private IDeadlockService deadlockService;
        private ParticleSystem currentHint;
        private GameObject hintGameObject;
        private const string GAMEOBJECT_HINT_NAME = "Hint FX";


        #endregion

        #region Monobehaviour

        private void Start()
        {
            ResetTime();
            deadlockService = GameObject.FindFirstObjectByType<DeadlockService>().GetComponent<IDeadlockService>();
        }

        private void Update()
        {
            if (IsActive)
            {
                if (!hasStarted)
                {
                    startAfterSeconds -= Time.deltaTime;
                    EvaluateStart();
                }
                else
                {
                    durationSeconds -= Time.deltaTime;
                    EvaluateHint();
                }
            }
        }

        #endregion

        #region Methods

        public void ResetTime()
        {
            hasStarted = false;
            isHinting = false;
            startAfterSeconds = StartAfterSeconds;
            durationSeconds = 0;
            if (hintGameObject)
            {
                Destroy(hintGameObject);
            }
        }        

        private void EvaluateStart()
        {
            if (startAfterSeconds <= 0)
            {
                hasStarted = true;
            }
        }

        private void EvaluateHint()
        {
            if (durationSeconds <= 0 && isHinting)
            {
                StopHinting();
            }
            else if (durationSeconds <= 0 && !isHinting)
            {
                StartHinting();
            }
        }

        private void StartHinting()
        {
            
            isHinting = true;
            durationSeconds = DurationSeconds;
            if (deadlockService.HasDeadlock())
            {
                return;
            }
            GenerateHint();
        }

        private void StopHinting()
        {
            isHinting = false;
            //Destroy(currentHint);
            Destroy(hintGameObject);
            durationSeconds = DurationSeconds;
        }

        private GameObject SetTransformToNewObject(Transform transform)
        {
            GameObject hintGameObject = new GameObject();
            hintGameObject.transform.position = transform.position;
            hintGameObject.transform.localPosition = transform.localPosition;
            hintGameObject.transform.rotation = transform.rotation;
            hintGameObject.transform.localRotation = transform.localRotation;
            hintGameObject.transform.localScale = transform.localScale;
            return hintGameObject;
        }

        private void GenerateHint()
        {
            hintGameObject = SetTransformToNewObject(deadlockService.GetLastMatchedPiece().GetTransform());
            hintGameObject.name = GAMEOBJECT_HINT_NAME;
            hintGameObject.transform.position = new Vector3(hintGameObject.transform.position.x, hintGameObject.transform.position.y + (Constants.TILE_SIZE / 4f), hintGameObject.transform.position.z);
            currentHint = Instantiate(HintFX, hintGameObject.transform);
        }
            

        #endregion
    }
}
