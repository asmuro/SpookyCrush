using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Score
{
    public class GoalPanel : MonoBehaviour
    {
        #region Fields
        

        #endregion

        #region Properties

        public Image Image;        
        public TMP_Text Text;       
        public Guid GoalId;

        #endregion

        #region Monobehaviour

        void Start()
        {
            
        }      

        #endregion

        #region Private Methods
       

        #endregion

        #region Public Methods

        public void Initialize(Guid goalId, Sprite sprite, string goalText)
        {
            this.Image.sprite = sprite;
            this.Text.text = goalText;
            this.Image.preserveAspect = true;
            this.GoalId = goalId;
        }

        public void SetTextGoal(string goalText)
        {
            this.Text.text = goalText;
        }

        #endregion
    }
}
