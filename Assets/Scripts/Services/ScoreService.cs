using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts.Score;
using System.Diagnostics;
using Assets.Scripts.PieceMatchers;
using System.Text.RegularExpressions;

namespace Assets.Scripts.Services
{
    public class ScoreService : MonoBehaviour, IScoreService
    {
        #region Fields

        private int score;
        Stopwatch comboWatch;        
        private IMessageService messageService;
        private IMatchService matchService;
        private int standardMatchLenght;

        #endregion

        #region Properties

        public float TimeBetweenCombo = 1f;
        public int simplePieceScore = 3;
        public int extraPieceScore = 1;

        #endregion

        #region Monobehaviour

        private void Start()
        {
            messageService = GameObject.FindFirstObjectByType<MessageService>().GetComponent<IMessageService>() ?? throw new Exception("IMessageService not found");
            matchService = GameObject.FindFirstObjectByType<MatchService>().GetComponent<IMatchService>() ?? throw new Exception("IMatchService not found");            
            standardMatchLenght = matchService.GetStandardMatchLength();
            messageService.UpdateScore(score);
        }

        #endregion

        #region Methods

        public void UpdateScore(List<MatchData> matchsData)
        {
            foreach (var match in matchsData)
            {
                if(IsStandardMatch(match.PiecesDestroyed))
                {
                    score += match.PiecesDestroyed;
                }
                else
                {
                    score += (match.PiecesDestroyed + GetExtraPieceScore(match.PiecesDestroyed)) * GetMultiplyPieceScore(match.PiecesDestroyed);
                }                
            }
            messageService.UpdateScore(score);            
        }

        public int GetScore()
        {
            return this.score;
        }

        #endregion

        #region Private methods

        private bool IsStandardMatch(int piecesDestroyed)
        {
            return piecesDestroyed == standardMatchLenght;
        } 

        private int GetExtraPieceScore(int piecesDestroyed)
        {
            return (piecesDestroyed % standardMatchLenght) * extraPieceScore;
        }

        private int GetMultiplyPieceScore(int piecesDestroyed)
        {
            return (piecesDestroyed / standardMatchLenght);
        }

        #endregion
    }
}
