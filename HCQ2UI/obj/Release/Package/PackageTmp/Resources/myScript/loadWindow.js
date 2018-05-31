//显示加载层
function showLoadBoxs() {
    var load = document.getElementById("divLoad");
    if (load != null)
        return false;
    //窗口宽，高
    var bWidth = parseInt(document.documentElement.scrollWidth);
    var bHeight = parseInt(document.documentElement.scrollHeight);
    var aWidth = "70px", aHeight = "25px";//图片
    var loadDiv = document.createElement("div");
    loadDiv.id = "divLoad";
    var loadStyle = "top:" + (bHeight / 2).toFixed(0) + "px;left:" + (bWidth / 1.8).toFixed(0) + "px;height:" + aHeight + ";width:" + aWidth + ";position:absolute;z-index:1000;background:url(../Resources/layer-3.0.1/skin/default/loading-0.gif) no-repeat";
    loadDiv.style.cssText = loadStyle;
    if (document.children != undefined)
        document.children[0].appendChild(loadDiv);
}

//清除加载层
function delLoadBoxs() {
    var my = document.getElementById("divLoad");
    if (my == null)
        my = parent.document.getElementById("divLoad");
    if (my != null)
        my.parentNode.removeChild(my);
}
//页面加载完毕清除加载层
window.onload = function () {
    delLoadBoxs();
}
//$(function () {
//    delLoadBoxs();
//});
