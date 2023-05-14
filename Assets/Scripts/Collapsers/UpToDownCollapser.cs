using Assets.Scripts.BoardFunctionality;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Collapsers
{
    public class UpToDownCollapser : Collapser
    {
        private Board board;

        #region Monobehavior

        // Use this for initialization
        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<Board>();
        }

        #endregion

        #region ICollapser

        public override Vector3 GetPositionWithOffset(Vector3 position)
        {
            throw new NotImplementedException();
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

        #endregion
    }
}
