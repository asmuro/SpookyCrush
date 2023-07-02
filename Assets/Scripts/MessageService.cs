using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MessageService : MonoBehaviour, IMessageService
    {
        #region Constants

        private const string DEADLOCK_TEXT_TAG = "DeadlockText";
        private const string SCORE_TEXT_TAG = "ScoreText";

        #endregion

        #region Fields

        private TMP_Text deadlockText;
        private TMP_Text scoreText;

        #endregion

        #region Monobehaviour

        public void Start()
        {
            deadlockText = GameObject.FindGameObjectsWithTag(DEADLOCK_TEXT_TAG)[0]?.GetComponent<TMP_Text>() ?? throw new Exception("deadlockText not found");
            scoreText = GameObject.FindGameObjectsWithTag(SCORE_TEXT_TAG)[0]?.GetComponent<TMP_Text>() ?? throw new Exception("scoreText not found"); ;
        }

        #endregion

        #region Public Methods

        public void ShowDeadlockText()
        {
            if (deadlockText != null)
            {
                deadlockText.text = ConstantsToTranslate.DEADLOCK_TEXT;
            }
        }

        public void HideDeadlockText()
        {
            if (deadlockText != null)
            {
                deadlockText.text = String.Empty;
            }
        }

        public void UpdateScore(int newScore)
        {
            if (deadlockText != null)
            {
                scoreText.text = newScore.ToString("D3");
            }
        }        

        #endregion

        #region Private Methods



        #endregion
    }
}
