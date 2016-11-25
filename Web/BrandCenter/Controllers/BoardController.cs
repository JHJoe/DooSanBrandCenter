using DooSan.BrandCenter.Biz.Boards.Models;
using DooSan.BrandCenter.Biz.Boards.Service;
using DooSan.BrandCenter.FrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BrandCenter.Controllers
{
    public class BoardController : Base.BaseController
    {
        public BoardParam param;
        public BoardService service;

        // GET: Board
        public ActionResult Index()
        {
            return RedirectToAction("BrandStoryList");
        }

        #region ###     메뉴들       ###

        #region ###     Brand Story       ###

        #region ###     목록       ###

        public ActionResult BrandStoryList
        (
            int pn = 1,
            int ps = 10
        )
        {
            param = new BoardParam();
            param.PN = pn;
            param.PS = ps;
            param.IS_BRANDSTORY = true;
            param.MenuCode = "1701kr";

            return BoardList(param);
        }

        #endregion

        #region ###     등록       ###

        public ActionResult BrandStoryReg()
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = true;
            param.MenuCode = "1701kr";
            
            return BoardReg(param);
        }

        #endregion

        #region ###     수정       ###

        public ActionResult BrandStoryEdit(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = true;
            param.MenuCode = "1701kr";
            
            return BoardEdit(param);
        }

        #endregion

        #region ###     상세       ###

        public ActionResult BrandStoryView(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = true;
            param.MenuCode = "1701kr";
            
            return BoardView(param);
        }

        #endregion

        #endregion

        #region ###     두산 브랜드 새소식       ###

        #region ###     목록       ###

        public ActionResult BrandNewsList
        (
            int pn = 1,
            int ps = 10
        )
        {
            param = new BoardParam();
            param.PN = pn;
            param.PS = ps;
            param.IS_BRANDSTORY = false;
            param.MenuCode = "1702kr";

            return BoardList(param);
        }

        #endregion

        #region ###     등록       ###

        public ActionResult BrandNewsReg()
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1702kr";

            return BoardReg(param);
        }

        #endregion

        #region ###     수정       ###

        public ActionResult BrandNewsEdit(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1702kr";

            return BoardEdit(param);
        }

        #endregion

        #region ###     상세       ###

        public ActionResult BrandNewsView(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1702kr";

            return BoardView(param);
        }

        #endregion

        #endregion

        #region ###     자주하는 질문       ###

        #region ###     목록       ###

        public ActionResult FAQList
        (
            int pn = 1,
            int ps = 10
        )
        {
            param = new BoardParam();
            param.PN = pn;
            param.PS = ps;
            param.IS_BRANDSTORY = false;
            param.MenuCode = "1703kr";

            return BoardList(param);
        }

        #endregion

        #region ###     등록       ###

        public ActionResult FAQReg()
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1703kr";

            return BoardReg(param);
        }

        #endregion

        #region ###     수정       ###

        public ActionResult FAQEdit(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1703kr";

            return BoardEdit(param);
        }

        #endregion

        #region ###     상세       ###

        public ActionResult FAQView(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1703kr";

            return BoardView(param);
        }

        #endregion

        #endregion

        #region ###     문의하기       ###

        #region ###     목록       ###

        public ActionResult QNAList
        (
            int pn = 1,
            int ps = 10
        )
        {
            param = new BoardParam();
            param.PN = pn;
            param.PS = ps;
            param.IS_BRANDSTORY = false;
            param.MenuCode = "1704kr";

            return BoardList(param);
        }

        #endregion

        #region ###     등록       ###

        public ActionResult QNAReg()
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1704kr";

            return BoardReg(param);
        }

        #endregion

        #region ###     수정       ###

        public ActionResult QNAEdit(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1704kr";

            return BoardEdit(param);
        }

        #endregion

        #region ###     상세       ###

        public ActionResult QNAView(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1704kr";

            return BoardView(param);
        }

        #endregion

        #endregion

        #region ###     CI바로다운로드       ###

        #region ###     목록       ###

        public ActionResult CIDownloadList
        (
            int pn = 1,
            int ps = 10
        )
        {
            param = new BoardParam();
            param.PN = pn;
            param.PS = ps;
            param.IS_BRANDSTORY = false;
            param.MenuCode = "1705kr";

            return BoardList(param);
        }

        #endregion

        #region ###     등록       ###

        public ActionResult CIDownloadReg()
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1705kr";

            return BoardReg(param);
        }

        #endregion

        #region ###     수정       ###

        public ActionResult CIDownloadEdit(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1705kr";

            return BoardEdit(param);
        }

        #endregion

        #region ###     상세       ###

        public ActionResult CIDownloadView(int? id)
        {
            param = new BoardParam();

            param.IS_BRANDSTORY = false;
            param.MenuCode = "1705kr";

            return BoardView(param);
        }

        #endregion

        #endregion

        #endregion

        #region ###     게시판       ###

        #region ###     목록화면       ###

        public ActionResult BoardList(BoardParam _param)
        {
            param = _param == null ? new BoardParam() : _param;

            string BaordType = "0601kr";

            try
            {


                //  쿼리된값으로 BaordType 정리
                //  BaordType
            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }
            
            if (BaordType.Equals("0601kr"))
                return View("BoardList");           //  목록형
            else
                return View("ThumbBoardList");  //  섬네일형

            return View();
        }

        #endregion

        #region ###     등록화면       ###

        public ActionResult BoardReg(BoardParam _param)
        {
            param = _param == null ? new BoardParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View();
        }

        #endregion

        #region ###     상세       ###

        public ActionResult BoardView(BoardParam _param)
        {
            param = _param == null ? new BoardParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View();
        }

        #endregion

        #region ###     수정화면       ###

        public ActionResult BoardEdit(BoardParam _param)
        {
            param = _param == null ? new BoardParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View();
        }

        #endregion

        #region ###     조횟수업데이트       ###

        [HttpPost]
        public JsonResult BoardViewCnt(BoardParam _param)
        {
            param = _param == null ? new BoardParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            //TODO : 리턴값에 최종 조횟수 포함
            return Json(new { });
        }
        
        #endregion


        #region ###     덧글       ###

        #region ###     등록       ###

        [HttpPost]
        public JsonResult BoardCommentReg(BoardCommentParam _param)
        {
            BoardCommentParam param = _param == null ? new BoardCommentParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return Json(new { });
        }

        #endregion

        #region ###     수정       ###

        [HttpPost]
        public JsonResult BoardCommentEdit(BoardCommentParam _param)
        {
            BoardCommentParam param = _param == null ? new BoardCommentParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return Json(new { });
        }

        #endregion

        #region ###     삭제       ###

        [HttpPost]
        public JsonResult BoardCommentDel(BoardCommentParam _param)
        {
            BoardCommentParam param = _param == null ? new BoardCommentParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return Json(new { });
        }

        #endregion

        #region ###     목록       ###

        [ChildActionOnly]
        public ActionResult BoardCommentList(BoardCommentParam _param)
        {
            BoardCommentParam param = _param == null ? new BoardCommentParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            return View();
        }

        #endregion

        #endregion


        #region ###     좋아요       ###

        [HttpPost]
        public JsonResult Like(LikeParam _param)
        {
            LikeParam param = _param == null ? new LikeParam() : _param;

            try
            {

            }
            catch (Exception ex)
            {
                new AppException(LayerTypes.Business, ex.Message, ex);
            }

            //TODO : 리턴값에 최종 좋아요 수 포함
            return Json(new { });
        }
        
        #endregion


        #endregion
    }
}