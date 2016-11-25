using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using DooSan.BrandCenter.Biz.Boards.Models;
using DooSan.BrandCenter.FrameWork.Utils;
using DooSan.BrandCenter.Biz.Boards.Service;
using DooSan.BrandCenter.Biz.Boards.Repository;

namespace BrandCenter.Controllers
{
    public partial class AdminController : Base.BaseController
    {
        public BoardCategoryParam param;
        public BoardService service;

        #region ###     게시판 카테고리 관리       ###

        #region ###     목록       ###

        public ViewResult BoardCategoryList()
        {
            VM_BoardCategoryList model = new VM_BoardCategoryList();
            param = new BoardCategoryParam();
            
            try
            {
                service = new BoardService(new BoardRepository());
                model = service.GetBoardCategoryList(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View(model);
        }

        #endregion

        #region ###     상세       ###

        public ViewResult BoardCategoryView(int? id)
        {
            VM_BoardCategory model = new VM_BoardCategory();

            param = new BoardCategoryParam();
            param.CATEGORY_IDX = id == null ? 0 : id.Value;

            try
            {
                service = new BoardService(new BoardRepository());
                model = service.GetBoardCategory(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View(model);
        }

        #endregion

        #region ###     등록       ###

        public ViewResult BoardCategoryReg(int? id)
        {
            VM_BoardCategory model = new VM_BoardCategory();

            param = new BoardCategoryParam();
            param.CATEGORY_IDX = id == null ? 0 : id.Value;

            try
            {
                service = new BoardService(new BoardRepository());
                model = service.GetBoardCategory(param);
                //ViewBag.ComCodes = model.ComCodes;
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult BoardCategoryReg(BoardCategoryParam _param)
        {
            if (_param != null) param = _param; else param = new BoardCategoryParam();
            CRUDReturn rtnModel = new CRUDReturn();

            //TODO : 사용자변경!!!
            param.INPUT_USER = "테스트유저";

            try
            {
                service = new BoardService(new BoardRepository());
                rtnModel = service.SetBoardCategoryReg(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return Json(rtnModel);
        }

        #endregion

        #region ###     수정       ###

        public ViewResult BoardCategoryEdit(int? id)
        {
            VM_BoardCategory model = new VM_BoardCategory();

            param = new BoardCategoryParam();
            param.CATEGORY_IDX = id == null ? 0 : id.Value;

            try
            {
                service = new BoardService(new BoardRepository());
                model = service.GetBoardCategory(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult BoardCategoryEdit(BoardCategoryParam _param)
        {
            if (_param != null) param = _param; else param = new BoardCategoryParam();
            CRUDReturn rtnModel = new CRUDReturn();

            //TODO : 사용자변경!!!
            param.UPDATE_USER = "테스트유저";

            try
            {
                service = new BoardService(new BoardRepository());
                rtnModel = service.SetBoardCategoryEdit(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return Json(rtnModel);
        }

        #endregion

        #region ###     삭제       ###

        [HttpPost]
        public JsonResult BoardCategoryDel(BoardCategoryParam _param)
        {
            if (_param != null) param = _param; else param = new BoardCategoryParam();
            CRUDReturn rtnModel = new CRUDReturn();

            //TODO : 사용자변경!!!
            param.UPDATE_USER = "테스트유저";

            try
            {
                service = new BoardService(new BoardRepository());
                rtnModel = service.SetBoardCategoryDel(param);
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return Json(rtnModel);
        }

        #endregion

        #endregion


    }
}