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
            if (piece.GetRow() == board.Height - 1) return piece.IsMatched();
            if (piece.GetRow() == board.Height - 2) return piece.IsMatched();
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() + 1).tag == piece.tag
                && board.GetPiece(piece.GetColumn(), piece.GetRow() + 2).tag == piece.tag)
                return true;
            return piece.IsMatched();
        }

        private bool HasTwoUnder(Piece piece)
        {
            if (piece.GetRow() == 0) return piece.IsMatched();
            if (piece.GetRow() == 1) return piece.IsMatched();
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() - 1).tag == piece.tag
                && board.GetPiece(piece.GetColumn(), piece.GetRow() - 2).tag == piece.tag)
                return true;
            return piece.IsMatched();
        }

        private bool HasOneOnEachLevel(Piece piece)
        {
            if (piece.GetRow() == 0) return piece.IsMatched();
            if (piece.GetRow() == board.Height - 1) return piece.IsMatched();
            if (board.GetPiece(piece.GetColumn(), piece.GetRow() - 1).tag == piece.tag 
                && board.GetPiece(piece.GetColumn(), piece.GetRow() + 1).tag == piece.tag)
                return true;
            return piece.IsMatched();
        }       

        #endregion
    }
}