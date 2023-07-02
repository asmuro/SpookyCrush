using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IMatchService
    {
        bool IsMatch(ILogicPiece piece);
        bool IsMatch(ILogicPiece[,] allPiecesClone, ILogicPiece piece);

        bool MovingPieceUpIsMatch(int row, int column);
        bool MovingPieceDownIsMatch(int row, int column);
        bool MovingPieceLeftIsMatch(int row, int column);
        bool MovingPieceRightIsMatch(int row, int column);


        int GetStandardMatchLength();
    }
}
