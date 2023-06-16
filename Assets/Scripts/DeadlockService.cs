using Assets.Scripts.BoardFunctionality;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using UnityEngine;

namespace Assets.Scripts
{
    public class DeadlockService : MonoBehaviour
    {
        #region Constants

        private const int FIRST_COLUMN = 0;
        private const int SECOND_COLUMN = 1;

        #endregion

        #region Fields

        private IBoard board;
        private PieceMatcher[] pieceMatchers;
        private MessageService messageService;

        #endregion

        #region  MonoBehaviour

        // Use this for initialization
        void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<Board>();
            messageService = GameObject.FindFirstObjectByType<MessageService>();
            pieceMatchers = board.PieceMatchers;
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        /// <summary>
        /// Detect deadlock moving up and right each piece.
        /// </summary>
        /// <returns>True if has deadlocks</returns>
        public bool HasDeadlock()
        {
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    if (MovingPieceUpIsMatch(i,j))
                    {
                        messageService.HideDeadlockText();
                        return false;
                    }                   

                    if (MovingPieceRightIsMatch(i, j))
                    {
                        messageService.HideDeadlockText();
                        return false;
                    }
                }
            }
            Debug.Log("DEADLOCK");
            messageService.ShowDeadlockText();
            return true;
        }

        private bool MovingPieceUpIsMatch(int row, int column)
        {
            ILogicPiece[,] allPiecesClone = CloneAllPieces();
            var currentPiece = allPiecesClone[row, column];
            if (this.CanSwapUp(currentPiece))
            {
                Swap(allPiecesClone, Direction.Up, currentPiece);

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
            if (this.CanSwapRight(currentPiece))
            {
                Swap(allPiecesClone, Direction.Right, currentPiece);
                
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
            foreach (var pieceMatcher in pieceMatchers)
            {
                if (pieceMatcher.IsMatch(allPiecesClone, currentPiece))
                {
                    Debug.Log($"Match Move right i:{currentPiece.GetColumn()} j:{currentPiece.GetRow()} ");
                    return true;
                }
            }
            return false;
        }

        private ILogicPiece[,] CloneAllPieces()
        {
            var allPieces = board.GetAllPieces();
            LogicPiece[,] clone = new LogicPiece[this.board.Width, this.board.Height];
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    clone[i, j] = CopyPiece(allPieces[i,j]);
                }
            }
            
            return clone;
        }

        private LogicPiece CopyPiece(IPiece original)
        {
            return new LogicPiece(original.Name + "clone", original.Tag,
                original.GetColumn(), original.GetRow());
        }

        #region Swap

        private void Swap(ILogicPiece[,] allPiecesClone, Direction direction, ILogicPiece piece)
        {
            if (piece != null)
            {
                switch (direction)
                {
                    case Direction.Up:
                        this.SwapUp(allPiecesClone, piece);
                        break;                    
                    default:
                        this.SwapRight(allPiecesClone, piece);
                        break;
                }
            }
        }

        #region UpSwap

        private void SwapUp(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (this.CanSwapUp(piece))
            {
                var upperPiece = GetUpperPiece(allPiecesClone, piece);
                if (upperPiece != null)
                {
                    UpSwapUpdateMatrix(allPiecesClone, piece, upperPiece);
                    UpSwapPieceColumnAndRow(piece, upperPiece);
                }
            }
        }

        private bool CanSwapUp(ILogicPiece piece)
        {
            return piece.GetRow() < board.Height - 1;
        }

        private ILogicPiece GetUpperPiece(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return allPiecesClone[piece.GetColumn(), piece.GetRow() + 1];
        }

        private void UpSwapUpdateMatrix(ILogicPiece[,] allPiecesClone, ILogicPiece piece, ILogicPiece upperPiece)
        {
            allPiecesClone[piece.GetColumn(), piece.GetRow()] = upperPiece;
            allPiecesClone[piece.GetColumn(), piece.GetRow() + 1] = piece;
        }

        private void UpSwapPieceColumnAndRow(ILogicPiece piece, ILogicPiece upperPiece)
        {
            piece.SetRow(piece.GetRow() + 1);
            upperPiece.SetRow(upperPiece.GetRow() - 1);
        }

        #endregion
        
        #region RightSwap

        private void SwapRight(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            if (this.CanSwapRight(piece))
            {
                var rightPiece = GetRightPiece(allPiecesClone, piece);
                if (rightPiece != null)
                {
                    RightSwapUpdateMatrix(allPiecesClone, piece, rightPiece);
                    RightSwapPieceColumnAndRow(piece, rightPiece);
                }
            }
        }

        private bool CanSwapRight(ILogicPiece piece)
        {
            return piece.GetColumn() < (board.Width - 1);
        }

        private ILogicPiece GetRightPiece(ILogicPiece[,] allPiecesClone, ILogicPiece piece)
        {
            return allPiecesClone[piece.GetColumn() + 1, piece.GetRow()];
        }

        private void RightSwapUpdateMatrix(ILogicPiece[,] allPiecesClone, ILogicPiece piece, ILogicPiece rightPiece)
        {
            allPiecesClone[piece.GetColumn(), piece.GetRow()] = rightPiece;
            allPiecesClone[piece.GetColumn() + 1, piece.GetRow()] =  piece;
        }

        private void RightSwapPieceColumnAndRow(ILogicPiece piece, ILogicPiece rightPiece)
        {
            piece.SetColumn(piece.GetColumn() + 1);
            rightPiece.SetColumn(rightPiece.GetColumn() - 1);
        }

        #endregion

        #endregion

        

    }
}
