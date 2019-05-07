layui.use(['layer', 'form', 'table', 'common'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
		common = layui.common;
    model_get();
    function model_config(data) {
        var url = window.location.origin + "/Hander/ModelSite.ashx?action=model_config&pageid=C693D950962A963CD0FB4A880DD6154E";
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
    function model_get() {
        var keyword = "";
        var url = "/Hander/ModelSite.ashx?action=model_get&pageid=C693D950962A963CD0FB4A880DD6154E&keyword=" + escape(escape(keyword));
        var tableIns = table.render({
            elem: '#userTables',
            height: "481px",
            limits: 20,
            limit: 20,
            loading: true,
            text: "暂无数据",
            cols: [[
                { field: 'id', width: 80, title: '序号', align: 'center' },
                { field: 'appid', width: 80, title: '应用ID', align: 'center' },
                { field: 'appname', width: 90, title: '应用名称', align: 'center' },
                { field: 'userid', width: 90, title: '用户ID', align: 'center' },
                { field: 'clientid', width: 120, title: '设备ID', align: 'center' },
                { field: 'producttype', width: 90, title: '标签类型', align: 'center' },
                { field: 'productid', width: 120, title: '标签ID', align: 'center' },
                { field: 'productdesc', width: 300, title: '标签描述', align: 'center' },
                { field: 'times', width: 100, title: '总停留次数', align: 'center' },
                { field: 'maxstaytime', width: 130, title: '最大间隔（秒）', align: 'center' },
                { field: 'lasttime', width: 160, title: '最近一次', align: 'center' },
                { title: '常用操作', width: 160, align: 'center', toolbar: '#userbar', fixed: "right" }
            ]],
            url: url,
            page: true,
            done: function (res, curr, count) {
                $("[data-field='appid']").css('display', 'none');
            }
            //even: true,

        });
    }

    //监听工具条
    table.on('tool(userTables)', function (obj) {
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

        } else if (obj.event === 'shouquan') {
            layer.alert('授权行：<br>' + JSON.stringify(data))
        } else if (obj.event === 'disable') {
            layer.alert('禁用行：<br>' + JSON.stringify(data))
        } else if (obj.event === 'del') {
            layer.confirm('真的删除行么', function (index) {
                obj.del();
                layer.close(index);
                var data = {
                    option: 3,
                    id: obj.data.id
                }
                message_config(data);
            });
        }
    });

});