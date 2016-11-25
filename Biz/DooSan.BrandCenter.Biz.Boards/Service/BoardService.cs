using DooSan.BrandCenter.Biz.Boards.Models;
using DooSan.BrandCenter.Biz.Boards.Repository;
using DooSan.BrandCenter.FrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Service
{
    public class BoardService
    {
        private static IBoardRepository _repository;

        public BoardService(IBoardRepository reposiroty)
        {
            _repository = reposiroty;
        }

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
        public VM_BoardCategoryList GetBoardCategoryList(BoardCategoryParam param)
        {
            VM_BoardCategoryList model = new VM_BoardCategoryList();

            try
            {
                model.BoardCategoryList = _repository.GetBoardCategoryList(param);
                if(!model.BoardCategoryList.Count.Equals(0))
                {
                    model.BoardCategoryList.ForEach(f => f.TYPE_NAME = Codes.GetComCodeName(f.TYPE, true));
                }

                string[] arrSearchCodes = { "06", "08" };
                model.ComCodes.AddRange(Codes.GetComCodeList(arrSearchCodes, "L"));

                model.param = param;
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }

        #endregion

        #region ###     상세       ###

        /// <summary>
        /// 상세
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public VM_BoardCategory GetBoardCategory(BoardCategoryParam param)
        {
            VM_BoardCategory model = new VM_BoardCategory();

            try
            {
                model.BoardCategory = _repository.GetBoardCategory(param);

                string[] arrSearchCodes = { "06", "08", "18" };
                model.ComCodes.AddRange(Codes.GetComCodeList(arrSearchCodes, "L"));

                model.param = param;
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }

        #endregion

        #region ###     등록       ###

        /// <summary>
        /// 등록
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CRUDReturn SetBoardCategoryReg(BoardCategoryParam param)
        {
            CRUDReturn model = new CRUDReturn();

            try
            {
                model = _repository.SetBoardCategoryReg(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }

        #endregion

        #region ###     수정       ###

        /// <summary>
        /// 수정
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CRUDReturn SetBoardCategoryEdit(BoardCategoryParam param)
        {
            CRUDReturn model = new CRUDReturn();

            try
            {
                model = _repository.SetBoardCategoryEdit(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }

        #endregion

        #region ###     삭제       ###

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CRUDReturn SetBoardCategoryDel(BoardCategoryParam param)
        {
            CRUDReturn model = new CRUDReturn();

            try
            {
                model = _repository.SetBoardCategoryDel(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }

        #endregion

        #endregion

        #endregion

        #region ###     사용자       ###

        #endregion
    }
}
