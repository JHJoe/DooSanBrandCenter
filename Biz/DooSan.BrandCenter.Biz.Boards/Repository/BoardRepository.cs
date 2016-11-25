using DooSan.BrandCenter.Biz.Boards.Models;
using DooSan.BrandCenter.BrandCenterDBConext;
using DooSan.BrandCenter.FrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Repository
{
    public class BoardRepository : IBoardRepository
    {
        public BoardRepository()
        {
            if (Codes.ComCodeList.Count.Equals(0)) Codes.ComCodeList = ComCodeList();
        }

        #region ###     공통코드       ###

        /// <summary>
        /// 공통코드 - 캐쉬용
        /// </summary>
        /// <returns></returns>
        private List<ComCode> ComCodeList()
        {
            List<ComCode> model = new List<ComCode>();

            try
            {
                model = ComCodeRepository.GetComCode("ComCode");
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }

        #endregion

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
        /// 게시판 카테고리 목록
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<BoardCategory> GetBoardCategoryList(BoardCategoryParam param)
        {
            List<BoardCategory> model = new List<BoardCategory>();
            
            try
            {
                using (var wwEntities = new BrandCenterEntities2())
                {
                    var _model = wwEntities.SP_ADM_BOARD_CATEGORY_LIST();
                    if (_model != null || !_model.Count().Equals(0))
                    {
                        model = _model.Select(s => new BoardCategory
                        {
                            CATEGORY_IDX = s.CATEGORY_IDX,
                            TYPE = s.TYPE,
                            BOARD_NAME = s.BOARD_NAME,
                            COMMENT_YN = s.COMMENT_YN,
                            MOVIE_YN = s.MOVIE_YN,
                            USE_YN = s.USE_YN,
                            BOARD_ORDER = s.BOARD_ORDER,
                            INPUT_USER = s.INPUT_USER,
                            INPUT_DATE = s.INPUT_DATE,
                            UPDATE_USER = s.UPDATE_USER,
                            UPDATE_DATE = s.UPDATE_DATE
                        })
                        .ToList();
                    }
                }
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
        /// 게시판 카테고리 상세
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BoardCategory GetBoardCategory(BoardCategoryParam param)
        {
            BoardCategory model = new BoardCategory();

            try
            {
                using (var wwEntities = new BrandCenterEntities2())
                {
                    var _model = wwEntities.SP_ADM_BOARD_CATEGORY_DETAIL(param.CATEGORY_IDX);
                    if (_model != null || !_model.Count().Equals(0))
                    {
                        model = _model.Select(s => new BoardCategory
                        {
                            CATEGORY_IDX = s.CATEGORY_IDX,
                            TYPE = s.TYPE,
                            BOARD_NAME = s.BOARD_NAME,
                            COMMENT_YN = s.COMMENT_YN,
                            MOVIE_YN = s.MOVIE_YN,
                            USE_YN = s.USE_YN,
                            BS_YN = s.BS_YN,
                            WRITE_YN = s.WRITE_YN,
                            BOARD_ORDER = s.BOARD_ORDER,
                            INPUT_USER = s.INPUT_USER,
                            INPUT_DATE = s.INPUT_DATE,
                            UPDATE_USER = s.UPDATE_USER,
                            UPDATE_DATE = s.UPDATE_DATE
                        })
                        .FirstOrDefault();
                    }
                }
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
        /// 게시판 카테고리 등록
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CRUDReturn SetBoardCategoryReg(BoardCategoryParam param)
        {
            CRUDReturn model = new CRUDReturn();

            try
            {
                using (var wwEntities = new BrandCenterEntities2())
                {
                    var _model = wwEntities.SP_ADM_BOARD_CATEGORY_REG(param.TYPE, param.BOARD_NAME, param.COMMENT_YN, param.MOVIE_YN, param.USE_YN, param.BS_YN, param.WRITE_YN, param.BOARD_ORDER, param.INPUT_USER);
                    if (_model != null || !_model.Count().Equals(0))
                    {
                        model = _model.Select(s => new CRUDReturn
                        {
                            RTN = s.RTN,
                            NUMBER = s.ERR_NUMBER,
                            MESSAGE = s.ERR_MESSAGE,
                            SERVERITY = s.ERR_SERVERITY,
                            STATE = s.ERR_STATE,
                            LINE = s.ERR_LINE,
                            PROCEDURE = s.ERR_PROCEDURE
                        })
                        .FirstOrDefault();
                    }
                }
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
        /// 게시판 카테고리 수정
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public CRUDReturn SetBoardCategoryEdit(BoardCategoryParam param)
        {
            CRUDReturn model = new CRUDReturn();

            try
            {
                using (var wwEntities = new BrandCenterEntities2())
                {
                    var _model = wwEntities.SP_ADM_BOARD_CATEGORY_EDIT(param.CATEGORY_IDX, param.TYPE, param.BOARD_NAME, param.COMMENT_YN, param.MOVIE_YN, param.USE_YN, param.BS_YN, param.WRITE_YN, param.BOARD_ORDER, param.UPDATE_USER);
                    if (_model != null || !_model.Count().Equals(0))
                    {
                        model = _model.Select(s => new CRUDReturn
                        {
                            RTN = s.RTN,
                            NUMBER = s.ERR_NUMBER,
                            MESSAGE = s.ERR_MESSAGE,
                            SERVERITY = s.ERR_SERVERITY,
                            STATE = s.ERR_STATE,
                            LINE = s.ERR_LINE,
                            PROCEDURE = s.ERR_PROCEDURE
                        })
                        .FirstOrDefault();
                    }
                }
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
                using (var wwEntities = new BrandCenterEntities2())
                {
                    var _model = wwEntities.SP_ADM_BOARD_CATEGORY_DEL(param.CATEGORY_IDX);
                    if (_model != null || !_model.Count().Equals(0))
                    {
                        model = _model.Select(s => new CRUDReturn
                        {
                            RTN = s.RTN,
                            NUMBER = s.ERR_NUMBER,
                            MESSAGE = s.ERR_MESSAGE,
                            SERVERITY = s.ERR_SERVERITY,
                            STATE = s.ERR_STATE,
                            LINE = s.ERR_LINE,
                            PROCEDURE = s.ERR_PROCEDURE
                        })
                        .FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return model;
        }

        #endregion

        #endregion

    }
}
