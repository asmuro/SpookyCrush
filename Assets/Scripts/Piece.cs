using Assets.Scripts;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.PieceMatchers;
using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Piece : MonoBehaviour, ICloneable, IPiece
{
    #region Fields

    public float SwipeAngle = 0;
    public float Speed = 100;    
    
    public event EventHandler SwipeFinished;

    public bool isMatched = false;
    private Swiper swiper;
    private Swapper swapper;
    private IMatchService matcherService;
    private Color originalColor;
    private StateMachine stateMachine;

    private int column;
    private int row;
    private Vector3 destinationPoint;
    private Vector3 futureDestinationPoint;
    private bool trueSwap = false;
    private bool falseSwap = false;
    private Direction falseSwapDirection;
    private bool isOffset = false;

    #endregion

    #region MonoBehaviour

    // Start is called before the first frame update
    void Start()
    {
        swiper = GameObject.FindGameObjectWithTag(Constants.SWIPER_TAG).GetComponent<Swiper>();
        swapper = GameObject.FindGameObjectWithTag(Constants.SWAPPER_TAG).GetComponent<Swapper>();
        matcherService = GameObject.FindFirstObjectByType<MatchService>().GetComponent<IMatchService>();
        stateMachine = FindObjectOfType<StateMachine>();
        if (futureDestinationPoint == Vector3.zero)
            destinationPoint = transform.position;
        else
            destinationPoint = futureDestinationPoint;        
    }

    // Update is called once per frame
    void Update()
    {
        this.SmoothMove();        
    }

    #endregion

    #region Constructors

    private Vector3 CreatePositionToRenderTheNewPiece(Vector2 tilePosition)
    {
        return new Vector3(tilePosition.x, tilePosition.y, Constants.PIECE_DEPTH);
    }

    public Piece Instantiate(Vector2 tilePosition, GameObject parent)
    {
        var piece = Instantiate(this, CreatePositionToRenderTheNewPiece(tilePosition), Quaternion.identity);        
        piece.destinationPoint = piece.transform.position;
        piece.transform.parent = parent.transform;
        piece.column = (int)tilePosition.x;
        piece.row = (int)tilePosition.y;
        piece.name = $"{nameof(Piece)} ( {tilePosition.x}, {tilePosition.y} )";
        piece.originalColor = new Color(piece.GetComponent<SpriteRenderer>().color.r,
            piece.GetComponent<SpriteRenderer>().color.g,
            piece.GetComponent<SpriteRenderer>().color.b,
            piece.GetComponent<SpriteRenderer>().color.a);
        
        return piece;
    }

    

    #endregion

    #region Visibility

    private void SetInvisible()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetVisible()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    #endregion

    #region Swipe

    private void OnMouseDown()    
    {
        if (stateMachine.State == State.Running)
        {
            swiper.SetStarterTouchPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void OnMouseUp()
    {
        if (stateMachine.State == State.Running)
        {
            swiper.SetFinalTouchPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            this.SwapPiece();
        }
    }

    #endregion

    #region Swap

    public void SwapPiece()
    {
        if (!swiper.IsStartAndFinalPositionEquals())
        {
            Direction direction = swiper.GetSwipeDirection();            
            if (IsGoingToBeMatch(direction))
            {
                trueSwap = true;
            }
            else
            {
                falseSwap = true;
                falseSwapDirection = direction;
            }
            swapper.Swap(direction, this);
        }
    }

    private bool IsGoingToBeMatch(Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                return matcherService.MovingPieceUpIsMatch(this.column, this.row);
            case Direction.Down:
                return matcherService.MovingPieceDownIsMatch(this.column, this.row);
            case Direction.Left:
                return matcherService.MovingPieceLeftIsMatch(this.column, this.row);
            default:
                return matcherService.MovingPieceRightIsMatch(this.column, this.row);
        }                
    }

    private Direction GetOppsiteFalseSwapDirection(Direction falseSwapDirection)
    {
        switch (falseSwapDirection)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Right;
            default: 
                return Direction.Left;
        }
    }

    #endregion

    #region Movement

    private void SmoothMove()
    {
        if (destinationPoint != transform.position            )
        {
            if (Vector2.Distance(destinationPoint, this.transform.position) < 0.1)
                transform.position = destinationPoint;
            else
            {
                transform.position = new Vector3(
                  Mathf.SmoothStep(transform.position.x, destinationPoint.x, Speed * Time.deltaTime),
                  Mathf.SmoothStep(transform.position.y, destinationPoint.y, Speed * Time.deltaTime),
                  Mathf.SmoothStep(transform.position.z, destinationPoint.z, Speed * Time.deltaTime));
            }                
        }
        else
        {
            if (trueSwap)
            {
                trueSwap = false;
                SwipeFinished?.Invoke(null, EventArgs.Empty);
            }        
            else if (falseSwap)
            {
                falseSwap = false;
                swapper.Swap(GetOppsiteFalseSwapDirection(falseSwapDirection), this);
                trueSwap = false;
            }
        }
    }

    #endregion

    #region Destroy

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    #endregion

    #region Accessors    

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public UnityEngine.Transform GetTransform()
    {
        return transform;
    }

    public bool GetIsMatched()
    {
        return isMatched;
    }

    public void SetIsMatched(bool isMatched)
    {
        this.isMatched = isMatched;
        if (this.isMatched)
        {
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();            
            sprite.color = new Color(0f, 0f, 0f, .2f);
        }
        else
        {
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
            if (sprite.color != originalColor)
                sprite.color = originalColor;
        }
    }

    public int GetColumn()
    {
        return column;
    }

    public void SetColumn(int column)
    {
        this.column = column;
        this.UpdateName();
        this.SetDestination(new Vector3(this.column, this.transform.position.y, Constants.PIECE_DEPTH));
    }

    public int GetRow()
    {
        return row;
    }

    public void SetRow(int row)
    {
        this.row = row;
        this.UpdateName();
        this.SetDestination(new Vector3(this.transform.position.x, this.row, Constants.PIECE_DEPTH));
    }

    private void UpdateName()
    {
        this.name = $"{nameof(Piece)} ( {this.column}, {this.row} )";
    }

    public void SetRowAndColumn(int row, int column)
    {
        this.column = column;
        this.row = row;
        this.UpdateName();
        this.SetDestination(new Vector3(this.column, this.row, Constants.PIECE_DEPTH));
    }

    public void SetDestination(Vector3 destination)
    {
        this.destinationPoint = destination;
    }    

    public void SetFutureDestination(Vector3 futureDestination)
    {
        this.futureDestinationPoint = futureDestination;
    }

    public void SetIsOffset(bool isOffset)
    {
        this.isOffset = isOffset;
    }

    public bool GetIsOffset()
    {
        return isOffset;
    }

    public string Tag => this.tag;

    public string Name => this.name;

    #endregion

    #region ICloneable

    public object Clone()
    {
        var cloned = (Piece)this.MemberwiseClone();
        cloned.name = cloned.name + " clone ";
        return cloned;
    }

    public ILogicPiece Copy()
    {
        return CopyLogicPiece(this);
    }

    private static ILogicPiece CopyLogicPiece(IPiece original)
    {
        if (original != null)
        {
            return new LogicPiece(original.Name + "clone", original.Tag,
                original.GetColumn(), original.GetRow());
        }
        return null;
    }

    #endregion
}
