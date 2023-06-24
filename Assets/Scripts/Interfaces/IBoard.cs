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
        #region Structure

        int Width { get; }

        int Height { get; }

        IPiece[,] GetAllPieces();

        IPiece GetPiece(int i, int j);

        void SetPiece(int i, int j, IPiece piece);

        ILogicPiece[,] CloneAllPieces();

        #endregion

        #region Shuffle

        void OnShuffleFinished();

        #endregion

        #region Collapse

        void OnCollapsedColumns();

        void OnCollapsedOffsetPieces();

        void OnInitialCollapsedColumns();

        #endregion

        #region BlankSpaces

        bool IsBlankSpace(int x, int y);

        #endregion
    }
}
