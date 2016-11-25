using DooSan.BrandCenter.BrandCenterDBConext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Models
{
    /// <summary>
    /// 목록
    /// </summary>
    public class VM_BoardCategoryList
    {
        public VM_BoardCategoryList()
        {
            this.param = new BoardCategoryParam();
            this.ComCodes = new List<ComCode>();
            this.BoardCategoryList = new List<BoardCategory>();
        }

        public BoardCategoryParam param { get; set; }
        public List<ComCode> ComCodes { get; set; }
        public List<BoardCategory> BoardCategoryList { get; set; }
    }

    /// <summary>
    /// 상세
    /// </summary>
    public class VM_BoardCategory
    {
        public VM_BoardCategory()
        {
            this.param = new BoardCategoryParam();
            this.ComCodes = new List<ComCode>();
            this.BoardCategory = new BoardCategory();
        }

        public BoardCategoryParam param { get; set; }
        public List<ComCode> ComCodes { get; set; }
        public BoardCategory BoardCategory { get; set; }
    }

    public class BoardCategory : TBL_BOARD_CATEGORY
    {
        /// <summary>
        /// 게시판 타입 코드값
        /// </summary>
        public string TYPE_NAME { get; set; }
    }


}
