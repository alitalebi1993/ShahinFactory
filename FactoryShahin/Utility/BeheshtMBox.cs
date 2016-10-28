using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class BeheshtMBox
    {
        public enum Result { Yes, No, OK };
        public enum MessageType { YesNo, OK };
        public enum Icon { Info, Warning, Question };

        public static Result Show(string Text, string Title = "", Icon Icon = Icon.Info, MessageType MessageType = MessageType.OK)
        {  
            View.FormMessageBox FMB = new View.FormMessageBox(Title, Text, MessageType, Icon);
            FMB.ShowDialog();
            return FMB.Result;
        }


    }
}
