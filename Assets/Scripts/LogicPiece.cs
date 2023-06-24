using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Assets.Scripts
{
    public class LogicPiece: ILogicPiece
    {
        #region Fields

        private int column;
        private int row;
        private bool isMatched;
        private string tag;
        private string name;

        #endregion

        #region Constructors

        public LogicPiece(string name, string tag, int column, int row)
        {
            this.name = name;
            this.tag = tag;
            this.column = column;
            this.row = row;
            this.isMatched = false;            
        }

        #endregion

        #region Properties

        string ILogicPiece.Name => this.name;        

        string ILogicPiece.Tag => this.tag;

        #endregion

        #region Accessors

        int ILogicPiece.GetRow() => this.row;        

        void ILogicPiece.SetRow(int row)
        {
            this.row = row;
            this.UpdateName();
        }

        int ILogicPiece.GetColumn() => this.column;

        void ILogicPiece.SetColumn(int column)
        {
            this.column = column;
            this.UpdateName();
        }

        bool ILogicPiece.GetIsMatched() => this.isMatched;

        ILogicPiece ILogicPiece.Copy()
        {
            return LogicPiece.CopyLogicPiece(this);
        }

        private static ILogicPiece CopyLogicPiece(ILogicPiece original)
        {
            if (original != null)
            {
                return new LogicPiece(original.Name + "clone", original.Tag,
                    original.GetColumn(), original.GetRow());
            }
            return null;
        }

        #endregion

        #region Private Methods

        private void UpdateName()
        {
            this.name = $"{nameof(LogicPiece)} ( {this.column}, {this.row} )";
        }

        #endregion
    }
}
