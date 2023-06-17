using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IDeadlockService
    {
        /// <summary>
        /// Detect deadlock moving up and right each piece.
        /// </summary>
        /// <returns>True if has deadlocks</returns>
        bool HasDeadlock();

        IPiece GetLastMatchedPiece();
    }
}
