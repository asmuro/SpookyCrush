using Assets.Scripts.Interfaces;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board
    {
        private bool matchesDestroyed = false;
        
        private void MarkMatches()
        {
            foreach (var piece in AllPieces)
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
            matchesDestroyed = false;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    piece = AllPieces[i, j];
                    if (piece && piece.GetIsMatched())
                    {
                        piece.Destroy();
                        AllPieces[i, j] = null;
                        matchesDestroyed = true;
                    }
                }
            }
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
