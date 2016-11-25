using BrandCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandCenter.DAL;

namespace BrandCenter.Helper
{
    public class CommonHelper
    {

        public static Dictionary<string, string> GetCodeDetailsToDictionary(DefaultContext db, string MasterCode, string lancode = "kr")
        {
            var details = from s in db.tblCodeMaster
                          .Where(s => s.LARG_DIVS.Equals(MasterCode) && s.LANGCODE.Equals(lancode))
                          orderby s.SORTORDER, s.SMLL_DIVS
                          select s;

            return details.ToDictionary(o => o.SMLL_DIVS, o => o.NAME);
        }

        public static List<CodeItem> GetCodeDetailsList(DefaultContext db, string MasterCode, string lancode = "kr")
        {
            var details = from s in db.tblCodeMaster
                          .Where(s => s.LARG_DIVS.Equals(MasterCode) && s.LANGCODE.Equals(lancode))
                          orderby s.SORTORDER, s.SMLL_DIVS
                          select 
                          new CodeItem
                          {
                              SMLL_DIVS = s.SMLL_DIVS, NAME = s.NAME, LANGCODE = s.LANGCODE, NOTE = s.NOTE
                          };

            return details.ToList();
        }

    }
}