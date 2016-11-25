using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandCenter.Helper
{
    public class Message
    {
        public static string GetMessage(string msgCode, string defaultMessage = "")
        {
            string returnMessage = string.Empty;
            //to do: db에서 채워진 dic 에서 msgCode와 GetSession.Language 검색해서 가져오는걸로. 
            //GetSession.Language;
            if (TempMessage.ContainsKey(msgCode))  //실제론 언어코드까지 중첩체크해야하는데 임시라서.
                returnMessage = TempMessage[msgCode];
            else
                returnMessage = defaultMessage;

            return returnMessage;
        }

        public static Dictionary<string, string> TempMessage = new Dictionary<string, string>()
        {
            { "SelectText", "--Select--" }

        };

    }
}