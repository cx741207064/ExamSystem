layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;

    getcertifiprint();
    bindprint();
    function bindprint() {
        $("#btn_print").bind("click", function () {
            $("#btn_div").hide();
            window.print();
            $("#btn_div").show();
        })
    }

    function getcertifiprint() {
        var unsignupurl = "/Handler/BaseData.ashx?action=getticketprint&TicketNum=" + escape(Params.getParamsFormUrl("TicketNum"));
        Params.Ajax(unsignupurl, "get", "", getcertifiprint_success, get_fail);
    }

    function getcertifiprint_success(ret) {
        if (ret.Code == "0") {
            if (ret.Msg && ret.Msg.length > 0) {
                top.layer.msg(ret.Msg, { icon: 5 });
            }
            $("#HeaderUrl").attr("src", ret.Data.HeaderUrl);
            $("#StartTime").val(ret.Data.StartTime);
            $("#TicketNum").val(ret.Data.TicketNum);
            $("#Name").val(ret.Data.Name);
            $("#Sex").val(ret.Data.Sex);
            $("#CardId").val(ret.Data.CardId);
        }
        else {
            top.layer.msg(ret.Msg, { icon: 5 });
        }
    }

    function get_fail(ret) {
        top.layer.msg("服务器异常", { icon: 5 });
    }
});