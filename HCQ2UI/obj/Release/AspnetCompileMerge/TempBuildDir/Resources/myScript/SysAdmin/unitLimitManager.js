/*******************************************************
 *  系统设置>单位权限 相关操作js
 * <p>Title: userManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年3月9日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//单位--权限配置管理
var per_id,lintzTree;//权限id
var menuLimitManager = { 
    //初始化zTree
    initzTree: function () {
        per_id = $.getUrls("per_id");
        var setting = {
            view: {
                showIcon: false,
                selectedMulti: false,
                fontCss: getFontCss
            },
            async: {
                enable: true,
                dataType: "text",
                type: "post",
                url: ctx + "/SysLimit/GetUnitTreeData/" + per_id,
                autoParam: ["id"]
            },
            data: {
                key: { title: "name", name: "name" },
                simpleData: { enable: true, idKey: "id", pIdKey: "pId", rootPId: 0 }
            },
            check: {
                enable: true,
                autoCheckTrigger: true,
                chkStyle: "checkbox",
                chkboxType: { "Y": "p", "N": "ps" }
            },
            callback: {
                onAsyncError :function() {
                    layer.msg("初始化单位结构树失败~",{icon:5});
                },
                onClick: function (event, treeId, treeNode) {
                    layer.msg(treeNode.name);
                }
            }
        };
        lintzTree = $("#unitTree").initzTreeView(setting);
    },
    //获取所有被选中的数据
    getMenuData: function () {
        var addData = "", delData = "", reak = "undeal";//默认不需要后端处理
        var treeObj = lintzTree;
        var nodes = treeObj.getCheckedNodes(true);//选中的
        var unodes = treeObj.getCheckedNodes(false);//未选中的
        if (nodes.length > 0) {
            $.each(nodes, function(index, item) {
                if ($(item).attr("everstate") === "uncheck")
                    addData += item.id + ",";
            });
        } else {
            //全部删除，没有选中一个
            reak = "deal";//需要后端处理
            return ";|" + reak;
        }
        if (unodes.length > 0) {
            $.each(unodes, function (index, item) {
                if ($(item).attr("everstate") === "checked")
                    delData += item.id + ",";
            });
        }
        if (addData != "" || delData != "")
            reak = "deal";//需要后端处理
        return addData + ";" + delData + "|" + reak;
    }
}
