/*
* inTblId：表格id
* inWindow
* fileName：文件名
*/
function jsToExcel(inTblId, inWindow,fileName) {
    if (IEVersion()) { //如果是IE浏览器  
        try {  
            var allStr = ""; var curStr = "";  
            if (inTblId != null && inTblId != "" && inTblId != "null") {  
                curStr = getTblData(inTblId, inWindow);  
            }  
            if (curStr != null) {  
                allStr += curStr;  
            }  
            else {  
                alert("你要导出的表不存在！");  
                return;  
            }  
            //var fileName = getExcelFileName();  
            doFileExport(fileName, allStr);  
        }  
        catch (e) {  
            alert("导出发生异常:" + e.name + "->" + e.description + "!");  
        }  
    } else {  
        window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=divGvData]').html()));  
        e.preventDefault();  
    }  
}
//判断是否为IE浏览器
function IEVersion() {
    var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串  
    var isIE = userAgent.indexOf("compatible") > -1 && userAgent.indexOf("MSIE") > -1; //判断是否IE<11浏览器  
    var isEdge = userAgent.indexOf("Edge") > -1 && !isIE; //判断是否IE的Edge浏览器  
    var isIE11 = userAgent.indexOf('Trident') > -1 && userAgent.indexOf("rv:11.0") > -1;
    if (isIE) {
        var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
        reIE.test(userAgent);
        var fIEVersion = parseFloat(RegExp["$1"]);
        if (fIEVersion == 7) {
            return 7;
        } else if (fIEVersion == 8) {
            return 8;
        } else if (fIEVersion == 9) {
            return 9;
        } else if (fIEVersion == 10) {
            return 10;
        } else {
            return 6;//IE版本<=7
        }
    } else if (isEdge) {
        return 'edge';//edge
    } else if (isIE11) {
        return 11; //IE11  
    } else {
        return null;//不是ie浏览器
    }
}
function getTblData(inTbl, inWindow) {
    var rows = 0; var tblDocument = document;  
    if (!!inWindow && inWindow != "") {  
        if (!document.all(inWindow)) {  
            return null;  
        } else {  
            tblDocument = eval(inWindow).document;  
        }  
    }  
    var curTbl = tblDocument.getElementById(inTbl);  
    if (curTbl.rows.length > 65000) {  
        alert('源行数不能大于65000行');  
        return false;  
    }  
    if (curTbl.rows.length <= 1) {  
        alert('数据源没有数据');  
        return false;  
    }  
    var outStr = "";  
    if (curTbl != null) {  
        for (var j = 0; j < curTbl.rows.length; j++) {  
            for (var i = 0; i < curTbl.rows[j].cells.length; i++) {  
                if (i == 0 && rows > 0) {  
                    outStr += " \t"; rows -= 1;  
                }  
                var tc = curTbl.rows[j].cells[i];  
                if (j > 0 && tc.hasChildNodes() && tc.firstChild.nodeName.toLowerCase() == "input") {  
                    if (tc.firstChild.type.toLowerCase() == "checkbox") {  
                        if (tc.firstChild.checked == true) {  
                            outStr += "是" + "\t";  
                        } else {  
                            outStr += "否" + "\t";  
                        }  
                    }  
                } else {  
                    outStr += " " + curTbl.rows[j].cells[i].innerText + "\t";  
                }  
                if (curTbl.rows[j].cells[i].colSpan > 1) {  
                    for (var k = 0; k < curTbl.rows[j].cells[i].colSpan - 1; k++) {  
                        outStr += " \t";  
                    }  
                }  
                if (i == 0) {  
                    if (rows == 0 && curTbl.rows[j].cells[i].rowSpan > 1) {  
                        rows = curTbl.rows[j].cells[i].rowSpan - 1;  
                    }  
                }  
            }  
            outStr += "\r\n";  
        }  
    } else {  
        outStr = null; alert(inTbl + "不存在!");  
    }  
    return outStr;  
}  
function getExcelFileName() {  
    var d = new Date(); var curYear = d.getYear(); var curMonth = "" + (d.getMonth() + 1);  
    var curDate = "" + d.getDate(); var curHour = "" + d.getHours(); var curMinute = "" +  
        d.getMinutes(); var curSecond = "" + d.getSeconds();  
    if (curMonth.length == 1) {  
        curMonth = "0" + curMonth;  
    }  
    if (curDate.length == 1) {  
        curDate = "0" + curDate;  
    }  
    if (curHour.length == 1) {  
        curHour = "0" + curHour;  
    }  
    if (curMinute.length == 1) {  
        curMinute = "0" + curMinute;  
    }  
    if (curSecond.length == 1) {  
        curSecond = "0" + curSecond;  
    }  
    var fileName = "设备状态" + curYear + curMonth + curDate + curHour + curMinute + curSecond + ".xls";  
    return fileName;  
}  
function doFileExport(inName, inStr) {  
    var xlsWin = null;  
    if (!!document.all("glbHideFrm")) {  
        xlsWin = glbHideFrm;  
    } else {  
        var width = 1; var height = 1;  
        var openPara = "left=" + (window.screen.width / 2 + width / 2) + ",top=" + (window.screen.height + height / 2) +  
            ",scrollbars=no,width=" + width + ",height=" + height;  
        xlsWin = window.open("", "_blank", openPara);  
    }  
    xlsWin.document.write(inStr);  
    xlsWin.document.close();  
    xlsWin.document.execCommand('Saveas', true, inName);  
    xlsWin.close();  
}  
