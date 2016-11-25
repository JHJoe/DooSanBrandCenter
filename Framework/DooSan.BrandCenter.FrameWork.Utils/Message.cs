using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.FrameWork.Utils
{
    public class Message
    {
        public static string GetMessage(string msgCode, string defaultMessage = "")
        {
            string returnMessage = string.Empty;

            //to do: db에서 채워진 dic 에서 검색해서 가져오는걸로. 
            returnMessage = defaultMessage;

            return returnMessage;
        }
    }
}
