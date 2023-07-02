using Assets.Scripts.BoardFunctionality;
using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Matches
{
    /// <summary>
    /// Match is 3 pieces of the same type horizontal aligned, next to each other
    /// </summary>
    public class Match3Horizontal : IPieceMatcher
    {
        private IBoard board;
        private const int FIRST_COLUMN = 0;
        private const int SECOND_COLUMN = 1;       

        #region Match3Horizontal

        public bool IsMatch(ILogicPiece piece)
        {
            return HasTwoAtTheRight(piece) || HasTwoAtTheLeft(piece) || HasOneOnEachSide(piece);
        }

        private bool HasTwoAtTheRight(ILogicPiece piece)
        {
            if (IsPieceInTheLastColumn(piece) ||
                IsPieceInTheNextToLastColumn(piece) ||
                board.GetPiece(piece.GetColumn() + 1, piece.GetRow()) == null ||
                board.GetPiece(piece.GetColumn() + 2, piece.GetRow()) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn() + 1, piece.GetRow()).Tag == piece.Tag
                && board.GetPiece(piece.GetColumn() + 2, piece.GetRow()).Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasTwoAtTheLeft(ILogicPiece piece)
        {
            if (IsPieceInTheFirstColumn(piece) ||
                IsPieceInTheSecondColumn(piece) ||
                board.GetPiece(piece.GetColumn() - 1, piece.GetRow()) == null ||
                board.GetPiece(piece.GetColumn() - 2, piece.GetRow()) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn() - 1, piece.GetRow()).Tag == piece.Tag
                && board.GetPiece(piece.GetColumn() - 2, piece.GetRow()).Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasOneOnEachSide(ILogicPiece piece)
        {
            if (IsPieceInTheFirstColumn(piece) ||
                IsPieceInTheLastColumn(piece) ||
                board.GetPiece(piece.GetColumn() - 1, piece.GetRow()) == null ||
                board.GetPiece(piece.GetColumn() + 1, piece.GetRow()) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn() - 1, piece.GetRow()).Tag == piece.Tag 
                && board.GetPiece(piece.GetColumn() + 1, piece.GetRow()).Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }       

        private bool IsPieceInTheFirstColumn(ILogicPiece piece)
        {
            return piece.GetColumn() == FIRST_COLUMN;
        }

        private bool IsPieceInTheSecondColumn(ILogicPiece piece)
        {
            return piece.GetColumn() == SECOND_COLUMN;
        }

        private bool IsPieceInTheLastColumn(ILogicPiece piece)
        {
            return piece.GetColumn() == board.Width - 1;
        }

        private bool IsPieceInTheNextToLastColumn(ILogicPiece piece)
        {
            return piece.GetColumn() == board.Width - 2;
        }

        #endregion

        #region Match3Horizontal Clone Board        

        public bool IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return HasTwoAtTheRight(allPiecesClone, piece) || HasTwoAtTheLeft(allPiecesClone, piece) || HasOneOnEachSide(allPiecesClone, piece);
        }

        private bool HasTwoAtTheRight(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (IsPieceInTheLastColumn(piece) ||
                IsPieceInTheNextToLastColumn(piece) ||
                allPiecesClone[piece.GetColumn() + 1, piece.GetRow()] == null ||
                allPiecesClone[piece.GetColumn() + 2, piece.GetRow()] == null)
                return piece.GetIsMatched();

            if (allPiecesClone[piece.GetColumn() + 1, piece.GetRow()].Tag == piece.Tag
                && allPiecesClone[piece.GetColumn() + 2, piece.GetRow()].Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasTwoAtTheLeft(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (IsPieceInTheFirstColumn(piece) ||
                IsPieceInTheSecondColumn(piece) ||
                allPiecesClone[piece.GetColumn() - 1, piece.GetRow()] == null ||
                allPiecesClone[piece.GetColumn() - 2, piece.GetRow()] == null)
                return piece.GetIsMatched();

            if (allPiecesClone[piece.GetColumn() - 1, piece.GetRow()].Tag == piece.Tag
                && allPiecesClone[piece.GetColumn() - 2, piece.GetRow()].Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasOneOnEachSide(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (IsPieceInTheFirstColumn(piece) ||
                IsPieceInTheLastColumn(piece) ||
                allPiecesClone[piece.GetColumn() - 1, piece.GetRow()] == null ||
                allPiecesClone[piece.GetColumn() + 1, piece.GetRow()] == null)
                return piece.GetIsMatched();

            if (allPiecesClone[piece.GetColumn() - 1, piece.GetRow()].Tag == piece.Tag
                && allPiecesClone[piece.GetColumn() + 1, piece.GetRow()].Tag == piece.Tag)
                return true;
            return piece.GetIsMatched();
        }

        public void SetBoard(IBoard board)
        {
            this.board = board;
        }

        #endregion

        #region Public Methods

        public int MatchLenght => 3;

        #endregion
    }
}