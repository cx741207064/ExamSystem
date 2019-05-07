layui.use(['layer', 'form', 'table', 'common'], function() {
	var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
		common = layui.common;
	sysapppage_get_bygroup();
    sysappevent_get();
	function sysapppage_get_bygroup() {
	    var url = window.location.origin + "/Hander/SysConfig.ashx?action=sysapppage_get_bygroup&keyword=";
	    $.ajax({
	        method: "Get",
	        url: url,
	        async: false,
	        contentType: "application/x-www-form-urlencoded",
	        dataType: "json",
	        success: function (ret) {
	            if (ret.Data != null && ret.Data.length > 0) {
	                var html = "";
	                $.each(ret.Data, function (i, m) {
	                    html += '<option value="' + m.Item1 + '">' + m.Item2 + '</option>';
	                    if (i == 0) {
	                        var html2 = "";
	                        $.each(m.Item3, function (j, n) {
	                            html2 += '<option value="' + n.apppageid + '">' + n.apppagename + '</option>';
	                        })
	                        $("#notice1 select[name='apppagevalues']").html(html2);
	                    }
	                });
	                if (html.length > 0) {
	                    $("#notice1 select[name='appvalues']").html(html);
	                    $("#notice1").attr("appvalues", JSON.stringify(ret.Data));
	                }
	            }
	        },
	        error: function (ret) {
	            layer.msg(ret)
	        }
	    });
	}
	function sysappevent_get() {
	    var keyword = "";
	    var url = "/Hander/SysConfig.ashx?action=sysappevent_get&keyword=" + keyword;
	    var tableIns = table.render({
	        elem: '#userTables',
	        height: "481px",
	        limits: 20,
	        limit: 20,
	        loading: true,
	        text: "暂无数据",
	        cols: [[
                { field: 'id', width: 100, title: '序号', align: 'center' },
                { field: 'appid', title: '应用ID', align: 'center' },
                { field: 'appname', width: 160, title: '应用名称', align: 'center' },
                { field: 'appeventid', width: 360, title: '事件ID', align: 'center' },
                { field: 'apppageid', title: '页面ID', align: 'center' },
                { field: 'apppagename', width: 219, title: '页面名称', align: 'center' },
                { field: 'appeventname', width: 200, title: '事件名称', align: 'center' },
                { field: 'apppageurl', width: 360, title: '页面相对地址', align: 'left' },
                { field: 'appeventremark', width: 140, title: '备注', align: 'center' },
                { title: '常用操作', width: 160, align: 'center', toolbar: '#userbar', fixed: "right" }
	        ]],
	        url: url,
	        page: true,
	        done: function(res, curr, count){
	            $("[data-field='appid']").css('display', 'none');
	            $("[data-field='apppageid']").css('display', 'none');
	        }
	        //even: true,
	    });
	}
	function sysappevent_config(data) {
	    var url = window.location.origin + "/Hander/SysConfig.ashx?action=sysappevent_config";
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
	                sysappevent_get();
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
		    $("#notice2 input[name='apppagename']").val(data.apppagename);
		    $("#notice2 input[name='appeventid']").val(data.appeventid);
		    $("#notice2 input[name='appeventname']").val(data.appeventname);
		    $("#notice2 input[name='appeventremark']").val(data.appeventremark);
		    layer.open({
		        type: 1
                , title: '修改页面'
                , area: ['490px', '460px']
                , shade: 0
                , content: $("#notice2")
                , btn: ['确定'] //只是为了演示
                , yes: function () {
                    if ($("#notice2 input[name='appeventname']").val().length > 0) {
                        var data = {
                            option: 2,
                            appid: obj.data.appid,
                            apppageid: obj.data.apppageid,
                            appeventid: obj.data.appeventid,
                            appeventname: obj.data.appeventname,
                            appeventremark: obj.data.appeventremark,
                            appeventnamenew: $("#notice2 input[name='appeventname']").val(),
                            appeventremarknew: $("#notice2 input[name='appeventremark']").val(),
                        }
                        if (data.appeventname != data.appeventnamenew || data.appeventremark != data.appeventremarknew) {
                            sysappevent_config(data);
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
				sysappevent_config(data);
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
	            layer.msg("无应用页面供选择，请先配置页面");
	            return;
	        }
	        $("#notice1 input[name='appeventname']").val("");
	        $("#notice1 input[name='appeventremark']").val("");
	        layer.open({
	            type: 1 
                , title: '新增事件'
                , area: ['490px', '360px']
                , shade: 0
                , content: $("#notice1")
                , btn: ['确定'] //只是为了演示
                , yes: function () {
                    if ($("#notice1 input[name='appeventname']").val().length > 0) {
                        var data = {
                            option: 1,
                            appid: $("#notice1 select[name='appvalues']")[0].value,
                            apppageid: $("#notice1 select[name='apppagevalues']")[0].value,
                            appeventname: $("#notice1 input[name='appeventname']").val(),
                            appeventremark: $("#notice1 input[name='appeventremark']").val()
                        }
                        sysappevent_config(data);
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