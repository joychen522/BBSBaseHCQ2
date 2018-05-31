/*******************************************************
 *  微信题库 相关操作js
 * <p>Title: newsManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//微信题库管理
var $table, addOrEditUrl,new_id;
var open_index;//上传图片窗口ID
var newsManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        newsManager.initSelect();
        newsManager.initTable();
        newsManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化下拉
    initSelect:function(){
        $('#m_type').initSelectpicker("NewsType");
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/SysNewsMessage/GetNewsData",
            cutHeight: 45,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            toolbar: "#news_tool",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    newsName: $("#newsName").val()
                }
                return params;
            },
            columns: [
                {
                    radio: true
                }, {
                    field: "Number",
                    title: "序号",
                    align: "center",
                    formatter: function (value, row, index) {
                        return index + 1;
                    }
                }, {
                    field: "m_title",
                    title: "标题",
                    align: "left"
                }, {
                    field: "m_content",
                    title: "内容",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value != null && value != "") {
                            if (value.length > 15)
                                return value.substring(0, 15) + "...";
                            else
                                return value;
                        }
                    }
                }, {
                    field: "m_type",
                    title: "类别",
                    align: "center"
                }, {
                    field: "create_date",
                    title: "发布日期",
                    align: "center",
                    formatter: function (value, row, index) {
                        if (value)
                            return $.formatDate(new Date(parseInt(value.slice(6))));
                        else
                            return "";
                    }
                }, {
                    field: "create_user_name",
                    title: "发布用户",
                    align: "center"
                }
            ]
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //添加
        $('#newsAdd').click(function () {
            new_id = 0;
            newsManager.editForm();
        });
        //编辑
        $('#newsEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            newsManager.editForm(row[0]);
            new_id = row[0].m_id;
            new_id = (new_id) ? new_id : 0;
        });
        //删除
        $('#newsDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选择新闻吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/SysNewsMessage/DeleteNews/' + row[0].m_id,
                    data: { user_identify: row[0].user_identify },
                    dataType: 'json',
                    async: false,
                    success: function (data) {
                        layer.close(index);
                        if (data.Statu === 0) {
                            layer.msg("删除成功...");
                            $table.bootstrapTable('refresh');
                        }
                        else
                            layer.msg(data.Msg, { icon: 5 });
                    }
                });
            }, function () { });
        });
        //上传图片
        $('#upImg').on('click', function () {
            //打开编辑
            open_index = layer.open({
                id: 'ifreamSetUserHead',
                title: ['上传新闻图片', 'font-size:14px;'],
                type: 2,
                content: ctx + '/Main/HeadImgList?url=/SysNewsMessage/UploadNewsImg?id=' + new_id + '&anyImg=news',
                scroll: true,//是否显示滚动条、默认不显示
                btn: '',
                area: ['95%', '90%']
            });
        });
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "添加新闻", addOrEditUrl = ctx + "/SysNewsMessage/AddNews";
        if (row) {
            $title = "编辑新闻";
            addOrEditUrl = ctx + "/SysNewsMessage/EditNews";
        }
        $('#img_focus_imgage').attr('src', '');
        $('#focus_imgage').val('');
        //$('#img_messList_imgage').attr('src', '');
        //$('#messList_imgage').val('');
        //$('#img_messDetail_imgage').attr('src', '');
        //$('#messDetail_imgage').val('');
        $('#groupFormTable').resetHideValidForm();
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#groupForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['800px', '98%'],
            success: function (li, o) {
                if (row) {
                    $('#groupFormTable')[0].reset();//重置表单
                    $('#groupFormTable').LoadForm(row);//表单填充数据
                    if(row.focus_imgage)
                        $('#img_focus_imgage').attr('src', row.focus_imgage.toString());
                    //if (row.messList_imgage)
                    //    $('#img_messList_imgage').attr('src', row.messList_imgage.toString());
                    //if (row.messDetail_imgage)
                    //    $('#img_messDetail_imgage').attr('src', row.messDetail_imgage.toString());
                }
            },
            yes: function (li, o) {
                if ($('#groupFormTable').valid()) {
                    //验证通过
                    $('#groupFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        beforeSubmit: function (arr, $form, options) {
                            layer.msg("提交数据~", { icon: 1, time: 5000 });
                        },
                        success: function (result, status, xhr, $form) {
                            if (result.Statu === 0) {
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
    },
    //设置图片路径
    setImgUrl: function (url) {
        //关闭窗口
        newsManager.closeWindows();
        //设置图片路径
        if(url)
        {
            //焦点新闻
            $('#focus_imgage').val(url + "/csharp_focus_imgage.jpg");
            $('#img_focus_imgage').attr('src', url + "/csharp_focus_imgage.jpg");
            ////列表新闻
            //$('#messList_imgage').val(url + "/csharp_messList_imgage.jpg");
            //$('#img_messList_imgage').attr('src', url + "/csharp_messList_imgage.jpg");
            ////新闻详细
            //$('#messDetail_imgage').val(url + "/csharp_messDetail_imgage.jpg");
            //$('#img_messDetail_imgage').attr('src', url + "/csharp_messDetail_imgage.jpg");
        }
    },
    //关闭窗口
    closeWindows: function () {
        if (open_index)
            layer.close(open_index);
    }
}