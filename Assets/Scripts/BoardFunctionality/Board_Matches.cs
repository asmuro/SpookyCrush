using Assets.Scripts.Interfaces;
using Assets.Scripts.Services;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board
    {
        #region Fields

        private bool _matchesDestroyed = false;

        #endregion

        private void MarkMatches()
        {
            foreach (var piece in allPieces)
            {
                piece?.SetIsMatched(matchService.IsMatch(piece));
            }
        }

        private void DestroyMatches()
        {
            IPiece piece;
            _matchesDestroyed = false;
            int piecesDestroyed = 0;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    piece = allPieces[i, j];
                    if (piece != null && piece.GetIsMatched())
                    {                        
                        piece.Destroy();
                        allPieces[i, j] = null;
                        _matchesDestroyed = true;
                        piecesDestroyed++;
                    }
                }
            }
            matchCounterService.AddMatch(piecesDestroyed);
        }

        private bool WillCreateAMatch(Piece piece)
        {
            return matchService.IsMatch(piece);            
        }
    }
}
