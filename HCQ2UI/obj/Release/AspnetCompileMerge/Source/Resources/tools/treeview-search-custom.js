/*******************************************************
 *  treeview管理类
 * <p>Title: treeviewHelper.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年2月17日 下午5:41:57
 * @version 1.0
 * *****************************************************/
; (function ($) {
    //初始化树，带查询功能
    $.fn.initTreeView = function (options) {
        var This = this;//当前div元素
        var unitId = $(This).attr("id") + "unitTreeview";
        var $selectableTree = null;
        var defaultOption = {
            checkFirst:false,//是否默认选中第一个节点
            title: '选择单位',//提示信息
            showSearch: true,//是否显示搜索框
            showCheckbox: false,//是否显示checkbox框
            width: 'col-sm-2', //col-sm-2 -10
            data: null,
            levels: 1,
            color: "#428bca",
            highlightSelected: true, //是否高亮选中
            //nodeIcon: 'glyphicon glyphicon-globe',
            emptyIcon: '', //没有子节点的节点图标
            onNodeSelected: null,
            onNodeUnchecked: null,
            onNodeUnselected: null,
            onNodeChecked: null  
        }
        if (options != null) {
            for (p in options) {
                defaultOption[p] = options[p];
            }
        }
        $(This).addClass(defaultOption.width + " autoHeight left-sm-3");
        $(This).css("padding-right", "0");
        var title = "<input id=\"selRow\" class=\"selUnit\" type=\"text\" placeholder=\"输入名称查询\" />";
        if (!defaultOption.showSearch)
            title = "<h5>" + defaultOption.title + "</h5>";
        $("<div class=\"ibox float-e-margins float-unit\">" +
            "<div class=\"ibox-title\" style=\"border-width: 0;padding:10px 5px 7px;\">" + title + "</div>" +
            "<div class=\"ibox-unit\">" + ///ibox-content 
            "<div id=\"" + unitId + "\" class=\"test\"></div>" +
            "</div>" +
            "</div>").appendTo($(This));
        $("#" + unitId).empty();//清空
        $selectableTree = $('#' + unitId).treeview({
            data: defaultOption.data,
            levels: defaultOption.levels,
            color: defaultOption.color,
            highlightSelected: true, //是否高亮选中
            showCheckbox:defaultOption.showCheckbox,//是否显示checkbox
            nodeIcon: defaultOption.nodeIcon,
            emptyIcon: '', //没有子节点的节点图标glyphicon 
            onNodeChecked: function (event, node) {
                if (defaultOption.onNodeChecked != null)
                    defaultOption.onNodeChecked(event, node);
            },
            onNodeUnchecked: function (event, node) {
                if (defaultOption.onNodeUnchecked != null)
                    defaultOption.onNodeUnchecked(event, node);
            },
            onNodeSelected: function (event, node) {
                if (defaultOption.onNodeSelected != null)
                    defaultOption.onNodeSelected(event, node);
            },
            onNodeUnselected: function (event, node) {
                if (defaultOption.onNodeUnselected != null)
                    defaultOption.onNodeUnselected(event, node);
            }
        });
        var findSelectableNodes = function () {
            return $selectableTree.treeview('search', [$('#selRow').val(), { ignoreCase: false, exactMatch: false }]);
        };
        var selectableNodes = findSelectableNodes;
        //绑定搜索框事件
        $('#selRow').keyup(function () {
            $("#" + unitId).empty();//清空
            selectableNodes = findSelectableNodes();
        });
        //默认选择第一个目录树
        //if (defaultOption.checkFirst) 
        //    $('.list-group li').first().css({ color: "#ffffff", background: "#18A689" });
        return $selectableTree;
    }
    //初始化单位树，带查询功能
    $.fn.initB01TreeView = function (options) {
        var This = this;
        var $selectableTree;
        if (options.hasOwnProperty("data") && options.data != null)
            $selectableTree = This.initTreeView(options);
        else {
            $.ajax({
                url: ctx + '/DebtStatistics/GetUnitInfo',
                type: "post",
                async: false,
                dataType: 'json',
                success: function(mess) {
                    if (mess.Statu === 0) {
                        options.data = mess.Data;
                        $selectableTree = This.initTreeView(options);
                    } else
                        layer.open({
                            shade: false,
                            title: false,
                            content: mess.Msg,
                            btn: ''
                        });
                },
                error: function() {
                    layer.msg('数据异常~', { icon: 5 });
                }
            });
        }
        return $selectableTree;
    }
})($);


