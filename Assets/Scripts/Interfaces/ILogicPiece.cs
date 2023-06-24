using System;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface ILogicPiece
    {
        /// <summary>
        /// Gets the row of the piece. 
        /// </summary>              
        int GetRow();

        /// <summary>
        /// Gets the column of the piece
        /// </summary>              
        int GetColumn();

        /// <summary>
        /// Get in the piece is matched
        /// </summary>              
        bool GetIsMatched();

        /// <summary>
        /// Sets the row to the piece
        /// </summary>
        /// <param name="row">new row</param>
        void SetRow(int  row);

        /// <summary>
        /// Sets the column of the piece
        /// </summary>
        /// <param name="column">new column</param>
        void SetColumn(int column);

        string Tag { get; }

        string Name { get; }

        ILogicPiece Copy();        
    }
}


