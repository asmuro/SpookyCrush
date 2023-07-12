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

        public int NumberNeeded;
        public int NumberCollected;
        public Sprite GoalSprite;
        public string MatchValue;
        public Guid GoalId = Guid.NewGuid();
        public bool GoalCompleted = false;

        #endregion
    }
}
