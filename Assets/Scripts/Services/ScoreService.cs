using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts.Score;
using System.Diagnostics;

namespace Assets.Scripts.Services
{
    public class ScoreService : MonoBehaviour, IScoreService
    {
        #region Fields

        private int score;
        Stopwatch comboWatch;
        private IMessageService messageService;

        #endregion

        #region Properties

        public float TimeBetweenCombo = 1f;

        #endregion

        #region Monobehaviour

        private void Start()
        {
            messageService = GameObject.FindFirstObjectByType<MessageService>().GetComponent<IMessageService>();
            messageService.UpdateScore(score);
        }

        #endregion

        #region Methods

        public void UpdateScore(List<MatchData> matchData)
        {
            foreach (var match in matchData)
            {
                score += match.PiecesDestroyed;
            }
            messageService.UpdateScore(score);
        }

        #endregion

        #region Private methods

        //private bool IsCombo()
        //{
            //ShowCombo
            //Store score to multiply after combos finished
        //}



        #endregion
    }
}
