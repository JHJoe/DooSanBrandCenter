using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DooSan.BrandCenter.FrameWork.Static
{
    #region CMS 멤버

    public class SessionClass
    {
        //모든 세션 컬럼이 채워지는건 아니고 경우 따라 채워지지 않는 경우도 있다.

        //public LoginType LoginType; //인증 구분. REFILL시 못채움

        public string LoginID; //이게 키- AD키, 매장키
        //주로 사용하는 코드
        public string DisplayName;
        public string EmpID; //사번,  
        public string USERNAME;
        public LangType Language; //언어설정: kr or en or zh
        public string EMail;      
        //public string MOBILE;
        //public string TEL;

        //굳이 필요한지?
        //        public string epOriginalEncript;  //ep에서 전달받은 원본 키
        //        public ShopType SHOP_TYPE;  //중국, 국내 kr zh  - ep 매장

        //        public BrandClass BrandClassType;  //브랜드별 영역

        public string GROUP_ID;
        //public string GROUPNAME; //그룹 이름(권한설정시 그룹)

        public bool ADMIN;  //systemadmin.
//        public bool IsStore;  //매장인지 아닌지

  //      public string formid;

        //public IEnumerable<TEAM> AUTH_TEAMS;
        //public Dictionary<string, string> AUTH_BRANDS;
    }

    #endregion
    
}
