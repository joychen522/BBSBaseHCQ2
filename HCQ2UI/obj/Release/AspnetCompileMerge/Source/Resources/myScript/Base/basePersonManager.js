/*******************************************************
 *  基本人员 相关操作js
 * <p>Title: basePersonManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var addOrEditUrl,//添加、编辑保存地址
    orgId = null,//组织机构ID(单位代码ID)
    user_identify;//人员身份证/唯一标识
var tips;//提示
var basePersonManager = {
    //1.0 初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        basePersonManager.initData();
        basePersonManager.initSelect();
        basePersonManager.bindEvent();
        basePersonManager.initUser();
        parent.delLoadBoxs();
    },
    //2.0 初始化参数
    initData: function () {
        orgId = $.getUrls("orgId");
        user_identify = $.getUrls("user_identify");
        $('#org_id').val(orgId);
        if (!user_identify)
            $('#head_img').attr("src", "~/Resources/mainFrame/img/a5.jpg".replace("~", window.location.origin + ctx));
    },
    //3.0 初始化下拉
    initSelect: function () {
        //$('#user_edu').initSelectpicker("UserEdu", null, true);
    },
    //4.0 绑定事件
    bindEvent: function () {

    }
}
