using Assets.Scripts.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IBoard
    {
        public IPiece[,] GetAllPieces();

        PieceMatcher[] PieceMatchers { get; }

        void OnShuffleFinished();

        int Width { get; }

        int Height { get; }
    }
}
