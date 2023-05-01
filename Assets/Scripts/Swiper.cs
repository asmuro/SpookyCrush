using Assets.Scripts;
using UnityEngine;

public class Swiper : MonoBehaviour
{
    private Vector2 starterTouchPosition;
    private Vector2 finalTouchPosition;
    

    private const int RIGHT_MAX_SWAP_ANGLE = 45;
    private const int RIGHT_MIN_SWAP_ANGLE = -45;
    private const int UP_MAX_SWAP_ANGLE = 135;
    private const int UP_MIN_SWAP_ANGLE = 45;
    private const int LEFT_MIN_POSITIVE_SWAP_ANGLE = 135;
    private const int LEFT_MIN_NEGATIVE_SWAP_ANGLE = -135;
    private const int DOWN_MAX_SWAP_ANGLE = -45;
    private const int DOWN_MIN_SWAP_ANGLE = -135;
    

    // Start is called before the first frame update
    void Start()
    {
        
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
    
    public Direction GetSwipeDirection()
    {
        var swipeAngle = this.CalculateAngleBetweenFirstAndFinalPosition();

        if (this.IsUpSwipe(swipeAngle))
            return Direction.Up;
        if (this.IsDownSwipe(swipeAngle))
            return Direction.Down;
        if (this.IsLeftSwipe(swipeAngle))
            return Direction.Left;
        else
            return Direction.Right;
        
    }

    #region ShouldSwap

    private bool IsUpSwipe(float swipeAngle)
    {
        return (swipeAngle > UP_MIN_SWAP_ANGLE && swipeAngle <= UP_MAX_SWAP_ANGLE);
    }

    private bool IsDownSwipe(float swipeAngle)
    {
        return (swipeAngle > DOWN_MIN_SWAP_ANGLE && swipeAngle <= DOWN_MAX_SWAP_ANGLE);
    }

    private bool IsLeftSwipe(float swipeAngle)
    {
        return (swipeAngle < LEFT_MIN_NEGATIVE_SWAP_ANGLE || swipeAngle > LEFT_MIN_POSITIVE_SWAP_ANGLE);
    }

    private bool IsRightSwipe(float swipeAngle)
    {
        return (swipeAngle > RIGHT_MIN_SWAP_ANGLE && swipeAngle <= RIGHT_MAX_SWAP_ANGLE);
    }    

    #endregion    
}
