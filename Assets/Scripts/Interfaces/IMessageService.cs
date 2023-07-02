using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IMessageService
    {

        void ShowDeadlockText();

        void HideDeadlockText();

        void UpdateScore(int newScore);

        //void ShowCombo(int comboStrike);
    }
}
