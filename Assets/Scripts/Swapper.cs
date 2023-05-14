using Assets.Scripts.BoardFunctionality;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Swapper : MonoBehaviour
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

        #region Swap

        public void Swap(Direction direction, Piece piece)
        {
            if (piece)
            {
                switch (direction)
                {
                    case Direction.Up:
                        this.SwapUp(piece);
                        break;
                    case Direction.Down:
                        this.SwapDown(piece);
                        break;
                    case Direction.Left:
                        this.SwapLeft(piece);
                        break;
                    default:
                        this.SwapRight(piece);
                        break;
                }
            }
        }

        #region UpSwap

        private void SwapUp(Piece piece)
        {
            if (this.CanSwapUp(piece))
            {
                var upPiece = GetUpperPiece(piece);
                if (upPiece)
                {
                    UpSwapUpdateMatrix(piece, upPiece);
                    UpSwapPieceColumnAndRow(piece, upPiece);                    
                }
            }
        }

        private bool CanSwapUp(Piece piece)
        {
            return piece.GetRow() < board.Height - 1;
        }

        private Piece GetUpperPiece(Piece piece)
        {
            return board.GetPiece(piece.GetColumn(), piece.GetRow() + 1);
        }

        private void UpSwapUpdateMatrix(Piece piece, Piece leftPiece)
        {
            board.SetPiece(piece.GetColumn(), piece.GetRow(), leftPiece);
            board.SetPiece(piece.GetColumn(), piece.GetRow() + 1, piece);
        }

        private void UpSwapPieceColumnAndRow(Piece piece, Piece leftPiece)
        {
            piece.SetRow(piece.GetRow() + 1);
            leftPiece.SetRow(leftPiece.GetRow() - 1);
        }

        #endregion

        #region DownSwap

        private void SwapDown(Piece piece)
        {
            if (this.CanSwapDown(piece))
            {
                var downPiece = GetDownPiece(piece);
                if (downPiece)
                {
                    DownSwapUpdateMatrix(piece, downPiece);
                    DownSwapPieceColumnAndRow(piece, downPiece);                    
                }
            }
        }

        private bool CanSwapDown(Piece piece)
        {
            return piece.GetRow() > 0;
        }

        private Piece GetDownPiece(Piece piece)
        {
            return board.GetPiece(piece.GetColumn(), piece.GetRow() - 1);
        }

        private void DownSwapUpdateMatrix(Piece piece, Piece leftPiece)
        {            
            board.SetPiece(piece.GetColumn(), piece.GetRow(), leftPiece);
            board.SetPiece(piece.GetColumn(), piece.GetRow() - 1, piece);            
        }

        private void DownSwapPieceColumnAndRow(Piece piece, Piece leftPiece)
        {
            piece.SetRow(piece.GetRow() - 1);
            leftPiece.SetRow(leftPiece.GetRow() + 1);
        }

        #endregion

        #region LeftSwap

        private void SwapLeft(Piece piece)
        {
            if (this.CanSwapLeft(piece))
            {
                var leftPiece = GetLeftPiece(piece);
                if (leftPiece)
                {
                    LeftSwapUpdateMatrix(piece, leftPiece);
                    LeftSwapPieceColumnAndRow(piece, leftPiece);                    
                }
            }
        }

        private bool CanSwapLeft(Piece piece)
        {
            return piece.GetColumn() > 0;            
        }

        private Piece GetLeftPiece(Piece piece)
        {
            return board.GetPiece(piece.GetColumn() - 1, piece.GetRow());            
        }

        private void LeftSwapUpdateMatrix(Piece piece, Piece leftPiece)
        {
            board.SetPiece(piece.GetColumn(), piece.GetRow(), leftPiece);
            board.SetPiece(piece.GetColumn() - 1, piece.GetRow(), piece);            
        }

        private void LeftSwapPieceColumnAndRow(Piece piece, Piece leftPiece)
        {
            piece.SetColumn(piece.GetColumn() - 1);
            leftPiece.SetColumn(leftPiece.GetColumn() + 1);
        }

        #endregion

        #region RightSwap

        private void SwapRight(Piece piece)
        {            
            if (this.CanSwapRight(piece))
            {
                var rightPiece = GetRightPiece(piece);
                if (rightPiece)
                {
                    RightSwapUpdateMatrix(piece, rightPiece);
                    RightSwapPieceColumnAndRow(piece, rightPiece);                    
                }
            }            
        }

        private bool CanSwapRight(Piece piece)
        {
            return piece.GetColumn() < (board.Width - 1);
        }

        private Piece GetRightPiece(Piece piece)
        {
            return board.GetPiece(piece.GetColumn() + 1, piece.GetRow());
        }

        private void RightSwapUpdateMatrix(Piece piece, Piece rightPiece)
        {             
            board.SetPiece(piece.GetColumn(), piece.GetRow(), rightPiece);
            board.SetPiece(piece.GetColumn() + 1, piece.GetRow(), piece);           
        }

        private void RightSwapPieceColumnAndRow(Piece piece, Piece rightPiece)
        {
            piece.SetColumn(piece.GetColumn() + 1);
            rightPiece.SetColumn(rightPiece.GetColumn() - 1);
        }

        #endregion        

        #endregion
    }
}