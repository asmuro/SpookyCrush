using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using System.Collections;
using Unity.VisualScripting;
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

    #region Board Creation

    private void CreateBoardAndPieces()
    {
        firstTilePosition = TilePrefab.GetFirstTilePosition(Width, Height);
        Vector2 tilePosition = new Vector2();
        for (int i = 0; i < Width; i++) 
        { 
            for(int j = 0; j < Height; j++)
            {
                tilePosition = new Vector2(i, j);                
                CreateTile(tilePosition, i, j);
                CreatePiece(tilePosition, i, j);                
            }
        }
    }

    #endregion

    #region Refresh Board

    private void OnSwapFinished(object sender, System.EventArgs e)
    {
        CleanMatches();
        RunMatches();
        DestroyMatches();
    }

    #endregion

    #region Matches

    private void CleanMatches()
    {
        foreach (var piece in AllPieces)
        {
            if (piece)
                piece.SetIsMatched(false);
        }
    }

    private void RunMatches()
    {
        foreach (var piece in AllPieces)
        {
            if (piece)
            {
                foreach (PieceMatcher pieceMatcher in PieceMatchers)
                {
                    piece.SetIsMatched(pieceMatcher.IsMatch(piece));
                }
            }
        }
    }

    private void DestroyMatches()
    {
        Piece piece;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                piece = AllPieces[i,j];
                if (piece && piece.GetIsMatched())
                {
                    piece.Destroy();
                    AllPieces[i, j] = null;
                }
            }
        }
        StartCoroutine(nameof(CollapseColumns));
    }

    private IEnumerator CollapseColumns()
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
        yield return new WaitForSeconds(.1f);
    }    
    
    #endregion

    private void CreateTile(Vector2 tilePosition, int i, int j)
    {
        TilePrefab.Instantiate(tilePosition, this.gameObject, i, j);
    }

    private void CreatePiece(Vector2 tilePosition, int i, int j)
    {
        Piece instance = InstantiateFromPrefab(tilePosition, i, j);
        while (WillCreateAMatch(instance))
        {
            instance.Destroy();
            instance = InstantiateFromPrefab(tilePosition, i, j);
        }

        instance.SwapFinished += OnSwapFinished;        
    }

    private Piece InstantiateFromPrefab(Vector2 tilePosition, int i, int j)
    {
        Piece prefabPieceSelected = this.SelectPieceToCreate();
        Piece instance = prefabPieceSelected.Instantiate(tilePosition, this.gameObject, i, j);
        AllPieces[i, j] = instance;
        return instance;
    }

    private Piece SelectPieceToCreate()
    {        
        return Pieces[Random.Range(0, Pieces.Length)]; ;
    }    

    private bool WillCreateAMatch(Piece piece)
    {
        foreach (PieceMatcher pieceMatcher in PieceMatchers)
        {
            if (pieceMatcher.IsMatch(piece))
                return true;
        }
        return false;
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
