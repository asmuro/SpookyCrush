using Assets.Scripts.Interfaces;
using Assets.Scripts.Score;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    /// <summary>
    /// In charge of counting the pieces's matches
    /// </summary>
    public class MatchCounterService : MonoBehaviour, IMatchCounterService
    {
        #region Fields

        private List<MatchData> matchData;
        Stopwatch watchBetweenMatches;
        IScoreService scoreService;

        #endregion

        #region Monobehaviour

        private void Start()
        {
            matchData = new List<MatchData>();
            scoreService = GameObject.FindFirstObjectByType<ScoreService>().GetComponent<IScoreService>();
        }

        private void Update()
        {
            SendMatchesToScore();
        }

        #endregion

        #region Public Methods

        public void AddMatch(int piecesDestroyed)
        {
            if(watchBetweenMatches == null)
            {
                matchData.Add(new MatchData() { 
                    PiecesDestroyed = piecesDestroyed, 
                    TicksDifferenceWithLastScore = 0, 
                    HasAddedToScore = false, 
                    CanBeDeleted = false  });
            }
            else
            {
                watchBetweenMatches.Stop();
                matchData.Add(new MatchData() {   
                    PiecesDestroyed = piecesDestroyed, 
                    TicksDifferenceWithLastScore = watchBetweenMatches.ElapsedTicks, 
                    HasAddedToScore = false, 
                    CanBeDeleted = false });
            }

            watchBetweenMatches = Stopwatch.StartNew();
        }

        #endregion

        #region Private Methods

        private void SendMatchesToScore()
        {            
            List<MatchData> matchDataToSend = new List<MatchData>();
            //Está creando una copia o es una referencia?
            matchData.ForEach(m => matchDataToSend.Add(m));
            scoreService.UpdateScore(matchDataToSend);
            DeleteOldDataMatches(matchDataToSend);
        }

        private void DeleteOldDataMatches(List<MatchData> matchDataSent)
        {
            matchDataSent.ForEach(m => matchData.Remove(m));
        }

        #endregion
    }
}
