//전역 변수

//escape은 유니코드, encodeURIComponent는 utf-8

var ROOT_PATH = rooturl;

var urlmatches = document.URL.match(/^https?\:\/\/([^\/?#]+)(?:[\/?#]|$)/i);

$(function () {

    //전체선택 체크박스 클릭
    $("#allCheck").click(function () {
        //만약 전체 선택 체크박스가 체크된상태일경우
        if ($("#allCheck").prop("checked")) {
            //해당화면에 전체 checkbox들을 체크해준다
            $("input[type=checkbox]").prop("checked", true);
            // 전체선택 체크박스가 해제된 경우
        } else {
            //해당화면에 모든 checkbox들의 체크를해제시킨다.
            $("input[type=checkbox]").prop("checked", false);
        }
    })
})

function checkAll(bx) {
    var cbs = document.getElementsByTagName('input');
    for (var i = 0; i < cbs.length; i++) {
        if (cbs[i].type == 'checkbox') {
            cbs[i].checked = bx.checked;
        }
    }
}

function parseURL(url) {
    parsed_url = {}

    if (url == null || url.length == 0)
        return parsed_url;

    protocol_i = url.indexOf('://');
    parsed_url.protocol = url.substr(0, protocol_i);

    remaining_url = url.substr(protocol_i + 3, url.length);
    domain_i = remaining_url.indexOf('/');
    domain_i = domain_i == -1 ? remaining_url.length - 1 : domain_i;
    parsed_url.domain = remaining_url.substr(0, domain_i);
    parsed_url.path = domain_i == -1 || domain_i + 1 == remaining_url.length ? null : remaining_url.substr(domain_i + 1, remaining_url.length);

    domain_parts = parsed_url.domain.split('.');
    switch (domain_parts.length) {
        case 2:
            parsed_url.subdomain = null;
            parsed_url.host = domain_parts[0];
            parsed_url.tld = domain_parts[1];
            break;
        case 3:
            parsed_url.subdomain = domain_parts[0];
            parsed_url.host = domain_parts[1];
            parsed_url.tld = domain_parts[2];
            break;
        case 4:
            parsed_url.subdomain = domain_parts[0];
            parsed_url.host = domain_parts[1];
            parsed_url.tld = domain_parts[2] + '.' + domain_parts[3];
            break;
    }

    parsed_url.parent_domain = parsed_url.host + '.' + parsed_url.tld;

    return parsed_url;
}



function SetSessionFileinfo(filekey, FILELENGTH) {
    return XmlRequest(rooturl + "public/DataRequest.aspx", "workType=SetDownLoadSeq&keyWord=" + filekey + "&keyWord2=" + FILELENGTH);

}


//jquery 모달. 이름으로 처리
//(function ($) {
//    $.fn.extend({
//        leanModal: function (options) {
//            var defaults = { top: 100, overlay: 0.5, closeButton: null }; var overlay = $("<div id='lean_overlay'></div>"); $("body").append(overlay); options = $.extend(defaults, options); return this.each(function () {
//                var o = options; $(this).click(function (e) {
//                    var modal_id = $(this).attr("href"); $("#lean_overlay").click(function () { close_modal(modal_id) }); $(o.closeButton).click(function () { close_modal(modal_id) }); var modal_height = $(modal_id).outerHeight(); var modal_width = $(modal_id).outerWidth();
//                    $("#lean_overlay").css({ "display": "block", opacity: 0 }); $("#lean_overlay").fadeTo(200, o.overlay); $(modal_id).css({ "display": "block", "position": "fixed", "opacity": 0, "z-index": 11000, "left": 50 + "%", "margin-left": -(modal_width / 2) + "px", "top": o.top + "px" }); $(modal_id).fadeTo(200, 1); e.preventDefault()
//                })
//            }); function close_modal(modal_id) { $("#lean_overlay").fadeOut(200); $(modal_id).css({ "display": "none" }) }
//        }
//    })
//})(jQuery);

//모달 
//가리개 - 숨기기는 $('#mask').hide(); 개체는 <div id="mask"></div> 스타일은 #mask {  position:absolute;   left:0;  top:0;  z-index:9000;  background-color:#000;  display:none;}
//모달 콜백.-크롬 땜에
function ModalBackDisable(callback) {
    wrapWindowByMask();
    if (typeof callback == "function") {
        callback();
    }
    unwrapWindowByMask();
}
function ModalBackDisableParam(callback, parameters) {
    wrapWindowByMask();
    if (typeof callback == "function") {
        //        callback();
        callback.apply(this, parameters);
    }
    unwrapWindowByMask();
}
//가리개 - 숨기기는 $('#mask').hide(); 개체는 <div id="mask"></div> 스타일은 #mask {  position:absolute;   left:0;  top:0;  z-index:9000;  background-color:#000;  display:none;}
function unwrapWindowByMask() {
    $('#mask').fadeOut(1000);
    //         $('#mask').hide();
}

function wrapWindowByMask() {
    //화면의 높이와 너비를 구한다.
    var maskHeight = $(document).height();
    var maskWidth = $(window).width();

    //마스크의 높이와 너비를 화면 것으로 만들어 전체 화면을 채운다.
    $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

    //애니메이션 효과
    $('#mask').show();
    //    $('#mask').fadeIn(0);
    //    $('#mask').fadeTo("fast", 0.4);
    //    pause(1000);

}
// 그냥 n millis 동안 멈추기
function pause(numberMillis) {
    var now = new Date();
    var exitTime = now.getTime() + numberMillis;

    while (true) {
        now = new Date();
        if (now.getTime() > exitTime)
            return;
    }
}


//devexpress 입력 컨트롤 공통 validation
function OnTxtNullValidation(s, e) {
    var txtsrc = e.value;
    if (txtsrc == null)
        e.isValid = false;
}


//그리드 헤더 편집 창

function grid_ContextMenu(s, e) {
    if (e.objectType == "header") {
        grid.ShowCustomizationWindow();
    }
}



function StrGetValue(cellobj) {
    if (cellobj == null)
        return "";
    else
        return cellobj;
}
function NumStrGetValue(cellobj) {
    if (cellobj == null)
        return "0";
    else
        return cellobj;
}
function NumGetValue(cellobj) {
    if (cellobj == null)
        return 0;
    else
        return cellobj;
}

function ParseParameterValues(paramString, requestName) {
    if (paramString == null) return;
    var fullParams = paramString.split('&');
    if (fullParams == null) return "";

    var col;
    var returnValue;

    for (var i = 0; i < fullParams.length; i++) {
        col = fullParams[i].split('=');
        if (col != null && col[0] == requestName) {
            returnValue = col[1];
            break;
        }
    }
    return returnValue;
}

function RequestValue(requestName) {
    if (requestName == null) return "";

    if (location.search == null) return "";

    var fullParams = location.search.replace('?', '').split('&');

    if (fullParams == null) return "";

    var col;
    var returnValue;

    for (var i = 0; i < fullParams.length; i++) {
        col = fullParams[i].split('=');
        if (col != null && col[0] == requestName) {
            returnValue = col[1];
            break;
        }
    }
    return returnValue;
}


function layer_open(el) {

    var temp = $('#' + el);
    var bg = temp.prev().hasClass('bg');	//dimmed 레이어를 감지하기 위한 boolean 변수

    if (bg) {
        $('.layer').fadeIn();	//'bg' 클래스가 존재하면 레이어가 나타나고 배경은 dimmed 된다. 
    } else {
        temp.fadeIn();
    }

    // 화면의 중앙에 레이어를 띄운다.
    if (temp.outerHeight() < $(document).height()) temp.css('margin-top', '-' + temp.outerHeight() / 2 + 'px');
    else temp.css('top', '0px');
    if (temp.outerWidth() < $(document).width()) temp.css('margin-left', '-' + temp.outerWidth() / 2 + 'px');
    else temp.css('left', '0px');

    temp.find('a.cbtn').click(function (e) {
        if (bg) {
            $('.layer').fadeOut(); //'bg' 클래스가 존재하면 레이어를 사라지게 한다. 
        } else {
            temp.fadeOut();
        }
        e.preventDefault();
    });

    $('.layer .bg').click(function (e) {	//배경을 클릭하면 레이어를 사라지게 하는 이벤트 핸들러
        $('.layer').fadeOut();
        e.preventDefault();
    });

}

function CommonSMModal(url, width, height, parameter) {
    var feature = 'dialogWidth=' + width + 'px; dialogHeight=' + height + 'px; scroll=yes; status=no; help=no;'
    //var retVal = window.showModalDialog(rooturl + 'Common_iFrame.aspx?'+url,parameter,feature);
    var retVal = window.showModalDialog(url, parameter, feature);
    return retVal;
}
function CommonSMModalAdd(url, width, height, parameter, addfeature) {
    var feature = addfeature + ';dialogWidth=' + width + 'px; dialogHeight=' + height + 'px; scroll=yes; status=no; help=no;'

    //    var retVal = window.showModalDialog(rooturl + 'Common_iFrame.aspx?'+url,parameter,feature);
    var retVal = window.showModalDialog(url, parameter, feature);
    return retVal;
}
function GetPosition(PosLeft, PosTop) {
    var PosMx = event.offsetX;
    var PosMy = event.offsetY;
    var sPosMx = event.screenX;
    var sPosMy = event.screenY;

    //	var PosLeft = sPosMx -PosMx + 'px';						
    //	var PosTop = sPosMy + (20-PosMy) + 'px';                      


    if (PosLeft == null || PosLeft == 0) { PosLeft = sPosMx - PosMx + 'px' }
    else if (PosLeft != null && PosLeft != 0) { PosLeft = parseInt(PosLeft) + sPosMx - PosMx }
    if (PosTop == null || PosTop == 0) { PosTop = sPosMy + (20 - PosMy) + 'px' }
    else if (PosTop != null && PosTop != 0) { PosTop = parseInt(PosTop) + sPosMy + (20 - PosMy) }


    return ' POSITION: absolute;dialogLeft:' + PosLeft + ';dialogTop:' + PosTop;
}

//화면 정중앙에 띄우기
function GetCenter(width, height) {

    var PosLeft = (screen.width - width) / 2;
    var PosTop = (screen.height - height) / 2;

    return 'POSITION: absolute;dialogLeft:' + PosLeft + ';dialogTop:' + PosTop;
}


// 팝업
function Popup(url, name, width, height) {
    //    if(url.indexOf("?") == -1)
    //    {
    //        url = url + "?WIDTH=" + width + "&HEIGHT=" + height;    
    //    }
    //    else
    //    {
    //        url = url + "&WIDTH=" + width + "&HEIGHT=" + height;
    //    }

    win = window.open(url, name, "toolbar=no,width=" + width + ",height=" + height + ",menubar=no,status=no,scrollbars=auto,left=" + ((screen.width - width) / 2) + ",top=" + ((screen.height - height) / 2));
    win.focus();
    return win;
}


// 팝업
function PopupScroll(url, name, width, height) {
    //    if(url.indexOf("?") == -1)
    //    {
    //        url = url + "?WIDTH=" + width + "&HEIGHT=" + height;    
    //    }
    //    else
    //    {
    //        url = url + "&WIDTH=" + width + "&HEIGHT=" + height;
    //    }

    win = window.open(url, name, "toolbar=no,width=" + width + ",height=" + height + ",resizable=yes,toolbar=no,menubar=no,status=no,scrollbars=yes,left=" + ((screen.width - width) / 2) + ",top=" + ((screen.height - height) / 2));
    win.focus();
    return win;
}

function PopupWithOption(url, name, width, height, option) {
    if (url.indexOf("?") == -1) {
        url = url + "?WIDTH=" + width + "&HEIGHT=" + height;
    }
    else {
        url = url + "&WIDTH=" + width + "&HEIGHT=" + height;
    }

    //window.open(url, name, "width=" + width + ",height=" + height + option +  ",left="+((screen.width-width)/2)+",top="+((screen.height-height)/2));    
    win = window.open(url, name, "width=" + width + ",height=" + height + "," + option + ",left=" + ((screen.width - width) / 2) + ",top=" + ((screen.height - height) / 2));
    win.focus();
    return win;
}

function PopupResize(url, name, width, height) {
    win = window.open(url, name, "toolbar=no,width=" + width + ",height=" + height + ",menubar=no,status=no,scrollbars=yes,resizable=yes,left=" + ((screen.width - width) / 2) + ",top=" + ((screen.height - height) / 2));
    win.focus();
    return win;
}

function CommonSMPopup(strstyle, strcontrol, feature, searchbtn, parameter) {
    var SendParameter;

    var strscrid = "";


    SendParameter = "?scrid=" + strscrid + "&Control=" + strcontrol + "&Style=" + strstyle + "&SearchBtn=" + searchbtn + "&Param=" + parameter;

    var url = "/IANroot/SMPopup.aspx";

    window.open(url + SendParameter, "", feature);

}

function CommonOpen(OpenInd, url, params) {

    var sts = "";
    var opennm = OpenInd;

    switch (OpenInd) {

        case 'RPT':
            if (url == "")
                url = "../RPT/ActiveRpt.aspx";

            if (params != "")
                url += "?" + params;

            sts = "";
            opennm = "RPT";
            sts = "resizable=no,width=1000,height=800";
            break;

        case 'XLS':
            if (url == "")
                url = "/IANroot/Common/XlsDown.aspx";

            if (params != "")
                url += "?" + params;

            sts = "";
            //					opennm = "public";
            break;
        default:
            break;
    }

    obj = window.open(url, opennm, sts);
    obj.focus();

}

function trim(text) {
    var charCode = -1;
    do {
        charCode = text.substring(0, 1).charCodeAt();
        if (charCode == 32 || charCode == 160)
            text = text.substring(1, text.length);
    } while (charCode == 32 || charCode == 160);
    do {
        charCode = text.substring(text.length - 1, text.length);
        if (charCode == 32 || charCode == 160)
            text = text.substring(0, text.length - 1);
    } while (charCode == 32 || charCode == 160);
    return text;
}







//페이지내 코드를 줄이기 위해 만들어진 함수들


function ViewCreateF(type) {
    //		alert(document.all.hidSelectRow.value);
    if (type == "I" || type == "U") {
        if (type == "U") {
            if (cellselectobj == null && document.all.hidSelectRow.value == "") {
                alert("경고"); return;
            }
        }
        document.all.addb1_1.style.display = 'none';
        document.all.addb1_2.style.display = '';
        //document.all.trdiv1.style.display='';
    }
    else {
        document.all.addb1_1.style.display = '';
        document.all.addb1_2.style.display = 'none';
        //document.all.trdiv1.style.display='none';
    }
}




/**
* Time 형식인지 체크(느슨한 체크)
*/
function isValidTimeFormat(time) {
    return (!isNaN(time) && time.length == 12);
}

/**
* 유효하는(존재하는) Time 인지 체크
* ex) var time = form.time.value; //'200702310000'
*     if (!isValidTime(time)) {
*         alert("올바른 날짜가 아닙니다.");
*     }
*/
function isValidTime(time) {
    var year = time.substring(0, 4);
    var month = time.substring(4, 6);
    var day = time.substring(6, 8);
    var hour = time.substring(8, 10);
    var min = time.substring(10, 12);

    if (parseInt(year, 10) >= 1900 && isValidMonth(month) &&
        isValidDay(year, month, day) && isValidHour(hour) &&
        isValidMin(min)) {
        return true;
    }
    return false;
}


/**
* Time 스트링을 자바스크립트 Date 객체로 변환
* parameter time: Time 형식의 String
*/
function toTimeObject(time) { //parseTime(time)
    var year = time.substr(0, 4);
    var month = time.substr(4, 2) - 1; // 1월=0,12월=11
    var day = time.substr(6, 2);
    var hour = time.substr(8, 2);
    var min = time.substr(10, 2);

    return new Date(year, month, day, hour, min);
}


/**
* 자바스크립트 Date 객체를 Time 스트링으로 변환
* parameter date: JavaScript Date Object
*/
function toTimeString(date) { //formatTime(date)
    var year = date.getFullYear();
    var month = date.getMonth() + 1; // 1월=0,12월=11이므로 1 더함
    var day = date.getDate();
    var hour = date.getHours();
    var min = date.getMinutes();

    if (("" + month).length == 1) { month = "0" + month; }
    if (("" + day).length == 1) { day = "0" + day; }
    if (("" + hour).length == 1) { hour = "0" + hour; }
    if (("" + min).length == 1) { min = "0" + min; }

    return ("" + year + month + day + hour + min)
}
/**08.01.05추가
* 자바스크립트 Date 객체를 date스트링으로 변환
* parameter date: JavaScript Date Object
*/
function toDateString(date) { //formatTime(date)
    var year = date.getFullYear();
    var month = date.getMonth() + 1; // 1월=0,12월=11이므로 1 더함
    var day = date.getDate();


    if (("" + month).length == 1) { month = "0" + month; }
    if (("" + day).length == 1) { day = "0" + day; }

    return ("" + year + month + day)
}
function toDateString2(date) { //formatTime(date)
    var year = date.getFullYear();
    var month = date.getMonth()// + 1; // 1월=0,12월=11이므로 1 더함
    var day = date.getDate();


    if (("" + month).length == 1) { month = "0" + month; }
    if (("" + day).length == 1) { day = "0" + day; }

    return ("" + year + month + day)
}
/**
* 현재 시각을 Time 형식으로 리턴
*/
function getCurrentTime() {
    return toTimeString(new Date());
}

//오늘 날짜 리턴 (YYYYMMDD)
function getToDate() {
    var year = date.getFullYear();
    var month = date.getMonth() + 1; // 1월=0,12월=11이므로 1 더함
    var day = date.getDate();
    if (("" + month).length == 1) { month = "0" + month; }
    if (("" + day).length == 1) { day = "0" + day; }

    return ("" + year + month + day)
}

//오늘 날짜 구분자 추가하여 리턴 (YYYY-MM-DD)
function getToDateFormat(sep) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1; // 1월=0,12월=11이므로 1 더함
    var day = date.getDate();
    if (("" + month).length == 1) { month = "0" + month; }
    if (("" + day).length == 1) { day = "0" + day; }

    return ("" + year + "sep" + month + "sep" + day)
}
//날짜 구분자 추가하여 리턴 (YYYY-MM-DD)
function getToDateFormat2(sep, disDate) {
    var year = disDate.getFullYear();
    var month = disDate.getMonth() + 1; // 1월=0,12월=11이므로 1 더함
    var day = disDate.getDate();
    if (("" + month).length == 1) { month = "0" + month; }
    if (("" + day).length == 1) { day = "0" + day; }

    return ("" + year + "sep" + month + "sep" + day)
}
//문자열 date에 대해 날짜 구분자 추가하여 리턴 (YYYY-MM-DD)
function getToDateFormat3(sep, disDate) {
    var year = disDate.substr(0, 4);
    var month = disDate.substr(4, 2);
    var day = disDate.substr(6, 2);
    if (("" + month).length == 1) { month = "0" + month; }
    if (("" + day).length == 1) { day = "0" + day; }

    return ("" + year + "sep" + month + "sep" + day)
}
// pDay:계산할 일자
function addDay(yyyy, mm, dd, pDay) {
    var oDate;
    dd = dd * 1 + pDay * 1;
    //mm--; // 1월=0,12월=11이므로 1 더함
    oDate = new Date(yyyy, mm, dd)
    return oDate;
}

//pMonth:계산할 월
function addMonth(yyyy, mm, dd, pMonth) {
    var cDate; // 계산
    var oDate; // 리턴
    var cYear, cMonth, cDay
    mm = mm * 1 + ((pMonth * 1) - 1);
    cDate = new Date(yyyy, mm, dd)
    cYear = cDate.getFullYear();
    cMonth = cDate.getMonth();
    cDay = cDate.getDate();
    oDate = (dd == cDay) ? cDate : new Date(cYear, cMonth, 0); // 넘어간 월의 첫쨋날 에서 하루를 뺀 날짜 객체를 생성한다.
    return oDate;
}

//세자리마다 콤마(,) 찍기
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
/*
function commas(num)
{
 var result ="";
 for(var i=0; i<num.length; i++) 
 {
  var tmp = num.length-(i+1)
  if(i%3==0 && i!=0) 
   result = ',' + result;
  result = num.charAt(tmp) + result;
 }
 return result;
}
function setComma(num){
 var returnVal = String(num);

 while(returnVal.match(/^(-?\d+)(\d{3})/)){
  returnVal = returnVal.replace(/^(-?\d+)(\d{3})/,'$1,$2');
 }
 return returnVal;
}
*/

function RemoveComma(str) {
    while (str.indexOf(",") != -1) {
        str = str.replace(",", "");
    }
    return str;
}
function RemoveMask(str, rms) {
    while (str.indexOf(rms) != -1) {
        str = str.replace(rms, "");
    }
    return str;
}


//입력값 체크 
function comma() {
    var s = event.srcElement;
    if (isNaN(s.value.replace(/,/g, ''))) {
        //숫자만 입력가능합니다.
        s.value = s.value.substr(0, s.value.length - 1);
        return;
    }

    //첫자리에 "0"의 수 오는것 방지
    var rs = s.value.replace(/,/g, '');
    if ((eval(rs) <= 0 && rs.length > 1) || (eval(rs) > 0 && rs.substr(0, 1) == '0'))
        s.value = s.value.substr(0, s.value.length - 1);

    //세자리마다 콤마(,) 찍기
    if (s.value.length > 3) s.value = addCommas(s.value.replace(/,/g, ''));
}



function GetXMLHttpRequest() {
    var xmlhttp = null;
    if (window.XMLHttpRequest) {// code for all new browsers
        xmlhttp = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) {// code for IE5 and IE6
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    if (xmlhttp == null) {
        alert("Your browser does not support XMLHTTP.");
    }
    return (xmlhttp);
    //    return xmlhttp;
}

// Create the request object; Microsoft failed to properly
// implement the XMLHttpRequest in IE7 (can't request local files),
// so we use the ActiveXObject when it is available
// This function can be overriden by calling jQuery.ajaxSetup
//xhr: window.XMLHttpRequest && (window.location.protocol !== "file:" || !window.ActiveXObject) ?
//			function ()
//			{
//			    return new window.XMLHttpRequest();
//			} :
//			function ()
//			{
//			    try
//			    {
//			        return new window.ActiveXObject("Microsoft.XMLHTTP");
//			    } catch (e) { }
//			},

function XmlRequest(fileName, dataFaram) {
    var mRequestCont = null;
    //    var xmlHTTP = new ActiveXObject("Msxml2.XMLHTTP");
    var xmlHTTP = GetXMLHttpRequest();

    xmlHTTP.onreadystatechange = function () {
        if (xmlHTTP.readyState == 4) {
            if (xmlHTTP.status == 200) {
                mRequestCont = xmlHTTP.responseText;
            }
            else {
                //				    return "Fail";
                return "Error: returned status code " + xmlHTTP.status + " " + xmlHTTP.statusText;
            }
        }
    };


    xmlHTTP.open("POST", fileName, false);
    xmlHTTP.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlHTTP.send(dataFaram);


    /*-다중으로 수정했는데.. 별 차이는 없는듯
	if (xmlHTTP.status != 200 )
	 {
	    xmlHTTP = null;
	    return "Fail";
     }
	var mRequestCont = xmlHTTP.responseText;
	xmlHTTP = null;
	
	*/

    var rtnmsg = mRequestCont.toString().replace(/\\n/g, "\n");

    return rtnmsg;
}

function ajaxMsgServiceCall(msgcode) {
    var rtnmsg;
    $.ajax({
        type: "POST",
        url: rooturl + "public/MessageRequest.aspx/GetMessageCode",
        //            data: "{}",
        //            data: JSON.stringify('ttt'),
        data: "{ MsgCode: '" + msgcode + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var msgresp = result.d;
            //alert(msgresp);
            rtnmsg = msgresp;
            //var resultArr = $.parseJSON(result.d);
            //                for (var i = 0; i < resultArr.length; i++) {
            //alert(resultArr);
        }
    });

    return rtnmsg;
}
//비동기 콜이라서 메세지로 쓰기엔 부적합
function RsxGet_ajax(msgcode) {
    return ajaxMsgServiceCall(msgcode.toUpperCase());
}
function RsxGet(msgcode) {
    return XmlRequest(rooturl + "public/MessageRequest.aspx", "msgcode=" + msgcode.toUpperCase());
}

var msg = new MessageHandler();
function MessageHandler() {
    this.AlertUrl = rooturl + "common/Msgbox/Alert.htm";
    this.AlertTextUrl = rooturl + "common/Msgbox/AlertText.htm";
    this.TimeAlertUrl = rooturl + "common/Msgbox/TimeAlert.htm";
    this.ConfirmUrl = rooturl + "common/Msgbox/Confirm.htm";
    this.InfoUrl = rooturl + "common/Msgbox/Alert.htm";


    this.message = function (msgcode, msg) {
        return msg;
        //msgcode 찾고 없으면 msg 출력
        //return RsxGet(msg);
    }

    this.message0 = function (msg) {
        return RsxGet(msg);
    }

    this.alert2 = function (msg) {
        return alert(msg);
    }


    this.alert = function (msgcode, msg) {
        //msgcode 찾고 없으면 msg 출력
        return alert(msg);
    }


    this.alert0 = function (msg) {
        //return alert(msg);
        //임시로 다국어 주석
        return alert(RsxGet(msg));
        /*
		var o = new MessagePopUp("popup",RsxGet(msg),this.AlertUrl);
		var returnValue = o.show();
		return true;*/
    }
    this.alertMsg = function (msg) {
        var o = new MessagePopUp("popup", msg, this.AlertUrl);
        var returnValue = o.show();
        return true;
    }

    this.alertText = function (msg) {
        var o = new MessagePopUp("popup", msg, this.AlertTextUrl);
        o.Height = 320;
        var returnValue = o.show();
        return true;
    }

    this.timeAlert = function (msg) {
        var o = new MessagePopUp("popup", msg, this.TimeAlertUrl);
        var returnValue = o.show();
        return true;
    }

    this.confirm = function (msgcode, msg) {
        //msgcode 찾고 없으면 msg 출력
        return confirm(msg);
    }

    this.confirm0 = function (msg) {
        return confirm(RsxGet(msg));
        /*
		var o = new MessagePopUp("popup",RsxGet(msg),this.ConfirmUrl);
		var returnValue = o.show();
		return returnValue;*/
    }
    this.confirmText = function (msg) {
        //return confirm(msg);
        var o = new MessagePopUp("popup", msg, this.ConfirmUrl);
        var returnValue = o.show();
        return returnValue;
    }
    this.showInfo = function (url, msg) {
        var o = new MessagePopUp("popup", msg, this.ConfirmUrl);
        var returnValue = o.show();
        return true;
    }
}


function MessagePopUp(msgType, msg, url) {
    this.MessageType = msgType;
    this.Message = msg;
    this.NavigateUrl = url;

    this.X = null;

    this.Y = null;
    this.Width = null;
    this.Height = null;
    this.DefaultWidth = 358;
    this.DefaultHeight = 178;

    this.show = function () {
        if (this.MessageType == "popup")	// 
        {
            if (this.NavigateUrl == null) {
                alert('need navigate url!'); return false;
            }

            return this.openPopUp();
        }
        else if (this.MessageType == "layer") {
            return this.openLayer();
        }
    }
    /*  */
    this.openPopUp = function () {
        var sUrl = this.NavigateUrl;

        // Parameter
        var oArguments = new Object();
        oArguments.Message = this.Message;

        // Feature
        var sFeatures = "status:no;scroll:no;dialogWidth:" + this.getWidth() + "px;dialogTop:" + this.getY() + ";dialogHeight:" + this.getHeight() + "px; dialogLeft:" + this.getX() + ";"

        //return window.open(sUrl);

        return window.showModalDialog(sUrl, oArguments, sFeatures);
        //return window.showModelessDialog(sUrl,oArguments, sFeatures);


    }

    this.getIcon = function () {
    }

    this.openLayer = function () {


    }

    this.getX = function () {
        if (this.X == null) this.X = (screen.width - this.getWidth()) / 2;
        return this.X;
    }

    this.getY = function () {
        if (this.Y == null) this.Y = (screen.height - this.getHeight()) / 2;
        return this.Y;
    }

    this.getWidth = function () {
        if (this.Width == null) this.Width = this.DefaultWidth;
        return this.Width;
    }
    this.getHeight = function () {
        if (this.Height == null) this.Height = this.DefaultHeight;
        return this.Height;
    }

    this.getId = function () {
        return "gs_msg_table";
    }

    this.getElement = function () {
        return document.getElementById(this.getId());
    }
}

// prevDate < nextDate 면 true를 리턴 
function CompareIgdGridDate(prevDateCell, nextDateCell) {

    var prevDate = prevDateCell.getText().replace('-', '').replace('-', '');

    var nextDate = nextDateCell.getText().replace('-', '').replace('-', '');

    if (prevDate <= nextDate)
        return true;
    else
        return false;
}

/* +------------------------------------------------------------------------+ */
/* |                                                                        | */
/* +------------------------------------------------------------------------+ */
/*phil : 3항연산자( ? : )의 널 및 값 비교 간단 리턴 함수. */
function ifelsethree(val, compval, rplcval) {
    //var rtnVal = val == null || trim(val) == compval ? rplcval : trim(val);
    var rtnVal = val == null || val == compval ? rplcval : val;
    return rtnVal;
}
/*phil :  숫자인지 체크   */
function isNumber(input) {
    //	    alert('input = ' + input);
    var chars = "0123456789";
    return containsCharsOnly(input, chars);
}
/*phil : 각 종 문자 혹은 숫자 등의 체크를 위한 검증 함수 */
function containsCharsOnly(input, chars) {
    //for (var inx = 0; inx < input.value.length; inx++) {
    for (var inx = 0; inx < input.length; inx++) {
        if (chars.indexOf(input.charAt(inx)) == -1)
            return false;
    }
    return true;
}


//공백 문자열 없애기
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/gi, "");
}

//소수점 2째자리까지 나타내기
function plus2Digit(inputData) {

    if (String(inputData).trim() == "" || isNaN(inputData)) {  // 공백이거나 숫자가 아니면 공백을 돌려준다.
        return "";
    }

    var retValue;

    var p = Math.pow(10, 2);        // 소수 둘째자리까지...
    retValue = Math.round(inputData * p) / p;

    if (retValue.toString().indexOf(".") < 0) {    // 정수가 나왔으면..
        retValue = retValue.toString() + ".00";
    }
    else {             // 소수점이 있으면 . 은 붙이지 않는다
        retValue = retValue.toString() + "00";
    }

    retValue = retValue.substring(0, retValue.indexOf(".") + 3);

    return retValue;
}

//소수점 4째자리까지 나타내기
function plus4Digit(inputData) {

    if (String(inputData).trim() == "" || isNaN(inputData)) {  // 공백이거나 숫자가 아니면 공백을 돌려준다.
        return "";
    }

    var retValue;

    var p = Math.pow(10, 4);        // 소수 넷째자리까지...
    retValue = Math.round(inputData * p) / p;

    if (retValue.toString().indexOf(".") < 0) {    // 정수가 나왔으면..
        retValue = retValue.toString() + ".0000";
    }
    else {             // 소수점이 있으면 . 은 붙이지 않는다
        retValue = retValue.toString() + "0000";
    }

    retValue = retValue.substring(0, retValue.indexOf(".") + 5);

    return retValue;
}



/*============================================================================
 Function : 소수점 절삭 처리 
 Input    : amount       : 절삭처리할 금액 
      limitDigitCount : 금액에 지정된 소수점 자리
   Return   : 지정된 소소점 자리의  금액     
   examples : setDigitCount(amount , 2);
============================================================================*/
function setDigitCount(amount, limitDigitCount) {

    var returnAmount = 0;

    if (amount == "") {
        returnAmount = 0;
    } else {
        returnAmount = amount;
    }

    returnAmount = returnAmount + "";

    if (returnAmount.indexOf(".") != -1) {
        var arrNumberValue = returnAmount.split('.');
        var digitNum = "";
        var digitCount = 0;

        /* 소숫점 변동 */
        if (arrNumberValue.length > 1 && arrNumberValue[1].length > 0) {

            if (!isNaN(limitDigitCount) && parseFloat(limitDigitCount) > 0) {
                digitCount = parseFloat(limitDigitCount);
            }

            if (arrNumberValue[1].length < digitCount) digitCount = arrNumberValue[1].length;
            digitNum = arrNumberValue[1].substring(0, digitCount);

            if (digitNum != "") {
                returnAmount = arrNumberValue[0] + "." + digitNum;
            } else {
                returnAmount = arrNumberValue[0];
            }
        }
    }

    return returnAmount;
}


// Left 빈자리 만큼 padStr 을 붙인다.
function lpad(src, len, padStr) {
    var retStr = "";
    var padCnt = Number(len) - String(src).length;
    for (var i = 0; i < padCnt; i++) retStr += String(padStr);
    return retStr + src;
}
// 창 닫기. IE 6 or IE 7. confirm 없이. Frame내부용
function frame_Close() {
    var useragent = window.navigator.appVersion;
    var msie = useragent.indexOf("MSIE ");
    var version = useragent.substring(msie + 5, msie + 8);

    if (version >= "7.0") {
        top.window.open('about:blank', '_self').close();
    }
    else {
        top.parent.opener = null;
        top.parent.window.close();
    }

    return;

}
// 창 닫기. IE 6 or IE 7. confirm 없이. 팝업용
function page_Close() {
    var useragent = window.navigator.appVersion;
    var msie = useragent.indexOf("MSIE ");
    var version = useragent.substring(msie + 5, msie + 8);

    if (version >= "7.0") {
        window.open('about:blank', '_self').close();
    }
    else {
        parent.opener = null;
        parent.window.close();
    }

    return;

}

// 해당월의 마지막 일자를 리턴 

function getLastDate(month, year) {
    var days;
    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        days = 31;
    else if (month == 4 || month == 6 || month == 9 || month == 11)
        days = 30;
    else if (month == 2) {
        if (isLeapYear(year)) {
            days = 29;
        }
        else {
            days = 28;
        }
    }

    return year + '-' + month + '-' + days;
}


// 해당월의 첫 일자를 리턴
function getFirstDate(month, year) {
    return year + '-' + month + '-' + '01';
}



// 윤년의 계산

function isLeapYear(Year) {
    if (((Year % 4) == 0) && ((Year % 100) != 0) || ((Year % 400) == 0)) {
        return (true);
    }
    else {
        return (false);
    }
}



// 전표 결재선 변경 공통함수 
/*
    prof_date - 전표 생성일
    prof_dept - 전표 귀속부서
    prof_clss - 전표 차수 
    prof_main - 전표번호 
    sttu      - 상태 
*/
function ApprLineChange(prof_date, prof_dept, prof_main, prof_clss, busn_divs, sttu) {

    if (prof_date == '') {
        alert('결재선 변경할 증빙이 존재 하지 않습니다.')
        return null;
    }

    if (sttu != 'IM') {
        alert('결재선을 변경할 수 없는 상태입니다.');
        return null;
    }

    var param = '?PROF_DATE=' + prof_date + '&PROF_DEPT=' + prof_dept + '&PROF_MAIN=' + prof_main + '&PROF_CLSS=' + prof_clss + '&BUSN_DIVS=' + busn_divs;
    var rtnarr = CommonSMModal("common/popup/ApplLineChng.aspx" + param, 450, 600, "");

    /***********************************************************
    PROCEDURE명     : PROC_DHS_SC_GET_APPR_LINE
    description    :  결재선 조회 
                     
     parameter       : I_PROF_DATE         전표일자
                I_PROF_DEPT         전표부서
                I_PROF_CLSS         전표차수 
                I_PROF_MAIN         전표번호 
                I_BUSN_DIVS         업무구분
     OUT PARAMETER    :                       
                P_APPR_LINE         결재라인
                P_UP_LINE           결재라인
                P_MSG               결과 메세지  OK 이면 성공 그외엔 에러 

   ***********************************************************/

    try {

        var params = new Array(prof_date, prof_dept, prof_clss, prof_main, busn_divs, "", "", "");

        var db = new ServerProc("PROC_DHS_SC_GET_APPR_LINE", params);
        var dataSet = db.ExecuteDataSet();

        var apr_line = '';
        var up_line = '';
        var msg = '';

        if (dataSet.Tables[0].Rows.length > 0)	//데이터 총레코드수, 0 이상이면 바인딩함
        {
            apr_line = dataSet.Tables[0].Rows[0][5];
            up_line = dataSet.Tables[0].Rows[0][6];
            msg = dataSet.Tables[0].Rows[0][7];
        }

        if (msg != 'OK') {
            alert(msg);
            return null;
        }
        else {
            var rtnArray = new Array(apr_line, up_line, msg);
            return rtnArray;
        }

    } catch (err) { alert("err : " + err.description); return null; }
}

// * ==============================================
//	GetPopUp 
//  2007/12/28 이동열
//  사용 예 :
//  function fn_Popup() {
//		    var arrValue = new Array();
//		    
//		    arrValue[0] = "DEPT_CODE$기술";     // 이름$값
//		    arrValue[1] = "CMPN_DIVS$C";
//		    
//		    strFeature = "width=500px,height=500px,status=no,scroll=no";
//          strFeature = "dialogWidth:600px;dialogHeight:445px;status:no;scroll:no";  // showModalDialog 사용시
//		    fn_comm_Popup("<%=ResolveUrl("~/Common/Popup/OAS_W_Dept_P.aspx") %>", arrValue, strFeature);
//		}
// * ==============================================
function fn_comm_Popup(strURL, arrValue, strPopupName, strFeature) {

    var strParam = "";
    for (var i = 0; i < arrValue.length; i++) {
        strParam += arrValue[i].split("$")[0] + "=" + arrValue[i].split("$")[1] + "&";
    }

    // 수정(2008.01.07) window.open ->  window.showModalDialog
    //oWin = window.open(strURL + "?" + strParam ,strPopupName, strFeature);

    var width = strFeature.split(",")[0].split("=")[1].replace(/px/g, '');
    var height = strFeature.split(",")[1].split("=")[1].replace(/px/g, '');

    var value = window.open(strURL + "?" + strParam, strPopupName, strFeature + ",left=" + ((screen.width - width) / 2) + ",top=" + ((screen.height - height) / 2));
}

function fn_comm_Modal(strURL, width, height, arrValue) {

    var strParam = "";
    for (var i = 0; i < arrValue.length; i++) {
        strParam += arrValue[i].split("$")[0] + "=" + arrValue[i].split("$")[1] + "&";
    }

    var PosLeft = (screen.width - width) / 2;
    var PosTop = (screen.height - height) / 2;

    var feature = "POSITION:absolute; dialogLeft:" + PosLeft + "; dialogTop:" + PosTop + "; dialogWidth=" + width + "px; dialogHeight=" + height + "px; scroll=yes; resizable=yes; status=no; help=no; edge:raised;";
    var retVal = window.showModalDialog(strURL + "?" + strParam, strParam, feature);
    return retVal;

}



function RefillSessVlagHousNum(vlag, housNum) {
    var rtnv = XmlRequest(rooturl + "common/page/XmlSessionChange2.aspx", "vlag=" + vlag + "&housNum=" + housNum);

    if (rtnv == "SessNull") {
        location.href = "common/page/SeesionExpire.aspx";
    }
}


function RefillSessVlagCntrInfo(vlag, housNum, cntrname, pyform) {
    var rtnv = XmlRequest(rooturl + "common/page/XmlSessionChange2.aspx", "vlag=" + vlag + "&housNum=" + housNum + "cntrname=" + cntrname + "&pyform=" + pyform);

    if (rtnv == "SessNull") {
        location.href = "common/page/SeesionExpire.aspx";
    }
}

// 원금, 부가세율을 입력받아 공급가액을 리턴해주는 함수 
function calSuplyAmt(SellAmt, VatRate) {
    var vat_rate = VatRate / 100;
    var vat_real = 1 + vat_rate;
    var sply_amt = Math.round((SellAmt / vat_real) - 0.4);
    return sply_amt;
}

String.prototype.bytes = function () {
    var str = this;
    var l = 0;
    for (var i = 0; i < str.length; i++) l += (str.charCodeAt(i) > 128) ? 2 : 1;
    return l;
}

// 한글을 포함한 length 길이 체크
function displayBytes(sz, id) {
    var form = document.form;
    var obj = document.getElementById(id);

    if (obj.value.bytes() > sz) { //80바이트를 넘기면
        if (event.keyCode != '8') //백스페이스는 지우기작업시 바이트 체크하지 않기 위해서
        {
            alert("영문 " + sz + "자, 한글 " + sz / 2 + "자 까지 입력이 가능합니다.");
        }
        obj.value = obj.value.substring(0, obj.value.length - 1);
    }
    //eval('document.all.' + id + '_bytes').innerHTML = eval('form.' + id).value.bytes() + " byte";
}

// * ==============================================
//	addLoadEvent 
//  양퀴가 만든거.
//  window onload를 2개 이상 쓰는거.
//		    
// * ==============================================

function addLoadEvent(func) {
    var ondonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            //init();
            func();
        }
    }
}


var hMD;

function showMD() {
    hMD = window.showModelessDialog("../common/Loading.htm", "WaitDialog",
"dialogHeight:150px; dialogWidth:200px; scroll:no; status:no; help:no;");
}

function closeMD() {
    // debugger;
    if (hMD != null) {
        hMD.close();
    }
}

function show_wating() {
    //alert("");
    divWating.style.posLeft = document.body.clientWidth / 2 - 100;
    divWating.style.posTop = document.body.clientHeight / 2 - 100;
    divWating.style.display = "";
}

function close_wating() {
    document.all.divWating.style.display = "none";
    //		divWating.style.visibility = "hidden";

}