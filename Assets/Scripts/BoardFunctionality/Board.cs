using Assets.Scripts.Collapsers;
using Assets.Scripts.Matches;
using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board : MonoBehaviour
    {
        public int Width;
        public int Height;        
        public BackgroundTile TilePrefab;
        public Piece[] Pieces;
        public PieceMatcher[] PieceMatchers;
        public Collapser Collapser;
        public Timer TimerToStartPlaying;

        private Piece[,] AllPieces;
        private bool _boardRefilled = false;

        // Start is called before the first frame update
        void Start()
        {
            AllPieces = new Piece[Width, Height];
            CreateBoardAndPieces();
            MarkMatches();
            TimerToStartPlaying.TimerEnded += OnTimerEnded;
        }

        private void OnTimerEnded(object sender, System.EventArgs e)
        {
            MakeAllPiecesVisibles();
            TimerToStartPlaying.TimerEnded -= OnTimerEnded;
            InitialCollapseColumns();            
        }

        #region Refresh Board

        private void OnSwapFinished(object sender, System.EventArgs e)
        {
            DestroyAndCollapse();            
        }

        public void OnCollapsedColumns()
        {
            if (_boardRefilled)
            {
                _boardRefilled = false;
                DestroyAndCollapse();                
            }
            else
            {
                RefillBoard();
            }
        }

        private void OnBoardRefilled()
        {
            MakeAllPiecesVisibles();
            CollapseOffsetPieces();            
        }

        private void DestroyAndCollapse()
        {
            MarkMatches();
            DestroyMatches();
            if (matchesDestroyed)
                CollapseColumns();
        }

        #endregion        

        #region Accessesors

        public Piece GetPiece(int i, int j)
        {
            return AllPieces[i, j];
        }

        public void SetPiece(int i, int j, Piece piece)
        {
            AllPieces[i, j] = piece;
        }

        #endregion
    }
}
