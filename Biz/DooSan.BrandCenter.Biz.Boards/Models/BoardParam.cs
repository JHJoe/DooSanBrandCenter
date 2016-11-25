using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DooSan.BrandCenter.Biz.Boards.Models
{

    #region ###     게시판 공통       ###

    /// <summary>
    /// 게시판 공통
    /// </summary>
    public class BoardBase
    {
        private int? _PAGE_NO = 1;
        private int? _PAGE_SIZE = 10;
        private bool? _isBoard = true;
        private bool? _isBrandStory = false;
        private string _menuCode = "";

        /// <summary>
        /// 페이지 번호
        /// </summary>
        public int PN
        {
            get { return this._PAGE_NO.Value; }
            set { this._PAGE_NO = _PAGE_NO == null ? 1 : value; }
        }

        /// <summary>
        /// 페이징 단위
        /// </summary>
        public int PS
        {
            get { return this._PAGE_SIZE.Value; }
            set { this._PAGE_SIZE = _PAGE_SIZE == null ? 10 : value; }
        }
        /// <summary>
        /// 게시판여부 - 파일등록시
        /// </summary>
        public bool IS_BOARD
        {
            get { return this._isBoard.Value; }
            set { this._isBoard = _isBoard == null ? true : value; }
        }
        /// <summary>
        /// 브랜드스토리 메뉴구분 - 메인등록 기능 활성화용
        /// </summary>
        public bool IS_BRANDSTORY
        {
            get { return this._isBrandStory.Value; }
            set { this._isBrandStory = _isBrandStory == null ? false : value; }
        }
        /// <summary>
        /// 메뉴코드
        /// </summary>
        public string MenuCode
        {
            get { return this._menuCode; }
            set { this._menuCode = string.IsNullOrEmpty(value) ? "" : value; }
        }
    }

    #endregion

    #region ###     게시판       ###

    /// <summary>
    /// 게시판
    /// </summary>
    public class BoardParam : BoardBase
    {
        private int? _BOARD_IDX = 0;
        private string _TITLE = "";
        private string _CONTENT = "";
        private string _NOTICE_ATTR = "";
        private string _NOTICE_DURATION = "";
        private string _FILE_YN = "";
        private string _MAIN_YN = "";

        private string _SORT_DATE = "";
        private string _SORT_LIKE = "";
        private string _SORT_CNT = "";

        private string _INPUT_USER = "";
        private string _INPUT_DATE = "";
        private string _UPDATE_USER = "";
        private string _UPDATE_DATE = "";
        private int? _CATEGORY_IDX = 0;
        
        /// <summary>
        /// 게시판 일련번호
        /// </summary>
        public int BOARD_IDX
        {
            get { return this._BOARD_IDX.Value; }
            set { this._BOARD_IDX = _BOARD_IDX == null ? 0 : value; }
        }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE
        {
            get { return this._TITLE; }
            set { this._TITLE = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 내용
        /// </summary>
        public string CONTENT
        {
            get { return this._CONTENT; }
            set { this._CONTENT = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 공지속성
        /// </summary>
        public string NOTICE_ATTR
        {
            get { return this._NOTICE_ATTR; }
            set { this._NOTICE_ATTR = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 공지기간
        /// </summary>
        public string NOTICE_DURATION
        {
            get { return this._NOTICE_DURATION; }
            set { this._NOTICE_DURATION = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 첨부파일여부
        /// </summary>
        public string FILE_YN
        {
            get { return this._FILE_YN; }
            set { this._FILE_YN = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 메인여부
        /// </summary>
        public string MAIN_YN
        {
            get { return this._MAIN_YN; }
            set { this._MAIN_YN = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 정렬 - 등록일
        /// </summary>
        public string SORT_DATE
        {
            get { return this._SORT_DATE; }
            set { this._SORT_DATE = string.IsNullOrEmpty(value) ? "D" : value; }
        }
        /// <summary>
        /// 정렬 - 좋아요
        /// </summary>
        public string SORT_LIKE
        {
            get { return this._SORT_LIKE; }
            set { this._SORT_LIKE = string.IsNullOrEmpty(value) ? "D" : value; }
        }
        /// <summary>
        /// 정렬 - 조회수
        /// </summary>
        public string SORT_CNT
        {
            get { return this._SORT_CNT; }
            set { this._SORT_CNT = string.IsNullOrEmpty(value) ? "D" : value; }
        }
        /// <summary>
        /// 등록자 정보
        /// </summary>
        public string INPUT_USER
        {
            get { return this._INPUT_USER; }
            set { this._INPUT_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 등록일
        /// </summary>
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        /// <summary>
        /// 수정자 정보
        /// </summary>
        public string UPDATE_USER
        {
            get { return this._UPDATE_USER; }
            set { this._UPDATE_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 수정일
        /// </summary>
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        /// <summary>
        /// 게시판 카테고리 일련번호
        /// </summary>
        public int CATEGORY_IDX
        {
            get { return this._CATEGORY_IDX.Value; }
            set { this._CATEGORY_IDX = _CATEGORY_IDX == null ? 0 : value; }
        }
    }

    #endregion

    #region ###     게시판 댓글       ###

    /// <summary>
    /// 게시판 댓글
    /// </summary>
    public class BoardCommentParam
    {
        private int? _BOARD_IDX = 0;

        private string _COMMENT = "";
        private string _COMMENT_DATE = "";
        private string _COMMENT_GROUP = "";
        private string _COMMENT_ORDER = "";

        private string _INPUT_USER = "";
        private string _INPUT_DATE = "";
        private string _UPDATE_USER = "";
        private string _UPDATE_DATE = "";

        /// <summary>
        /// 게시판 일련번호
        /// </summary>
        public int BOARD_IDX
        {
            get { return this._BOARD_IDX.Value; }
            set { this._BOARD_IDX = _BOARD_IDX == null ? 0 : value; }
        }
        /// <summary>
        /// 댓글-덧글
        /// </summary>
        public string COMMENT
        {
            get { return this._COMMENT; }
            set { this._COMMENT = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 등록일
        /// </summary>
        public string COMMENT_DATE
        {
            get { return this._COMMENT_DATE; }
            set { this._COMMENT_DATE = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 댓글-덧글 그룹번호
        /// </summary>
        public string COMMENT_GROUP
        {
            get { return this._COMMENT_GROUP; }
            set { this._COMMENT_GROUP = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 댓글-덧글 순서
        /// </summary>
        public string COMMENT_ORDER
        {
            get { return this._COMMENT_ORDER; }
            set { this._COMMENT_ORDER = string.IsNullOrEmpty(value) ? "" : value; }
        }

        /// <summary>
        /// 등록자 정보
        /// </summary>
        public string INPUT_USER
        {
            get { return this._INPUT_USER; }
            set { this._INPUT_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 등록일
        /// </summary>
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        /// <summary>
        /// 수정자 정보
        /// </summary>
        public string UPDATE_USER
        {
            get { return this._UPDATE_USER; }
            set { this._UPDATE_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 수정일
        /// </summary>
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }

    #endregion

    #region ###     게시판 관리       ###

    /// <summary>
    /// 게시판 관리
    /// </summary>
    public class BoardCategoryParam
    {
        private int? _CATEGORY_IDX = 0;
        private string _TYPE = "";
        private string _BOARD_NAME = "";
        private string _COMMENT_YN = "";
        private string _MOVIE_YN = "";
        private string _USE_YN = "";
        private string _BS_YN = "";
        private string _WRITE_YN = "";
        private int? _BOARD_ORDER = 0;
        private string _INPUT_USER = "";
        private string _INPUT_DATE = "";
        private string _UPDATE_USER = "";
        private string _UPDATE_DATE = "";

        /// <summary>
        /// 게시판 카테고리 일련번호
        /// </summary>
        public int CATEGORY_IDX
        {
            get { return this._CATEGORY_IDX.Value; }
            set { this._CATEGORY_IDX = _CATEGORY_IDX == null ? 0 : value; }
        }
        /// <summary>
        /// 타입(0601:섬네일/ 0602:목록)
        /// </summary>
        public string TYPE
        {
            get { return this._TYPE; }
            set { this._TYPE = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 게시판명
        /// </summary>
        [Display(Name = "게시판 명")]
        [Required(ErrorMessage = "게시판명은 20자 이내로 적어주세요.")]
        public string BOARD_NAME
        {
            get { return this._BOARD_NAME; }
            set { this._BOARD_NAME = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 댓글 사용여부
        /// </summary>
        public string COMMENT_YN
        {
            get { return this._COMMENT_YN; }
            set { this._COMMENT_YN = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 동영상링크 사용여부
        /// </summary>
        public string MOVIE_YN
        {
            get { return this._MOVIE_YN; }
            set { this._MOVIE_YN = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 게시판사용여부
        /// </summary>
        public string USE_YN
        {
            get { return this._USE_YN; }
            set { this._USE_YN = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 브랜드스토리여부
        /// </summary>
        public string BS_YN
        {
            get { return this._BS_YN; }
            set { this._BS_YN = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 글쓰기권한여부
        /// </summary>
        public string WRITE_YN
        {
            get { return this._WRITE_YN; }
            set { this._WRITE_YN = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 순서
        /// </summary>
        [Display(Name = "순서")]
        [Range(0, 20, ErrorMessage = "순서는 1 ~ 20 사이의 숫자만 넣어주세요.")]
        public int BOARD_ORDER
        {
            get { return this._BOARD_ORDER.Value; }
            set { this._BOARD_ORDER = _BOARD_ORDER == null ? 0 : value; }
        }
        /// <summary>
        /// 등록자 정보
        /// </summary>
        public string INPUT_USER
        {
            get { return this._INPUT_USER; }
            set { this._INPUT_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 등록일
        /// </summary>
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        /// <summary>
        /// 수정자 정보
        /// </summary>
        public string UPDATE_USER
        {
            get { return this._UPDATE_USER; }
            set { this._UPDATE_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 수정일
        /// </summary>
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }

    #endregion

    #region ###     Like - 게시판, 덧글       ###

    public class LikeParam
    {
        private int? _Idx = 0;
        private string _TYPE = "";
        private string _INPUT_USER = "";
        private string _INPUT_DATE = "";
        private string _UPDATE_USER = "";
        private string _UPDATE_DATE = "";

        /// <summary>
        /// 게시판 or 덧글일련번호
        /// </summary>
        public int IDX
        {
            get { return this._Idx.Value; }
            set { this._Idx = _Idx == null ? 0 : value; }
        }
        /// <summary>
        /// 게시판 or 덧글 구분용
        /// 게시판:B / 덧글:C
        /// </summary>
        public string TYPE
        {
            get { return this._TYPE; }
            set { this._TYPE = string.IsNullOrEmpty(value) ? "B" : value; }
        }
        /// <summary>
        /// 등록자 정보
        /// </summary>
        public string INPUT_USER
        {
            get { return this._INPUT_USER; }
            set { this._INPUT_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 등록일
        /// </summary>
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        /// <summary>
        /// 수정자 정보
        /// </summary>
        public string UPDATE_USER
        {
            get { return this._UPDATE_USER; }
            set { this._UPDATE_USER = string.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// 수정일
        /// </summary>
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }

    #endregion

}
