using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board
    {
        private void CreateBoardAndPieces()
        {
            Vector2 tilePosition;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    tilePosition = new Vector2(i, j);
                    CreateTile(tilePosition, i, j);
                    CreatePieceWithoutMatches(tilePosition);
                }
            }
        }

        private void CreateTile(Vector2 tilePosition, int i, int j)
        {
            TilePrefab.Instantiate(tilePosition, this.gameObject, i, j);
        }

        private void CreatePieceWithoutMatches(Vector2 tilePosition)
        {
            Piece instance = InstantiateFromPrefab(tilePosition);
            while (WillCreateAMatch(instance))
            {
                instance.Destroy();
                instance = InstantiateFromPrefab(tilePosition);
            }

            instance.SwipeFinished += OnSwapFinished;
        }

        private void CreatePiece(Vector2 tilePosition)
        {
            Piece instance = InstantiateFromPrefab(tilePosition);
            instance.SwipeFinished += OnSwapFinished;
        }

        private Piece InstantiateFromPrefab(Vector2 tilePosition)
        {
            Piece prefabPieceSelected = this.SelectPieceToCreate();
            Piece instance = prefabPieceSelected.Instantiate(tilePosition, this.gameObject);
            instance.SetFutureDestination(tilePosition);
            AllPieces[(int)tilePosition.x, (int)tilePosition.y] = instance;
            return instance;
        }

        private Piece SelectPieceToCreate()
        {
            return Pieces[Random.Range(0, Pieces.Length)]; ;
        }
    }
}
