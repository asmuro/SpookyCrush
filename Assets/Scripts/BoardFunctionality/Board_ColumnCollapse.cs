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

        private IEnumerator CollapseColumnsCoroutine()
        {
            int columnsToCollapse = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (AllPieces[i, j] == null)
                        columnsToCollapse++;
                    else if (columnsToCollapse > 0)
                    {
                        AllPieces[i, j].SetRow(AllPieces[i, j].GetRow() - columnsToCollapse);
                        AllPieces[i, AllPieces[i, j].GetRow()] = AllPieces[i, j];
                        AllPieces[i, j] = null;
                    }
                }
                columnsToCollapse = 0;
            }
            yield return new WaitForSeconds(.4f);
            this.OnCollapsedColumns();
        }
    }
}
