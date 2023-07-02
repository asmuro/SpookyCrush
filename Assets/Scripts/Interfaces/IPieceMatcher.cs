using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IPieceMatcher
    {
        /// <summary>
        /// Returns if a specific <see cref="Piece"/> is a part of a match. 
        /// The definition of a match is specified in each implementation of the interface.
        /// </summary>
        /// <param name="piece">A piece of the puzzle</param>
        /// <returns>If the piece belongs to a match (true) or not(false)</returns>
        bool IsMatch(ILogicPiece piece);

        public abstract bool IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece piece);

        void SetBoard(IBoard board);

        int MatchLenght { get; }
    }
}