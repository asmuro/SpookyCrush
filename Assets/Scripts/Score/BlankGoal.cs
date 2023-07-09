using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Score
{
    [Serializable]
    public class BlankGoal
    {
        #region Properties

        public int numberNeeded;
        public int numberCollected;
        public Sprite goalSprite;
        public string matchValue;

        #endregion
    }
}
