
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
            onLoadSuccess: function(data) {
                
            },
            columns: DataSoure["Columns"]
        });
    },
    refresh: function (DataSoure) {
        //刷新table
        $("#" + DataSoure["Contoller"] + "").bootstrapTable("refresh");
    }
};

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