using Assets.Scripts.BoardFunctionality;
using UnityEngine;

namespace Assets.Scripts.Matches
{
    /// <summary>
    /// Match is 3 pieces of the same type horizontal aligned, next to each other
    /// </summary>
    public class Match3Horizontal : PieceMatcher
    {
        private Board board;
        private const int FIRST_COLUMN = 0;
        private const int SECOND_COLUMN = 1;

        // Use this for initialization
        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<Board>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Match3Horizontal

        public override bool IsMatch(Piece piece)
        {
            return HasTwoAtTheRight(piece) || HasTwoAtTheLeft(piece) || HasOneOnEachSide(piece);
        }

        private bool HasTwoAtTheRight(Piece piece)
        {
            if (IsPieceInTheLastColumn(piece) ||
                IsPieceInTheNextToLastColumn(piece) ||
                board.GetPiece(piece.GetColumn() + 1, piece.GetRow()) == null ||
                board.GetPiece(piece.GetColumn() + 2, piece.GetRow()) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn() + 1, piece.GetRow()).tag == piece.tag
                && board.GetPiece(piece.GetColumn() + 2, piece.GetRow()).tag == piece.tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasTwoAtTheLeft(Piece piece)
        {
            if (IsPieceInTheFirstColumn(piece) ||
                IsPieceInTheSecondColumn(piece) ||
                board.GetPiece(piece.GetColumn() - 1, piece.GetRow()) == null ||
                board.GetPiece(piece.GetColumn() - 2, piece.GetRow()) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn() - 1, piece.GetRow()).tag == piece.tag
                && board.GetPiece(piece.GetColumn() - 2, piece.GetRow()).tag == piece.tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasOneOnEachSide(Piece piece)
        {
            if (IsPieceInTheFirstColumn(piece) ||
                IsPieceInTheLastColumn(piece) ||
                board.GetPiece(piece.GetColumn() - 1, piece.GetRow()) == null ||
                board.GetPiece(piece.GetColumn() + 1, piece.GetRow()) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn() - 1, piece.GetRow()).tag == piece.tag 
                && board.GetPiece(piece.GetColumn() + 1, piece.GetRow()).tag == piece.tag)
                return true;
            return piece.GetIsMatched();
        }       

        private bool IsPieceInTheFirstColumn(Piece piece)
        {
            return piece.GetColumn() == FIRST_COLUMN;
        }

        private bool IsPieceInTheSecondColumn(Piece piece)
        {
            return piece.GetColumn() == SECOND_COLUMN;
        }

        private bool IsPieceInTheLastColumn(Piece piece)
        {
            return piece.GetColumn() == board.Width - 1;
        }

        private bool IsPieceInTheNextToLastColumn(Piece piece)
        {
            return piece.GetColumn() == board.Width - 2;
        }

        #endregion
    }
}