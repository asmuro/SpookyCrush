using Assets.Scripts.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class GoalService : MonoBehaviour
    {
        #region Properties

        public BlankGoal[] LevelGoals;
        public GameObject goalPrefab;
        public GameObject goalIntroParent;
        public GameObject goalGameParent;

        #endregion

        #region MonoBehaviour

        private void Start()
        {
            SetupIntroGoals();
        }

        #endregion

        #region Public Methods

        public void SetupIntroGoals()
        {
            for (int i=0; i<LevelGoals.Length; i++)
            {
                SetGoalIntroPanelImageAndText(i);
                SetGoalGamePanelImageAndText(i);
            }
        }

        #endregion

        #region Private Methods

        private void SetGoalIntroPanelImageAndText(int levelGoalIndex)
        {
            GameObject goal = Instantiate(goalPrefab, goalIntroParent.transform.position, Quaternion.identity);
            goal.transform.SetParent(goalIntroParent.transform);
            GoalPanel goalPanel = goal.GetComponent<GoalPanel>();
            goalPanel.Sprite = LevelGoals[levelGoalIndex].goalSprite;
            goalPanel.String = $"0/{LevelGoals[levelGoalIndex].numberNeeded}";
            goalPanel.Image.preserveAspect = true;
        }

        private void SetGoalGamePanelImageAndText(int levelGoalIndex)
        {
            GameObject gameGoal = Instantiate(goalPrefab, goalGameParent.transform.position, Quaternion.identity);
            gameGoal.transform.SetParent(goalGameParent.transform);
            GoalPanel goalPanel2 = gameGoal.GetComponent<GoalPanel>();
            goalPanel2.Sprite = LevelGoals[levelGoalIndex].goalSprite;
            goalPanel2.String = $"0/{LevelGoals[levelGoalIndex].numberNeeded}";
            goalPanel2.Image.preserveAspect = true;
        }

        #endregion
    }
}
