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
                        CreatePiece(new Vector2(i, j));
                    }
                }
            }

            yield return new WaitForSeconds(.8f);
            this.OnBoardRefilled();            
        }
    }
}
