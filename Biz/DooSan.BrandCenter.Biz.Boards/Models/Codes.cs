using DooSan.BrandCenter.BrandCenterDBConext;
using DooSan.BrandCenter.FrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Models
{
    public class Codes
    {
        /// <summary>
        /// 공통코드
        /// </summary>
        public static List<ComCode> ComCodeList = new List<ComCode>();


        /// <summary>
        /// 공통코드 추출
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="isName">Name : true, Note : false</param>
        /// <returns></returns>
        public static string GetComCodeName(string Code, bool isName = false)
        {
            string rtn = "";

            try
            {
                rtn = ComCodeList
                        .Where(w => !string.IsNullOrEmpty(w.CODE) && w.CODE.Equals(Code))
                        .Select(s => isName.Equals(true) ? s.NAME : s.NOTE)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return rtn;
        }

        /// <summary>
        /// 공통코드 검색
        /// </summary>
        /// <param name="Major"></param>
        /// <returns></returns>
        public static List<ComCode> GetComCodeList(string Major)
        {
            List<ComCode> model = new List<ComCode>();

            try
            {
                model = ComCodeList
                            .Where(w => w.LARG_DIVS.Equals(Major))
                            .ToList();
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }


        /// <summary>
        /// 공통코드 검색
        /// </summary>
        /// <param name="SearchCode">코드들</param>
        /// <param name="mnc">코드컬럼선택 (L:Major Code, S:Minor Code, C:Code)</param>
        /// <returns></returns>
        public static List<ComCode> GetComCodeList(string[] SearchCode, string mnc)
        {
            List<ComCode> model = new List<ComCode>();

            try
            {
                switch (mnc)
                {
                    case "L":   // Major Code 검색
                        foreach (string code in SearchCode)
                        {
                            model.AddRange(Codes.ComCodeList.Where(w => w.LARG_DIVS.Equals(code)).ToList());
                        }
                        break;
                    case "S":   // Minor Code 검색
                        foreach (string code in SearchCode)
                        {
                            model.AddRange(Codes.ComCodeList.Where(w => w.SMLL_DIVS.Equals(code)).ToList());
                        }
                        break;
                    case "C":   // Code 검색
                        foreach (string code in SearchCode)
                        {
                            model.AddRange(Codes.ComCodeList.Where(w => w.CODE.Equals(code)).ToList());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }
    }

    public class ComCode : tblCodeMaster
    {
        /// <summary>
        /// 캐쉬 생성 시간
        /// </summary>
        public long CacheUpdate { get; set; }
    }
}
