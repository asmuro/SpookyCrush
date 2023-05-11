using Assets.Scripts;
using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Piece : MonoBehaviour
{
    public float swipeAngle = 0;
    public const float DEPTH = -0.1f;
    public float Speed = 100;
    public bool _isMatched = false;
    public event EventHandler SwapFinished;

    private Swiper _swiper;
    private Swapper _swapper;
    private Color _originalColor;

    private int _column;
    private int _row;
    private Vector3 _destinationPoint;
    private bool _swapping = false;

    #region MonoBehaviour

    // Start is called before the first frame update
    void Start()
    {
        _swiper = GameObject.FindGameObjectWithTag(Constants.SWIPER_TAG).GetComponent<Swiper>();
        _swapper = GameObject.FindGameObjectWithTag(Constants.SWAPPER_TAG).GetComponent<Swapper>();
        this._destinationPoint = transform.position;
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
        return new Vector3(tilePosition.x, tilePosition.y, DEPTH);
    }

    public Piece Instantiate(Vector2 tilePosition, GameObject parent, int i, int j)
    {
        var piece = Instantiate(this, CreatePositionToRenderTheNewPiece(tilePosition), Quaternion.identity);
        this._destinationPoint = piece.transform.position;
        piece.transform.parent = parent.transform;
        piece._column = i;
        piece._row = j;
        piece.name = $"{nameof(Piece)} ( {i}, {j} )";
        piece._originalColor = new Color(piece.GetComponent<SpriteRenderer>().color.r,
            piece.GetComponent<SpriteRenderer>().color.g,
            piece.GetComponent<SpriteRenderer>().color.b,
            piece.GetComponent<SpriteRenderer>().color.a);        
        return piece;
    }

    #endregion

    #region Swipe

    private void OnMouseDown()
    {
        _swiper.SetStarterTouchPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));        
    }

    private void OnMouseUp()
    {
        _swiper.SetFinalTouchPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        this.SwapPiece();
    }

    #endregion

    #region Swap

    public void SwapPiece()
    {
        if (!_swiper.IsStartAndFinalPositionEquals())
            _swapper.Swap(_swiper.GetSwipeDirection(), this);        
    }

    #endregion

    #region Movement

    private void SmoothMove()
    {
        if (_destinationPoint != transform.position            )
        {
            if (Vector2.Distance(_destinationPoint, this.transform.position) < 0.1)
                transform.position = _destinationPoint;
            else
            {
                transform.position = new Vector3(
                  Mathf.SmoothStep(transform.position.x, _destinationPoint.x, Speed * Time.deltaTime),
                  Mathf.SmoothStep(transform.position.y, _destinationPoint.y, Speed * Time.deltaTime),
                  Mathf.SmoothStep(transform.position.z, _destinationPoint.z, Speed * Time.deltaTime));
            }
            if (!_swapping)
                _swapping = true;
        }
        else
        {
            if (_swapping)
            {
                _swapping = false;
                SwapFinished.Invoke(null, EventArgs.Empty);
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

    public void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }    

    public bool GetIsMatched()
    {
        return _isMatched;
    }

    public void SetIsMatched(bool isMatched)
    {
        _isMatched = isMatched;
        if (_isMatched)
        {
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();            
            sprite.color = new Color(0f, 0f, 0f, .2f);
        }
        else
        {
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
            if (sprite.color != _originalColor)
                sprite.color = _originalColor;
        }
    }

    public int GetColumn()
    {
        return _column;
    }

    public void SetColumn(int column)
    {
        this._column = column;
        this.UpdateName();
        this.SetDestination(new Vector3(this._column, this.transform.position.y, Piece.DEPTH));
    }

    public int GetRow()
    {
        return _row;
    }

    public void SetRow(int row)
    {
        this._row = row;
        this.UpdateName();
        this.SetDestination(new Vector3(this.transform.position.x, this._row, Piece.DEPTH));
    }

    private void UpdateName()
    {
        this.name = $"{nameof(Piece)} ( {this._column}, {this._row} )";
    }

    public void SetDestination(Vector3 destination)
    {
        this._destinationPoint = destination;
    }    

    #endregion
}
