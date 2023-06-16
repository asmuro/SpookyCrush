using Assets.Scripts.BoardFunctionality;
using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Collapsers
{
    public class UpToDownCollapser : Collapser
    {
        public Vector3 Offset;

        private Board board;        

        #region Monobehavior

        // Use this for initialization
        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<Board>();
        }

        #endregion

        #region ICollapser

        public override Vector3 GetPositionOffset()
        {
            return Offset;
        }

        public override IEnumerator Collapse()
        {
            int columnsToCollapse = 0;
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    if (board.GetPiece(i, j) == null && !board.IsBlankSpace(i, j))
                        columnsToCollapse++;
                    else if (columnsToCollapse > 0)
                    {
                        IPiece currentPiece = board.GetPiece(i, j);
                        if (currentPiece != null)
                        {
                            var blankSpaceOffset = 0;
                            if (IsFuturePositionInvolvingBlankSpace(i, currentPiece.GetRow(), currentPiece.GetRow() - columnsToCollapse))
                            {
                                blankSpaceOffset = GetBlankSpaceOffset(i, currentPiece.GetRow());
                            }                            
                            currentPiece.SetRow(currentPiece.GetRow() - columnsToCollapse - blankSpaceOffset);
                            board.SetPiece(i, currentPiece.GetRow(), currentPiece);
                            board.SetPiece(i, j, null);
                        }
                    }
                }
                columnsToCollapse = 0;
            }
            yield return new WaitForSeconds(.4f);
            board.OnCollapsedColumns();
        }

        private bool IsFuturePositionInvolvingBlankSpace(int x, int yFrom, int yTo)
        {
            for (int k = yFrom; k >= yTo; k--)
            {
                if (board.IsBlankSpace(x, k))
                    return true;
            }
            return false;
        }
        

        private int GetBlankSpaceOffset(int x, int y)
        {
            int offset = 0;            

            for (int k = y; k >= 0; k--)
            {
                if (board.IsBlankSpace(x, k))
                    offset++;                
            }
            
            return offset;
        }

        public override IEnumerator CollapseOffsetPieces()
        {
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    if (board.GetPiece(i, j) != null && board.GetPiece(i, j).GetIsOffset())
                    {
                        IPiece currentPiece = board.GetPiece(i, j);
                        currentPiece?.SetDestination(new Vector3(currentPiece.GetColumn(), currentPiece.GetRow(),Piece.PIECE_DEPTH));                        
                    }
                }                
            }
            yield return new WaitForSeconds(.6f);
            board.OnCollapsedOffsetPieces();
        }

        public override IEnumerator InitialCollapse()
        {
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    IPiece currentPiece = board.GetPiece(i, j);
                    currentPiece?.SetDestination(new Vector3(currentPiece.GetColumn(), currentPiece.GetRow(), Piece.PIECE_DEPTH));
                }                
            }
            yield return new WaitForSeconds(.6f);
            board.OnInitialCollapsedColumns();
        }

        #endregion
    }
}
