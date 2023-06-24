using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using UnityEngine;

namespace Assets.Scripts.PieceMatchers
{
    public class MatchService : MonoBehaviour, IMatchService
    {
        #region Properties

        public PieceMatcher[] PieceMatchers;

        #endregion

        #region Fields

        private IBoard board;
        private ILogicSwapService logicSwapService;

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            if(PieceMatchers == null || PieceMatchers.Length == 0)
            {
                throw new System.Exception("0 piece Matchers configured");
            }
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<IBoard>();
            logicSwapService = GameObject.FindFirstObjectByType<LogicSwapService>().GetComponent<ILogicSwapService>();
        }

        #endregion

        #region IMatcherService

        bool IMatchService.IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            foreach (var pieceMatcher in PieceMatchers)
            {
                if (pieceMatcher.IsMatch(allPiecesClone, piece))
                {
                    Debug.Log($"Match Move right i:{piece.GetColumn()} j:{piece.GetRow()} ");
                    return true;
                }
            }
            return false;
        }

        bool IMatchService.IsMatch(ILogicPiece piece)
        {
            if (piece != null)
            {
                foreach (IPieceMatcher pieceMatcher in PieceMatchers)
                {
                    if (pieceMatcher.IsMatch(piece))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool IMatchService.MovingPieceUpIsMatch(int row, int column)
        {
            ILogicPiece[,] allPiecesClone = board.CloneAllPieces();
            var currentPiece = allPiecesClone[row, column];
            if (logicSwapService.CanSwap(Direction.Up, currentPiece))
            {
                logicSwapService.Swap(allPiecesClone, Direction.Up, currentPiece);

                if (CheckMatchAfterSwap(allPiecesClone, currentPiece, row, column))
                {
                    return true;
                }
            }
            else
            {
                if ((this as IMatchService).IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }
            
            return false;
        }

        bool IMatchService.MovingPieceDownIsMatch(int row, int column)
        {
            ILogicPiece[,] allPiecesClone = board.CloneAllPieces();
            var currentPiece = allPiecesClone[row, column];
            if (logicSwapService.CanSwap(Direction.Down, currentPiece))
            {
                logicSwapService.Swap(allPiecesClone, Direction.Down, currentPiece);

                if (CheckMatchAfterSwap(allPiecesClone, currentPiece, row, column))
                {
                    return true;
                }
            }
            else
            {
                if ((this as IMatchService).IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }

            return false;
        }

        bool IMatchService.MovingPieceLeftIsMatch(int row, int column)
        {
            ILogicPiece[,] allPiecesClone = board.CloneAllPieces();
            var currentPiece = allPiecesClone[row, column];
            if (logicSwapService.CanSwap(Direction.Left, currentPiece))
            {
                logicSwapService.Swap(allPiecesClone, Direction.Left, currentPiece);
                if (CheckMatchAfterSwap(allPiecesClone, currentPiece, row, column))
                { 
                    return true; 
                }                
            }
            else
            {
                if ((this as IMatchService).IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }

            return false;
        }

        
        bool IMatchService.MovingPieceRightIsMatch(int row, int column)
        {
            ILogicPiece[,] allPiecesClone = board.CloneAllPieces();
            var currentPiece = allPiecesClone[row, column];
            if (logicSwapService.CanSwap(Direction.Right, currentPiece))
            {
                logicSwapService.Swap(allPiecesClone, Direction.Right, currentPiece);

                if (CheckMatchAfterSwap(allPiecesClone, currentPiece, row, column))
                {
                    return true;
                }
            }
            else
            {
                if ((this as IMatchService).IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region private methods

        private bool CheckMatchAfterSwap(ILogicPiece[,] allPiecesClone, ILogicPiece currentPiece, int row, int column)
        {
            if ((this as IMatchService).IsMatch(allPiecesClone, currentPiece))
            {
                return true;
            }

            currentPiece = allPiecesClone[row, column];
            if ((this as IMatchService).IsMatch(allPiecesClone, currentPiece))
            {
                return true;
            }

            return false;
        }


        #endregion
    }
}
