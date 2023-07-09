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
        #region Properties

        public Image Image;
        public Sprite Sprite;
        public TMP_Text Text;
        public string String;

        #endregion

        #region Monobehaviour

        void Start()
        {
            SetSpriteAndString();
        }      

        #endregion

        #region Private Methods

        private void SetSpriteAndString()
        {
            this.Image.sprite = this.Sprite;
            this.Text.text = this.String;
        }

        #endregion
    }
}
