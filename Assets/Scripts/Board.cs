using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int Width;
    public int Height;    
    public BackgroundTile TilePrefab;
    public Piece[] Dots;
    private Piece[,] AllPieces;

    private Vector2 firstTilePosition;
    private BackgroundTile[,] allTiles;
    



    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[Width, Height];
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
        return Dots[UnityEngine.Random.Range(0, Dots.Length)];
    }

    public Piece GetRightPiece(Piece currentPiece)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (currentPiece == AllPieces[i, j] && i < Width)
                    return AllPieces[i + 1, j];                
            }
        }

        return Piece.CreateNullPiece();
    }

    public void RightSwap(Piece currentPiece)
    {
        if(currentPiece.GetColumn() < (Width - 1))
        {
            var rightPiece = AllPieces[currentPiece.GetColumn() + 1, currentPiece.GetRow()];

            SwapPiecesMatrix(currentPiece, rightPiece);
            SwapPieceColumnAndRow(currentPiece, rightPiece);            
            SwapPiecesRenderPositions(currentPiece, rightPiece);
        }        
    }

    private void SwapPieceColumnAndRow(Piece currentPiece, Piece rightPiece)
    {
        currentPiece.SetColumn(currentPiece.GetColumn() + 1);        
        rightPiece.SetColumn(rightPiece.GetColumn() - 1);
    }

    private void SwapPiecesMatrix(Piece currentPiece, Piece rightPiece)
    {
        AllPieces[currentPiece.GetColumn(), currentPiece.GetRow()] = rightPiece;
        AllPieces[currentPiece.GetColumn() + 1, currentPiece.GetRow()] = currentPiece;
    }

    private void SwapPiecesRenderPositions(Piece currentPiece, Piece rightPiece)
    {
        Vector3 currentPiecePosition = new Vector3(currentPiece.transform.position.x, currentPiece.transform.position.y, Piece.DEPTH);
        currentPiece.SetDestination(new Vector3(rightPiece.transform.position.x, rightPiece.transform.position.y, Piece.DEPTH));
        rightPiece.SetDestination(currentPiecePosition);                
    }

}
