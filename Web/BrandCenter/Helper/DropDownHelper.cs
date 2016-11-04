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
        public static SelectList GetCodeMasterDropDownList(DefaultContext db, string largeDiv, object selected = null)
        {
            var countryQuery = from c in db.tblCodeMaster
                               where c.LARG_DIVS == largeDiv
                               orderby c.SMLL_DIVS, c.SMLL_DIVS
                               select c;

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