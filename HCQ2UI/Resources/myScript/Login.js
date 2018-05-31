/*给jquery扩展方法*/
(function ($) {
    $.extend($, {
        Login: function (data, accessFun, errFun) {
            if (data == null || data == undefined)
                return false;
            switch (data.Statu) {
                case 0:
                    if (accessFun) accessFun(data);
                    break;
                default:
                    if (errFun) errFun(data);
                    break;
            }
        }
    });
}(jQuery));