using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Services
{
    internal class SoundService : MonoBehaviour, ISoundService
    {
        #region Properties

        public AudioSource[] destroySounds;

        #endregion

        #region ISoundService

        public void PlayRandomDestroyNoise()
        {
            destroySounds[UnityEngine.Random.Range(0, destroySounds.Length)].Play();
        }

        #endregion
    }
}
