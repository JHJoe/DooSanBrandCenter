using BrandCenter.DAL;
using BrandCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandCenter.Helper
{
    public class DropDownHelper
    {
        //public static SelectList GetCodeMasterDropDownList(DefaultContext db, string largeDiv, object selected = null)
        //{
        //    var countryQuery = from c in db.tblCodeMaster
        //                       where c.LARG_DIVS == largeDiv
        //                       orderby c.SORTORDER, c.SMLL_DIVS
        //                       select c;

        //    return new SelectList(countryQuery, "SMLL_DIVS", "NAME", selected);
        //}

        public static SelectList GetBlankDropDownList(string FirstSelectText = null)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(FirstSelectText))
                dic.Add("", FirstSelectText);
            else
                dic.Add("", Message.GetMessage("SelectText"));
            return new SelectList(dic, "Key", "Value");

        }

        public static SelectList GetCodeMasterDropDownList(DefaultContext db, string MasterCode, object selected = null, string FirstSelectText = null, string lancode = "kr")
        {
            var countryQuery = from c in db.tblCodeMaster
                              .Where(s => s.LARG_DIVS.Equals(MasterCode) && s.LANGCODE.Equals(lancode))
                               orderby c.SORTORDER, c.SMLL_DIVS
                               select c;

            if (!string.IsNullOrEmpty(FirstSelectText))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("", FirstSelectText);
                dic = dic.Concat(countryQuery.ToDictionary(o => o.SMLL_DIVS, o => o.NAME)).ToDictionary(x => x.Key, x => x.Value);
                return new SelectList(dic, "Key", "Value", selected);
            }
            else
                return new SelectList(countryQuery, "SMLL_DIVS", "NAME", selected);
        }


        public static SelectList GetGroupDropDownList(DefaultContext db, object selected = null)
        {
            var countryQuery = from c in db.tblGroup
                               orderby c.GroupId
                               select c;

            return new SelectList(countryQuery, "GroupId", "Name", selected);
        }


    }
}