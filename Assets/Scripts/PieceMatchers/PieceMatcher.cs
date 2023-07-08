using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Matches
{
    public abstract class PieceMatcher : MonoBehaviour, IPieceMatcher
    {
        public abstract int MatchLenght { get; }

        public abstract bool IsMatch(ILogicPiece piece);
        public abstract bool IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece piece);

        public abstract void SetBoard(IBoard board);
    }
}