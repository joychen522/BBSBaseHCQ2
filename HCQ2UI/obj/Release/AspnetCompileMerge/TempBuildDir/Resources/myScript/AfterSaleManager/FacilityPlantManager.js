/*******************************************************
 * 项目设备 相关操作js
 * <p>Title:FacilityPlantManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年10月16日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $table, addOrEditUrl, selUnit = null,
    unitName, unitCode;//项目名称，项目代码
var facilityPlantManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        facilityPlantManager.initUnitData();
        facilityPlantManager.initTable();
        facilityPlantManager.bindEvent();
        facilityPlantManager.initSelect();
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
            $table.bootstrapTable('refresh');
        });
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/FacilityPlant/InitTable",
            cutHeight: 5,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    area_code: (selUnit === null) ? "" : selUnit,//区域代码
                    unit_code: unitCode,//所选项目代码
                    facityStatus: encodeURI($('#facityStatus').val()),//设备状态
                    installName: encodeURI($('#installName').val()),//安装人
                    skillName: encodeURI($('#skillName').val())//技术支持
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
                align: 'center',
                formatter: function (value, row, index) {
                    if (!value)
                        return "";
                    var title = "";
                    if (row.deviceid)
                        title = " title='温馨提示：当前设备编码不属于系统设备！' ";
                    return "<a " + title + " onclick=\"facilityPlantManager.openTrackRecord('" + row.unit_code + "','" + row.fa_number + "');\">" + value + "</a>";
                }
            },
            {
                field: 'fa_status_text',
                title: '设备状态',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value && value === "正常")
                        return "<font color='green'>" + value + "</font>";
                    return "<font color='red'>" + value + "</font>";
                }
            },
            {
                field: 'unit_name',
                title: '所在项目',
                align: 'center'
            },
            {
                field: 'tail_after',
                title: '跟踪过',
                align: 'center',
                formatter: function (value, row, index) {
                    if (value)
                        return value + "天";
                    return "";
                }
            },
            {
                field: 'touch_name',
                title: '联系人',
                align: 'center'
            },
            {
                field: 'touch_phone',
                title: '联系电话',
                align: 'center'
            },
            {
                field: 'install_name',
                title: '安装人',
                align: 'center'
            },
            {
                field: 'install_date',
                title: '安装时间',
                align: 'center'
            },
             {
                 field: 'skiller',
                 title: '技术支持',
                 align: 'center'
             },
           {
               field: 'user_note',
	            title: '备注',
	            align: 'center'
	        }],
            onClickRow: function (row, $element) {
            }
        }
        $table = tableHelper.initTable("exampleTableToolbar", options);
    },
    //打开设备跟踪
    openTrackRecord: function (unit_code, fa_number) {
        $.ajax({
            type: 'post',
            url: ctx + '/TrackRecord/GetTrackObjByID',
            dataType: 'json',
            async: false,
            data: { unit_code: unit_code, fa_number: fa_number },
            success: function (data) {
                if (data.Statu != 0) {
                    layer.msg(data.Msg, { icon: 5 });
                    return false;
                }
                var row = data.Data;
                //清空表单
                $('#trackFormTable').resetHideValidForm();
                layer.open({
                    title: ['设备跟踪详细信息', 'font-size:18px;'],
                    type: 1,
                    content: $('#track_form'),
                    scroll: true,//是否显示滚动条、默认不显示
                    btn: ['确定', '取消'],
                    area: ['480px', '430px'],
                    success: function (li, o) {
                        if (row != null) {
                            $('#trackFormTable')[0].reset();//重置表单
                            $('#trackFormTable').LoadForm(row);//表单填充数据
                        }
                    }
                });
            }
        });
    },
    //初始化下拉
    initSelect: function () {
        $('#tr_status').initSelectpicker("TrackStatus");
        $.ajax({
            type: 'post',
            url: ctx + '/FacilityPlant/GetServiceUserDictionary',
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.Statu === 0) {
                    var sel = "";
                    $.each(data.Data, function (index, item) {
                        $("<option value='" + item.CodeValue + "' hassubinfo=\"true\">" + item.CodeText + "</option>").appendTo($('#installID'));
                        $("<option value='" + item.CodeValue + "' hassubinfo=\"true\">" + item.CodeText + "</option>").appendTo($('#skillerID'));
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
    //绑定默认事件
    bindEvent: function () {
        //查询
        $('#btnSearch').click(function () {
            $table.bootstrapTable('refresh');
        });
        //添加
        $('#facityAdd').click(function () {
            var area_code = $('#area_code').val(),
                unit_name = $('#unit_name').val(),
                unit_code = $('#unit_code').val();
            if (!area_code || !unit_name || !unit_code) {
                layer.msg("温馨提示：请先正确选择区域下的项目！");
                return false;
            }
            facilityPlantManager.editForm();
        });
        //编辑
        $('#facityEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            facilityPlantManager.editForm(row[0]);
        });
        //删除
        $('#facityDel').click(function () {
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
                    url: ctx + '/FacilityPlant/DelFacilityByID/' + row[0].fp_id,
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
            addOrEditUrl = ctx + "/FacilityPlant/AddFacility";//新增
        else {
            $title = "编辑项目设备";
            addOrEditUrl = ctx + "/FacilityPlant/EditFacility?fp_id=" + row.fp_id;//编辑
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
            area: ['480px', '98%'],
            success: function (li, o) {
                if (row) {
                    $('#orgFormTable')[0].reset();//重置表单
                    $('#orgFormTable').LoadForm(row);//表单填充数据
                    if (row.install_id)
                        $('#installID').selectpicker('val', row.install_id.split(','));
                    if (row.skiller_id)
                        $('#skillerID').selectpicker('val', row.skiller_id.split(','));
                }
            },
            yes: function (li, o) {
                var install_id = $('#installID').val(), skiller_id = $('#skillerID').val();
                var strID = "", strName = "";
                if (install_id) {
                    var installID = $('#installID')[0];
                    //安装人
                    for (var i = 0; i < install_id.length; i++) {
                        for (var j = 0; j < installID.length; j++) {
                            if (install_id[i] === installID[j].value) {
                                strID += installID[j].text + ",";
                                strName += installID[j].value + ",";
                                continue;
                            }
                        }
                    }
                    if (strID)
                        $('#install_name').val(strID.substr(0, strID.length - 1));
                    if (strName)
                        $('#install_id').val(strName.substr(0, strName.length - 1));
                }
                if (skiller_id) {
                    var skillerID = $('#skillerID')[0];
                    strID = ""; strName = "";
                    //技术支持
                    for (var i = 0; i < skiller_id.length; i++) {
                        for (var j = 0; j < skillerID.length; j++) {
                            if (skiller_id[i] === skillerID[j].value) {
                                strID += skillerID[j].text + ",";
                                strName += skillerID[j].value + ",";
                                continue;
                            }
                        }
                    }
                    if (strID)
                        $('#skiller').val(strID.substr(0, strID.length - 1));
                    if (strName)
                        $('#skiller_id').val(strName.substr(0, strName.length - 1));
                }
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