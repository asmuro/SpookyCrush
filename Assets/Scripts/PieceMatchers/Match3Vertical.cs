using Assets.Scripts.BoardFunctionality;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Matches
{
    /// <summary>
    /// Match is 3 pieces of the same type vertically aligned, next to each other
    /// </summary>
    public class Match3Vertical : PieceMatcher
    {
        #region Constants

        private const int FIRST_ROW = 0;
        private const int SECOND_ROW = 1;

        #endregion

        #region Fields

        private IBoard board;

        #endregion

        #region Monobehaviour
        
        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<IBoard>();
        }        

        #endregion

        #region Match3Horizontal

        public override bool IsMatch(ILogicPiece piece)
        {
            return HasTwoAbove(piece) || HasTwoUnder(piece) || HasOneOnEachLevel(piece);
        }

        private bool HasTwoAbove(ILogicPiece piece)
        {
            if (IsPieceInTheLastRow(piece) ||
                IsPieceInTheNextToLastRow(piece) ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() + 1) == null ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() + 2) == null)                 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() + 1).Tag == piece.Tag
                && board.GetPiece(piece.GetColumn(), piece.GetRow() + 2).Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasTwoUnder(ILogicPiece piece)
        {
            if (IsPieceInTheFirstRow(piece) ||
                IsPieceInTheSecondRow(piece) ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() - 1) == null ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() - 2) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() - 1).Tag == piece.Tag
                && board.GetPiece(piece.GetColumn(), piece.GetRow() - 2).Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasOneOnEachLevel(ILogicPiece piece)
        {
            if (IsPieceInTheFirstRow(piece) ||
                IsPieceInTheLastRow(piece) ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() - 1) == null ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() + 1) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() - 1).Tag == piece.Tag 
                && board.GetPiece(piece.GetColumn(), piece.GetRow() + 1).Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        #endregion

        #region Match3Vertical Clone Board        

        public override bool IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return HasTwoAbove(allPiecesClone, piece) || HasTwoUnder(allPiecesClone, piece) || HasOneOnEachLevel(allPiecesClone, piece);
        }

        private bool HasTwoAbove(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (IsPieceInTheLastRow(piece) ||
                IsPieceInTheNextToLastRow(piece) ||
                allPiecesClone[piece.GetColumn(), piece.GetRow() + 1] == null ||
                allPiecesClone[piece.GetColumn(), piece.GetRow() + 2] == null)
                return piece.GetIsMatched();

            if (allPiecesClone[piece.GetColumn(), piece.GetRow() + 1].Tag == piece.Tag
                && allPiecesClone[piece.GetColumn(), piece.GetRow() + 2].Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasTwoUnder(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (IsPieceInTheFirstRow(piece) ||
                IsPieceInTheSecondRow(piece) ||
                allPiecesClone[piece.GetColumn(), piece.GetRow() - 1] == null ||
                allPiecesClone[piece.GetColumn(), piece.GetRow() - 2] == null)
                return piece.GetIsMatched();

            if (allPiecesClone[piece.GetColumn(), piece.GetRow() - 1].Tag == piece.Tag
                && allPiecesClone[piece.GetColumn(), piece.GetRow() - 2].Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasOneOnEachLevel(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (IsPieceInTheFirstRow(piece) ||
                IsPieceInTheLastRow(piece) ||
                allPiecesClone[piece.GetColumn(), piece.GetRow() - 1] == null ||
                allPiecesClone[piece.GetColumn(), piece.GetRow() + 1] == null)
                return piece.GetIsMatched();

            if (allPiecesClone[piece.GetColumn(), piece.GetRow() - 1].Tag == piece.Tag
                && allPiecesClone[piece.GetColumn(), piece.GetRow() + 1].Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        #endregion

        #region Helpers

        private bool IsPieceInTheFirstRow(ILogicPiece piece)
        {
            return piece.GetRow() == FIRST_ROW;
        }

        private bool IsPieceInTheSecondRow(ILogicPiece piece)
        {
            return piece.GetRow() == SECOND_ROW;
        }

        private bool IsPieceInTheLastRow(ILogicPiece piece)
        {
            return piece.GetRow() == board.Height - 1;
        }

        private bool IsPieceInTheNextToLastRow(ILogicPiece piece)
        {
            return piece.GetRow() == board.Height - 2;
        }

        #endregion
    }
}