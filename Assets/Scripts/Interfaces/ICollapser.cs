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
        /// Collapses all the offset pieces in the direction defined in the <see cref="ICollapser"/>. 
        /// </summary>              
        IEnumerator CollapseOffsetPieces();

        /// <summary>
        /// Initial collapse the pieces in the direction defined in the <see cref="ICollapser"/> from the original Offset
        /// </summary>              
        IEnumerator InitialCollapse();

        /// <summary>
        /// Gets the offset of the <see cref="ICollapser"/> applied, so the piece is created outside the camera and then move into position
        /// </summary>        
        /// <returns>The position with the offset applied</returns>
        Vector3 GetPositionOffset();
    }
}
