using UnityEngine;

public partial class Board : MonoBehaviour
{
    public int Width;
    public int Height;    
    public BackgroundTile TilePrefab;
    public Piece[] Pieces;

    private Piece[,] AllPieces;
    private Vector2 firstTilePosition;    

    // Start is called before the first frame update
    void Start()
    {
        AllPieces = new Piece[Width, Height];
        Setup();
    }

    private void Setup()
    {
        firstTilePosition = TilePrefab.GetFirstTilePosition(Width, Height);
        Vector2 tilePosition = new Vector2();
        for (int i = 0; i < Width; i++) 
        { 
            for(int j = 0; j < Height; j++)
            {
                tilePosition = NextTilePosition(i, j, tilePosition);
                CreateTile(tilePosition, i, j);
                Piece instantiatedDot = CreateDot(tilePosition, i, j);
                AllPieces[i, j] = instantiatedDot;
            }
        }
    }        

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

    private Piece CreateDot(Vector2 tilePosition, int i, int j)
    {
        Piece prefabDotSelected = this.SelectDotToCreate();
        return prefabDotSelected.Instantiate(tilePosition, this.gameObject, i, j);        
    }

    private Piece SelectDotToCreate()
    {
        return Pieces[Random.Range(0, Pieces.Length)];
    }    

    public Piece GetPiece(int i, int j)
    {
        return AllPieces[i, j];
    }

    public void SetPiece(int i, int j, Piece piece)
    {
        AllPieces[i, j] =  piece;
    }
}
