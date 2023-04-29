using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PieceSwapper : MonoBehaviour
{
    private Vector2 starterTouchPosition;
    private Vector2 finalTouchPosition;
    private Board board;

    private const int RIGHT_MAX_SWAP_ANGLE = 45;
    private const int RIGHT_MIN_SWAP_ANGLE = -45;
    private const int UP_MAX_SWAP_ANGLE = 136;
    private const int UP_MIN_SWAP_ANGLE = 45;
    private const string BOARD = "Board";

    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.FindGameObjectWithTag(BOARD).GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Properties

    public void SetStarterTouchPosition(Vector2 swipeStartPosition)
    { this.starterTouchPosition = swipeStartPosition; }

    public void SetFinalTouchPosition(Vector2 swipeFinalPosition)
    { this.finalTouchPosition = swipeFinalPosition; }

    #endregion

    private float CalculateAngleBetweenFirstAndFinalPosition()
    {
        return Mathf.Atan2(finalTouchPosition.y - starterTouchPosition.y, finalTouchPosition.x - starterTouchPosition.x) * 180 / Mathf.PI;        
        
    }

    public void SwapPiece(Piece currentPiece)
    {
        var swipeAngle = CalculateAngleBetweenFirstAndFinalPosition();
        Debug.Log(swipeAngle);
        if (this.ShouldSwapRight(swipeAngle))
            this.SwapRight(currentPiece);
        
    }

    #region ShouldSwap

    private bool ShouldSwapUp(float swipeAngle)
    {
        return (swipeAngle > UP_MIN_SWAP_ANGLE && swipeAngle <= UP_MAX_SWAP_ANGLE);
    }

    private bool ShouldSwapRight(float swipeAngle)
    {
        return (swipeAngle > RIGHT_MIN_SWAP_ANGLE && swipeAngle <= RIGHT_MAX_SWAP_ANGLE);
    }

    #endregion

    #region Swap

    private void SwapUp(Piece currentPiece)
    {

    }

    private void SwapRight(Piece currentPiece)
    {
        board.RightSwap(currentPiece);        
    }

    #endregion
}
