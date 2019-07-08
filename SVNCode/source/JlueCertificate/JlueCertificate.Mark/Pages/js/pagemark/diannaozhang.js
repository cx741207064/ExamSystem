layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload', 'laydate'], function () {
    var layer = layui.layer,
        form = layui.form,
        table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
        common = layui.common,
        laydate = layui.laydate;

    function getToken() {
        var d1 = new Date().getDate();
        var d2 = "B6958FD846174F2AAA10DADFD374147E";
        return $.md5(d1 + d2);
    }
    var curnum = 1;
    getHangye();
    function getHangye() {
        var url = "/Handler/UserCenter.ashx?action=hangye";
        Params.Ajax(url, "get", "", getHangye_success, getHangye_fail);
    }
    function getHangye_success(ret) {
        getHangyeStudent();
        if (ret.Code == "0") {
            //getHangyeStudent();
        } else {
            layer.msg(ret.Msg);
        }
    }
    function getHangye_fail(ret) {
        layer.msg("服务器异常")
    }

    function getHangyeStudent() {
        //curnum = 1;
        //var _hangye = $("#hangye")[0].value;
        var url = "/Handler/UserCenter.ashx?action=gethangye&hangye=1";
        Params.Ajax(url, "get", "", getHangyeStudent_success, getHangye_fail);
    }

    function getHangyeStudent_success(ret) {
        var _data = ret.Data
        laypage.render({
            elem: 'laypage'
            , count: 100
            , limit: 100
            , layout: ['count', 'prev', 'page', 'next']
            , jump: function (obj, first) {
                console.log(obj)
                if (!first) {
                    curnum = obj.curr;
                    var _hangye = Number($("#hangye")[0].value);
                    var url = "/Handler/UserCenter.ashx?action=hangyestudent&hangye=" + _hangye + "&page=" + curnum + "&limit=156";
                    Params.Ajax(url, "get", "", getHangye_success, getHangye_fail);
                }
            }
        });
    }
})