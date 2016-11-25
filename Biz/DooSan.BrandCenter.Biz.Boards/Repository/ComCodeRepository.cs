using DooSan.BrandCenter.Biz.Boards.Cache;
using DooSan.BrandCenter.Biz.Boards.Models;
using DooSan.BrandCenter.BrandCenterDBConext;
using DooSan.BrandCenter.FrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Repository
{
    public class ComCodeRepository
    {
        public static AppCache Cache = new AppCache();

        /// <summary>
        /// 공통코드
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static List<ComCode> GetComCode(string cacheKey)
        {
            List<ComCode> model = new List<ComCode>();

            try
            {
                int casheHour = 24;
                int cacheMin = 60;
                int cacheSec = 60;
                int cacheTime = casheHour * cacheMin * cacheSec;
                var _cacheModel = Cache.Get(cacheKey) as List<ComCode>;

                if (_cacheModel != null)
                {
                    if (_cacheModel.Count.Equals(0))
                    {
                        long nowSecound = DooSan.BrandCenter.Biz.Boards.Methods.Method.ConvertToUnixTime(DateTime.Now.AddSeconds(cacheTime - (cacheTime * 2)));

                        if (_cacheModel.FirstOrDefault().CacheUpdate.CompareTo(nowSecound) < 0)
                        {
                            var _model = GetComCode();

                            if (!_model.Count.Equals(0))
                            {
                                // 기존 캐쉬 제거
                                Cache.Invalidate(cacheKey);
                                Cache.Set(cacheKey, _model, cacheMin * cacheSec);
                            }
                        }
                    }
                }
                else
                {
                    var _model = GetComCode();

                    if (!_model.Count.Equals(0))
                    {
                        Cache.Invalidate(cacheKey);
                        Cache.Set(cacheKey, _model, cacheMin * cacheSec);
                    }
                }

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            model = Cache.Get(cacheKey) as List<ComCode>;
            if (model == null) model = new List<ComCode>();

            return model;
        }

        public static List<ComCode> GetComCode()
        {
            List<ComCode> model = new List<ComCode>();

            try
            {
                using (var wwEntities = new BrandCenterEntities2())
                {
                    model = wwEntities.tblCodeMaster
                                                .Select(s => new ComCode
                                                {
                                                    LARG_DIVS = s.LARG_DIVS,
                                                    SMLL_DIVS = s.SMLL_DIVS,
                                                    LANGCODE = s.LANGCODE,
                                                    NAME = s.NAME,
                                                    NOTE = s.NOTE,
                                                    MOBILE_YN = s.MOBILE_YN,
                                                    SORTORDER = s.SORTORDER,
                                                    INPUT_USER = s.INPUT_USER,
                                                    INPUT_DATE = s.INPUT_DATE,
                                                    UPDATE_USER = s.UPDATE_USER,
                                                    UPDATE_DATE = s.UPDATE_DATE,
                                                    JOBID = s.JOBID,
                                                    CODE = s.CODE
                                                })
                                                .ToList<ComCode>();

                    if (model != null && !model.Count.Equals(0))
                    {
                        model.ForEach(f => f.CacheUpdate = DooSan.BrandCenter.Biz.Boards.Methods.Method.ConvertToUnixTime());
                    }
                }
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }
    }
}
