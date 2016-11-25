using DooSan.BrandCenter.FrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Methods
{
    public class Method
    {
        /// <summary>
        /// 알파벳, 숫자 추출
        /// </summary>
        /// <param name="str">알파벳</param>
        /// <param name="strType">추출타잎, (알파벳+숫자:a, 알파벳:t, 숫자:n)</param>
        /// <returns></returns>
        public static string GetTextNumber(string str, string strType)
        {
            string rtnValue = "";

            string strPatton = "";

            try
            {
                switch (strType)
                {
                    case "a":
                        strPatton = @"[^0-9a-zA-Z]";
                        break;
                    case "t":
                        strPatton = @"[^a-zA-Z]";
                        break;
                    case "n":
                        strPatton = @"[^0-9]";
                        break;
                }

                rtnValue = Regex.Replace(str, strPatton, "");
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return rtnValue;
        }

        #region ###     UnixType TimeStamp Methods       ###

        /// <summary>
        /// Datetime to UnixTime
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            long _long = (long)(datetime - sTime).TotalMilliseconds;

            return _long;
        }

        /// <summary>
        /// Datetime to UnixTime
        /// </summary>
        /// <returns></returns>
        public static long ConvertToUnixTime()
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            long _long = (long)(DateTime.Now - sTime).TotalMilliseconds;

            return _long;
        }

        /// <summary>
        /// UnixTimeStamp To DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);
            return dtDateTime;
        }

        #endregion

    }
}
