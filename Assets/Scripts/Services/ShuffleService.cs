using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class ShuffleService: MonoBehaviour, IShuffle
    {
        #region Fields

        private IBoard board;
        private ISwapService swapService;

        #endregion

        #region Monobehaviour

        public void Start()
        {
            board = GameObject.FindGameObjectWithTag(Constants.BOARD_TAG).GetComponent<IBoard>();
            swapService = GameObject.FindGameObjectWithTag(Constants.SWAPPER_TAG).GetComponent<ISwapService>();
        }

        #endregion


        #region public Methods

        public IEnumerator ShuffleBoardCo()
        {
            IPiece[,] allPieces = board.GetAllPieces();
            for (int i = 0; i < board.Width; i++)
            {
                for (int j = 0; j < board.Height; j++)
                {
                    var swapX = Random.Range(0, board.Width);
                    var swapY = Random.Range(0, board.Height);
                    ShuffleCo(allPieces[i, j], allPieces[swapX, swapY]);                    
                }
            }
            yield return new WaitForSeconds(0.4f);
            board.OnShuffleFinished();
        }

        private void ShuffleCo(IPiece firstPiece, IPiece secondPiece)
        {
            swapService.Swap(firstPiece, secondPiece);            
        }

        #endregion
    }
}
