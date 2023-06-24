
namespace Assets.Scripts.Interfaces
{
    internal interface ISwapService
    {
        void Swap(Direction direction, IPiece piece);

        void Swap(IPiece firstPiece, IPiece secondPiece);        
    }
}
