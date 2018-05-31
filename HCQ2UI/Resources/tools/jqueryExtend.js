/**
 * 扩展
 */

;(function($){
	/**
	 * 加载select
	 * 取消了placeholder不能选择、因为表单serialize无法获取 设置了disabled的值
	 * */
	$.fn.bootstrapSelect = function(options,data)
	{
		var This = this;
		
		var defaults={
				url:'',//URL地址
				method:'post',//请求方式
				valueField:'',//value使用的字段
				textField:'',//text使用的字段
				dataFormatter:null,//过滤数据源
				async:false,//默认同步加载
				success:null,//成功
				error:null,//失败
				onselect:null,//选中触发。参数：value、text
				htmlsuccess:null//option加载完成
		};
		
		var methods = {
			load:function(data){
				$(This).empty();
				
				var valuefield = $(This).attr('valueField'),
				
					textfield = $(This).attr('textField');
				
				
				var op =$('<option value="" selected>'+$(This).attr('placeholder')+'</option>');
				
				$(This).append(op);
				for(i in data)
				{
					var op =$('<option value="'+data[i][valuefield]+'">'+data[i][textfield]+'</option>');
					
					$(This).append(op);
				}
			}
		};
		
		var setting = $.extend({},defaults,options);
		
		if((typeof options) =='string')
		{
			methods[options](data);
			return;
		}
		
		$.ajax({
			url:setting.url,
			type:setting.method,
			dataType:'json',
			async:setting.async,
			success:function(result){
				if(setting.dataFormatter!=null)
				{
					result = setting.dataFormatter(result);
				}
				if(setting.success!=null){
					setting.success(result);
					return;
				}
				
				$(This).attr('valueField',setting.valueField);
				$(This).attr('textField',setting.textField);
				
				methods['load'](result);
				
				if(setting.onselect!=null)
				{
					$(This).bind('change',function(){
						var value = $(This).val(),
						
							text= $(This).find('option:selected').text();
						
						setting.onselect(value,text);
					});					
				}
				if(setting.htmlsuccess!=null)
				{
					var Inter = setInterval(function(){
						if($(This).find('option').length==result.length+1)
						{
							setting.htmlsuccess();
							window.clearInterval(Inter);
						}
					},500);
				}	
			},
			error:function(a,b,c){
				if(setting.error!=null)
				{
					setting.error(a,b,c);
				}
			}
		});
		
	}
	
	/*
	*form提交
	*/
	$.fn.saveForm = function(url,even)
	{
		var This = this;
		$.ajax({
			url:url,
			type:'post',
			data:$(This).serialize(),
			beforeSend:function(){
				layer.msg('正在提交数据',{icon:16});
			},
			success:function(result){
				if(result.indexOf('<span>请联系管理员</span>')!=-1)
				{
					layer.open({title:'提示',content:'<div class="has-error"><label class="control-label">系统出错请联系管理员！</label></div>',icon:2});
					return;
				}	
				var result = eval('(' + result + ')');
				setTimeout(function(){
				if(result.isInitialed == "1"){
					layer.open({title:'提示',content:'此单位已完成初始化，若要修改此类信息，请带上相关的文件到人社局事管股进行变更',icon:7});
					return;
				}
				if (result.meg) {
					layer.msg('操作成功',{icon:1});
				} else {
					layer.open({title:'提示',content:result.errorMsg,icon:2});
				}
				if(even!=null)
				{
					even(result);
				}
				},1000)
			}
		});
	}
	

	/*
	 * bootstrap SelectTree--使用bootstrap的文本框和ztree
	 * */
	$.fn.bootstrapSelectTree=function(options,data){
		if(typeof options =="string")
		{
			return $.fn.bootstrapSelectTree.methods[options](this,data);
		}
	}
	
	$.fn.bootstrapSelectTree.defaults={
			ztreeId:'',//ztree树ID
			ztreeContentId:'',//ztree树容器ID
			url:'',//请求URL
			valname:'text',//value存在值得属性名称
			onSelect:null,//选择后执行返回 整个节点数据
			multiple:false,//是否允许多选、不允许	
			editable:true,//是否允许输入文字在文本框、默认允许
			nullisShow:false,//数据为空时是否显示下拉
			onformatter:null,//格式化数据
			width:'200',//下拉框的宽度
			height:'250',//下拉高度	
			placeholder:'',//文本提示
			top:33//下拉的位置	
	};
	$.fn.bootstrapSelectTree.setting={};
	$.fn.bootstrapSelectTree.methods={
		init:function(This,options){
			options.ztreeId='treeUnit_'+$(This).attr('id');
			options.ztreeContentId='treeUnitContent_'+$(This).attr('id');

			var setting = $.extend({},$.fn.bootstrapSelectTree.defaults,options);
			$.fn.bootstrapSelectTree.setting = $.extend({},$.fn.bootstrapSelectTree.defaults,options);
			
			if($('#'+options.ztreeContentId).length>0){
				$('#'+options.ztreeContentId).remove();
			}
			if($('#'+options.ztreeContentId).length==0){
				$('body').append($('<div id="'+options.ztreeContentId+'" class="menuContent" style="background:#ffffff;display:none;position: absolute;overflow-y:auto;overflow-x:auto;min-width:170px;height:auto;max-height:200px; z-index: 1012121000212; border: 1px solid #66afe9;"></div>'));
				$('#'+options.ztreeContentId).append($('<p class="load" style="font-size:12px;height:18px;">加载中...</p>'));
				$('#'+options.ztreeContentId).append($('<ul id="'+options.ztreeId+'" class="ztree" style="margin-top: 0; width:100%;background:#ffffff;"></ul>'));
			}
			
			$(This).val('');
			$(This).attr('selectId','');
			
			if(setting.placeholder!='')
			{
				$(This).attr('placeholder',setting.placeholder);
			}	
			if(!setting.editable)
			{
				$(This).attr('readonly','readonly');
			}
				
			var zTreeSetting;
			if(!setting.multiple)
			{
				zTreeSetting= {
	                view: {
	                    dblClickExpand: false
	                },
	                data: {
	                    simpleData: {
	                        enable: true
	                    },
	                    key:{
	                    	name:'text'
	                    }
	                },
	                callback: {
	                    onClick: function(i,treeid,treeObj){
	                    	$(This).val(treeObj[setting.valname]);
	                    	$(This).attr('selectId',treeObj.id);
	                    	$('#'+setting.ztreeContentId).hide();
	                    	if(setting.onSelect!=null)
	                    	{
	                    		setting.onSelect(treeObj);
	                    	}	
	                    }
	                }
	            };
			}else
			{
				//允许多选
				zTreeSetting={
					check: {
						enable: true,
						chkStyle:'checkbox',
						chkboxType: {"Y":"", "N":""}
					},
					view: {
						dblClickExpand: false
					},
					data: {
						simpleData: {
							enable: true
						},
						key:{
	                    	name:'text'
	                    }
					},
					callback: {
						beforeClick: function(treeId, treeNode){
							var zTree = $.fn.zTree.getZTreeObj(setting.ztreeId);
							zTree.checkNode(treeNode, !treeNode.checked, null, true);
							return false;
						},
						onCheck:function(e, treeId, treeNode){
							var zTree = $.fn.zTree.getZTreeObj(setting.ztreeId),
							nodes = zTree.getCheckedNodes(true),
							v = "";
							ids="";
							for (var i=0, l=nodes.length; i<l; i++) {
								v += nodes[i][setting.valname] + ",";
								ids+=nodes[i].id+",";
							}
							if (v.length > 0 ) v = v.substring(0, v.length-1);
							
							if (ids.length > 0 ) ids = ids.substring(0, ids.length-1);
							var cityObj = $(This);
							
							cityObj.val(v);
							cityObj.attr("selectId",ids);
							
							if(setting.onSelect!=null)
	                    	{
	                    		setting.onSelect(treeObj);
	                    	}	
						}
					}
				};
			}
			//加载树
			$.post(setting.url,function(zNodes){
				$('#'+setting.ztreeContentId+' .load').remove();
				if(setting.onformatter!=null)
				{
					zNodes = setting.onformatter(zNodes);
				}	
				$.fn.zTree.init($('#'+setting.ztreeId),zTreeSetting,zNodes);
			},'json');
			
			var onBodyDown = function(event){
				if (!(event.target.id == setting.ztreeContentId || $(event.target).parents("#"+setting.ztreeContentId).length > 0)) {
					hideMenu();
			    }
			}
			var hideMenu = function(){
				$('#'+setting.ztreeContentId).fadeOut('fast');
				$('body').unbind('mousedown',onBodyDown);
			}
			
			$(This).bind('focus',function(){
				//返回所有数据
				var treeObj = $.fn.zTree.getZTreeObj(setting.ztreeId);
				if(treeObj.getNodes().length==0 && setting.nullisShow==true)
				{
					return;
				}	
				
				var outerWidth = $(This).outerWidth();
				
				var offsetObj = $(This).offset();
				
				if($(This).val()=='')
				{					
					treeObj = $.fn.zTree.getZTreeObj(setting.ztreeId);
					
					if(treeObj!=null)
					{
						treeObj.checkAllNodes(false);
					}
				}	
				$('#'+setting.ztreeContentId).css({width:outerWidth+'px',left: offsetObj.left + "px", top: offsetObj.top + setting.top + "px" }).slideDown("fast");
				
				$('body').bind('mousedown',onBodyDown);
			});
			$(window).scroll(function(){
				var mtop = $(This).offset().top;
				 var offsetTop = mtop + $(window).scrollTop() +"px";  
		         $(This).animate({top : offsetTop },{ duration:600 , queue:false });  
			});
		},
		selectNode:function(This,id){
			if(id!=null){
				var ztreeId='treeUnit_'+$(This).attr('id');
				//传入值选中
				var ztreeObj=null;
				var ThisVal = $(This).val();
				var setInterId = setInterval(function(){
					ztreeObj = $.fn.zTree.getZTreeObj(ztreeId);
					if(ztreeObj!=null)
					{
						$(This).attr('selectId',id);
						var ids = id.split(',');
						for(i in ids)
						{
							var selectedNode = ztreeObj.getNodeByParam("id",ids[i],null);
							ztreeObj.selectNode(selectedNode);
							ztreeObj.checkNode(selectedNode,true,false,true);
							
							ThisVal=selectedNode.text+',';
							if (ThisVal.length > 0 ) ThisVal = ThisVal.substring(0, ThisVal.length-1);
							$(This).val(ThisVal);
							
						}
						window.clearInterval(setInterId);
					}	
				},500);
			}
		},
		getData:function(This){
			//返回所有数据
			var ztreeId = 'treeUnit_'+$(This).attr('id');
			
			var treeObj = $.fn.zTree.getZTreeObj(ztreeId);
			return treeObj.getNodes();
		},
		resetSelected:function(This){
			//清空选中节点
			var ztreeId = 'treeUnit_'+$(This).attr('id');
			var treeObj = $.fn.zTree.getZTreeObj(ztreeId);
			if(treeObj!=null)
			{
				treeObj.cancelSelectedNode();
			}
		}
	}
	/*
	 * 加载表单
	 * */
	$.fn.LoadForm=function(data,options)
	{
		var This = this;
		var setSelectVal = function (key,val) {
		    var Inter = setInterval(function () {
                $('#' + key).selectpicker('val', (val || val===false)?val.toString():val);
		        window.clearInterval(Inter);
		    }, 500);
		}
		for (key in data)
		{
		    $(This).find("[name='" + key + "']").each(function () {
		        var This = this;
		        if ($(this).hasClass("selectpicker") && key) {
		            setSelectVal(key, data[key]);
				}else{
					$(this).val(data[key]);
				}
			});
		}
		return this;
	}
	/*
	 * 禁用表单;true禁用false解禁
	 * */
	$.fn.disabledForm=function(data,bl)
	{
		var This = this;
		
		for(key in data)
		{
			$(This).find("[name='"+key+"']").each(function(){
				$(this).attr('disabled',bl);
			});
		}
		return this;
	}	
	/*重置表单验证状态并取消表单的验证提示*/
	$.fn.resetHideValidForm=function(){
		var validator = $(this).validate();
		validator.resetForm();
		
		$(this).find('input').each(function(){
			$(this).tooltip('hide');
		});
		$(this).find('select').each(function(){
			$(this).tooltip('hide');
		});
		$(this).find('textarea').each(function(){
			$(this).tooltip('hide');
		});
		return this;
	}
})($);