/*******************************************************
 *  系统设置>区域权限 相关操作js
 * <p>Title: areaManager.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年10月11日 下午5:41:57
 * @version 1.0
 * *****************************************************/
//区域--权限配置管理
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
                url: ctx + "/SysLimit/GetAreaTreeData/" + per_id,
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
                    layer.msg("初始化区域结构树失败~",{icon:5});
                },
                onClick: function (event, treeId, treeNode) {
                    layer.msg(treeNode.name);
                }
            }
        };
        lintzTree = $("#areaTree").initzTreeView(setting);
    },
    //获取所有被选中的数据
    getMenuData: function () {
        var reak = "undeal";//默认不需要后端处理
        var addAreaData = "", addPersonData = "", delAreaData = "", delPersonData = "";
        var treeObj = lintzTree;
        var nodes = treeObj.getCheckedNodes(true);//选中的
        var unodes = treeObj.getCheckedNodes(false);//未选中的
        if (nodes.length > 0) {
            $.each(nodes, function (index, item) {
                var temp = $(item);
                if (temp.attr("tree_type") === "area") {
                    if (temp.attr("everstate") === "uncheck")
                        //1.1 区域
                        addAreaData += item.id + ",";
                }
                else if (temp.attr("tree_type") === "person") {
                    if (temp.attr("everstate") === "uncheck")
                        //1.2 人员
                        addPersonData += item.pId + "[" + item.id + ",";
                }
            });
        } else {
            //全部删除，没有选中一个
            reak = "deal";//需要后端处理
            return ":|" + reak;
        }
        if (unodes.length > 0) {
            $.each(unodes, function (index, item) {
                var temp = $(item);
                if (temp.attr("tree_type") === "area") {
                    if (temp.attr("everstate") === "checked")
                        //1.1 区域
                        delAreaData += item.id + ",";
                }
                else if (temp.attr("tree_type") === "person") {
                    if (temp.attr("everstate") === "checked")
                        //1.2 人员
                        delPersonData += item.id + ",";
                }
            });
        }
        if (addAreaData || addPersonData || delAreaData || delPersonData)
            reak = "deal";//需要后端处理
        return addAreaData + ";" + addPersonData + ":" + delAreaData + ";" + delPersonData + "|" + reak;
        //return addData + ";" + delData + "|" + reak;  addPersonData：1[21,2[22
    }
}
