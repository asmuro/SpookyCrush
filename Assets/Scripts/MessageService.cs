using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MessageService:MonoBehaviour
    {
        #region Constants

        private const string DEADLOCK_TEXT_TAG = "DeadlockText";
        

        #endregion

        #region Fields

        private GameObject deadlockText;

        #endregion

        #region Monobehaviour

        public void Start()
        {
            deadlockText = GameObject.FindGameObjectsWithTag(DEADLOCK_TEXT_TAG)[0];
        }

        #endregion

        #region Public Methods

        public void ShowDeadlockText()
        {
            deadlockText.GetComponent<TMP_Text>().text = ConstantsToTranslate.DEADLOCK_TEXT;            
        }

        public void HideDeadlockText()
        {
            deadlockText.GetComponent<TMP_Text>().text = String.Empty;
        }

        #endregion
    }
}
