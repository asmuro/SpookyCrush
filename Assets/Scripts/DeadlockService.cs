using Assets.Scripts.BoardFunctionality;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using Assets.Scripts.PieceMatchers;
using UnityEngine;

namespace Assets.Scripts
{
    public class DeadlockService : MonoBehaviour, IDeadlockService
    {
        #region Constants

        private const int FIRST_COLUMN = 0;
        private const int SECOND_COLUMN = 1;

        #endregion

        #region Fields

        private IBoard board;        
        private MessageService messageService;
        private IMatchService matcherService;
        private IPiece lastPieceMatchDetected;
        private ILogicSwapService logicSwapService;

        #endregion

        #region  MonoBehaviour

        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<IBoard>();
            matcherService = GameObject.FindFirstObjectByType<MatchService>().GetComponent<IMatchService>();
            messageService = GameObject.FindFirstObjectByType<MessageService>();
            logicSwapService = GameObject.FindGameObjectWithTag(Constants.SWAPPER_TAG).GetComponent<ILogicSwapService>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion        

        #region Match

        private bool MovingPieceUpIsMatch(int row, int column)
        {
            ILogicPiece[,] allPiecesClone = CloneAllPieces();
            var currentPiece = allPiecesClone[row, column];
            if (logicSwapService.CanSwap(Direction.Up, currentPiece))
            {
                logicSwapService.Swap(allPiecesClone, Direction.Up, currentPiece);

                if (IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }

                currentPiece = allPiecesClone[row, column];
                if (IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }
            else
            {
                if (IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }

            return false;
        }

        private bool MovingPieceRightIsMatch(int row, int column)
        {
            ILogicPiece[,] allPiecesClone = CloneAllPieces();
            var currentPiece = allPiecesClone[row, column];
            if (logicSwapService.CanSwap(Direction.Right, currentPiece))
            {
                logicSwapService.Swap(allPiecesClone, Direction.Right, currentPiece);
                
                if (IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }

                currentPiece = allPiecesClone[row, column];
                if (IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }
            else
            {
                if (IsMatch(allPiecesClone, currentPiece))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece currentPiece)
        {
            return matcherService.IsMatch(allPiecesClone, currentPiece);            
        }

        #endregion

        #region Clone

        private ILogicPiece[,] CloneAllPieces()
        {
            var allPieces = board.GetAllPieces();
            LogicPiece[,] clone = new LogicPiece[this.board.Width, this.board.Height];
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    clone[i, j] = CopyPiece(allPieces[i, j]);
                }
            }

            return clone;
        }

        private LogicPiece CopyPiece(IPiece original)
        {
            if (original != null)
            {
                return new LogicPiece(original.Name + "clone", original.Tag,
                    original.GetColumn(), original.GetRow());
            }
            return null;
        }

        #endregion                

        #region IDeadlockService

        bool IDeadlockService.HasDeadlock()
        {
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    if (matcherService.MovingPieceUpIsMatch(i, j))
                    {
                        messageService.HideDeadlockText();
                        lastPieceMatchDetected = board.GetPiece(i, j);
                        return false;
                    }

                    if (matcherService.MovingPieceRightIsMatch(i, j))
                    {
                        messageService.HideDeadlockText();
                        lastPieceMatchDetected = board.GetPiece(i, j);
                        return false;
                    }
                }
            }
            Debug.Log("DEADLOCK");
            messageService.ShowDeadlockText();
            return true;
        }

        IPiece IDeadlockService.GetLastMatchedPiece()
        {
            return lastPieceMatchDetected;
        }

        #endregion

    }
}
