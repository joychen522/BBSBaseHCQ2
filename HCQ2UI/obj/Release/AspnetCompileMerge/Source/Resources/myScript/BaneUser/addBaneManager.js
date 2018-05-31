/*******************************************************
 *  添加戒毒人员 相关操作js
 * <p>Title: addBaneManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var addOrEditUrl,//添加、编辑保存地址
    orgId = null,//组织机构ID
    user_identify;//戒毒人员身份证
var tips;//提示
var addBaneManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        addBaneManager.initData();
        addBaneManager.initSelect();
        addBaneManager.bindEvent();
        addBaneManager.initUser();
        parent.delLoadBoxs();
    },
    //初始化参数
    initData:function(){
        orgId = $.getUrls("orgId");
        user_identify = $.getUrls("user_identify");
        $('#org_id').val(orgId);
        if (!user_identify)
            $('#head_img').attr("src", "~/Resources/mainFrame/img/a5.jpg".replace("~", window.location.origin + ctx));
    },
    //初始化下拉
    initSelect: function () {
        $('#user_edu').initSelectpicker("UserEdu", null, true);
        $('#job_status').initSelectpicker("JobStatus", null, true);
        $('#bane_type').initSelectpicker("BaneType", null, true);
        $('#marital_status').initSelectpicker("MaritalStatus", null, true);
        $('#user_status').initSelectpicker("UserStatus", null, true);
        $('#end_reason').initSelectpicker("EndReason", null, true);
    },
    //修改头像回调
    editHeadBack: function($url){
        if ($url)
            $('#head_img').attr("src", $url.toString().replace("~", window.location.origin + ctx) + "?_t=" + new Date().getTime());
    },
    //绑定默认事件
    bindEvent: function () {
        //上传头像
        $('#head_img').on('mouseover', function () {
            if (!tips) {
                tips = layer.tips('点击更换头像', '#head_img', {
                    tips: [3, '#78BA32']
                });
            }
        }).on('mouseout', function () {
            if (tips)
                layer.close(tips);
            tips = null;
        }).on('click', function () {
            if (!user_identify) {
                layer.msg("温馨提示：请先保存基本信息再上传头像！");
                return false;
            }
            //打开编辑
            var index = layer.open({
                id: 'ifreamSetUserHead',
                title: ['上传头像', 'font-size:14px;'],
                type: 2,
                content: ctx + '/Main/HeadImgList?url=/BaneUser/UploadFile&user_identify=' + user_identify,
                scroll: true,//是否显示滚动条、默认不显示
                btn: '',
                area: ['800px', '90%']
            });
        });
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //违法犯罪记录
        $('#btnCriminal').on('click', function () {
            if (!user_identify) {
                layer.msg("请先保存人员基本数据再录入档案袋！");
                return false;
            }
            layer.open({
                title: ['违法犯罪记录一栏', 'font-size:18px;'],
                type: 2,
                content: ctx + '/BaneUser/CriminalList?user_identify=' + user_identify,
                scroll: true,//是否显示滚动条、默认不显示
                btn: '',
                area: ['1000px', '95%'],
                cancel: function (li, o) {

                }
            });
        });
        //家庭成员
        $('#btnFamily').on('click', function () {
            if (!user_identify) {
                layer.msg("请先保存人员基本数据再录入档案袋！");
                return false;
            }
            layer.open({
                title: ['家庭成员记录一栏', 'font-size:18px;'],
                type: 2,
                content: ctx + '/BaneUser/FamilyList?user_identify=' + user_identify + '&fr_type=0',
                scroll: true,//是否显示滚动条、默认不显示
                btn: '',
                area: ['1000px', '95%'],
                cancel: function (li, o) {

                }
            });
        });
        //社会关系
        $('#btnSociety').on('click', function () {
            if (!user_identify) {
                layer.msg("请先保存人员基本数据再录入档案袋！");
                return false;
            }
            layer.open({
                title: ['社会关系记录一栏', 'font-size:18px;'],
                type: 2,
                content: ctx + '/BaneUser/FamilyList?user_identify=' + user_identify + '&fr_type=1',
                scroll: true,//是否显示滚动条、默认不显示
                btn: '',
                area: ['1000px', '95%'],
                cancel: function (li, o) {

                }
            });
        });
        //定期检测记录
        $('#btnUrinalysis').on('click', function () {
            if (!user_identify) {
                layer.msg("请先保存人员基本数据再录入档案袋！");
                return false;
            }
            layer.open({
                title: ['定期检测记录一栏', 'font-size:18px;'],
                type: 2,
                content: ctx + '/BaneUser/UrinalysisList?user_identify=' + user_identify,
                scroll: true,//是否显示滚动条、默认不显示
                btn: '',
                area: ['1000px', '95%'],
                cancel: function (li, o) {

                }
            });
        });
        //新增
        $('#btnAdd').on('click', function () {
            user_identify = "";
            $('#head_img').attr("src", "~/Resources/mainFrame/img/a5.jpg".replace("~", window.location.origin + ctx));
            $('#baneUserFormTable')[0].reset();//重置表单
        });
        //采集虹膜
        $('#btnIris').on('click', function () {
            layer.open({
                type: 2,
                title: "虹膜采集",
                content: ctx + "/Iris.html",
                area: ["700px", "400px"],
                maxmin: true,
                cancel: function () {

                }
            });
        });
        //保存
        $('#btnSave').click(function () {
            addBaneManager.addOrEditForm();
        });
        //返回
        $('#btnCancel').on('click', function () {
            baneManager.closeAddForm();
        });
    },
    //添加，编辑人员信息
    addOrEditForm: function () {
        addOrEditUrl = ctx + '/BaneUser/AddUser';
        if (user_identify)
            addOrEditUrl = ctx + '/BaneUser/EditUser';
        //禁用提交按钮
        var $submitBtn = $('#btnSave').button('loading');
        //隐藏select必填验证
        var ifExec=true,selDataVal=$('#baneUserFormTable').find("select[aria-required='true']");
        if(selDataVal){
            $.each(selDataVal,function(index,item){
                if(!$(item).val()){
                    ifExec=false;
                    var message=$(item).attr("title");
                    help.message(message);
                    return false;
                }
            });
        }
        if (!ifExec) {
            $submitBtn.button('reset');
            return false;
        }
        if ($('#baneUserFormTable').valid()) {
            $('#baneUserFormTable').ajaxSubmit({
                url: addOrEditUrl,
                type: "post",
                data: { org_id: orgId },
                dataType: "json",
                beforeSubmit: function (arr, $form, options) {
                    layer.msg("提交数据~", { icon: 1, time: 5000 });
                },
                success: function (result, status, xhr, $form) {
                    if (result.Statu === 0) {
                        user_identify = $('#user_identify').val();
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
        $submitBtn.button('reset');
    },
    Iris: function (small, big) {
        $("#iris_data1").val(small);
        $("#iris_data2").val(big);
        layer.msg("虹膜信息采集成功！", { icon: 6 });
    },
    //身份证不为空 填充数据
    initUser: function () {
        if (!user_identify)
            return false;
        $.ajax({
            type: 'post',
            url: ctx + '/BaneUser/GetUserById',
            data: { "user_identify": user_identify },
            dataType: 'json',
            cache: false,
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    if (data.Data) {
                        $('#baneUserFormTable')[0].reset();//重置表单
                        $('#baneUserFormTable').autoLoadForm(data.Data);//表单填充数据
                        //填充头像
                        if (data.Data.user_photo)
                            $('#head_img').attr("src", data.Data.user_photo.toString().replace("~", window.location.origin + $.ctx()));
                        else
                            $('#head_img').attr("src", ctx + "/Resources/mainFrame/img/a5.jpg");
                        //清空user_photo路径
                        $('#user_photo').val("");
                    }
                } else
                    layer.alert(result.Msg, { icon: 5 });
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    }
}
