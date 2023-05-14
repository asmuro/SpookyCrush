using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Matches
{
    public abstract class PieceMatcher : MonoBehaviour, IPieceMatcher
    {
        public abstract bool IsMatch(Piece piece);        
        
    }
}