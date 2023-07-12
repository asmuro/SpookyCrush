using Assets.Scripts.Interfaces;
using Assets.Scripts.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class GoalService : MonoBehaviour, IGoalService
    {
        #region Fields

        private List<GoalPanel> currentGoalsPanel = new List<GoalPanel>();
        private int goalsCompleted = 0;
        private bool goalReached = false;

        #endregion

        #region Properties

        public BlankGoal[] LevelGoals;
        public GameObject goalPrefab;
        public GameObject goalIntroParent;
        public GameObject goalGameParent;        

        #endregion        

        #region MonoBehaviour

        void Start()
        {
            SetupGoals();
        }

        void Update()
        {
         
        }

        #endregion

        #region Public Methods        

        public void UpdateGoals()
        {   
            foreach(var levelGoal in LevelGoals)
            {
                if (!levelGoal.GoalCompleted)
                {
                    if (levelGoal.NumberCollected >= levelGoal.NumberNeeded)
                    {
                        goalsCompleted++;
                        currentGoalsPanel.First(c => c.GoalId == levelGoal.GoalId).SetTextGoal($"{levelGoal.NumberNeeded}/{levelGoal.NumberNeeded}");
                        levelGoal.GoalCompleted = true;
                    }
                    else
                    {
                        currentGoalsPanel.First(c => c.GoalId == levelGoal.GoalId).SetTextGoal($"{levelGoal.NumberCollected}/{levelGoal.NumberNeeded}");
                    }
                }
            }

            if(!goalReached && goalsCompleted >= LevelGoals.Length)
            {
                goalReached = true;
                Debug.Log("You won!");
            }
        }

        public void CompareGoal(string goalToCompare)
        {
            foreach(var levelGoal in LevelGoals)
            {
                if (levelGoal.MatchValue.Equals(goalToCompare))
                {
                    levelGoal.NumberCollected++;
                    UpdateGoals();
                }
            }            
        }

        #endregion

        #region Private Methods

        private void SetupGoals()
        {
            for (int i = 0; i < LevelGoals.Length; i++)
            {
                SetGoalIntroPanelImageAndText(LevelGoals[i]);
                SetGoalGamePanelImageAndText(LevelGoals[i]);
            }
        }

        private void SetGoalIntroPanelImageAndText(BlankGoal blankGoal)
        {            
            GameObject UIGoal = CreateUIGoal(goalIntroParent);
            GoalPanel goalPanel = UIGoal.GetComponent<GoalPanel>();
            goalPanel.Initialize(blankGoal.GoalId, blankGoal.GoalSprite, $"0/{blankGoal.NumberNeeded}");            
        }

        private void SetGoalGamePanelImageAndText(BlankGoal blankGoal)
        {            
            GameObject UIGoal = CreateUIGoal(goalGameParent);
            GoalPanel goalPanel = UIGoal.GetComponent<GoalPanel>();
            goalPanel.Initialize(blankGoal.GoalId, blankGoal.GoalSprite, $"0/{blankGoal.NumberNeeded}");                        
            currentGoalsPanel.Add(goalPanel);
        }       
        
        private GameObject CreateUIGoal(GameObject parent)
        {
            GameObject goal = Instantiate(goalPrefab, parent.transform.position, Quaternion.identity);
            goal.transform.SetParent(parent.transform);
            return goal;
        }

        #endregion
    }
}
