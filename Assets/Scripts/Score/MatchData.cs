using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Score
{
    public class MatchData
    {
        public int PiecesDestroyed;
        public long TicksDifferenceWithLastScore;
        public bool HasAddedToScore;
        public bool CanBeDeleted;
    }
}
