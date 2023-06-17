using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IPiece: ILogicPiece
    {
        void SetIsMatched(bool isMatched);

        Vector3 GetPosition();

        Transform GetTransform();

        void Destroy();

        bool GetIsOffset();

        void SetDestination(Vector3 destination);

        void SetFutureDestination(Vector3 futureDestination);

        void SetRowAndColumn(int row, int column);

        void SetVisible();

        void SetIsOffset(bool isOffset);

        string Name { get; }
    }
}


