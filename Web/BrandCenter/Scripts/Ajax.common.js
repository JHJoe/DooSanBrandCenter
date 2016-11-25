/*
공통Script들
*/
var BreandCenterHelper = function () {

    this.params = {}

    this.DoAjaxSync = function (Url, jsonData, resultFunc, errorFunc) {
        $.ajax({
            type: "post"
            , url: Url
            , async: false
            , cashe: false
            , data: $.param(jsonData)
            , success: function (data) { resultFunc(data); }
            , error: function (data, state, e) { errorFunc(data, state, e); }
        });
    }

    this.DoAjaxSyncUrl = function (Url, jsonData, resultFunc, errorFunc) {
        $.ajax({
            type: "post"
            , url: Url
            , async: false
            , cashe: false
            , data: $.param(jsonData)
            , success: function (data) { resultFunc(data); }
            , error: function (data, state, e) { errorFunc(data, state, e); }
        });
    }

    this.DoAjaxAsync = function (Url, jsonData, resultFunc, errorFunc) {
        $.ajax({
            type: "post"
            , url: Url
            , async: true
            , cashe: false
            , data: $.param(jsonData)
            , success: function (data) { resultFunc(data); }
            , error: function (data, state, e) { errorFunc(data, state, e); }
        });
    }

    this.DoAjaxAsyncHtml = function (Url, jsonData, resultFunc, errorFunc) {
        $.ajax({
            type: "post"
            , url: Url
            , async: true
            , cashe: false
            , data: $.param(jsonData)
            , dataType: "html"
            , success: function (data) { resultFunc(data); }
            , error: function (data, state, e) { errorFunc(data, state, e); }
        });
    }

    this.DoAjaxReturnHtml = function (Url, jsonData, resultFunc, errorFunc) {
        $.ajax({
            type: "post"
            , url: Url
            , async: true
            , cashe: false
            , data: JSON.stringify(jsonData)
            , contentType: "application/json; charset=utf-8"
            , dataType: "html"
            , success: function (data) { resultFunc(data); }
            , error: function (data, state, e) { errorFunc(data, state, e); }
        });
    }

    this.DoAjaxAsyncLoadingHtml = function (Url, idName, jsonData, resultFunc, errorFunc, beforeFunc, completeFunc) {
        $.ajax({
            type: "post"
            , url: Url
            , async: true
            , cashe: false
            , data: $.param(jsonData)
            , dataType: "html"
            , success: function (data) { resultFunc(data, idName); }
            , error: function (data, state, e) { errorFunc(data, state, e); }
            , beforeSend: function () { beforeFunc(idName); }
            , complete: function () { completeFunc(idName); }
        });
    }

    this.DoAjaxAsyncLoading = function (Url, idName, jsonData, resultFunc, errorFunc, beforeFunc, completeFunc) {
        $.ajax({
            type: "post"
            , url: Url
            , async: true
            , cashe: false
            , data: $.param(jsonData)
            , dataType: "json"
            , success: function (data) { resultFunc(data, idName); }
            , error: function (data, state, e) { errorFunc(data, state, e); }
            , beforeSend: function () { beforeFunc(idName); }
            , complete: function () { completeFunc(idName); }
        });
    }


    // 문자열 공백제거하기
    this.CallTextTrim = function (txt) {
        var retVal = "";
        var charVal = "";

        for (i = 0; i < txt.length; i++) {
            charVal = txt.charAt(i);

            if (charVal != " ") { retVal += charVal; }
        }

        return retVal;
    }

    // 문자열 변환하기
    this.CallTextReplace = function (txt, pre, after) {
        for (var i = 0; i < txt.length; i++) {
            if (txt.substring(i, i + pre.length) == pre) {
                txt = txt.substring(0, i) + after + txt.substring(i + pre.length, txt.length);
            }
        }

        return txt;
    }

    this.GetWeekDay = function (sDate) {
        var yy = parseInt(sDate.substr(0, 4), 10);
        var mm = parseInt(sDate.substr(5, 2), 10);
        var dd = parseInt(sDate.substr(8), 10);

        var d = new Date(yy, mm - 1, dd);
        var weekday = new Array(7);
        weekday[0] = "0";  //일
        weekday[1] = "1";  //월
        weekday[2] = "2";  //화
        weekday[3] = "3";  //수
        weekday[4] = "4";  //목
        weekday[5] = "5";  //금
        weekday[6] = "6";  //토

        //alert(d + " / " + d.getDay());

        return weekday[d.getDay()];
    }

    this.isDate = function (dateStr) {
        var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
        var matchArray = dateStr.match(datePat); // is the format ok?

        if (matchArray == null) {
            alert("Please enter date as either mm/dd/yyyy or mm-dd-yyyy.");
            return false;
        }

        month = matchArray[1]; // p@rse date into variables
        day = matchArray[3];
        year = matchArray[5];

        if (month < 1 || month > 12) { // check month range
            alert("Month must be between 1 and 12.");
            return false;
        }

        if (day < 1 || day > 31) {
            alert("Day must be between 1 and 31.");
            return false;
        }

        if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
            alert("Month " + month + " doesn`t have 31 days!")
            return false;
        }

        if (month == 2) { // check for february 29th
            var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
            if (day > 29 || (day == 29 && !isleap)) {
                alert("February " + year + " doesn`t have " + day + " days!");
                return false;
            }
        }

        return true; // date is valid
    }

    this.Datediff = function (fromDate, toDate, interval) {
        /*
        * DateFormat month/day/year hh:mm:ss
        * ex.
        * datediff('01/01/2011 12:00:00','01/01/2011 13:30:00','seconds');
        */
        var date = new Date();
        var second = 1000, minute = second * 60, hour = minute * 60, day = hour * 24, week = day * 7;

        var years = date.getFullYear();
        var months = date.getMonth();
        var days = date.getDate();

        fromDate = fromDate || years + "-" + months + "-" + days;
        toDate = toDate || years + "-" + months + "-" + days;

        fromDate = new Date(fromDate);
        toDate = new Date(toDate);
        var timediff = toDate - fromDate;
        if (isNaN(timediff)) return NaN;
        switch (interval) {
            case "years": return toDate.getFullYear() - fromDate.getFullYear();
            case "months": return (
                        (toDate.getFullYear() * 12 + toDate.getMonth())
                        -
                        (fromDate.getFullYear() * 12 + fromDate.getMonth())
                    );
            case "weeks": return Math.floor(timediff / week);
            case "days": return Math.floor(timediff / day);
            case "hours": return Math.floor(timediff / hour);
            case "minutes": return Math.floor(timediff / minute);
            case "seconds": return Math.floor(timediff / second);
            default: return undefined;
        }
    }

    //셀렉트 박스 초기화
    this.selectInit = function (objName) {
        //        for (var i = objName.length; i >= 0; i--) {
        //            objName.options[0] = null
        //        }

        $("#" + objName + " option").remove();
    }

    this.validateEmail = function (emailAddr) {
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
        return emailPattern.test(emailAddr);
    }

    this.Popup = function (url, jsonData, title, width, height, left, top) {
        title == "" ? "" : title;
        width == "" ? "320" : width;
        height == "" ? "240" : height;

        var x, y, wid, hei;
        x = screen.width;
        y = screen.height;
        wid = (x / 2) - (width / 2);
        hei = (y / 2) - (height / 2);

        left == "" ? wid : left;
        top == "" ? hei : top;

        window.open(url + "?" + $.param(jsonData), title, 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no, resizable=no,copyhistory=no ,width=' + width + ', height=' + height + ', left=' + left + ',top=' + top + '');
    }

}
