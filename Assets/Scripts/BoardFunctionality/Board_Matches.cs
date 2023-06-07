using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board
    {
        private bool _matchesDestroyed = false;
        
        private void MarkMatches()
        {
            foreach (var piece in allPieces)
            {
                if (piece)
                {
                    foreach (IPieceMatcher pieceMatcher in PieceMatchers)
                    {
                        piece.SetIsMatched(pieceMatcher.IsMatch(piece));
                    }
                }
            }
        }

        private void DestroyMatches()
        {
            Piece piece;
            _matchesDestroyed = false;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    piece = allPieces[i, j];
                    if (piece && piece.GetIsMatched())
                    {
                        StartCoroutine(InstatiateExplosionFXCo(piece.transform.position));
                        piece.Destroy();
                        allPieces[i, j] = null;
                        _matchesDestroyed = true;
                    }
                }
            }
        }

        IEnumerator InstatiateExplosionFXCo(Vector3 point)
        {
            ParticleSystem particle = ExplosionFX[Random.Range(0, ExplosionFX.Length)];
            var rightFX = Instantiate(particle, new Vector3(point.x, point.y, -1), Quaternion.identity);
            rightFX.Play();            
            
            yield return new WaitForSeconds(1f);
            Destroy(rightFX);
        }        

        private bool WillCreateAMatch(Piece piece)
        {
            foreach (IPieceMatcher pieceMatcher in PieceMatchers)
            {
                if (pieceMatcher.IsMatch(piece))
                    return true;
            }
            return false;
        }
    }
}
