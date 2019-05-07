layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;
    var curnum = 1;
    var limitcount = 10;
    getorganiza();
    $("#btnquery").on("click", function () {
        getorganiza();
    })
    $("#btnadd").on("click", function () {
        $("#div_PWD").show();
        $("#Name").val("");
        $("#Password").val("");
        $("#AppName").val("");
        $("#ClassId").val("");
        $("#Path").val("");
        $("#Describe").val("");
        $("#btnadd2").attr("type", "1")
        $("#btnadd2 cite").html("新增")
        var _title = "添加机构";
        layer.open({
            type: 1
            , title: _title
            , area: ['400px', '500px']
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
            Name: $("#Name").val(),
            Password: $.md5($("#Password").val()),
            AppName: $("#AppName").val(),
            ClassId: $("#ClassId").val(),
            Path: $("#Path").val(),
            Describe: $("#Describe").val()
        }
        if (_data.Name.length == 0) {
            layer.msg("机构名不能为空");
            return
        }
        if (_data.Password.length == 0) {
            layer.msg("密码不能为空");
            return
        }
        if (_data.AppName.length == 0) {
            layer.msg("简称不能为空");
            return
        }
        if (_data.ClassId.length == 0) {
            layer.msg("机构ID不能为空");
            return
        }
        if (_data.Path.length == 0) {
            layer.msg("机构url不能为空");
            return
        }
        if (_data.Describe.length == 0) {
            layer.msg("描述不能为空");
            return
        }
        _data.Id = Number($("#btnadd2").attr("dataid"))
        var _type = Number($("#btnadd2").attr("type"))
        if (_type == 1) {
            Params.Ajax("/Handler/UserCenter.ashx?action=addorganiza", "post", _data, addorganiza_success, get_fail);
        }
        else if (_type == 2) {
            Params.Ajax("/Handler/UserCenter.ashx?action=updateorganiza", "post", _data, addorganiza_success, get_fail);
        }
    })
    function addorganiza_success(ret) {
        if (ret.Code == 0) {
            var _type = Number($("#btnadd2").attr("type"))
            if (_type == 1) {
                layer.msg("新增成功");
            }
            else if (_type == 2) {
                layer.msg("修改成功");
            }
            getorganiza();
            setTimeout(function () {
                layer.closeAll();
            }, 1000)
        }
        else {
            debugger;
            layer.msg(ret.Msg);
        }
    }
    function getorganiza() {
        curnum = 1;
        limitcount = 10;
        var name = $("#condes input[name='name']").val();
        var url = "/Handler/UserCenter.ashx?action=getorganiza&name=" + escape(name);
        Params.Ajax(url, "get", "", getorganiza_success, get_fail);
    }
    function getorganiza_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data
            table.render({
                elem: '#userTables',
                height: "488px",
                loading: true,
                text: "暂无数据",
                cols: [[
                    { type: 'numbers' },
                    { field: 'Name',  title: '机构名', align: 'center' },
                    { field: 'CreateTime',  title: '创建时间', align: 'left' },
                    { field: 'Describe',  title: '备注', align: 'left' },
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
                                var url = "/Handler/UserCenter.ashx?action=getorganiza&name=" + escape(name) + "&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getUser_success, get_fail);
                            }
                        }
                    })
                }
            });
        } else {
            layer.msg(ret.Msg);
        }
    }
    function get_fail(ret) {
        layer.msg("服务器异常")
    }
    function deleteorganiza_success(ret) {
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
    function updateorganizapwd_success(ret) {
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
            $("#div_PWD").hide();
            $("#btnadd2").attr("type", "2")
            $("#btnadd2").attr("dataid", data.Id)
            $("#btnadd2 cite").html("修改")
            $("#Name").val(data.Name);
            $("#Password").val("");
            $("#AppName").val(data.AppName);
            $("#ClassId").val(data.ClassId);
            $("#Path").val(data.Path);
            $("#Describe").val(data.Describe);
            var _title = "账号修改";
            layer.open({
                type: 1
                , title: _title
                , area: ['400px', '500px']
                , shade: 0
                , content: $("#notice1")
                , yes: function () {

                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        } else if (obj.event === 'del') {
            layer.confirm('真的删除行么', function (index) {
                Params.Ajax("/Handler/UserCenter.ashx?action=deleteorganiza", "post", data, deleteorganiza_success, get_fail);
            });
        }
        else if (obj.event === 'shouquan') {
            layer.prompt({
                formType: 1,
                value: '',
                title: '请输入新密码',
                area: ['800px', '350px'] //自定义文本域宽高
            }, function (value, index, elem) {
                if (value.length > 0) {
                    debugger
                    var _data = {
                        ID: obj.data.Id,
                        Password: $.md5(value)
                    }
                    Params.Ajax("/Handler/UserCenter.ashx?action=updateorganizapwd", "post", _data, updateorganizapwd_success, get_fail);
                    layer.close(index);
                }
            });
        }
    });
});