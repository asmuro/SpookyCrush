using Assets.Scripts.Collapsers;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Matches;
using Assets.Scripts.PieceMatchers;
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
        
        public Collapser Collapser;
        public Timer TimerToStartPlaying;        
        public ParticleSystem[] ExplosionFX;
        public float DestroyExplosionFXAfterSeconds = 1;
        public Vector2[] BlankSpaces;
        
        public StateMachine StateMachine;
        public ShuffleService ShuffleService;

        private IPiece[,] allPieces;
        private bool[,] blankSpacesMap;
        private bool boardRefilled = false;
        private IDeadlockService deadlockService;
        private IHintService hintService;
        private IMatchService matchService;
        private IMatchCounterService matchCounterService;


        // Start is called before the first frame update
        void Start()
        {
            deadlockService = GameObject.FindFirstObjectByType<DeadlockService>().GetComponent<IDeadlockService>() ?? throw new Exception("IDeadlockService not found"); ;
            hintService = GameObject.FindFirstObjectByType<HintService>().GetComponent<IHintService>() ?? throw new Exception("IHintService not found"); ;
            matchService = GameObject.FindFirstObjectByType<MatchService>().GetComponent<IMatchService>() ?? throw new Exception("IMatchService not found"); ;
            matchCounterService = GameObject.FindFirstObjectByType<MatchCounterService>().GetComponent<IMatchCounterService>() ?? throw new Exception("IMatchCounterService not found"); ;
            allPieces = new Piece[Width, Height];
            CreateBlankSpacesMap();
            CreateBoardAndPieces();
            MarkMatches();
            TimerToStartPlaying.TimerEnded += OnTimerEnded;           
        }

        private void OnTimerEnded(object sender, EventArgs e)
        {
            MakeAllPiecesVisibles();
            TimerToStartPlaying.TimerEnded -= OnTimerEnded;
            InitialCollapseColumns();            
        }

        #region Refresh Board

        private void OnSwapFinished(object sender, EventArgs e)
        {
            hintService.ResetTime();
            StateMachine.State = Enums.State.Wait;            
            DestroyAndCollapse();            
        }

        private void OnBoardRefilled()
        {
            MakeAllPiecesVisibles();
            CollapseOffsetPieces();
        }

        private void CheckDeadlock()
        {
            if (deadlockService.HasDeadlock())
            {
                this.StateMachine.State = Enums.State.Wait;
                StartCoroutine(ShuffleService.ShuffleBoardCo());
            }
            else
            {

            }
        }

        public void OnShuffleFinished()
        {
            DestroyAndCollapse();
            CheckDeadlock();
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

        #region IBoard

        IPiece[,] IBoard.GetAllPieces()
        {
            return allPieces;
        }

        int IBoard.Width { get => Width; }
        int IBoard.Height { get => Height; }        

        IPiece IBoard.GetPiece(int i, int j)
        {
            return allPieces[i, j];
        }

        void IBoard.SetPiece(int i, int j, IPiece piece)
        {
            allPieces[i, j] = piece;
        }

        ILogicPiece[,] IBoard.CloneAllPieces()
        {
            ILogicPiece[,] clone = new ILogicPiece[this.Width, this.Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    clone[i, j] = allPieces[i, j].Copy();
                }
            }

            return clone;
        }        

        #endregion

        public void LaunchDeadlock()
        {
            this.deadlockService.HasDeadlock();
        }
    }
}
