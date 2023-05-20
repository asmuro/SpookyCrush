using Assets.Scripts.BoardFunctionality;
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
                    if (board.GetPiece(i, j) == null)
                        columnsToCollapse++;
                    else if (columnsToCollapse > 0)
                    {
                        Piece currentPiece = board.GetPiece(i, j);
                        currentPiece.SetRow(currentPiece.GetRow() - columnsToCollapse);
                        board.SetPiece(i, currentPiece.GetRow(), currentPiece);                        
                        board.SetPiece(i, j, null);
                    }
                }
                columnsToCollapse = 0;
            }
            yield return new WaitForSeconds(.4f);
            board.OnCollapsedColumns();
        }

        public override IEnumerator CollapseOffsetPieces()
        {
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    if (board.GetPiece(i, j).GetIsOffset())
                    {
                        Piece currentPiece = board.GetPiece(i, j);
                        currentPiece.SetDestination(new Vector3(currentPiece.GetColumn(), currentPiece.GetRow(),Piece.PIECE_DEPTH));                        
                    }
                }                
            }
            yield return new WaitForSeconds(.4f);            
        }

        public override IEnumerator InitialCollapse()
        {
            int columnsToCollapse = (int)GetPositionOffset().y;
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    Piece currentPiece = board.GetPiece(i, j);
                    currentPiece.SetDestination(new Vector3(currentPiece.GetColumn(), currentPiece.GetRow(), Piece.PIECE_DEPTH));                        
                }                
            }
            yield return new WaitForSeconds(.4f);            
        }

        #endregion
    }
}
