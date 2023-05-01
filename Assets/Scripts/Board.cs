using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using UnityEngine;

public partial class Board : MonoBehaviour
{
    public int Width;
    public int Height;    
    public BackgroundTile TilePrefab;
    public Piece[] Pieces;
    public PieceMatcher[] PieceMatchers;    

    private Piece[,] AllPieces;
    private Vector2 firstTilePosition;    

    // Start is called before the first frame update
    void Start()
    {
        AllPieces = new Piece[Width, Height];
        CreateBoardAndPieces();
        RunMatches();
    }

    private void CreateBoardAndPieces()
    {
        firstTilePosition = TilePrefab.GetFirstTilePosition(Width, Height);
        Vector2 tilePosition = new Vector2();
        for (int i = 0; i < Width; i++) 
        { 
            for(int j = 0; j < Height; j++)
            {
                tilePosition = NextTilePosition(i, j, tilePosition);
                CreateTile(tilePosition, i, j);
                CreatePiece(tilePosition, i, j);                
            }
        }
    }

    private void OnSwapFinished(object sender, System.EventArgs e)
    {
        CleanMatches();
        RunMatches();
    }

    #region Matches

    private void CleanMatches()
    {
        foreach (var piece in AllPieces)
        {
            piece.SetIsMatched(false);
        }
    }

    private void RunMatches()
    {
        foreach (var piece in AllPieces)
        {
            foreach (PieceMatcher pieceMatcher in PieceMatchers)
            {
                piece.SetIsMatched(pieceMatcher.IsMatch(piece));
            }
        }
    }

    #endregion

    private Vector2 NextTilePosition(int i, int j, Vector2 lastTilePosition)
    {
        if (i == 0 && j == 0)
            return firstTilePosition;
        else
        {
            float xPos = NextCoordinatePosition(i, firstTilePosition.x, lastTilePosition.x);
            float yPos = NextCoordinatePosition(j, firstTilePosition.y, lastTilePosition.y);

            return new Vector2(xPos, yPos);
        }            
    }

    private float NextCoordinatePosition(int coordinate,float initialPosition, float lastCoordinatePosition)
    {
        if (coordinate == 0)
            return initialPosition;
        else
            return (initialPosition + TilePrefab.TileSize * coordinate);
    }

    private void CreateTile(Vector2 tilePosition, int i, int j)
    {
        TilePrefab.Instantiate(tilePosition, this.gameObject, i, j);
    }

    private void CreatePiece(Vector2 tilePosition, int i, int j)
    {
        Piece prefabDotSelected = this.SelectDotToCreate();
        Piece instance = prefabDotSelected.Instantiate(tilePosition, this.gameObject, i, j);
        instance.SwapFinished += OnSwapFinished;
        AllPieces[i, j] = instance;        
    }

    private Piece SelectDotToCreate()
    {
        return Pieces[Random.Range(0, Pieces.Length)];
    }

    #region Accessesors

    public Piece GetPiece(int i, int j)
    {
        return AllPieces[i, j];
    }

    public void SetPiece(int i, int j, Piece piece)
    {
        AllPieces[i, j] =  piece;
    }

    #endregion
}
