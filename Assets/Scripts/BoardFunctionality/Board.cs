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
        public ParticleSystem[] ExplosionFX;
        public float DestroyExplosionFXAfterSeconds = 1;
        public Vector2[] BlankSpaces;

        private StateMachine StateMachine;
        private Piece[,] allPieces;
        private bool[,] blankSpacesMap;
        private bool boardRefilled = false;

        // Start is called before the first frame update
        void Start()
        {
            allPieces = new Piece[Width, Height];
            CreateBlankSpacesMap();
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
            StateMachine.State = Enums.State.Wait;
            DestroyAndCollapse();            
        }

        public void OnCollapsedColumns()
        {
            if (boardRefilled)
            {
                boardRefilled = false;
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
            DestroyAndCollapse();
        }

        private void DestroyAndCollapse()
        {
            MarkMatches();
            DestroyMatches();
            if (_matchesDestroyed)
                CollapseColumns();
            else
                StateMachine.State = Enums.State.Running;
        }

        #endregion        

        #region Accessesors

        public Piece GetPiece(int i, int j)
        {
            return allPieces[i, j];
        }

        public void SetPiece(int i, int j, Piece piece)
        {
            allPieces[i, j] = piece;
        }

        #endregion
    }
}
