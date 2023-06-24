using Assets.Scripts.BoardFunctionality;
using Assets.Scripts.Interfaces;
using Assets.Scripts.PieceMatchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class LogicSwapService : MonoBehaviour, ILogicSwapService
    {
        #region Fields

        private IBoard board;

        #endregion

        #region  MonoBehaviour
        
        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<IBoard>();            
        }

        #endregion

        #region ILogicSwapService
        bool ILogicSwapService.CanSwap(Direction direction, ILogicPiece piece)
        {
            if (piece != null)
            {
                switch (direction)
                {
                    case Direction.Up:
                        return this.CanSwapUp(piece);
                    case Direction.Down:
                        return this.CanSwapDown(piece);
                    case Direction.Left:
                        return this.CanSwapLeft(piece);
                    default:
                        return this.CanSwapRight(piece);
                }
            }
            return false;
        }

        void ILogicSwapService.Swap(ILogicPiece[,] allPiecesClone, Direction direction, ILogicPiece piece)
        {
            if (piece != null)
            {
                switch (direction)
                {
                    case Direction.Up:
                        this.SwapUp(allPiecesClone, piece);
                        break;
                    case Direction.Down:
                        this.SwapDown(allPiecesClone, piece);
                        break;
                    case Direction.Left:
                        this.SwapLeft(allPiecesClone, piece);
                        break;
                    default:
                        this.SwapRight(allPiecesClone, piece);
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        #region UpSwap

        private void SwapUp(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (this.CanSwapUp(piece))
            {
                var upperPiece = GetUpperPiece(allPiecesClone, piece);
                if (upperPiece != null)
                {
                    UpSwapUpdateMatrix(allPiecesClone, piece, upperPiece);
                    UpSwapPieceColumnAndRow(piece, upperPiece);
                }
            }
        }

        private bool CanSwapUp(ILogicPiece piece)
        {
            return piece.GetRow() < board.Height - 1;
        }

        private ILogicPiece GetUpperPiece(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return allPiecesClone[piece.GetColumn(), piece.GetRow() + 1];
        }

        private void UpSwapUpdateMatrix(ILogicPiece[,] allPiecesClone, ILogicPiece piece, ILogicPiece upperPiece)
        {
            allPiecesClone[piece.GetColumn(), piece.GetRow()] = upperPiece;
            allPiecesClone[piece.GetColumn(), piece.GetRow() + 1] = piece;
        }

        private void UpSwapPieceColumnAndRow(ILogicPiece piece, ILogicPiece upperPiece)
        {
            piece.SetRow(piece.GetRow() + 1);
            upperPiece.SetRow(upperPiece.GetRow() - 1);
        }

        #endregion

        #region DownSwap

        private void SwapDown(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (this.CanSwapDown(piece))
            {
                var lowerPiece = GetDownPiece(allPiecesClone, piece);
                if (lowerPiece != null)
                {
                    DownSwapUpdateMatrix(allPiecesClone, piece, lowerPiece);
                    DownSwapPieceColumnAndRow(piece, lowerPiece);
                }
            }
        }

        private bool CanSwapDown(ILogicPiece piece)
        {
            return piece.GetRow() > 0;
        }

        private ILogicPiece GetDownPiece(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return allPiecesClone[piece.GetColumn(), piece.GetRow() - 1];
        }

        private void DownSwapUpdateMatrix(ILogicPiece[,] allPiecesClone, ILogicPiece piece, ILogicPiece lowerPiece)
        {
            allPiecesClone[piece.GetColumn(), piece.GetRow()] = lowerPiece;
            allPiecesClone[piece.GetColumn(), piece.GetRow() - 1] = piece;
        }

        private void DownSwapPieceColumnAndRow(ILogicPiece piece, ILogicPiece lowerPiece)
        {
            piece.SetRow(piece.GetRow() - 1);
            lowerPiece.SetRow(lowerPiece.GetRow() + 1);
        }

        #endregion

        #region LeftSwap

        private void SwapLeft(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (this.CanSwapLeft(piece))
            {
                var leftPiece = GetLeftPiece(allPiecesClone, piece);
                if (leftPiece != null)
                {
                    LeftSwapUpdateMatrix(allPiecesClone, piece, leftPiece);
                    LeftSwapPieceColumnAndRow(piece, leftPiece);
                }
            }
        }

        private bool CanSwapLeft(ILogicPiece piece)
        {
            return piece.GetColumn() > 0;
        }

        private ILogicPiece GetLeftPiece(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return allPiecesClone[piece.GetColumn() - 1, piece.GetRow()];
        }

        private void LeftSwapUpdateMatrix(ILogicPiece[,] allPiecesClone, ILogicPiece piece, ILogicPiece leftPiece)
        {
            allPiecesClone[piece.GetColumn(), piece.GetRow()] =  leftPiece;
            allPiecesClone[piece.GetColumn() - 1, piece.GetRow()] =  piece;
        }

        private void LeftSwapPieceColumnAndRow(ILogicPiece piece, ILogicPiece leftPiece)
        {
            piece.SetColumn(piece.GetColumn() - 1);
            leftPiece.SetColumn(leftPiece.GetColumn() + 1);
        }

        #endregion

        #region RightSwap

        private void SwapRight(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (this.CanSwapRight(piece))
            {
                var rightPiece = GetRightPiece(allPiecesClone, piece);
                if (rightPiece != null)
                {
                    RightSwapUpdateMatrix(allPiecesClone, piece, rightPiece);
                    RightSwapPieceColumnAndRow(piece, rightPiece);
                }
            }
        }

        private bool CanSwapRight(ILogicPiece piece)
        {
            return piece.GetColumn() < (board.Width - 1);
        }

        private ILogicPiece GetRightPiece(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return allPiecesClone[piece.GetColumn() + 1, piece.GetRow()];
        }

        private void RightSwapUpdateMatrix(ILogicPiece[,] allPiecesClone, ILogicPiece piece, ILogicPiece rightPiece)
        {
            allPiecesClone[piece.GetColumn(), piece.GetRow()] = rightPiece;
            allPiecesClone[piece.GetColumn() + 1, piece.GetRow()] = piece;
        }

        private void RightSwapPieceColumnAndRow(ILogicPiece piece, ILogicPiece rightPiece)
        {
            piece.SetColumn(piece.GetColumn() + 1);
            rightPiece.SetColumn(rightPiece.GetColumn() - 1);
        }

        #endregion

        #endregion


    }
}
