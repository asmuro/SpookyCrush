using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IShuffle
    {
        /// <summary>
        /// Shuffles the current board randomly preserving all the pieces
        /// </summary>
        IEnumerator ShuffleBoardCo();
    }
}
