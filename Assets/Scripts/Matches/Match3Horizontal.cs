using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Matches
{
    /// <summary>
    /// Match is 3 pieces of the same type horizontal aligned, next to each other
    /// </summary>
    public class Match3Horizontal : PieceMatcher
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
            return HasTwoAtTheRight(piece) || HasTwoAtTheLeft(piece) || HasOneOnEachSide(piece);
        }

        private bool HasTwoAtTheRight(Piece piece)
        {
            if (piece.GetColumn() == board.Width - 1) return false;
            if (piece.GetColumn() == board.Width - 2) return false;
            if (board.GetPiece(piece.GetColumn() + 1, piece.GetRow()).tag == piece.tag
                && board.GetPiece(piece.GetColumn() + 2, piece.GetRow()).tag == piece.tag)
                return true;
            return false;
        }

        private bool HasTwoAtTheLeft(Piece piece)
        {
            if (piece.GetColumn() == 0) return false;
            if (piece.GetColumn() == 1) return false;
            if (board.GetPiece(piece.GetColumn() - 1, piece.GetRow()).tag == piece.tag
                && board.GetPiece(piece.GetColumn() - 2, piece.GetRow()).tag == piece.tag)
                return true;
            return false;
        }

        private bool HasOneOnEachSide(Piece piece)
        {
            if (piece.GetColumn() == 0) return false;
            if (piece.GetColumn() == board.Width - 1) return false;
            if (board.GetPiece(piece.GetColumn() - 1, piece.GetRow()).tag == piece.tag 
                && board.GetPiece(piece.GetColumn() + 1, piece.GetRow()).tag == piece.tag)
                return true;
            return false;
        }       

        #endregion
    }
}