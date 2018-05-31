/*******************************************************
 *  微信题库 相关操作js
 * <p>Title: baneScoreManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年12月22日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//微信题库管理
var $table, addOrEditUrl;
var baneScoreManager = {
    //初始化页面
    initPage: function () {
        parent.showLoadBoxs();
        baneScoreManager.initTable();
        baneScoreManager.bindEvent();
        parent.delLoadBoxs();
    },
    //初始化Table
    initTable: function () {
        var options = {
            url: ctx + "/BaneChat/GetBaneScoreData",
            cutHeight: 5,
            showRefresh: false,
            showToggle: false,
            showColumns: false,
            toolbar: "#exampleToolbar",
            queryParams: function (params) {
                params = {
                    rows: params.limit,
                    //页面大小  
                    page: params.offset / params.limit + 1,
                    keyword: encodeURI($('#scoreName').val()) //标题查询
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
	            field: 'sub_title',
	            title: '标题',
	            align: 'center'
	        },
	        {
	            field: 'sub_value',
	            title: '答案选项',
	            align: 'center'
	        },
	        {
	            field: 'sub_score',
	            title: '分数',
	            align: 'center'
	        },
	        {
	            field: 'sub_note',
	            title: '备注',
	            align: 'center'
	        }],
            onClickRow: function (row, $element) {

            }
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
        $('#scoreAdd').click(function () {
            baneScoreManager.editForm();
        });
        //编辑
        $('#scoreEdit').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中行~", { icon: 5 });
                return false;
            }
            baneScoreManager.editForm(row[0]);
        });
        //删除
        $('#scoreDel').click(function () {
            var row = $table.bootstrapTable('getSelections');
            if (row != null && row.length == 0) {
                layer.msg("未选中记录行~", { icon: 5 });
                return false;
            }
            layer.confirm('您确定要删除当前选择用户？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                var index = layer.msg("删除中...", { icon: 6, time: 6000 });
                $.ajax({
                    type: 'post',
                    url: ctx + '/BaneUser/DelBaneUserById',
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
        //添加，减少题库操作
        //添加选项
        $('#answer_plus').on('click', function () {
            var len = $('#lab_value').children('div').size() + 1;
            var option_title=null;
            switch (len) {
                case 5: option_title="E"; break;
                case 6: option_title="F";break;
                case 7: option_title="G"; break;
                case 8: option_title="I";break;
            }
            if(!option_title){
                layer.msg("添加备选项错误，备选项最多8个~");
                return false;
            }
            //添加备选项
            $("<div class='form-group' id='lab_value" + len + "'><label>" + option_title + "</label><textarea class='form-control' id='sel" + len + "' name='sel" + len + "' rows='2' placeholder='请输入选项描述' required='' aria-required='true'></textarea></div>").appendTo($('#lab_value'));
            //添加答案选项
            $("<option tabindex='" + len + "' value='" + option_title + "'>" + option_title + "</option>").appendTo($('#sub_value'));
            $('#sub_value').selectpicker('refresh');
        });
        //减少选项
        $('#answer_minus').on('click', function () {
            var len = $('#lab_value').children('div').size();
            if (len <= 4) {
                layer.msg("最少必须有4个备选项~");
                return false;
            }
            //移除备选项
            $('#lab_value' + len).remove();
            //移除答案
            $('#sub_value').children("option[tabindex='" + len + "']").remove();
            $('#sub_value').selectpicker('refresh');
        });
    },
    //编辑、添加
    editForm: function (row) {
        var $title = "添加题库", addOrEditUrl = ctx + "/BaneChat/AddScore";
        if (row) {
            $title = "编辑题库";
            addOrEditUrl = ctx + "/BaneChat/EditScore";
        }
        $('#groupFormTable').resetHideValidForm();
        layer.open({
            title: [$title, 'font-size:18px;'],
            type: 1,
            content: $('#groupForm'),
            scroll: true,//是否显示滚动条、默认不显示
            btn: ['确定', '取消'],
            area: ['600px', '98%'],
            success: function (li, o) {
                //1. 清理多余选项
                var len = $('#lab_value').children('div').size();
                while (len > 4) {
                    //移除备选项
                    $('#lab_value' + len).remove();
                    //移除答案
                    $('#sub_value').children("option[tabindex='" + len + "']").remove();
                    $('#sub_value').selectpicker('refresh');
                    len--;
                }
                //2. 填充数据
                if (row) {
                    $('#groupFormTable')[0].reset();//重置表单
                    $('#groupFormTable').LoadForm(row);//表单填充数据
                    $.ajax({
                        type: 'post',
                        url: ctx + '/BaneChat/GetQuestionValue/'+row.sub_id,
                        dataType: 'json',
                        async: false,
                        success: function (data) {
                            if (data.Statu === 0) {
                                $.each(data.Data, function (index,item) {
                                    switch (item.score_option.toLowerCase()) {
                                        case "a": { $('#sel1').val(item.score_value); $('#sel1_value').val(item.score_id); } break;
                                        case "b": { $('#sel2').val(item.score_value); $('#sel2_value').val(item.score_id); } break;
                                        case "c": { $('#sel3').val(item.score_value); $('#sel3_value').val(item.score_id); } break;
                                        case "d": { $('#sel4').val(item.score_value); $('#sel4_value').val(item.score_id); } break;
                                        case "e": {
                                            //添加备选项
                                            $("<div class='form-group' id='lab_value5'><label>E</label><textarea class='form-control' id='sel5' name='sel5' rows='2' placeholder='请输入选项描述' required='' aria-required='true'></textarea><input type='hidden' id='sel5_value' value='" + item.score_id + "' /></div>").appendTo($('#lab_value'));
                                            $('#sel5').val(item.score_value);
                                            //添加答案选项
                                            $("<option tabindex='5' value='E'>E</option>").appendTo($('#sub_value'));
                                            $('#sub_value').selectpicker('refresh');
                                        } break;
                                        case "f": { //添加备选项
                                            $("<div class='form-group' id='lab_value6'><label>F</label><textarea class='form-control' id='sel6' name='sel6' rows='2' placeholder='请输入选项描述' required='' aria-required='true'></textarea><input type='hidden' id='sel6_value' value='" + item.score_id + "' /></div>").appendTo($('#lab_value'));
                                            $('#sel6').val(item.score_value);
                                            //添加答案选项
                                            $("<option tabindex='6' value='F'>F</option>").appendTo($('#sub_value'));
                                            $('#sub_value').selectpicker('refresh');
                                        } break;
                                        case "g": {
                                            $("<div class='form-group' id='lab_value7'><label>G</label><textarea class='form-control' id='sel7' name='sel7' rows='2' placeholder='请输入选项描述' required='' aria-required='true'></textarea><input type='hidden' id='sel7_value' value='" + item.score_id + "' /></div>").appendTo($('#lab_value'));
                                            $('#sel7').val(item.score_value);
                                            //添加答案选项
                                            $("<option tabindex='7' value='G'>G</option>").appendTo($('#sub_value'));
                                            $('#sub_value').selectpicker('refresh');
                                        } break;
                                        case "i": {
                                            $("<div class='form-group' id='lab_value8'><label>I</label><textarea class='form-control' id='sel8' name='sel8' rows='2' placeholder='请输入选项描述' required='' aria-required='true'></textarea><input type='hidden' id='sel8_value' value='" + item.score_id + "' /></div>").appendTo($('#lab_value'));
                                            $('#sel8').val(item.score_value);
                                            //添加答案选项
                                            $("<option tabindex='8' value='I'>I</option>").appendTo($('#sub_value'));
                                            $('#sub_value').selectpicker('refresh');
                                        } break;
                                    }
                                });
                                //设置答案项
                                $('#sub_value').selectpicker('val', row.sub_value.toUpperCase());
                            }
                            else
                                layer.msg(data.Msg, { icon: 5 });
                        }
                    });
                }
            },
            yes: function (li, o) {
                var temp = $('#lab_value').children('div'), options_value = "";
                if (row)
                    for (var i = 0; i < temp.size() ; i++)
                        options_value += ((!$(temp.eq(i)).children('input').val()) ? "" : $(temp.eq(i)).children('input').val()) + "∫" + $(temp.eq(i)).children('label').html() + "∬" + $(temp.eq(i)).children('textarea').val() + "∭";
                else 
                    for (var i = 0; i < temp.size() ; i++)
                        options_value += $(temp.eq(i)).children('label').html() + "∬" + $(temp.eq(i)).children('textarea').val() + "∭";
                if ($('#groupFormTable').valid()) {
                    //验证通过
                    $('#groupFormTable').ajaxSubmit({
                        url: addOrEditUrl,
                        type: "post",
                        dataType: "json",
                        data: { options_value: options_value },
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