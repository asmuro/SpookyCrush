using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public float swipeAngle = 0;
    public const float DEPTH = -0.1f;
    public float Speed = 100;

    private PieceSwapper _swapper;    
    private bool _isNull = false;
    private const string SWAPPER = "Swapper";
    private int _column;
    private int _row;
    private Vector3 _destinationPoint;

    // Start is called before the first frame update
    void Start()
    {
        _swapper = GameObject.FindGameObjectWithTag(SWAPPER).GetComponent<PieceSwapper>();
        this._destinationPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.SmoothMove();        
    }

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
        return piece;
    }

    private void OnMouseDown()
    {
        _swapper.SetStarterTouchPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));        
    }

    private void OnMouseUp()
    {
        _swapper.SetFinalTouchPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _swapper.SwapPiece(this);
    }

    public static Piece CreateNullPiece()
    {
        return new Piece() { _isNull = true };        
    }

    public void SetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    private void SmoothMove()
    {
        if (this._destinationPoint != transform.position)
        {
            transform.position = new Vector3(
                  Mathf.SmoothStep(transform.position.x, _destinationPoint.x, Speed * Time.deltaTime),
                  Mathf.SmoothStep(transform.position.y, _destinationPoint.y, Speed * Time.deltaTime),
                  Mathf.SmoothStep(transform.position.z, _destinationPoint.z, Speed * Time.deltaTime));
        }
    }

    #region Accessors

    public bool IsNull()
    {
        return _isNull;
    }

    public int GetColumn()
    {
        return _column;
    }

    public void SetColumn(int column)
    {
        this._column = column;
        this.UpdateName();
    }

    public int GetRow()
    {
        return _row;
    }

    public void SetRow(int row)
    {
        this._row = row;
        this.UpdateName();
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
