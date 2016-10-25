using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrandCenter.Models
{
    //[NotMapped]
    [Table("tblCodeMaster")]
    //헷갈리지 말라고 TBL을 통째로 뺀거고 실제로 이런 상황이 생긴다면 edm 혹은 tbi 를 붙여서 만든다.
    public class CodeMaster : DooSan.BrandCenter.BrandCenterDBConext.tblCodeMaster
    {
        [Column(Order = 0), System.ComponentModel.DataAnnotations.Key]
        public new string LARG_DIVS { get; set; }
        [Column(Order = 1), System.ComponentModel.DataAnnotations.Key]
        public new string SMLL_DIVS { get; set; }

    }
}