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
        IPiece[,] GetAllPieces();

        IPiece GetPiece(int i, int j);

        void SetPiece(int i, int j, IPiece piece);

        PieceMatcher[] PieceMatchers { get; }

        void OnShuffleFinished();

        int Width { get; }

        int Height { get; }

        void OnCollapsedColumns();

        void OnCollapsedOffsetPieces();

        void OnInitialCollapsedColumns();

        #region BlankSpaces

        bool IsBlankSpace(int x, int y);

        #endregion
    }
}
