/*******************************************************
 *  框架页 相关操作js
 * <p>Title: mainManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年1月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var ctx;
$(function () {
    ctx = $.ctx();
    mainManager.initMenuList();
    mainManager.initLoginMessage();//初始化登录信息
});
var mainManager = {
    //初始化菜单列表
    initMenuList:function() {
        $.ajax({
            type: 'post',
            url: ctx + '/SysMenu/GetIndexMenuData',
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    var menuList = mainManager.initMenuData(data.Data);
                    $('#side-menu').append($(menuList));
                }
                else
                    layer.msg(data.Msg, { icon: 5 });
            }
        });
    },
    //获取菜单数据
    initMenuData: function (data) {
        var $html="";
        $.each(data, function (index, item) {
            var mark = "";
            if (index === 0)
                mark = " class=\"active\" ";
            //一级目录
            if (!item.nodes || item.nodes.length === 0) {
                $html += "<li" + mark + "><a class='J_menuItem' href='" + ctx + "/" + item.folder_url + "'>";
                if (item.folder_image != null)
                    $html += "<i class='fa " + item.folder_image + "'></i>";
                else
                    $html += "<i class='fa fa-magic'></i>";
                $html += "<span class='nav-label'>" + item.text + "</span></a><li>";
            }
            else {
                $html += "<li" + mark + "><a href='#'>";
                if (item.folder_image != null)
                    $html += "<i class='fa " + item.folder_image + "'></i>";
                else
                    $html += "<i class='fa fa-edit'></i>";
                $html += "<span class='nav-label'>" + item.text + "</span><span class='fa arrow'></span></a>";
                $html += "<ul class='nav nav-second-level'>";
                $html += mainManager.recursionMenu(item.nodes);
                $html += "</ul></li>";
            }
        });
        return $html;
    },
    recursionMenu:function(result) {
        var $html="";
        $.each(result, function(index,item) {
            $html += "<li>";
            if (!item.nodes || item.nodes.length===0) 
                $html += "<a class='J_menuItem' href='" + ctx + "/" + item.folder_url + "'>" +
                    item.text + "</a>";
            else {
                $html += "<a href='#'>" + item.text + " <span class='fa arrow'></span></a>";
                $html += "<ul class='nav nav-third-level'>";
                $html += mainManager.recursionMenu(item.nodes);
                $html += "</ul>";
            }
            $html += "</li>";
        });
        return $html;
    },
    //初始化登录信息
    initLoginMessage: function () {
        var index=null;
        $('#btnChangePwd').click(function () {
            if(index!=null)
                layer.close(index);
        });
        var loginData = null;
        $.ajax({
            type: 'post',
            url: ctx + '/SysUser/GetLoginMessageData',
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    index = layer.open({
                        type: 1,
                        title: '温馨提示：  ',
                        area: ['300px', '200px'],
                        offset: 'rb', //右下角弹出
                        anim: 1, //动画
                        content: $("#loginMessage"),
                        success: function (layero, index) {
                            //显示之后执行事件
                            if (data.Data != null) {
                                if(data.Data.login_count!=null)
                                    $('#login_count').text(data.Data.login_count);
                                if (data.Data.login_ip != null)
                                    $('#login_ip').text(data.Data.login_ip);
                                if (data.Data.login_time != null)
                                $('#login_time').text($.formatDataLong(new Date(parseInt(data.Data.login_time.slice(6)))));
                                if (data.Data.last_ip != null)
                                $('#last_ip').text(data.Data.last_ip);
                                if (data.Data.last_time != null)
                                $('#last_time').text($.formatDataLong(new Date(parseInt(data.Data.last_time.slice(6)))));
                            }
                        }
                    });
                }
                else
                    layer.msg(data.Msg, { icon: 5 });
            }
        });
    },
    //编辑头像
    editHead: function () {
        //打开编辑
        var index = layer.open({
            id: 'ifreamSetUserHead',
            title: ['编辑头像', 'font-size:14px;'],
            type: 2,
            content: ctx + '/Main/HeadImgList',
            scroll: true,//是否显示滚动条、默认不显示
            btn: '',
            area: ['800px', '90%']
        });
    },
    //修改头像回调函数
    editHeadBack: function ($url) {
        if(!$url)
            $('#ownHead').attr("src", $url);
    },
    //编辑用户信息
    editUser: function () {
        $.ajax({
            type: 'post',
            url: ctx + '/Main/GetUserInfo',
            dataType: 'json',
            async: false,
            success: function (result) {
                mainManager.modifyUser(result.Data);
            }
        });
    },
    modifyUser: function (row) {
        if (row == null || row == "" || row == undefined) {
            layer.msg("信息异常~", { icon: 5 });
            return false;
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: ['编辑用户信息', 'font-size:18px;'],
            type: 1,
            content: $('#org_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '80%'],
            success: function (li, o) {
                if (row != null) {
                    row.user_pwd = "";
                    if (row.user_birth != null) {
                        row.user_birth = $.formatDate(new Date(parseInt(row.user_birth.slice(6))));
                    }
                    $('#orgFormTable')[0].reset();//重置表单
                    $('#orgFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                if ($('#orgFormTable').valid()) {
                    //验证通过
                    $('#orgFormTable').ajaxSubmit({
                        url: ctx + "/SysUser/EditUser?user_id=" + row.user_id,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            if (result.Statu === 0) {
                                layer.closeAll();
                                $table.bootstrapTable('refresh');
                                layer.msg('保存成功', { icon: 1 });
                            }
                            else
                                layer.alert(result.Msg, { icon: 5 });
                        },
                        error: function (xhr, status, error, $form) {
                            layer.msg("保存失败~", { icon: 5 });
                        }
                    });
                }
            },
            cancel: function (li, o) {

            }
        });
    }
}