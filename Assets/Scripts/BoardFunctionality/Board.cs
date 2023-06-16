using Assets.Scripts.Collapsers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using Assets.Scripts.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board : MonoBehaviour, IBoard
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
        public DeadlockService DeadlockService;
        public StateMachine StateMachine;
        public ShuffleService ShuffleService;

        private IPiece[,] allPieces;
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

        public void OnInitialCollapsedColumns()
        {
            CheckDeadlock();            
        }

        private void OnBoardRefilled()
        {
            MakeAllPiecesVisibles();
            CollapseOffsetPieces();                        
        }

        public void OnCollapsedOffsetPieces()
        {
            DestroyAndCollapse();
            CheckDeadlock();
        }

        private void CheckDeadlock()
        {
            if (DeadlockService.HasDeadlock())
            {
                this.StateMachine.State = Enums.State.Wait;
                StartCoroutine(ShuffleService.ShuffleBoardCo());
            }
        }

        public void OnShuffleFinished()
        {
            if (DeadlockService.HasDeadlock())
            {
                StartCoroutine(ShuffleService.ShuffleBoardCo());
            }
            else
            {
                DestroyAndCollapse();
                CheckDeadlock();
                this.StateMachine.State = Enums.State.Running;
            }
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

        public IPiece GetPiece(int i, int j)
        {
            return allPieces[i, j];
        }

        public void SetPiece(int i, int j, IPiece piece)
        {
            allPieces[i, j] = piece;
        }

        #endregion

        #region IBoard

        IPiece[,] IBoard.GetAllPieces()
        {
            return allPieces;
        }

        int IBoard.Width { get => Width; }
        int IBoard.Height { get => Height; }

        PieceMatcher[] IBoard.PieceMatchers => this.PieceMatchers;

        #endregion

        public void LaunchDeadlock()
        {
            this.DeadlockService.HasDeadlock();
        }
    }
}
