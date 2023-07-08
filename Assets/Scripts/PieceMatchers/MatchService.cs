using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.PieceMatchers
{
    public class MatchService : MonoBehaviour, IMatchService
    {
        #region Properties

        public IPieceMatcher[] Matchers;
        public Matcher[] PiecesMatchers;

        #endregion

        #region Fields

        private IBoard board;
        private ILogicSwapService logicSwapService;
        private bool serviceInitialized = false;

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            this.Initialize();   
        }

        #endregion

        #region IMatcherService

        bool IMatchService.IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            Initialize();
            foreach (var pieceMatcher in Matchers)
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
            Initialize();
            if (piece != null)
            {
                foreach (IPieceMatcher pieceMatcher in Matchers)
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

        int IMatchService.GetStandardMatchLength()
        {
            if (Matchers == null)
            {
                Initialize();
            }
            return Matchers.First().MatchLenght;
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

        private void CreateMatchers()
        {
            Matchers = new IPieceMatcher[PiecesMatchers.Length];
            for (int i = 0; i < PiecesMatchers.Length; i++)
            {
                var newMatcher = MatchFactory.Create(PiecesMatchers[i]);
                newMatcher.SetBoard(board);
                Matchers[i] = newMatcher;
            }
        }

        private void Initialize()
        {
            if (!serviceInitialized)
            {
                serviceInitialized = true;
                if (PiecesMatchers == null || PiecesMatchers.Length == 0)
                {
                    throw new System.Exception("0 piece PiecesMatchers configured in MatchService");
                }
                GetBoard();
                logicSwapService = GameObject.FindFirstObjectByType<LogicSwapService>().GetComponent<ILogicSwapService>();
                CreateMatchers();
            }
        }

        private void GetBoard()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<IBoard>() ?? throw new System.Exception("No board found in MatchService");
        }


        #endregion
    }
}
