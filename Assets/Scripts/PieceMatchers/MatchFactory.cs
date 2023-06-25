using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PieceMatchers
{
    public static class MatchFactory
    {
        public static IPieceMatcher Create(Matcher match)
        {
            switch (match)
            {
                case Matcher.ThreePiecesVertical:
                    return new Match3Vertical();

                case Matcher.ThreePiecesHorizontal:
                    return new Match3Horizontal();

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
