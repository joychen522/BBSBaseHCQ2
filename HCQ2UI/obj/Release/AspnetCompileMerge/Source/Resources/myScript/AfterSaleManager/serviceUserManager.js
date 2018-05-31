/*******************************************************
 *  用户组管理 相关操作js
 * <p>Title: serviceUserManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年10月20日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, addOrEditUrl, demo2 = null;
var serviceUserManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        serviceUserManager.initTable();
        serviceUserManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/FacilityPlant/InitServiceUserTable",
            cutHeight: 5,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    //页面大小  
                    rows: params.limit,
                    //第几页
                    page: params.offset / params.limit + 1,
                    sm_code: $('#smCode').val(),//所属模块
                    group_name: encodeURI($('#groupName').val())//用户组名称
                }
                return params;
            },
            columns: [
                 {
                     radio: true
                 }, {
                     field: '',//第一列序号
                     title: '序号',
                     align: 'center',
                     width: 50,
                     formatter: function (value, row, index) {
                         return index + 1;
                     }
                 }, {
                     field: 'user_name',
                     title: '用户名',
                     align: 'center'
                 }, {
                     field: 'login_name',
                     title: '登录名',
                     align: 'center'
                 }, {
                     field: 'user_qq',
                     title: 'QQ',
                     align: 'center'
                 }, {
                     field: 'user_phone',
                     title: '电话号码',
                     align: 'center'
                 }, {
                     field: 'user_email',
                     title: 'Email',
                     align: 'center'
                 }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //维护人员
        $('#suserEdit').click(function () {
            serviceUserManager.createDataBySelect();
        });
        //删除
        $('#suserDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要移除当前选中用户吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("移除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/FacilityPlant/DelServiceUserByID/' + row[0].su_id,
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu == 0) {
                            layer.msg("移除成功...");
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
    },
    //手动选择：生成数据
    createDataBySelect: function () {
        //获取全部人员
        $.ajax({
            url: ctx + '/FacilityPlant/GetAllPersons',
            type: "post",
            async: false,
            dataType: 'json',
            success: function (mess) {
                if (mess.Statu === 0) {
                    var obj = mess.Data;
                    $('.demo1').empty();//清空
                    $(obj).each(function () {
                        var str = "";
                        if (this['value'].toString().indexOf(":selected")>0)
                            str = " selected=\"selected\"";
                        $("<option value='" + this['value'] + "' " + str + " class='listBoxoption'>" + this['text'] + "</option>").appendTo($('.demo1'));
                    });
                    serviceUserManager.initListBox();
                }
                else
                    layer.open({
                        shade: false,
                        title: false,
                        content: mess.Msg,
                        btn: ''
                    });
            },
            error: function () {
                layer.msg('数据异常~', { icon: 5 });
            }
        });
    },
    //初始化ListBox
    initListBox: function () {
        if (demo2)
            demo2.bootstrapDualListbox('refresh');
        demo2 = $('.demo1').bootstrapDualListbox({
            nonSelectedListLabel: '未选择数据',
            selectedListLabel: '已选择数据',
            preserveSelectionOnMove: '撤销',
            filterTextClear: '显示全部',
            filterPlaceHolder: '输入关键字筛选',
            moveSelectedLabel: '单个选择',
            moveAllLabel: '全部选择',
            removeSelectedLabel: '单个撤销',
            removeAllLabel: '全部撤销',
            infoText: '显示全部 {0}',
            infoTextEmpty: '未选择记录',
            infoTextFiltered: '<span class="label label-warning">检索到</span> {0} 总共 {1}',
            moveOnSelect: false
        });
        var createIndex = layer.open({
            title: ['售后人员维护：支持多个关键字同时检索用：| 分割', 'font-size:18px;'],
            type: 1,
            content: $('#createDataBySel'),
            scroll: true, //是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['680px', '98%'],
            yes: function (li, o) {
                //确认生成数据，获取已经选择的数据
                var selData = $('[name="duallistbox_demo1"]').val();
                $.ajax({
                    url: ctx + '/FacilityPlant/SaveUserDataBySelect',
                    type: "post",
                    async: false,
                    data: { "personData": selData.join(',') },
                    dataType: 'json',
                    success: function (mess) {
                        if (mess.Statu === 0)
                            layer.msg(mess.Msg, { icon: 6 });
                        else
                            layer.msg(mess.Msg, { icon: 5 });
                        layer.close(createIndex);
                        $table.bootstrapTable('refresh');
                    },
                    error: function () {
                        layer.msg('数据异常~', { icon: 5 });
                    }
                });
            },
            cancel: function (li, o) {

            }
        });
    }
}