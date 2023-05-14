using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Collapsers
{
    public abstract class Collapser : MonoBehaviour, ICollapser
    {
        public abstract IEnumerator Collapse();

        public abstract Vector3 GetPositionWithOffset(Vector3 position);
        
    }
}
