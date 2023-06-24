
namespace Assets.Scripts.Interfaces
{
    internal interface ILogicSwapService
    {
        void Swap(ILogicPiece[,] allPiecesClone, Direction direction, ILogicPiece piece);

        bool CanSwap(Direction direction, ILogicPiece piece);        
    }
}
