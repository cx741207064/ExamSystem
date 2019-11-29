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
    getscorequery();
    $("#btnquery").on("click", function () {
        getscorequery();
    });

    function getscorequery() {
        var ticketnum = $("#condes input[name='ticketnum']").val();
        var url = "/Handler/ScoreSearch.ashx?action=getscore&ticketnum=" + escape(ticketnum);
        Params.Ajax(url, "get", "", getscore_success, get_fail);
    }
    function getscore_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data.all
            table.render({
                elem: '#userTables',
                //height: "488px",
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers' },
                    //{ field: 'id', width: 100, title: '序号', align: 'center' },
                    { field: 'TicketNum',title: '准考证号',width: 300, align: 'center' },
                    { field: 'Name',title: '姓名',width: 140, align: 'center' },
                    { field: 'CategoryName',title: '证书类别',width: 200, align: 'center' },
                    { field: 'ExamSubject',  title: '考核级次',width: 100, align: 'center' },
                    { field: 'CreateTime',  title: '发证时间',width: 250, align: 'center' },
                    { width: 180, title: '常用操作', align: 'center', toolbar: '#userbar', fixed: "right" }
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
                                var ticketnum = $("#condes input[name='ticketnum']").val();
                                var url = "/Handler/ScoreSearch.ashx?action=getscore&ticketnum=" + escape(ticketnum)
                                + "&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getscore_success, get_fail);
                            }
                        }
                    });
                }
            });
        } else {
            layer.msg(ret.Msg);
        }
    }
    function get_fail(ret) {
        layer.msg("服务器异常");
    }

    function getscoredetail(_ticketid, _OLSchoolUserId) {
        var url = "/Handler/ScoreSearch.ashx?action=getscoredetail&OLSchoolUserId=" + escape(_OLSchoolUserId) + "&ticketid=" + escape(_ticketid);
        Params.Ajax(url, "get", "", getscoredetail_success, get_fail);
    }
    function getscoredetail_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data.all;
            $("#scoresum").text(ret.Data.scoresum);
            $("#accountform").text(ret.Data.accountform);
            table.render({
                elem: '#Tables_subjectcore',
                height: "250px",
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers' },
                    //{ field: 'id', width: 100, title: '序号', align: 'center' },
                    { field: 'Name', width: 200, title: '课程名称', align: 'center' },
                    { field: 'Category', width: 100, title: '课程类别', align: 'center' },
                    { field: 'NormalScore', width: 100, title: '平时成绩', align: 'center' },
                    {
                        field: 'NormalResult', width: 100, title: '平时比重', align: 'center',
                        templet: function (d) {
                            return d.NormalResult + '%';
                        }
                    },
                    { field: 'ExamScore', width: 100, title: '考试成绩', align: 'center' },
                    {
                        field: 'ExamResult', width: 100, title: '考试比重', align: 'center',
                        templet: function (d) {
                            return d.ExamResult + '%';
                        }
                    },
                ]],
                data: _data,
                page: false,
                done: function (res, curr, count) {
                }
            });
        } else {
            layer.msg(ret.Msg);
        }
    }

    table.on('tool(userTables)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            var _title = "查看成绩";
            layer.open({
                type: 2
                , title: _title
                , area: ['730px', '430px']
                ,offset: '10px'
                , shade: 0
                , content: ['querydetail.html?Id=' + data.Id + "&OLSchoolUserId=" + data.OLSchoolUserId, 'no']
                , btn: []
                , success: function (layero, index) {
                    //getscoredetail(data.Id, data.OLSchoolUserId);
                }
                , yes: function () {

                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        }
        else if (obj.event === 'usual') {
            var _title = "平时成绩";
            layer.open({
                type: 2
                , title: _title
                , area: ['1000px', '430px']
                ,offset: '10px'
                , shade: 0
                ,moveOut: true
                , content: ['querynormaldetail.html?Id=' + data.Id + "&OLSchoolUserId=" + data.OLSchoolUserId, 'no']
                , btn: []
                , success: function (layero, index) {
                    //getscoredetail(data.Id, data.OLSchoolUserId);
                }
                , yes: function () {

                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        }
    });
});