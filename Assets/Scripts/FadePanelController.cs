using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class FadePanelController : MonoBehaviour
    {
        #region Properties

        public Animator panelAnimator;
        public Animator gameInfoAnimator;

        #endregion

        #region Public Methods

        public void Ok()
        {
            if (panelAnimator == null) { throw new ArgumentNullException("panelAnimator is not assigned to FadePanelController"); }
            if (gameInfoAnimator == null) { throw new ArgumentNullException("panelAnimator is not assigned to FadePanelController"); }
            panelAnimator.SetBool("Out", true);
            gameInfoAnimator.SetBool("Out", true);
        }

        #endregion

        #region private methods

        #endregion
    }


}
