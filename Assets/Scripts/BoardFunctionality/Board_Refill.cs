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
                    if(AllPieces[i,j] ==  null)
                    {
                        CreatePiece(new Vector2(i,j));
                        AllPieces[i, j].SetFutureDestination(AllPieces[i, j].GetPosition() + Collapser.GetPositionOffset());
                        AllPieces[i, j].SetIsOffset(true);
                    }
                }
            }

            yield return new WaitForSeconds(.2f);
            this.OnBoardRefilled();            
        }

        private void MakeAllPiecesVisibles()
        {
            foreach (var piece in AllPieces)
            {
                piece.SetVisible();
            }
        }
    }
}
