using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class LogicPiece: ILogicPiece
    {
        #region Fields

        private int column;
        private int row;
        private bool isMatched;
        private string tag;

        #endregion

        #region Constructors

        public LogicPiece(string name, string tag, int column, int row)
        {
            this.Name = name;
            this.tag = tag;
            this.column = column;
            this.row = row;
            this.isMatched = false;            
        }

        #endregion

        #region Properties

        public string Name;        

        string ILogicPiece.Tag => this.tag;

        #endregion

        #region Accessors

        public int GetRow() => this.row;
        

        public void SetRow(int row)
        {
            this.row = row;
        }

        public int GetColumn() => this.column;

        public void SetColumn(int column)
        {
            this.column = column;
        }

        public bool GetIsMatched() => this.isMatched;                

        #endregion
    }
}
