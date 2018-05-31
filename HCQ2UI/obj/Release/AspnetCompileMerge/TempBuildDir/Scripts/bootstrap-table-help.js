
var tableHelp = {
    LoadData: function (DataSoure, queryParams) {
        return $("#" + DataSoure["Contoller"] + "").bootstrapTable({
            url: DataSoure["url"],
            method: "post",
            dataType: "json",
            cache: false,
            height: DataSoure["Height"],
            queryParamsType: "limit",
            detailView: false,
            queryParams: queryParams,
            sidePagination: "server",
            striped: true,
            toolbar: DataSoure["tool"],
            pagination: true,
            contentType: 'application/x-www-form-urlencoded',
            sortable: true,
            pageNumber: 1,
            pageSize: 20,
            pageList: [10, 20, 30, 50],
            showColumns: false,
            paginationFirstText: "首页",
            paginationPreText: '上一页',
            paginationNextText: '下一页',
            paginationLastText: '尾页',
            minimumCountColumns: 2,
            clickToSelect: true,//选择行时选中单选，多选
            strictSearch: true,
            singleSelect: true,//单选
            clickToSelect: true,
            onLoadSuccess: function (data) {
    
            },
            columns: DataSoure["Columns"]
        });
    },
    refresh: function (DataSoure) {
        //刷新table
        $("#" + DataSoure["Contoller"] + "").bootstrapTable("refresh");
    }
};

//人员移动
var tableHelpMove = {
    LoadData: function (DataSoure, queryParams) {
        return $("#" + DataSoure["Contoller"] + "").bootstrapTable({
            url: DataSoure["url"],
            method: "post",
            dataType: "json",
            cache: false,
            height: DataSoure["Height"],
            queryParamsType: "limit",
            detailView: false,
            queryParams: queryParams,
            sidePagination: "server",
            striped: true,
            toolbar: DataSoure["tool"],
            contentType: 'application/x-www-form-urlencoded',
            sortable: true,
            minimumCountColumns: 2,
            clickToSelect: true,//选择行时选中单选，多选
            strictSearch: true,
            singleSelect: false,//单选
            clickToSelect: true,
            onLoadSuccess: function (data) {

            },
            columns: DataSoure["Columns"]
        });
    },
    refresh: function (DataSoure) {
        //刷新table
        $("#" + DataSoure["Contoller"] + "").bootstrapTable("refresh");
    }
};

var ExpendTable = {

    InitExpend: function () {
        /**
         * BootstrapTable的默认设置参数
         */
        //默认get
        $.fn.bootstrapTable.defaults.method = "post";
        //设置为 true 禁用 AJAX 数据缓存 默认：true
        $.fn.bootstrapTable.defaults.cache = false;
        //设置为 true 会有隔行变色效果默认：false
        $.fn.bootstrapTable.defaults.striped = true;
        //设置为 true 会在表格底部显示分页条/默认：false
        $.fn.bootstrapTable.defaults.pagination = true;
        //设置为false 将禁止所有列的排序、默认：true
        $.fn.bootstrapTable.defaults.sortable = true;
        //设置在哪里进行分页，可选值为 'client' 或者 'server'。设置 'server'时，必须设置 服务器数据地址（url）或者重写ajax方法、默认值：client
        $.fn.bootstrapTable.defaults.sidePagination = 'server';
        $.fn.bootstrapTable.defaults.paginationFirstText = '首页';
        $.fn.bootstrapTable.defaults.paginationPreText = '上一页';
        $.fn.bootstrapTable.defaults.paginationNextText = '下一页';
        $.fn.bootstrapTable.defaults.paginationLastText = '尾页';
        $.fn.bootstrapTable.defaultsminimumCountColumns = 2,
        $.fn.bootstrapTable.defaults.queryParamsType = "limit",
        $.fn.bootstrapTable.defaults.dataType = "json",
        $.fn.bootstrapTable.defaults.cache = false,
        //设置true 将在点击行时，自动选择rediobox 和 checkbox、默认值：false
        $.fn.bootstrapTable.defaults.clickToSelect = true;
        //发送到服务器的数据编码类型/默认：application/json、使用默认的后台：request.getParameter获取不到
        $.fn.bootstrapTable.defaults.contentType = 'application/x-www-form-urlencoded';
        //请求服务器数据时的参数
        $.fn.bootstrapTable.defaults.queryParams = function (params) {
            var pageIndex = params.offset / params.limit + 1
            var temp = {
                page: pageIndex, // 页码
                rows: params.limit // 页面大小
            };
            return temp;
        }
    }

}

var Person = {
    Detail: function (PersonID) {
        var index = parent.layer.open({
            type: 2,
            title: "人员详细信息",
            content: $.GetIISName() + "/Person/ArchiveDetail?PersonID=" + PersonID,
            area: ["100%", "100%"],
            maxmin: true
        });
        parent.layer.full(index);
    }
}