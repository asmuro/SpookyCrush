using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PieceSwapper : MonoBehaviour
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

        #region UpSwap

        public void SwapUp(Piece currentPiece)
        {
            if (this.CanSwapUp(currentPiece))
            {
                var upPiece = GetUpperPiece(currentPiece);

                UpSwapUpdateMatrix(currentPiece, upPiece);
                UpSwapPieceColumnAndRow(currentPiece, upPiece);
                SwapPiecesRenderPositions(currentPiece, upPiece);
            }
        }

        private bool CanSwapUp(Piece currentPiece)
        {
            return currentPiece.GetRow() < board.Height - 1;
        }

        private Piece GetUpperPiece(Piece currentPiece)
        {
            return board.GetPiece(currentPiece.GetColumn(), currentPiece.GetRow() + 1);
        }

        private void UpSwapUpdateMatrix(Piece currentPiece, Piece leftPiece)
        {
            board.SetPiece(currentPiece.GetColumn(), currentPiece.GetRow(), leftPiece);
            board.SetPiece(currentPiece.GetColumn(), currentPiece.GetRow() + 1, currentPiece);
        }

        private void UpSwapPieceColumnAndRow(Piece currentPiece, Piece leftPiece)
        {
            currentPiece.SetRow(currentPiece.GetRow() + 1);
            leftPiece.SetRow(leftPiece.GetRow() - 1);
        }

        #endregion

        #region DownSwap

        public void SwapDown(Piece currentPiece)
        {
            if (this.CanSwapDown(currentPiece))
            {
                var downPiece = GetDownPiece(currentPiece);

                DownSwapUpdateMatrix(currentPiece, downPiece);
                DownSwapPieceColumnAndRow(currentPiece, downPiece);
                SwapPiecesRenderPositions(currentPiece, downPiece);
            }
        }

        private bool CanSwapDown(Piece currentPiece)
        {
            return currentPiece.GetRow() > 0;
        }

        private Piece GetDownPiece(Piece currentPiece)
        {
            return board.GetPiece(currentPiece.GetColumn(), currentPiece.GetRow() - 1);
        }

        private void DownSwapUpdateMatrix(Piece currentPiece, Piece leftPiece)
        {
            board.SetPiece(currentPiece.GetColumn(), currentPiece.GetRow(), leftPiece);
            board.SetPiece(currentPiece.GetColumn(), currentPiece.GetRow() - 1, currentPiece);
        }

        private void DownSwapPieceColumnAndRow(Piece currentPiece, Piece leftPiece)
        {
            currentPiece.SetRow(currentPiece.GetRow() - 1);
            leftPiece.SetRow(leftPiece.GetRow() + 1);
        }

        #endregion

        #region LeftSwap

        public void SwapLeft(Piece currentPiece)
        {
            if (this.CanSwapLeft(currentPiece))
            {
                var leftPiece = GetLeftPiece(currentPiece);

                LeftSwapUpdateMatrix(currentPiece, leftPiece);
                LeftSwapPieceColumnAndRow(currentPiece, leftPiece);
                SwapPiecesRenderPositions(currentPiece, leftPiece);
            }
        }

        private bool CanSwapLeft(Piece currentPiece)
        {
            return currentPiece.GetColumn() > 0;
        }

        private Piece GetLeftPiece(Piece currentPiece)
        {
            return board.GetPiece(currentPiece.GetColumn() - 1, currentPiece.GetRow());
        }

        private void LeftSwapUpdateMatrix(Piece currentPiece, Piece leftPiece)
        {
            board.SetPiece(currentPiece.GetColumn(), currentPiece.GetRow(), leftPiece);
            board.SetPiece(currentPiece.GetColumn() - 1, currentPiece.GetRow(), currentPiece);
        }

        private void LeftSwapPieceColumnAndRow(Piece currentPiece, Piece leftPiece)
        {
            currentPiece.SetColumn(currentPiece.GetColumn() - 1);
            leftPiece.SetColumn(leftPiece.GetColumn() + 1);
        }

        #endregion

        #region RightSwap

        public void SwapRight(Piece currentPiece)
        {
            if (this.CanSwapRight(currentPiece))
            {
                var rightPiece = GetRightPiece(currentPiece);

                RightSwapUpdateMatrix(currentPiece, rightPiece);
                RightSwapPieceColumnAndRow(currentPiece, rightPiece);
                SwapPiecesRenderPositions(currentPiece, rightPiece);
            }
        }

        private bool CanSwapRight(Piece currentPiece)
        {
            return currentPiece.GetColumn() < (board.Width - 1);
        }

        private Piece GetRightPiece(Piece currentPiece)
        {
            return board.GetPiece(currentPiece.GetColumn() + 1, currentPiece.GetRow());
        }

        private void RightSwapUpdateMatrix(Piece currentPiece, Piece rightPiece)
        {
            board.SetPiece(currentPiece.GetColumn(), currentPiece.GetRow(), rightPiece);
            board.SetPiece(currentPiece.GetColumn() + 1, currentPiece.GetRow(), currentPiece);
        }

        private void RightSwapPieceColumnAndRow(Piece currentPiece, Piece rightPiece)
        {
            currentPiece.SetColumn(currentPiece.GetColumn() + 1);
            rightPiece.SetColumn(rightPiece.GetColumn() - 1);
        }

        #endregion

        private void SwapPiecesRenderPositions(Piece currentPiece, Piece rightPiece)
        {
            Vector3 currentPiecePosition = new Vector3(currentPiece.transform.position.x, currentPiece.transform.position.y, Piece.DEPTH);
            currentPiece.SetDestination(new Vector3(rightPiece.transform.position.x, rightPiece.transform.position.y, Piece.DEPTH));
            rightPiece.SetDestination(currentPiecePosition);
        }

        #endregion
    }
}