/*******************************************************
 *  search_zTree管理类
 * <p>Title: tsearch_zTree.js</p>
 * <p>Description:TODO</p>
 * @author Joychen
 * @date 2017年2月17日 下午5:41:57
 * @version 1.0
 * *****************************************************/
var zTree;
; (function ($) {
    //初始化zTree树
    $.fn.initzTreeView = function(options,btnShow) {
        var This = this;//当前div元素
        zTree = $(This).attr("id") + "zTreeData";
        var defaultOption = {
            view: {
                showIcon: false,
                selectedMulti: false,
                fontCss: {}
            },
            async: {
                enable: true,
                dataType: "text",
                type: "post",
                url: '',
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
                onAsyncSuccess: function (event, treeId, treeNode,msg) {
                },
                onClick: function (event, treeId, treeNode) {
                    layer.msg(treeNode.name);
                }
            }
        };
        if (options != null) {
            for (p in options) {
                defaultOption[p] = options[p];
            }
        }
        $(This).empty();
        var sear_width = "99%";
        if (btnShow != "ushow")
            sear_width = "70%";
        var search = "<div class=\"zTreeDemoBackground\" style=\"width: 100%;height:100%;\">" +
            "<input id=\"search_condition\" type=\"text\" class=\"form-control myzTree-search\" placeholder=\"请输入搜索条件\" value=\"\"  style=\"font-size: 14px; float: left; width: " + sear_width + "; margin: 1px;\" />";
        if (btnShow!="ushow") {
            search += "<button id=\"btnSelTree\" class=\"btn btn-primary\" style=\"margin: 1px;\" checkType=\"\" type=\"button\" onclick=\" setSelAllOption('" + zTree + "',this) \"><i class=\"fa fa-check\"></i>全部选择</button>";
        }
        search += "<ul id=" + zTree + " class=\"ztree\" style=\"width: 100%;height:90%;overflow-y:auto; margin-top: 0;\"></ul></div>";
        $(search).appendTo($(This));
        return  $.fn.zTree.init($("#" + zTree), defaultOption);
    }
})($);

var keyword = "";
$(function () {
    //给动态创建的查询框绑定鼠标弹起事件，获取当前文本框输入值
    $(document).on('keyup', '.myzTree-search', function () {
        keyword = $(this).val();
        search_ztree(zTree, 'search_condition');
    });
});
    /**
	 * 展开树
	 * @param treeId  
	 */
    function expand_ztree(treeId){
        var treeObj = $.fn.zTree.getZTreeObj(treeId);
        treeObj.expandAll(true);
    }
    
    /**
	 * 收起树：只展开根节点下的一级节点
	 * @param treeId
	 */
    function close_ztree(treeId){
        var treeObj = $.fn.zTree.getZTreeObj(treeId);
        var nodes = treeObj.transformToArray(treeObj.getNodes());
        var nodeLength = nodes.length;
        for (var i = 0; i < nodeLength; i++) {
            if (nodes[i].id == '0') {
                //根节点：展开
                treeObj.expandNode(nodes[i], true, true, false);
            } else {
                //非根节点：收起
                treeObj.expandNode(nodes[i], false, true, false);
            }
        }
    }
    
    /**
     * 搜索树，高亮显示并展示【模糊匹配搜索条件的节点s】
     * @param treeId
	 * @param searchConditionId 文本框的id
     */
	function search_ztree(treeId, searchConditionId){
		searchByFlag_ztree(treeId, searchConditionId, "");
	}
    
    /**
     * 搜索树，高亮显示并展示【模糊匹配搜索条件的节点s】
     * @param treeId
     * @param searchConditionId		搜索条件Id
     * @param flag 					需要高亮显示的节点标识
     */
	function searchByFlag_ztree(treeId, searchConditionId, flag){
	    //<1>.搜索条件
	    var searchCondition = keyword;//$('#' + searchConditionId).val();
		//<2>.得到模糊匹配搜索条件的节点数组集合
		var highlightNodes = new Array();
		if (searchCondition != "") {
			var treeObj = $.fn.zTree.getZTreeObj(treeId);
			highlightNodes = treeObj.getNodesByParamFuzzy("name", searchCondition, null);
		}
		//<3>.高亮显示并展示【指定节点s】
		highlightAndExpand_ztree(treeId, highlightNodes, flag);
	}
	
	/**
	 * 高亮显示并展示【指定节点s】
	 * @param treeId
	 * @param highlightNodes 需要高亮显示的节点数组
	 * @param flag			 需要高亮显示的节点标识
	 */
	function highlightAndExpand_ztree(treeId, highlightNodes, flag){
		var treeObj = $.fn.zTree.getZTreeObj(treeId);
		//<1>. 先把全部节点更新为普通样式
		var treeNodes = treeObj.transformToArray(treeObj.getNodes());
		for (var i = 0; i < treeNodes.length; i++) {
			treeNodes[i].highlight = false;
			treeObj.updateNode(treeNodes[i]);
		}
		//<2>.收起树, 只展开根节点下的一级节点
		//close_ztree(treeId);
		//<3>.把指定节点的样式更新为高亮显示，并展开
		if (highlightNodes != null) {
			for (var i = 0; i < highlightNodes.length; i++) {
				if (flag != null && flag != "") {
					if (highlightNodes[i].flag == flag) {
						//高亮显示节点，并展开
						highlightNodes[i].highlight = true;
						treeObj.updateNode(highlightNodes[i]);
						//高亮显示节点的父节点的父节点....直到根节点，并展示
						var parentNode = highlightNodes[i].getParentNode();
						var parentNodes = getParentNodes_ztree(treeId, parentNode);
						treeObj.expandNode(parentNodes, true, false, true);
						treeObj.expandNode(parentNode, true, false, true);
					}
				} else {
					//高亮显示节点，并展开
					highlightNodes[i].highlight = true;
					treeObj.updateNode(highlightNodes[i]);
				    //高亮显示节点的父节点的父节点....直到根节点，并展示
					var parentNode = highlightNodes[i].getParentNode();
					var parentNodes = getParentNodes(treeId, parentNode);
					treeObj.expandNode(parentNodes, true, false, true);
					treeObj.expandNode(parentNode, true, false, true);
				}
			}
		}
	}

	function getParentNodes(treeId, node) {
	    var nodes = [];
	    while (node != null) {
	        nodes.push(node);
	        node = node.getParentNode();
	    }
	    return nodes;
	}	

	/**
	 * 递归得到指定节点的父节点的父节点....直到根节点
	 */
	function getParentNodes_ztree(treeId, node){
		if (node != null) {
			var treeObj = $.fn.zTree.getZTreeObj(treeId);
			var parentNode = node.getParentNode();
			return getParentNodes_ztree(treeId, parentNode);
		} else {
			return node;
		}
	}
	
	/**
	 * 设置树节点字体样式
	 */
	function setFontCss_ztree(treeId, treeNode) {
		if (treeNode.id === 0) {
			//根节点
		    return { color: "#333", "font-weight": "bold" };
		} else if (treeNode.isParent === false){
			//叶子节点
			return (!!treeNode.highlight) ? {color:"#ff0000", "font-weight":"bold"} : {color:"#660099", "font-weight":"normal"};
		} else {
			//父节点
			return (!!treeNode.highlight) ? {color:"#ff0000", "font-weight":"bold"} : {color:"#333", "font-weight":"normal"};
		}
	}
	/**
    *   全选/全反选树
    */
	function setSelAllOption(treeId,obj) {
	    var treeObj = $.fn.zTree.getZTreeObj(treeId);
	    if ($(obj).attr("checkType")==="" || $(obj).attr("checkType") === "onSelect") {
	        treeObj.checkAllNodes(obj); //false
	        $(obj).attr("checkType", "Select");
	        $(obj).html("<i class=\"fa fa-close\"></i>全部取消");//全部取消
	    } else {
	        treeObj.checkAllNodes(false); //true
	        $(obj).attr("checkType", "onSelect");
	        $(obj).html("<i class=\"fa fa-check\"></i>全部选择");//全部选择
	    }
	}
    /**
    *   设置查询结构树样式
    */
    function getFontCss(treeId, treeNode) {
	    return (!!treeNode.highlight) ? { color: "#ff0000","font-size":"14px", "font-weight": "bold" } : { color: "#333","font-size":"14px", "font-weight": "normal" };
    }