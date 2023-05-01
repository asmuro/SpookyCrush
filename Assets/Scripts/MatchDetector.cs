using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts
{
    public class MatchDetector : MonoBehaviour
    {
        private Board board;
        private List<List<Piece>> _matches;

        // Use this for initialization
        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<Board>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void DetectAllMatchs()
        {
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    Piece currentPiece = board.GetPiece(i, j);
                    if (!currentPiece.IsMatched())
                        DetectPieceMatch(currentPiece);
                }
            }
        }

        private void DetectPieceMatch(Piece piece)
        {
            _matches.Add(GetLeftMatches(piece));            
        }

        private List<Piece> GetLeftMatches(Piece piece)
        {
            List<Piece> matches = new List<Piece>();
            int j = piece.GetRow();
            for (int i = piece.GetColumn(); i > 0; i++)
            {
                Piece leftPiece = board.GetPiece(i, j);
                if (piece.GetType() == leftPiece.GetType())
                {
                    piece.SetIsMatched(true);
                    leftPiece.SetIsMatched(true);
                    matches.Add(leftPiece);
                }
                else
                    return matches;
            }

            return matches;
        }

        private void DetectRightMatches(Piece piece)
        {
            for (int i = piece.GetColumn(); i < board.Width; i++)
            {
            }
        }        

        private void HighLightMatches()
        {

        }
    }
}