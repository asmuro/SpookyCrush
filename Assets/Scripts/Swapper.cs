using Assets.Scripts.BoardFunctionality;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class Swapper : MonoBehaviour, ISwapper
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

        public void Swap(Direction direction, IPiece piece)
        {
            if (piece != null)
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

        public void Swap(IPiece firstPiece, IPiece secondPiece)
        {
            if (firstPiece != null && secondPiece != null)
            {
                board.SetPiece(firstPiece.GetColumn(), firstPiece.GetRow(), secondPiece);
                board.SetPiece(secondPiece.GetColumn(), secondPiece.GetRow() , firstPiece);

                var secondPieceRow = secondPiece.GetRow();
                var secondPieceColumn = secondPiece.GetColumn();

                secondPiece.SetRowAndColumn(firstPiece.GetRow(), firstPiece.GetColumn());
                firstPiece.SetRowAndColumn(secondPieceRow, secondPieceColumn);                
            }
        }

        #region UpSwap

        private void SwapUp(IPiece piece)
        {
            if (this.CanSwapUp(piece))
            {
                var upperPiece = GetUpperPiece(piece);
                if (upperPiece != null)
                {
                    UpSwapUpdateMatrix(piece, upperPiece);
                    UpSwapPieceColumnAndRow(piece, upperPiece);                    
                }
            }
        }

        private bool CanSwapUp(IPiece piece)
        {
            return piece.GetRow() < board.Height - 1;
        }

        private IPiece GetUpperPiece(IPiece piece)
        {
            return board.GetPiece(piece.GetColumn(), piece.GetRow() + 1);
        }

        private void UpSwapUpdateMatrix(IPiece piece, IPiece upperPiece)
        {
            board.SetPiece(piece.GetColumn(), piece.GetRow(), upperPiece);
            board.SetPiece(piece.GetColumn(), piece.GetRow() + 1, piece);
        }

        private void UpSwapPieceColumnAndRow(IPiece piece, IPiece upperPiece)
        {
            piece.SetRow(piece.GetRow() + 1);
            upperPiece.SetRow(upperPiece.GetRow() - 1);
        }

        #endregion

        #region DownSwap

        private void SwapDown(IPiece piece)
        {
            if (this.CanSwapDown(piece))
            {
                var lowerPiece = GetDownPiece(piece);
                if (lowerPiece != null)
                {
                    DownSwapUpdateMatrix(piece, lowerPiece);
                    DownSwapPieceColumnAndRow(piece, lowerPiece);                    
                }
            }
        }

        private bool CanSwapDown(IPiece piece)
        {
            return piece.GetRow() > 0;
        }

        private IPiece GetDownPiece(IPiece piece)
        {
            return board.GetPiece(piece.GetColumn(), piece.GetRow() - 1);
        }

        private void DownSwapUpdateMatrix(IPiece piece, IPiece lowerPiece)
        {            
            board.SetPiece(piece.GetColumn(), piece.GetRow(), lowerPiece);
            board.SetPiece(piece.GetColumn(), piece.GetRow() - 1, piece);            
        }

        private void DownSwapPieceColumnAndRow(IPiece piece, IPiece lowerPiece)
        {
            piece.SetRow(piece.GetRow() - 1);
            lowerPiece.SetRow(lowerPiece.GetRow() + 1);
        }

        #endregion

        #region LeftSwap

        private void SwapLeft(IPiece piece)
        {
            if (this.CanSwapLeft(piece))
            {
                var leftPiece = GetLeftPiece(piece);
                if (leftPiece != null)
                {
                    LeftSwapUpdateMatrix(piece, leftPiece);
                    LeftSwapPieceColumnAndRow(piece, leftPiece);                    
                }
            }
        }

        private bool CanSwapLeft(IPiece piece)
        {
            return piece.GetColumn() > 0;            
        }

        private IPiece GetLeftPiece(IPiece piece)
        {
            return board.GetPiece(piece.GetColumn() - 1, piece.GetRow());            
        }

        private void LeftSwapUpdateMatrix(IPiece piece, IPiece leftPiece)
        {
            board.SetPiece(piece.GetColumn(), piece.GetRow(), leftPiece);
            board.SetPiece(piece.GetColumn() - 1, piece.GetRow(), piece);            
        }

        private void LeftSwapPieceColumnAndRow(IPiece piece, IPiece leftPiece)
        {
            piece.SetColumn(piece.GetColumn() - 1);
            leftPiece.SetColumn(leftPiece.GetColumn() + 1);
        }

        #endregion

        #region RightSwap

        private void SwapRight(IPiece piece)
        {            
            if (this.CanSwapRight(piece))
            {
                var rightPiece = GetRightPiece(piece);
                if (rightPiece != null)
                {
                    RightSwapUpdateMatrix(piece, rightPiece);
                    RightSwapPieceColumnAndRow(piece, rightPiece);                    
                }
            }            
        }

        private bool CanSwapRight(IPiece piece)
        {
            return piece.GetColumn() < (board.Width - 1);
        }

        private IPiece GetRightPiece(IPiece piece)
        {
            return board.GetPiece(piece.GetColumn() + 1, piece.GetRow());
        }

        private void RightSwapUpdateMatrix(IPiece piece, IPiece rightPiece)
        {             
            board.SetPiece(piece.GetColumn(), piece.GetRow(), rightPiece);
            board.SetPiece(piece.GetColumn() + 1, piece.GetRow(), piece);           
        }

        private void RightSwapPieceColumnAndRow(IPiece piece, IPiece rightPiece)
        {
            piece.SetColumn(piece.GetColumn() + 1);
            rightPiece.SetColumn(rightPiece.GetColumn() - 1);
        }

        #endregion        

        #endregion
    }
}