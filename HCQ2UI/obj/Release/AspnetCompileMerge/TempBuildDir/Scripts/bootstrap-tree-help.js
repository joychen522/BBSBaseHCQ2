var treeHelp = {
    BindTree: function (treeController, data, treeClick) {
        $("#" + treeController + "").treeview({
            levels: 1,
            color: "#428bca",
            data: data,
            nodeIcon: treeController == "toDotreeview" ? 'glyphicon glyphicon-envelope' : "",
            selectedBackColor: '#2DC1C3',
            onNodeSelected: function (e, o) {
                treeClick(o);
            }
        });
        $("#" + treeController + "").treeview("selectNode", [0, { silent: true }]);
    },
    RefreshTree: function (treeController, data, treeClick) {
        $("#" + treeController + "").treeview({
            levels: 1,
            color: "#428bca",
            selectedBackColor: '#2DC1C3',
            data: data,
            onNodeSelected: function (e, o) {
                treeClick(o);
            }
        })
    }
}

var Z_Tree_Help = {

    Load: function (perType, obj, clickFun) {

        var url = $.GetIISName() + "/Enterprise/GetEnterByUserID";
        if (perType == 0)//权限类别：0 表示加载所有项目。不根据权限
            url = $.GetIISName() + "/Enterprise/GetEnterTree";

        var setting = {
            view: {
                dblClickExpand: true,
                showIcon: Z_Tree_Help.ShowIconFun
            },
            async: {
                enable: true,
                dataType: "json",
                type: "post",
                url: url,
            },
            data: {
                key: { name: "text" }
            },
            callback: {
                onAsyncError: function () {
                    layer.msg("初始化项目结构树失败", { icon: 5 });
                },
                onClick: clickFun
            }
        };
        var zTreeNodes = [
		{
		    "name": "网站导航", open: true, children: [
               { "name": "google", "url": "http://g.cn", "target": "_blank" },
               { "name": "baidu", "url": "http://baidu.com", "target": "_blank" },
               { "name": "sina", "url": "http://www.sina.com.cn", "target": "_blank" }
		    ]
		}
        ];
        return $.fn.zTree.init($("#" + obj + ""), setting);
    },

    ShowIconFun: function (treeId, treeNode) {
        return true;
    }
}

var Lutai = {

    Load: function (shengid, shiid, textid, treeid, type, clickFun) {

        $("#" + shengid + "").css("border", "1").addClass("form-control");
        $("#" + shiid + "").css("border", "1").addClass("form-control");
        $("#" + textid + "").css("border", "0").
            css("border-top", "1px solid #E7EAEC").
            css("border-left", "1px solid #E7EAEC").
            attr("placeholder", "　请输入项目名称");

        var treeObj = null;
        var data = Lutai.Data(type);

        //首次加载
        var i = 0;
        var n = 0;
        $.each(data, function (index, element) {
            if (data[index].type == 1) {
                if (data[index].area_code.length == 2) {
                    $("#" + shengid + "").append("<option value='" + data[index].area_code + "'>" + data[index].text + "</option>");
                    if (i == 0) {
                        $.each(data, function (k, element) {
                            if (data[k].type == 1 && data[k].unit_pid == data[index].area_code) {
                                $("#" + shiid + "").append("<option value='" + data[k].area_code + "'>" + data[k].text + "</option>");
                                if (n == 0) {
                                    treeObj = Lutai.BindTree(data, data[k].area_code, treeid, clickFun, 0);
                                }
                                n++;
                            }
                        })
                    };
                    i++;
                }
            }
        });

        //邦定省联动
        i = 0;
        $("#" + shengid + "").change(function () {
            i = 0;
            var area_code = $(this).val();
            $("#" + shiid + "").empty();
            $.each(data, function (k, element) {
                if (data[k].type == 1 && data[k].unit_pid == area_code) {
                    $("#" + shiid + "").append("<option value='" + data[k].area_code + "'>" + data[k].text + "</option>");
                    if (i == 0)
                        treeObj = Lutai.BindTree(data, data[k].area_code, treeid, clickFun, 1);
                    i++;
                }
            })
        });

        //邦定市联动
        $("#" + shiid + "").change(function () {
            var area_code = $(this).val();
            treeObj = Lutai.BindTree(data, area_code, treeid, clickFun, 1);
        });

        var hiddenNodes = [];
        $("#" + textid + "").keyup(function () {
            var text = $(this).val();
            if (hiddenNodes != null && hiddenNodes != "")
                treeObj.showNodes(hiddenNodes);
            if (treeObj != null && text != null && text != "") {
                var nodes = treeObj.getNodesByParamFuzzy("text", text);
                hiddenNodes = treeObj.getNodesByFilter(filterFunc);
                //alert(JSON.stringify(hiddenNodes));
                treeObj.hideNodes(hiddenNodes);
            }
        });

        function filterFunc(node) {
            var isAcc = true;
            var _keywords = $("#" + textid + "").val();
            if (node.type == 1)
                isAcc = true;
            if (node.isParent || node.text.indexOf(_keywords) != -1)
                isAcc = false;
            return isAcc;
        }

        return treeObj;
    },

    BindTree: function (data, area_code, treeid, clickFun, type) {
        var treeData = [];

        $.each(data, function (index, element) {

            //正常情况
            if (data[index].type == 1 && data[index].unit_pid == area_code) {
                treeData.push({ "text": data[index].text, "type": 1, "unit_id": 0, "id": index, "pid": 0, "area_code": data[index].area_code });
                //查找该区域下面的项目
                $.each(data, function (k, element) {
                    if (data[k].type == 2 && data[k].area_code == data[index].area_code) {
                        treeData.push({ "text": data[k].text, "type": 2, "unit_id": data[k].unit_id, "id": k + 999, "pid": index, "area_code": data[k].area_code });
                    }
                })
            };

            //特殊情况，项目所属地区选择的省市
            if (data[index].type == 2 && data[index].area_code == area_code.substr(0, 2)) {
                treeData.push({ "text": data[index].text, "type": 2, "unit_id": data[index].unit_id, "id": index + 9999, "pid": 0, "area_code": data[index].area_code });
            }
            if (data[index].type == 2 && data[index].area_code == area_code) {
                treeData.push({ "text": data[index].text, "type": 2, "unit_id": data[index].unit_id, "id": index + 99999, "pid": 0, "area_code": data[index].area_code });
            }
        });

        var setting = {
            view: {
                dblClickExpand: true,
                showIcon: false
            },
            data: {
                key: { name: "text" },
                simpleData: {
                    enable: true, idKey: "id", pIdKey: "pid", rootPId: 0
                }
            },
            callback: {
                onAsyncError: function () {
                    layer.msg("初始化项目结构树失败", { icon: 5 });
                },
                onClick: clickFun
            }
        };

        if (type == 1)
            $.fn.zTree.getZTreeObj(treeid).destroy();
        var obj = $.fn.zTree.init($("#" + treeid + ""), setting, treeData);

        obj.expandAll(true);

        return obj;
    },

    Data: function (type) {
        var data = null;
        $.ajax({
            type: "post",
            dataType: "json",
            url: $.GetIISName() + "/Enterprise/GetTreeList",
            data: { type: type },
            async: false,
            success: function (result) {
                data = result;
            }
        });
        return data;
    }
}