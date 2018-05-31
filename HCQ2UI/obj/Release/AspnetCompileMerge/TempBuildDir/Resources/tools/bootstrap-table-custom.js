/*******************************************************
 *  table管理类
 * <p>Title: tableHelper.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2016年12月28日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var $tableInfo, //表格
    $queryHeight;//设置Table高度时需要减去的高度
var tableHelper = {
    //重置Table高度
    resetTableHeight:function() {
        return $(window).height() - $queryHeight;
    },
    //初始化Table
    initTable: function (id, options) {
        /// <summary>Table初始化函数</summary>     
        /// <param name="id" type="String">table ID</param>        
        /// <param name="options" type="int">需要初始化的数据</param>
        if (id == null || id == "" || id == undefined)
            return false;
        var defaultOption = {
            url: '',
            contentType: 'application/x-www-form-urlencoded',
            dataType: "json",
            method: 'post',
            search: 0,
            showRefresh: !0,
            showToggle: !0,
            showColumns: !0,
            iconSize: "outline",
            pagination: true,//启用分页
            queryParamsType: "limit",
            striped: true,             //是否显示行间隔色
            smartDisplay: false,
            sortable: true,//是否启用排序
            sortOrder: "asc",          //排序方式
            sidePagination: "server",
            uniqueId: "id",
            pageNumber: 1,
            pageSize: 10,
            pageList: [10, 30, 50, 100,1000],
            paginationFirstText: '首页',
            paginationPreText: '上一页',
            paginationNextText: '下一页',
            paginationLastText: '尾页',
            clickToSelect: true,//选择行时选中单选，多选
            strictSearch: true,
            singleSelect: true,//单选
            cutHeight: 5,//默认需呀减去的高度height: userManager.resetTableHeight(),//窗口改变时重置高度
            cardView: false,           //是否显示详细视图
            detailView: false,         //是否显示父子表
            minimumCountColumns: 2,
            queryParams:'',
            toolbar: '',
            showExport: false,                     //是否显示导出
            exportDataType: "basic",          //basic', 'all', 'selected'.
            exportTypes: ['json', 'xml', 'csv', 'txt', 'sql', 'excel'],//导出类型 'json', 'xml', 'png', 'csv', 'txt', 'sql', 'doc', 'excel', 'xlsx', 'pdf'
            columns: '',
            icons: {
                refresh: "glyphicon-repeat",
                toggle: "glyphicon-list-alt",
                columns: "glyphicon-list",
                export: 'glyphicon-export'
            },
            formatLoadingMessage:null,
            onLoadSuccess: null,
            onClickRow:null
        }
        if (options != null) {
            for (p in options) {
                defaultOption[p] = options[p];
            }
        }
        $queryHeight = defaultOption.cutHeight;
        $tableInfo = $("#" + id).bootstrapTable({
            url: defaultOption.url,
            contentType: defaultOption.contentType,
            dataType: defaultOption.dataType,
            method: defaultOption.method,
            search: defaultOption.search,
            showRefresh: defaultOption.showRefresh,
            showToggle: defaultOption.showToggle,
            showColumns: defaultOption.showColumns,
            iconSize: defaultOption.iconSize,
            pagination: defaultOption.pagination,
            queryParamsType: defaultOption.queryParamsType,
            striped: defaultOption.striped,             //是否显示行间隔色
            smartDisplay: defaultOption.smartDisplay,
            sortable: defaultOption.sortable,//是否启用排序
            sortOrder: defaultOption.sortOrder,          //排序方式
            sidePagination: defaultOption.sidePagination,
            uniqueId: defaultOption.uniqueId,
            pageNumber: defaultOption.pageNumber,
            pageSize: defaultOption.pageSize,
            pageList: defaultOption.pageList,
            paginationFirstText: defaultOption.paginationFirstText,
            paginationPreText: defaultOption.paginationPreText,
            paginationNextText: defaultOption.paginationNextText,
            paginationLastText: defaultOption.paginationLastText,
            clickToSelect: defaultOption.clickToSelect,//选择行时选中单选，多选
            strictSearch: defaultOption.strictSearch,
            singleSelect: defaultOption.singleSelect,//单选
            cutHeight: defaultOption.cutHeight,//默认需要减去的高度
            cardView: defaultOption.cardView,           //是否显示详细视图
            detailView: defaultOption.detailView,         //是否显示父子表
            minimumCountColumns: defaultOption.minimumCountColumns,
            queryParams: defaultOption.queryParams,
            toolbar: defaultOption.toolbar,
            showExport: defaultOption.showExport,
            exportDataType: defaultOption.exportDataType,
            columns: defaultOption.columns,
            height: tableHelper.resetTableHeight(),
            icons: defaultOption.icons,
            formatLoadingMessage: function () {
                if (defaultOption.formatLoadingMessage != null)
                    defaultOption.formatLoadingMessage();
            },
            onLoadSuccess: function (data) {
                if (defaultOption.onLoadSuccess != null)
                    defaultOption.onLoadSuccess(data);
            },
            onClickRow: function (row, $element) {
                if (defaultOption.onClickRow != null)
                    defaultOption.onClickRow(row, $element);
            }
        });
        return $tableInfo;
    }
}
//改变窗口大小时更改table高度
$(window).resize(function () {
    if ($tableInfo) {
        $tableInfo.bootstrapTable('resetView', {
            height: tableHelper.resetTableHeight()
        });
    }
});