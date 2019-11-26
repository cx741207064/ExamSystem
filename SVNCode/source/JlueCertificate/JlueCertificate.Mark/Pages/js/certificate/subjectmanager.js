layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload', 'laydate'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common,
        laydate = layui.laydate;

    var curnum = 1;
    var limitcount = 10;
    getsubjects();
    getolschoolsubjects();
    $("#btnquery").on("click", function () {
        getsubjects();
    })
    $("#btnadd").on("click", function () {
        var _title = "添加课程";
        layer.open({
            type: 1
            , title: _title
            , area: ['830px', '360px']
            , shade: 0
            , content: $("#notice1")
            , btn: ['确认添加']
            , success: function (layero, index) {
                $("#Name").val("");
                $("#Price").val("");
                $("#Category").val("");
                $("#Describe").val("");
                $("#OLPaperID").val("");
                if ($("#OLSchoolId option")[0]) {
                    $("#OLSchoolId option")[0].selected = true;
                }
                form.render('select');
            }
            , yes: function () {
                handelsubject("add");
            }
            , end: function () {

            }
            , zIndex: layer.zIndex
        });
    })

    function getsubjects() {
        var name = $("#condes input[name='subname']").val();
        var url = "/Handler/UserCenter.ashx?action=getallsubject&name=" + escape(name);
        Params.Ajax(url, "get", "", getsubjects_success, get_fail);
    }
    function getsubjects_success(ret) {
        if (ret.Code == "0") {
            if (ret.Msg && ret.Msg.length > 0) {
                top.layer.msg(ret.Msg);
            }
            var _data = ret.Data;
            table.render({
                id: 'subjectTables',
                elem: '#subjectTables',
                height: "500px",
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { checkbox: true },
                    { field: 'ID',  title: '序号', align: 'center' },
                    { field: 'Name',  title: '课程名称', align: 'center' },
                    { field: 'Category', title: '类型', align: 'center' },
                    { field: 'Describe',title: '描述', align: 'center' },
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
                                var name = $("#condes input[name='subname']").val();
                                var url = "/Handler/UserCenter.ashx?action=getallsubject&name=" + escape(name) + "&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getsubjects_success, get_fail);
                                //Params.Ajax(url, "get", "", getCertificate_success, get_fail);
                            }
                        }
                    })
                }
            });
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    function getolschoolsubjects() {
        var url = "/Handler/UserCenter.ashx?action=getolschoolsubjects";
        Params.Ajax(url, "get", "", getOLSchoolSubjects_success, null);
    }
    function getOLSchoolSubjects_success(ret) {
        if (ret.Code == "0") {
            var _htmlp = '';
            $.each(ret.Data.all, function (i, p) {
                _htmlp += '<option desc=\'' + JSON.stringify(p) + '\' value=\'' + p.AOMid + '\'>' + p.Sort_Name + '</option>';
            })
            $("#OLSchoolId").html(_htmlp);
            form.render('select');
        }
    }
    function handelsubject(type) {
        var _data = {
            ID: $("#ID").val(),
            Name: $("#Name").val(),
            Price: "",
            Category: $("#Category").val(),
            Describe: $("#Describe").val(),
            OLPaperID:$("#OLPaperID").val(),
        };
        if (_data.Name.length == 0) {
            top.layer.msg("课程名称不能为空", { icon: 2 });
            return
        }
        if (_data.Category.length == 0) {
            top.layer.msg("课程类别不能为空", { icon: 2 });
            return
        }
        $.each($("#OLSchoolId option"), function (i, m) {
            if (m.selected) {
                var _desc = $(m).attr("desc");
                if (_desc) {
                    var _descJson = JSON.parse(_desc);
                    if (_descJson) {
                        _data.OLSchoolId = _descJson.Sort_Id,
                        _data.OLSchoolName = _descJson.Sort_Name,
                        _data.OLSchoolProvinceId = _descJson.ProvinceId,
                        _data.OLSchoolCourseId = _descJson.CourseId,
                        _data.OLSchoolQuestionNum = _descJson.QuestionNum,
                        _data.OLSchoolMasterTypeId = _descJson.MasterTypeId,
                        _data.OLSchoolAOMid = _descJson.AOMid
                    }
                }
            }
        })
        var url = "/Handler/UserCenter.ashx?action=addsubject";
        if (type == "edit") {
            url = "/Handler/UserCenter.ashx?action=updatesubject";
        }
        Params.Ajax(url, "post", _data, handelsubject_success, handelsubject_fail);
    }
    function handelsubject_success(ret) {
        if (ret.Code == 0) {
            top.layer.msg("操作成功", { icon: 1 });
            getsubjects();
            setTimeout(function () {
                layer.closeAll();
            }, 1500)
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    function handelsubject_fail() {
        top.layer.msg("操作失败", { icon: 5 });
    }
    function get_fail(ret) {
        top.layer.msg("服务器异常")
    }
    //监听工具条
    table.on('tool(subjectTables)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            var _title = "修改课程";
            layer.open({
                type: 1
                , title: _title
                , area: ['830px', '360px']
                , shade: 0
                , content: $("#notice1")
                , btn: ['确认修改']
                , success: function (layero, index) {
                    $("#ID").val(data.ID);
                    $("#Name").val(data.Name);
                    $("#Price").val(data.Price);
                    $("#Category").val(data.Category);
                    $("#Describe").val(data.Describe);
                    $("#OLSchoolId").val(data.OLSchoolAOMid);
                    $("#OLPaperID").val(data.OLPaperID);
                    form.render('select');
                }
                , yes: function () {
                    handelsubject("edit");
                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        } else if (obj.event === 'del') {
            layer.confirm('注：确认删除' + data.Name + '-' + data.Category + '?', function (index) {
                var url = "/Handler/UserCenter.ashx?action=delsubject";
                Params.Ajax(url, "post", data, handelsubject_success, handelsubject_fail);
            });
        }
    });
});