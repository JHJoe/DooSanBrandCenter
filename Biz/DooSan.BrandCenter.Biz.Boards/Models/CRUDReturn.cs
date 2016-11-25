using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Models
{
    public class CRUDReturn
    {
        /// <summary>
        /// 성공여부 (Y/N)
        /// </summary>
        public string RTN { get; set; }
        /// <summary>
        /// 에러번호
        /// </summary>
        public int? NUMBER { get; set; }
        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string MESSAGE { get; set; }
        /// <summary>
        /// 에러 심각도
        /// </summary>
        public int? SERVERITY { get; set; }
        /// <summary>
        /// 에러 상태
        /// </summary>
        public int? STATE { get; set; }
        /// <summary>
        /// 에러 발생 행번호
        /// </summary>
        public int? LINE { get; set; }
        /// <summary>
        /// 에러 발생 프로시저명
        /// </summary>
        public string PROCEDURE { get; set; }
    }
}
