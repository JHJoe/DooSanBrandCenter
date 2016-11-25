using DooSan.BrandCenter.Biz.Boards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Repository
{
    public interface IBoardRepository
    {
        #region ###     관리자       ###

        #region ###     게시판관리       ###

        #region ###     목록화면       ###

        #endregion

        #region ###     등록화면       ###

        #endregion

        #region ###     상세       ###

        #endregion

        #region ###     수정화면       ###

        #endregion

        #endregion

        #region ###     게시판 카테고리 관리       ###

        #region ###     목록       ###

        /// <summary>
        /// 목록
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<BoardCategory> GetBoardCategoryList(BoardCategoryParam param);

        #endregion

        #region ###     상세       ###

        /// <summary>
        /// 상세
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BoardCategory GetBoardCategory(BoardCategoryParam param);

        #endregion

        #region ###     등록       ###

        /// <summary>
        /// 등록
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        CRUDReturn SetBoardCategoryReg(BoardCategoryParam param);

        #endregion

        #region ###     수정       ###

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        CRUDReturn SetBoardCategoryEdit(BoardCategoryParam param);

        #endregion

        #region ###     삭제       ###

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        CRUDReturn SetBoardCategoryDel(BoardCategoryParam param);

        #endregion

        #endregion

        #endregion

        #region ###     사용자       ###

        #endregion
    }
}
