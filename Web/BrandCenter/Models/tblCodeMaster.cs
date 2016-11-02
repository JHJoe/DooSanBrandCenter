using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrandCenter.Models
{
    /*  참조를 제거해서 통째로 주석처리. 상속형 구조
        [Table("tblCodeMaster")]
        //헷갈리지 말라고 TBL을 통째로 뺀거고 실제로 이런 상황이 생긴다면 edm 혹은 tbi 를 붙여서 만든다.
        public class CodeMaster : DooSan.BrandCenter.BrandCenterDBConext.tblCodeMaster
        {
            [Column(Order = 0), System.ComponentModel.DataAnnotations.Key]
            public new string LARG_DIVS { get; set; }
            [Column(Order = 1), System.ComponentModel.DataAnnotations.Key]
            public new string SMLL_DIVS { get; set; }

        }
    */
    [Table("tblCodeMaster")]
    //헷갈리지 말라고 TBL을 통째로 뺀거고 실제로 이런 상황이 생긴다면 edm 혹은 tbi 를 붙여서 만든다.
    public class CodeMaster
    {
        [Column(Order = 0), System.ComponentModel.DataAnnotations.Key]
        public string LARG_DIVS { get; set; }
        [Column(Order = 1), System.ComponentModel.DataAnnotations.Key]
        public string SMLL_DIVS { get; set; }

        public string NAME { get; set; }
        public string NOTE { get; set; }
        public Nullable<bool> MOBILE_YN { get; set; }

        public string SORTORDER { get; set; }
        
        public string INPUT_USER { get; set; }
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }

    }

}