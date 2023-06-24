using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board
    {
        private void CollapseColumns()
        {
            StartCoroutine(Collapser.Collapse());            
        }

        private void CollapseOffsetPieces()
        {
            StartCoroutine(Collapser.CollapseOffsetPieces());
        }
        

        private void InitialCollapseColumns()
        {
            StartCoroutine(Collapser.InitialCollapse());
        }

        #region IBoard

        void IBoard.OnCollapsedColumns()
        {
            if (boardRefilled)
            {
                boardRefilled = false;
                DestroyAndCollapse();
            }
            else
            {
                RefillBoard();
            }
        }

        void IBoard.OnInitialCollapsedColumns()
        {
            hintService.ResetTime();
            CheckDeadlock();
        }

        void IBoard.OnCollapsedOffsetPieces()
        {
            hintService.ResetTime();
            DestroyAndCollapse();
            CheckDeadlock();
        }

        #endregion
    }
}
