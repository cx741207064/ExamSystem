layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;
    var curnum = 1;
    var limitcount = 10;
    getCertificate();

    $("#btnquery").on("click", function () {
        getCertificate();
    })

    function getCertificate() {
        var studentid = $("#condes input[name='studentid']").val();
        var unsignupurl = "/Handler/ExamCenter.ashx?action=getstudentcertifi&studentid=" + escape(studentid);
        Params.Ajax(unsignupurl, "get", "", getCertificate_success, get_fail);
    }
    function getCertificate_success(ret) {

        if (ret.Code == "0") {
            if (ret.Msg && ret.Msg.length > 0) {
                top.layer.msg(ret.Msg);
            }
            //初始化学员网校userid
            $("#username").val(ret.Data.username);
            var _data = ret.Data.unsignup;
            table.render({
                elem: '#Tables_certificate1',
                height: ["200px"],
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers' },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'CategoryName', title: '证书类别', align: 'center' },
                    { field: 'ExamSubject',title: '考核级次', align: 'center' },
                    { field: 'StartTime', title: '本期考试开始时间', align: 'center' },
                    { field: 'EndTime',  title: '本期考试结束时间', align: 'center' },
                    { width: 200, title: '常用操作', align: 'center', toolbar: '#userbar_certificate1', fixed: "right" }
                ]],
                data: _data,
                page: false
            });

            var _data = ret.Data.signup
            table.render({
                elem: '#Tables_certificate2',
                loading: true,
                height: ["200px"],
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers' },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'TicketNum',  title: '准考证号', align: 'center' },
                    { field: 'CategoryName', title: '证书类别', align: 'center' },
                    { field: 'ExamSubject', title: '考核级次', align: 'center' },
                    { field: 'StartTime', title: '本期考试开始时间', align: 'center' },
                    { field: 'EndTime', title: '本期考试结束时间', align: 'center' },
                    { width: 200, title: '常用操作', align: 'center', toolbar: '#userbar_certificate2', fixed: "right" }
                ]],
                data: _data,
                page: false
            });
            var _data = ret.Data.hold
            table.render({
                elem: '#Tables_certificate3',
                height: ["200px"],
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers' },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'SerialNum',  title: '证书编号', align: 'center' },
                    { field: 'CategoryName',  title: '证书类别', align: 'center' },
                    { field: 'ExamSubject', title: '考核级次', align: 'center' },
                    { field: 'IssueDate',  title: '获取日期', align: 'center' },
                    {
                        field: 'CertState',  title: '发放状态', align: 'center',
                        templet: '<div>已发放</div>'
                    },
                    { width: 200, title: '常用操作', align: 'center', toolbar: '#userbar_certificate3', fixed: "right" }
                ]],
                data: _data,
                page: false
            });
        } else {
            top.layer.msg(ret.Msg);
        }
    }
    function get_fail(ret) {
        top.layer.msg("服务器异常", { icon: 5 });
    }
    //监听工具条
    table.on('tool(Tables_certificate1)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            var _title = "报考证书";
            layer.open({
                type: 1
                , title: _title
                , area: ['830px', '560px']
                , shade: 0
                , content: $("#notice1")
                , btn: ['确认报考']
                , success: function (layero, index) {
                    $("#CategoryName").val(data.CategoryName);
                    $("#ExamSubject").val(data.ExamSubject);
                    $("#password").val("");
                    getSubjects(data.Subject);
                }
                , yes: function () {
                    signup(data.CertifiId);
                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        }
    });

    function signup(certificateid) {
        var _data = {
            studentid: $("#condes input[name='studentid']").val(),
            username: $("#username").val(),
            password: $("#password").val(),
            certificateid: certificateid
        };
        var url = "/Handler/ExamCenter.ashx?action=signup";
        Params.Ajax(url, "post", _data, signup_success, signup_fail);
    }

    function signup_success(ret) {
        if (ret.Code == 0) {
            top.layer.msg("报考成功", { icon: 1 });
            getCertificate();
            setTimeout(function () {
                layer.closeAll();
            }, 1500);
        }
        else {
            top.layer.msg(ret.Msg, { icon: 5 });
        }
    }

    function signup_fail() {
        top.layer.msg("报名失败", { icon: 5 });
    }

    function getSubjects(_data) {
        table.render({
            elem: '#Tables_subjects',
            height: "185px",
            loading: true,
            text: { none: "暂无数据" },
            cols: [[
                { type: 'numbers' },
                { field: 'Name', width: 300, title: '课程名称', align: 'center' },
                { field: 'Price', width: 200, title: '购买价格', align: 'center' },
                { field: 'Category', width: 245, title: '类型', align: 'center' }
            ]],
            data: _data,
            page: false,
            done: function (res, curr, count) {
            }
        });
    }

    table.on('tool(Tables_certificate2)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            window.open('ticketprint.html?TicketNum=' + escape(data.TicketNum));
        }
    });

    table.on('tool(Tables_certificate3)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            window.open('certifiprint.html?SerialNum=' + escape(data.SerialNum));
        }
    });
});