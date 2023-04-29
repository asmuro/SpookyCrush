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

    #region Swap

    #region UpSwap

    public void SwapUp(Piece currentPiece)
    {
        if (this.CanSwapUp(currentPiece))
        {
            var upPiece = GetUpperPiece(currentPiece);

            UpSwapUpdateMatrix(currentPiece, upPiece);
            UpSwapPieceColumnAndRow(currentPiece, upPiece);
            SwapPiecesRenderPositions(currentPiece, upPiece);
        }
    }

    private bool CanSwapUp(Piece currentPiece)
    {
        return currentPiece.GetRow() < Height - 1;
    }

    private Piece GetUpperPiece(Piece currentPiece)
    {
        return AllPieces[currentPiece.GetColumn(), currentPiece.GetRow() + 1];
    }

    private void UpSwapUpdateMatrix(Piece currentPiece, Piece leftPiece)
    {
        AllPieces[currentPiece.GetColumn(), currentPiece.GetRow()] = leftPiece;
        AllPieces[currentPiece.GetColumn(), currentPiece.GetRow() + 1] = currentPiece;
    }

    private void UpSwapPieceColumnAndRow(Piece currentPiece, Piece leftPiece)
    {
        currentPiece.SetRow(currentPiece.GetRow() + 1);
        leftPiece.SetRow(leftPiece.GetRow() - 1);
    }

    #endregion

    #region DownSwap

    public void SwapDown(Piece currentPiece)
    {
        if (this.CanSwapDown(currentPiece))
        {
            var downPiece = GetDownPiece(currentPiece);

            DownSwapUpdateMatrix(currentPiece, downPiece);
            DownSwapPieceColumnAndRow(currentPiece, downPiece);
            SwapPiecesRenderPositions(currentPiece, downPiece);
        }
    }

    private bool CanSwapDown(Piece currentPiece)
    {
        return currentPiece.GetRow() > 0;
    }

    private Piece GetDownPiece(Piece currentPiece)
    {
        return AllPieces[currentPiece.GetColumn(), currentPiece.GetRow() - 1];
    }

    private void DownSwapUpdateMatrix(Piece currentPiece, Piece leftPiece)
    {
        AllPieces[currentPiece.GetColumn(), currentPiece.GetRow()] = leftPiece;
        AllPieces[currentPiece.GetColumn(), currentPiece.GetRow() - 1] = currentPiece;
    }

    private void DownSwapPieceColumnAndRow(Piece currentPiece, Piece leftPiece)
    {
        currentPiece.SetRow(currentPiece.GetRow() - 1);
        leftPiece.SetRow(leftPiece.GetRow() + 1);
    }

    #endregion

    #region LeftSwap

    public void SwapLeft(Piece currentPiece)
    {
        if (this.CanSwapLeft(currentPiece))
        {
            var leftPiece = GetLeftPiece(currentPiece);

            LeftSwapUpdateMatrix(currentPiece, leftPiece);
            LeftSwapPieceColumnAndRow(currentPiece, leftPiece);
            SwapPiecesRenderPositions(currentPiece, leftPiece);
        }
    }

    private bool CanSwapLeft(Piece currentPiece)
    {
        return currentPiece.GetColumn() > 0;
    }

    private Piece GetLeftPiece(Piece currentPiece)
    {
        return AllPieces[currentPiece.GetColumn() - 1, currentPiece.GetRow()];
    }

    private void LeftSwapUpdateMatrix(Piece currentPiece, Piece leftPiece)
    {
        AllPieces[currentPiece.GetColumn(), currentPiece.GetRow()] = leftPiece;
        AllPieces[currentPiece.GetColumn() - 1, currentPiece.GetRow()] = currentPiece;
    }

    private void LeftSwapPieceColumnAndRow(Piece currentPiece, Piece leftPiece)
    {
        currentPiece.SetColumn(currentPiece.GetColumn() - 1);
        leftPiece.SetColumn(leftPiece.GetColumn() + 1);
    }

    #endregion

    #region RightSwap

    public void SwapRight(Piece currentPiece)
    {
        if(this.CanSwapRight(currentPiece))
        {
            var rightPiece = GetRightPiece(currentPiece);

            RightSwapUpdateMatrix(currentPiece, rightPiece);
            RightSwapPieceColumnAndRow(currentPiece, rightPiece);            
            SwapPiecesRenderPositions(currentPiece, rightPiece);
        }        
    }

    private bool CanSwapRight(Piece currentPiece)
    {
        return currentPiece.GetColumn() < (Width - 1);
    }

    private Piece GetRightPiece(Piece currentPiece)
    {
        return AllPieces[currentPiece.GetColumn() + 1, currentPiece.GetRow()];
    }

    private void RightSwapUpdateMatrix(Piece currentPiece, Piece rightPiece)
    {
        AllPieces[currentPiece.GetColumn(), currentPiece.GetRow()] = rightPiece;
        AllPieces[currentPiece.GetColumn() + 1, currentPiece.GetRow()] = currentPiece;
    }

    private void RightSwapPieceColumnAndRow(Piece currentPiece, Piece rightPiece)
    {
        currentPiece.SetColumn(currentPiece.GetColumn() + 1);        
        rightPiece.SetColumn(rightPiece.GetColumn() - 1);
    }

    #endregion    

    private void SwapPiecesRenderPositions(Piece currentPiece, Piece rightPiece)
    {
        Vector3 currentPiecePosition = new Vector3(currentPiece.transform.position.x, currentPiece.transform.position.y, Piece.DEPTH);
        currentPiece.SetDestination(new Vector3(rightPiece.transform.position.x, rightPiece.transform.position.y, Piece.DEPTH));
        rightPiece.SetDestination(currentPiecePosition);                
    }

    #endregion
}
