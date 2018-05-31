/*******************************************************
 * 项目设备 相关操作js
 * <p>Title:trackRecordManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年10月16日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, addOrEditUrl, selUnit = null,
    unitName, unitCode;//项目名称，项目代码
var trackRecordManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        trackRecordManager.initUnitData();
        trackRecordManager.initTable();
        trackRecordManager.bindEvent();
        trackRecordManager.initSelect();
        parent.delLoadBoxs();
    },
    //获取单位数据
    initUnitData: function () {
        Lutai.Load("seSheng", "seShi", "searchArea", "testTree", 1, function (event, treeId, treeNode) {
            if (!treeNode || treeNode.type != 2) {
                selUnit = treeNode.area_code; unitName = ""; unitCode = "";
            } else {
                selUnit = treeNode.area_code;//区域代码
                unitName = treeNode.text;
                unitCode = treeNode.unit_id;//项目ID
            }
            $('#area_code').val(selUnit);
            //设置项目值
            $('#unit_name').val(unitName);
            $('#unit_code').val(unitCode);
            trackRecordManager.initNumberSel();
            $table.bootstrapTable('refresh');
        });
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/TrackRecord/InitTable",
            cutHeight: 5,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    area_code: (selUnit === null) ? "" : selUnit,//区域代码
                    unit_code: unitCode,//所选项目代码
                    trackStatus: $('#trackStatus').val(),//跟踪状态
                    trackDate: $('#trackDate').val()//跟踪时间
                }
                return params;
            },
            columns: [
            {
                radio: true
            },
	        {
	            field: '',//第一列序号
	            title: '序号',
	            align: 'center',
	            width: 50,
	            formatter: function (value, row, index) {
	                return index + 1;
	            }
	        },
            {
                field: 'fa_number',
                title: '设备编号',
                align: 'center'
            },
            {
                field: 'unit_name',
                title: '所在项目',
                align: 'center'
            },
            {
                field: 'tr_status_text',
                title: '跟踪状态',
                align: 'center'
            },
            {
                field: 'track_name',
                title: '跟踪人',
                align: 'center'
            },
            {
                field: 'track_date',
                title: '跟踪时间',
                align: 'center'
            },
            {
                field: 'tr_note',
                title: '备注',
                align: 'center'
            }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //初始化下拉
    initSelect: function () {
        //$('#trackStatus').initSelectpicker("TrackStatus");
        //$('#tr_status').initSelectpicker("TrackStatus");
        $.ajax({
            type: 'post',
            url: ctx + '/FacilityPlant/GetServiceUserDictionary',
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    var sel = "";
                    $.each(data.Data, function (index, item) {
                        if (index === 0)
                            sel = " selected='' ";
                        $("<option value='" + item.CodeText + "' " + sel + ">" + item.CodeText + "</option>").appendTo($('#track_name'));
                    });
                }
                else
                    layer.msg(data.Msg, { icon: 5 });
            }
        });
    },
    //绑定设备编号
    initNumberSel: function () {
        $('#fa_number').empty();
        $("<option value=\"\">空</option>").appendTo($('#fa_number'));
        //绑定设备编号
        $.ajax({
            type: 'post',
            url: ctx + '/TrackRecord/GetNumberDictionary',
            dataType: 'json',
            data: { area_code: selUnit, unit_code: unitCode },
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    $.each(data.Data, function (index, item) {
                        $("<option value='" + item.CodeValue + "'>" + item.CodeText + "</option>").appendTo($('#fa_number'));
                    });
                    $("#fa_number").selectpicker('refresh');
                }
            }
        });
    },
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //添加
        $('#trackAdd').click(function () {
            var area_code = $('#area_code').val(),
                unit_name = $('#unit_name').val(),
                unit_code = $('#unit_code').val();
            if (!area_code || !unit_name || !unit_code) {
                layer.msg("温馨提示：请先正确选择区域下的项目！");
                return false;
            }
            trackRecordManager.editForm();
        });
        //编辑
        $('#trackEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            trackRecordManager.editForm(row[0]);
        });
        //删除
        $('#trackDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选中项目记录吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/TrackRecord/DelTrackByID/' + row[0].tr_id,
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
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "添加项目设备";
        if (row == null || row == "" || row == undefined)
            addOrEditUrl = ctx + "/TrackRecord/AddTrack";//新增
        else {
            $title = "编辑项目设备";
            addOrEditUrl = ctx + "/TrackRecord/EditTrack?tr_id=" + row.tr_id;//编辑
        }
        //清空表单
        $('#orgFormTable').resetHideValidForm();
        //打开编辑
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#org_form'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['480px', '95%'],
            success: function (li, o) {
                if (row != null) {
                    $('#orgFormTable')[0].reset();//重置表单
                    $('#orgFormTable').LoadForm(row);//表单填充数据
                }
            },
            yes: function (li, o) {
                //判断是否为设备跟踪
                var tr_status = $('#tr_status').val();
                if (tr_status && tr_status === "01") {
                    //设备跟踪
                    if (!$('#fa_number').val()) {
                        layer.msg("选择设备跟踪必须录入设备编号！", { icon: 5 });
                        return false;
                    }
                }
                //跟踪人
                if ($('#orgFormTable').valid()) {
                    //验证通过
                    $('#orgFormTable').ajaxSubmit({
                        url: addOrEditUrl,
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