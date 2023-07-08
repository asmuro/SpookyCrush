using Assets.Scripts.Score;
using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IScoreService
    {
        void UpdateScore(List<MatchData> matchData);
        int GetScore();
    }
}
