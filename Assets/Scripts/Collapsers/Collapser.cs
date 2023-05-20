using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Collapsers
{
    public abstract class Collapser : MonoBehaviour, ICollapser
    {
        public abstract IEnumerator Collapse();

        public abstract IEnumerator CollapseOffsetPieces();

        public abstract IEnumerator InitialCollapse();

        public abstract Vector3 GetPositionOffset();
        
    }
}
