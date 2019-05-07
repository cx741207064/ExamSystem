layui.use(['layer', 'form', 'table', 'common'], function() {
	var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
		common = layui.common;
	sysapp_get();
	sysapppage_get();
	function sysapp_get() {
	    var url = window.location.origin + "/Hander/SysConfig.ashx?action=sysapp_get&keyword=";
	    $.ajax({
	        method: "Get",
	        url: url,
	        async: false,
	        contentType: "application/x-www-form-urlencoded",
	        dataType: "json",
	        success: function (ret) {
	            if (ret.data != null && ret.data.length > 0) {
	                var html = "";
	                $.each(ret.data, function (i, m) {
	                    html += '<option value="' + m.appid + '">' + m.appname + '</option>';
	                });
	                if (html.length > 0) {
	                    $("#notice1 select[name='appvalues']").html(html);
	                    $("#notice1").attr("appvalues", JSON.stringify(ret.data));
	                }
	            }
	        },
	        error: function (ret) {
	            layer.msg(ret)
	        }
	    });
	}
	function sysapppage_get() {
	    var keyword = "";
	    var url = "/Hander/SysConfig.ashx?action=sysapppage_get&keyword=" + escape(escape(keyword));
	    var tableIns = table.render({
	        elem: '#userTables',
	        height: "481px",
	        limits: 20,
	        limit: 20,
	        loading: true,
	        text: "暂无数据",
	        cols: [[
                { field: 'id', width: 100, title: '序号', align: 'center' },
                { field: 'appid', title: '应用ID', align: 'center',style:'display:none',display:'none' },
                { field: 'appname', width: 160, title: '应用名称', align: 'center' },
                { field: 'apppageid', width: 360, title: '页面ID', align: 'center' },
                { field: 'apppagename', width: 280, title: '页面名称', align: 'center' },
                { field: 'apppageurl', width: 360, title: '相对地址', align: 'left' },
                { field: 'apppageremark', width: 280, title: '备注', align: 'center' },
                { title: '常用操作', width: 160, align: 'center', toolbar: '#userbar', fixed: "right" }
	        ]],
	        url: url,
	        page: true,
	        done: function(res, curr, count){
	            $("[data-field='appid']").css('display', 'none');
	        }
	        //even: true,
	    });
	}
	function sysapppage_config(data) {
	    var url = window.location.origin + "/Hander/SysConfig.ashx?action=sysapppage_config";
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
	                sysapppage_get();
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

	//监听工具条
	table.on('tool(userTables)', function(obj) {
		var data = obj.data;
		if (obj.event === 'edit') {
		    $("#notice2 input[name='appname']").val(data.appname);
		    $("#notice2 input[name='apppageid']").val(data.apppageid);
		    $("#notice2 input[name='apppagename']").val(data.apppagename);
		    $("#notice2 input[name='apppageurl']").val(data.apppageurl);
		    $("#notice2 input[name='apppageremark']").val(data.apppageremark);
		    layer.open({
		        type: 1
                , title: '修改页面'
                , area: ['490px', '460px']
                , shade: 0
                , content: $("#notice2")
                , btn: ['确定'] //只是为了演示
                , yes: function () {
                    if ($("#notice2 input[name='apppagename']").val().length > 0 && $("#notice2 input[name='apppageurl']").val().length > 0) {
                        var data = {
                            option: 2,
                            appid: obj.data.appid,
                            apppageid: obj.data.apppageid,
                            apppagename: obj.data.apppagename,
                            apppageurl: obj.data.apppageurl,
                            apppageremark: obj.data.apppageremark,
                            apppagenamenew: $("#notice2 input[name='apppagename']").val(),
                            apppageurlnew: $("#notice2 input[name='apppageurl']").val(),
                            apppageremarknew: $("#notice2 input[name='apppageremark']").val()
                        }
                        if (data.apppagename != data.apppagenamenew || data.apppageurl != data.apppageurlnew || data.apppageremark != data.apppageremarknew) {
                            sysapppage_config(data);
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
				    apppageid: obj.data.apppageid
				}
				sysapppage_config(data);
			});
		}
	});

	$('#larry_group .layui-btn').on('click',function(){
          var type = $(this).data('type');
          active[type] ? active[type].call(this) : '';
	});
	var active = {
	    add: function () {
	        var _appvalues = $("#notice1").attr("appvalues");
	        if (!_appvalues || _appvalues.length == 0) {
	            layer.msg("无应用供选择，请先配置应用");
	            return;
	        }
	        $("#notice1 input[name='apppagename']").val("");
	        $("#notice1 input[name='apppageurl']").val("");
	        $("#notice1 input[name='apppageremark']").val("");
	        layer.open({
	            type: 1 
                , title: '新增页面'
                , area: ['490px', '360px']
                , shade: 0
                , content: $("#notice1")
                , btn: ['确定'] //只是为了演示
                , yes: function () {
                    if ($("#notice1 input[name='apppagename']").val().length > 0 && $("#notice1 input[name='apppageurl']").val().length > 0) {
                        var data = {
                            option: 1,
                            appid: $("#notice1 select[name='appvalues']")[0].value,
                            apppagename: $("#notice1 input[name='apppagename']").val(),
                            apppageurl: $("#notice1 input[name='apppageurl']").val(),
                            apppageremark: $("#notice1 input[name='apppageremark']").val()
                        }
                        sysapppage_config(data);
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