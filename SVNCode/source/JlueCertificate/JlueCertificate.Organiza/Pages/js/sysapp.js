layui.use(['layer', 'form', 'table', 'common'], function() {
	var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
		common = layui.common;
	sysapp_get();
	function sysapp_config(data) {
	    var url = window.location.origin + "/Hander/SysConfig.ashx?action=sysapp_config";
	    $.ajax({
	        method: "POST",
	        url: url,
	        async: false,
	        data: { Content: escape(escape(JSON.stringify(data))) },
	        contentType: "application/x-www-form-urlencoded",
	        dataType: "json",
	        success: function (ret) {
	            layer.closeAll();
	            if (ret.Code == 0) {
	                sysapp_get();
	                if (!ret.Data) {
	                    layer.msg(ret.Msg)
	                }
	            } else {
	                layer.msg(ret.Msg)
	            }
	        },
	        error: function (ret) {
	            layer.closeAll();
	            layer.msg(ret)
	        }
	    });
	}
	function sysapp_get() {
	    var keyword = "";
	    var url = "/Hander/SysConfig.ashx?action=sysapp_get&keyword=" + escape(escape(keyword));
	    var tableIns = table.render({
	        elem: '#userTables',
	        height: "481px",
	        limits: 10,
	        limit: 10,
	        loading: true,
            text:"暂无数据",
            cols: [[
                { field: '_id', width: 100, title: '序号', align: 'center' },
                { field: 'appid', width: 360, title: '应用ID', align: 'center' },
                { field: 'appname', width: 200, title: '应用名称', align: 'center' },
                { title: '常用操作', width: 160, align: 'center', toolbar: '#userbar', fixed: "right" }
            ]],
            url: url,
            page: true,
	        //even: true,

	    });
	}

	//监听工具条
	table.on('tool(userTables)', function(obj) {
		var data = obj.data;
		if (obj.event === 'edit') {
		    $("#notice2 input[name='title']").val(data.appid);
		    $("#notice2 input[name='username']").val(data.appname);
		    layer.open({
		        type: 1
                , title: '修改应用'
                , area: ['490px', '260px']
                , shade: 0
                , content: $("#notice2")
                , btn: ['确定'] //只是为了演示
                , yes: function () {
                    if ($("#notice2 input[name='username']").val().length > 0) {
                        var data = {
                            option: 2,
                            appid: obj.data.appid,
                            appname: obj.data.appname,
                            appnamenew: $("#notice2 input[name='username']").val()
                        }
                        if (data.appname != data.appnamenew) {
                            sysapp_config(data);
                        }
                        else {
                            layer.closeAll();
                        }
                    }
                }
                , zIndex: layer.zIndex
		    });
		}else if (obj.event === 'shouquan') {
			layer.alert('授权行：<br>' + JSON.stringify(data))
		}else if (obj.event === 'disable') {
			layer.alert('禁用行：<br>' + JSON.stringify(data))
		}else if (obj.event === 'del') {
		    layer.confirm('真的删除行么', function (index) {
		        obj.del();
				layer.close(index);
				var data = {
				    option: 3,
				    appid: obj.data.appid,
				    appname: obj.data.appname
				}
				sysapp_config(data);
			});
		}
	});

	$('#larry_group .layui-btn').on('click',function(){
          var type = $(this).data('type');
          active[type] ? active[type].call(this) : '';
	});
	var active = {
	    add: function () {
	        $("#notice1 input[name='username']").val("");
	        layer.open({
	            type: 1 
                , title: '新增应用'
                , area: ['490px', '260px']
                , shade: 0
                , content: $("#notice1")
                , btn: ['确定'] //只是为了演示
                , yes: function () {
                    if ($("#notice1 input[name='username']").val().length > 0) {
                        var data = {
                            option: 1,
                            appid: "",
                            appname: $("#notice1 input[name='username']").val()
                        }
                        sysapp_config(data);
                    }
                }
                , zIndex: layer.zIndex 
	        });
           //common.larryCmsMessage('最近好累，还是过段时间在写吧！','error');
        },
        edit:function(){
           common.larryCmsMessage('最近好累，还是过段时间在写吧！','error');
        },
        del:function(){
           common.larryCmsMessage('最近好累，还是过段时间在写吧！','error');
        }

	};
});