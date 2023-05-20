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
    }
}
