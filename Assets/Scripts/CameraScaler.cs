using Assets.Scripts.BoardFunctionality;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    #region Fields

    private Board _board;
    public float CameraOffset;
    public float AspectRatio = 0.625f;
    public float Padding = 2;

    #endregion

    #region Monobehaviour

    // Start is called before the first frame update
    void Start()
    {
        _board = FindAnyObjectByType<Board>();
        RepositionCamera(_board.Width, _board.Height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    public void RepositionCamera(float width, float height)
    {
        transform.position = new Vector3((width - 1f) / 2f, (height - 1f) / 2f, CameraOffset);
        if (_board.Width >= _board.Height)
            Camera.main.orthographicSize = (_board.Width / 2 + Padding) / AspectRatio;
        else
            Camera.main.orthographicSize = _board.Height / 2 + Padding;
    }
}
