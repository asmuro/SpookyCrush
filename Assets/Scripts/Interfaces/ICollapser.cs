using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    internal interface ICollapser
    {
        /// <summary>
        /// Collapses the pieces in the direction defined in the <see cref="ICollapser"/>. 
        /// </summary>              
        IEnumerator Collapse();

        /// <summary>
        /// Gets a position with the offset of the <see cref="ICollapser"/> applied, so the piece is created outside the camera and then move into position
        /// </summary>
        /// <param name="position">Final destionation of the piece once is created</param>
        /// <returns>The position with the offset applied</returns>
        Vector3 GetPositionWithOffset(Vector3 position);
    }
}
