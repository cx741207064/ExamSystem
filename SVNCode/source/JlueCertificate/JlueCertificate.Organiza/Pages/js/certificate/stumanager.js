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

    var signstate = true;
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
            var _data = ret.Data.unsignup;
            table.render({
                elem: '#Tables_certificate1',
                height: ["200px"],
                limit: 100,
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers',title: '序号',width:60 },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'CategoryName', title: '证书类别',width: 180, align: 'center' },
                    { field: 'ExamSubject', title: '考核级次',width: 120, align: 'center' },
                    { field: 'StartTime', title: '本期考试开始时间',width: 160, align: 'center' },
                    { field: 'EndTime', title: '本期考试结束时间',width: 160, align: 'center' },
                    {  title: '常用操作', align: 'center', toolbar: '#userbar_certificate1', fixed: "right" }
                ]],
                data: _data,
                page: false
            });

            var _data = ret.Data.signup
            table.render({
                elem: '#Tables_certificate2',
                loading: true,
                height: ["200px"],
                limit: 100,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers',title: '序号',width: 60 },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'TicketNum', title: '准考证号',width: 190, align: 'center' },
                    { field: 'CategoryName', title: '证书类别',width: 180, align: 'center' },
                    { field: 'ExamSubject', title: '考核级次',width: 110, align: 'center' },
                    { field: 'StartTime', title: '本期考试开始时间',width: 150, align: 'center' },
                    { field: 'EndTime', title: '本期考试结束时间',width: 150, align: 'center' },
                    { title: '常用操作', align: 'center', toolbar: '#userbar_certificate2', fixed: "right" }
                ]],
                data: _data,
                page: false
            });
            var _data = ret.Data.hold
            table.render({
                elem: '#Tables_certificate3',
                height: ["200px"],
                loading: true,
                limit: 100,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers',title: '序号' },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'SerialNum', title: '证书编号',width:200, align: 'center' },
                    { field: 'CategoryName', title: '证书类别',width: 180, align: 'center' },
                    { field: 'ExamSubject', title: '考核级次',width: 110, align: 'center' },
                    { field: 'IssueDate', title: '获取日期',width: 180, align: 'center' },
                    {
                        field: 'CertState', title: '发放状态',width: 120, align: 'center',
                        templet: '<div>已发放</div>'
                    },
                    {  title: '常用操作', align: 'center', toolbar: '#userbar_certificate3', fixed: "right" }
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
                , area: ['830px', '430px']
                ,offset: '10px'
                , shade: 0
                , content: $("#notice1")
                , btn: ['确认报考']
                , success: function (layero, index) {
                    $("#CategoryName").val(data.CategoryName);
                    $("#ExamSubject").val(data.ExamSubject);
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
        if (signstate) {
            var _data = {
                studentid: $("#condes input[name='studentid']").val(),
                certificateid: certificateid
            };
            signstate = false;
            var url = "/Handler/ExamCenter.ashx?action=signup";
            Params.Ajax(url, "post", _data, signup_success, signup_fail);
        }
        else {
            top.layer.msg("报考中请勿重复操作！", { icon: 5 });
        }
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
        signstate = true;
    }

    function signup_fail() {
        top.layer.msg("报名失败", { icon: 5 });
        signstate = true;
    }

    function getSubjects(_data) {
        table.render({
            elem: '#Tables_subjects',
            height: "185px",
            loading: true,
            text: { none: "暂无数据" },
            cols: [[
                { type: 'numbers', title: '序号',width: 60 },
                { field: 'Name', width: 300, title: '课程名称', align: 'center' },
                { field: 'Price', width: 150, title: '购买价格', align: 'center' },
                { field: 'Category', title: '类型', align: 'center' }
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
        if (obj.event === 'del') {
            layer.confirm('确定取消报名么', function (index) {
                Params.Ajax("/Handler/ExamCenter.ashx?action=cancel", "post", data, cancel_success, cancel_fail);
            });
        }
    });

    function cancel_success(ret) {
        if (ret.Code == 0) {
            top.layer.msg("取消成功", { icon: 1 });
            getCertificate();
            setTimeout(function () {
                layer.closeAll();
            }, 1000)
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    function cancel_fail(ret) {
        top.layer.msg("取消失败", { icon: 5 });
    }
    table.on('tool(Tables_certificate3)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            window.open('certifiprint.html?SerialNum=' + escape(data.SerialNum));
        }
    });
});