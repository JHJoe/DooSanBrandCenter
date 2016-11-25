using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.FrameWork.Static
{
    /// <summary>언어 타입</summary>
    public enum LangType
    {
        ko = 0,
        en = 1,
        zh = 2,
        bl = -1 //blank. db에 기본 언어가 있는데 상단에서 바꿀때를 위해
    }

    /// <summary>매장국가 타입</summary>
    public enum ShopType
    {
        kr = 0,
        zh = 2
    }

    /// <summary>인증 구분</summary>
    public enum LoginType
    {
        Single = 0,
        EP = 1,
        Agency = 2
    }


    public enum ExportType
    {
        XLS = 0,
        PDF = 1
        //CSV = 4,
        //RTF = 8
    }
    public enum Thumbtype
    {
        Home,
        List,
        Symbol
    }


    public enum EditType
    {
        Create,
        Update,
        View
    }



    //    public enum RoleGroupType
    //    {
    //        Admin = 1,
    //        User = 2,
    //        자회사CI담당 = 3,
    //그룹사CI담당 =         4,
    //제작업체담당 =          5,
    //제작업체    =           6,
    //디자인담당   =          7
    //    }




}
