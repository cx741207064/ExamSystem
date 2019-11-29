layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;

    getscoredetail();

    function getscoredetail() {
        var url = "/Handler/ScoreSearch.ashx?action=getscoredetail&ticketid=" + escape(Params.getParamsFormUrl("Id")) + "&OLSchoolUserId=" + escape(Params.getParamsFormUrl("OLSchoolUserId"));
        Params.Ajax(url, "get", "", getscoredetail_success, get_fail);
    }
    function getscoredetail_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data.all;
            $("#scoresum").text(ret.Data.scoresum);
            var scoresum = ret.Data.scoresum;
            if (scoresum >= 90) {
                $("#examresult").html("优秀");
            }
            else if (scoresum < 90 && scoresum >= 80) {
                $("#examresult").html("良好");
            }
            else if (scoresum < 80 && scoresum >= 60) {
                $("#examresult").html("及格");
            }
            else if (scoresum < 60) {
                $("#examresult").html("不及格");
            }
            $("#accountform").text(ret.Data.accountform);
            table.render({
                elem: '#Tables_subjectcore',
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers' },
                    //{ field: 'id', width: 100, title: '序号', align: 'center' },
                    { field: 'Name', title: '课程名称', align: 'center' },
                    { field: 'Category', title: '课程类别', align: 'center' },
                    { field: 'NormalScore', title: '平时成绩', align: 'center' },
                    {
                        field: 'NormalResult', title: '平时比重', align: 'center',
                        templet: function (d) {
                            return d.NormalResult + '%';
                        }
                    },
                    { field: 'ExamScore', title: '考试成绩', align: 'center' },
                    {
                        field: 'ExamResult', title: '考试比重', align: 'center',
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

    function get_fail(ret) {
        layer.msg("服务器异常");
    }
});