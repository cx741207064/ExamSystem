layui.use(['layer', 'form', 'table', 'common'], function() {
	var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
		common = layui.common;
	message_get();
	function message_config(data) {
	    var url = window.location.origin + "/Hander/MsgSite.ashx?action=message_config";
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
	                message_get();
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
	function message_get() {
	    var keyword = "";
	    var url = "/Hander/MsgSite.ashx?action=message_get&keyword=" + escape(escape(keyword));
	    var tableIns = table.render({
	        elem: '#userTables',
	        height: "481px",
	        limits: 20,
	        limit: 20,
	        loading: true,
            text:"暂无数据",
            cols: [[
                { field: 'id', width: 100, title: '序号', align: 'center' },
                { field: 'userid', width: 200, title: '用户ID', align: 'center' },
                { field: 'clientid', width: 360, title: '设备ID', align: 'center' },
                { field: 'eventname', width: 200, title: '事件', align: 'center' },
                { field: 'pagename', width: 200, title: '访问页面', align: 'center' },
                //{ field: 'details', width: 100, title: '事件', align: 'center', toolbar: '#userbar2' },
                { field: 'appid', width: 360, title: '应用ID', align: 'center' },
                { field: 'appname', width: 100, title: '应用名称', align: 'center' },
                { field: 'platform', width: 100, title: '平台', align: 'center' },
                { field: 'model', width: 100, title: '机型', align: 'center' },
                { field: 'os', width: 100, title: '操作系统', align: 'center' },
                { field: 'appver', width: 100, title: '应用版本', align: 'center' },
                { field: 'pageid', width: 200, title: '访问页面ID', align: 'center' },
                { field: 'starttime', width: 200, title: '访问开始时间', align: 'center' },
                { field: 'endtime', width: 200, title: '访问结束时间', align: 'center' },
                { field: 'createtime', width: 200, title: '消息时间', align: 'center' },
                { field: 'remark', width: 200, title: '备注', align: 'center' },
                { field: 'channel', width: 100, title: '用户渠道', align: 'center' },
                { field: 'ip', width: 160, title: 'IP', align: 'center' },
                { field: 'telphone', width: 200, title: '电话', align: 'center' },
                { title: '常用操作', width: 160, align: 'center', toolbar: '#userbar', fixed: "right" }
            ]],
            url: url,
            page: true,
            done: function(res, curr, count){
                $("[data-field='appid']").css('display', 'none');
                $("[data-field='pageid']").css('display', 'none');
                $("[data-field='telphone']").css('display', 'none');
                $("[data-field='remark']").css('display', 'none');
            }
	        //even: true,

	    });
	}

	//监听工具条
	table.on('tool(userTables)', function(obj) {
		var data = obj.data;
		if (obj.event === 'edit') {
		    //layer.msg(JSON.stringify(obj.data), { time: 10000 });

		    layer.open({
		        type: 1
                , offset: "auto"
                , area: ["950px", "350px"]
                , id: 'layerDemoauto'
                , content: JSON.stringify(obj.data)
                , btn: '关闭'
                , btnAlign: 'c' //按钮居中
                , shade: 0 //不显示遮罩
                , yes: function () {
                    layer.closeAll();
                }
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
				    id:obj.data.id
				}
				message_config(data);
			});
		}
	});

	$('#larry_group .layui-btn').on('click',function(){
          var type = $(this).data('type');
          active[type] ? active[type].call(this) : '';
	});
	var active = {
	    add: function () {
	        return;
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
                        message_config(data);
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