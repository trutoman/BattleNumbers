using System;
using System.Collections.Generic;
using System.Text;

namespace BattleNumbers.LogService
{
    interface ILogService
    {
        void Activate(bool activation);
        void AddText(string text);
        
    }
}
