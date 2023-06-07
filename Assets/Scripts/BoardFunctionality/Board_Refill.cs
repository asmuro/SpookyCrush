using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board
    {
        private void RefillBoard()
        {
            StartCoroutine(RefillBoardCoroutine());
        }

        private IEnumerator RefillBoardCoroutine()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if(allPieces[i,j] == null && !blankSpacesMap[i,j])
                    {
                        CreatePiece(new Vector2(i,j));
                        allPieces[i, j].SetFutureDestination(allPieces[i, j].GetPosition() + Collapser.GetPositionOffset());
                        allPieces[i, j].SetIsOffset(true);
                    }
                }
            }

            yield return new WaitForSeconds(.2f);
            this.OnBoardRefilled();            
        }

        private void MakeAllPiecesVisibles()
        {
            foreach (var piece in allPieces)
            {
                if (piece != null)
                {
                    piece.SetVisible();
                }
            }
        }
    }
}
