layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;
    var curnum = 1;
    var limitcount = 10;
    getUser();
    $("#btnquery").on("click", function () {
        getUser();
    })
    $("#btnadd").on("click", function () {
        $("#div_pwd").show();
        $("#name").val("");
        $("#pwd").val("");
        $("#btnadd2").attr("type", "1")
        $("#btnadd2 cite").html("新增")
        var _title = "添加账号";
        layer.open({
            type: 1
            , title: _title
            , area: ['750px', '530px']
            , shade: 0
            , content: $("#notice1")
            , yes: function () {
            }
            , end: function () {

            }
            , zIndex: layer.zIndex
        });
    })
    $("#btnadd2").on("click", function () {
        var _data = {
            name: $("#name").val(),
            pwd: $.md5($("#pwd").val()),
            level: $("#level")[0].value,
        }
        if (_data.name.length == 0) {
            layer.msg("用户名不能为空");
            return 
        }
        if ($("#pwd").val() == 0) {
            layer.msg("密码不能为空");
            return 
        }

        _data.id = Number($("#btnadd2").attr("dataid"))
        var _type = Number($("#btnadd2").attr("type"))
        if (_type == 1) {
            Params.Ajax("/Handler/UserCenter.ashx?action=adduser", "post", _data, addUser_success, getUser_fail);
        }
        else if (_type == 2) {
            Params.Ajax("/Handler/UserCenter.ashx?action=updateuser", "post", _data, addUser_success, getUser_fail);
        }
    })
    function addUser_success(ret) {
        if (ret.Code == 0) {
            var _type = Number($("#btnadd2").attr("type"))
            if (_type == 1) {
                layer.msg("新增成功");
            }
            else if (_type == 2) {
                layer.msg("修改成功");
            }
            getUser();
            setTimeout(function () {
                layer.closeAll();
            },1000)
        }
        else {
            debugger;
            layer.msg(ret.Msg);
        }
    }
    function getUser() {
        curnum = 1;
        limitcount = 10;
        var name = $("#condes input[name='name']").val();
        var url = "/Handler/UserCenter.ashx?action=getuser&name=" + escape(name);
        Params.Ajax(url, "get", "", getUser_success, getUser_fail);
    }
    function getUser_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data
            table.render({
                elem: '#userTables',
                height: "488px",
                loading: true,
                text: "暂无数据",
                cols: [[
                    { field: 'id', title: '序号', align: 'center' },
                    { field: 'name', title: '用户名', align: 'center' },
                    { field: 'level',  title: '账号类型', align: 'center' },
                    { field: 'createtime',title: '创建时间', align: 'left' },
                    { width: 200, title: '常用操作', align: 'center', toolbar: '#userbar', fixed: "right" }
                ]],
                data: _data,
                page: false,
                done: function (res, curr, count) {
                    laypage.render({
                        elem: 'laypage'
                        , count: ret.Stamp
                        , curr: curnum
                        , limit: limitcount
                        , layout: ['prev', 'page', 'next', 'skip', 'count', 'limit']
                        , jump: function (obj, first) {
                            if (!first) {
                                curnum = obj.curr;
                                limitcount = obj.limit;
                                var name = $("#condes input[name='name']").val();
                                var cardid = $("#condes input[name='cardid']").val();
                                var url = "/Handler/UserCenter.ashx?action=getuser&name=" + escape(name) + "&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getUser_success, getUser_fail);
                            }
                        }
                    })
                }
            });
        } else {
            layer.msg(ret.Msg);
        }
    }
    function getUser_fail(ret) {
        layer.msg("服务器异常")
    }
    function deletestudent_success(ret) {
        if (ret.Code == 0) {
            layer.msg("删除成功");
            getUser();
            setTimeout(function () {
                layer.closeAll();
            }, 1000)
        }
        else {
            layer.msg(ret.Msg);
        }
    }
    function updateuserpwd_success(ret) {
        if (ret.Code == 0) {
            layer.msg("更新成功");
            setTimeout(function () {
                layer.closeAll();
            }, 1000)
        }
        else {
            layer.msg(ret.Msg);
        }
    }
    //监听工具条
    table.on('tool(userTables)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            $("#div_pwd").hide();
            $("#btnadd2").attr("type", "2")
            $("#btnadd2").attr("dataid", data.id)
            $("#btnadd2 cite").html("修改")
            $("#pwd").val(data.pwd);
            $("#name").val(data.name);
            var _level = "1";
            if (data.level == "管理员") {
                _level = "9";
            }
            $.each($("#level option"), function (i, n) {
                if (n.value == _level) {
                    n.selected = true;
                }
            })
            form.render('select');
            var _title = "账号修改";
            layer.open({
                type: 1
                , title: _title
                , area: ['850px', '530px']
                , shade: 0
                , content: $("#notice1")
                , yes: function () {

                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        } else if (obj.event === 'shouquan') {
            layer.prompt({
                formType: 1,
                value: '',
                title: '请输入新密码',
                area: ['800px', '350px'] //自定义文本域宽高
            }, function (value, index, elem) {
                if (value.length > 0) {
                    var _data = {
                        id: obj.data.id,
                        pwd: $.md5(value)
                    }
                    Params.Ajax("/Handler/UserCenter.ashx?action=updateuserpwd", "post", _data, updateuserpwd_success, getUser_fail);
                    layer.close(index);
                }
            });
        } else if (obj.event === 'disable') {
            layer.alert('禁用行：<br>' + JSON.stringify(data))
        } else if (obj.event === 'del') {
            layer.confirm('真的删除行么', function (index) {
                Params.Ajax("/Handler/UserCenter.ashx?action=deleteuser", "post", data, deletestudent_success, getUser_fail);
            });
        }
    });
});