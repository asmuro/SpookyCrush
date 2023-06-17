using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.BoardFunctionality
{
    public partial class Board
    {
        private void CreateBlankSpacesMap()
        {
            this.blankSpacesMap = new bool[this.Width,this.Height];
            foreach (Vector2 tile in BlankSpaces)
            {
                this.blankSpacesMap[(int)tile.x, (int)tile.y] = true;
            }
        }

        #region IBoard

        bool IBoard.IsBlankSpace(int x, int y) => this.blankSpacesMap[x, y];

        #endregion

    }
}
