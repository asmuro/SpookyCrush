using UnityEngine;

namespace Assets.Scripts.Matches
{
    /// <summary>
    /// Match is 3 pieces of the same type vertically aligned, next to each other
    /// </summary>
    public class Match3Vertical : PieceMatcher
    {
        private Board board;        

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
            return HasTwoAbove(piece) || HasTwoUnder(piece) || HasOneOnEachLevel(piece);
        }

        private bool HasTwoAbove(Piece piece)
        {
            if (piece.GetRow() == board.Height - 1 ||
                piece.GetRow() == board.Height - 2 ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() + 1) == null ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() + 2) == null)                 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() + 1).tag == piece.tag
                && board.GetPiece(piece.GetColumn(), piece.GetRow() + 2).tag == piece.tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasTwoUnder(Piece piece)
        {
            if (piece.GetRow() == 0 ||
                piece.GetRow() == 1 ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() - 1) == null ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() - 2) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() - 1).tag == piece.tag
                && board.GetPiece(piece.GetColumn(), piece.GetRow() - 2).tag == piece.tag)
                return true;
            return piece.GetIsMatched();
        }

        private bool HasOneOnEachLevel(Piece piece)
        {
            if (piece.GetRow() == 0 ||
                piece.GetRow() == board.Height - 1 ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() - 1) == null ||
                board.GetPiece(piece.GetColumn(), piece.GetRow() + 1) == null) 
                return piece.GetIsMatched();
            
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() - 1).tag == piece.tag 
                && board.GetPiece(piece.GetColumn(), piece.GetRow() + 1).tag == piece.tag)
                return true;
            return piece.GetIsMatched();
        }       

        #endregion
    }
}