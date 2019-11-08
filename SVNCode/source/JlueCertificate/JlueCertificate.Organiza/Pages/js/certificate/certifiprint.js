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
        var unsignupurl = "/Handler/BaseData.ashx?action=getcertifiprint&SerialNum=" + escape(Params.getParamsFormUrl("SerialNum"));
        Params.Ajax(unsignupurl, "get", "", getcertifiprint_success, get_fail);
    }

    function getcertifiprint_success(ret) {
        if (ret.Code == "0") {
            if (ret.Msg && ret.Msg.length > 0) {
                top.layer.msg(ret.Msg, { icon: 5 });
            }
            $("#HeaderUrl").attr("src", ret.Data.HeaderUrl);
            $("#Name").val(ret.Data.Name);
            $("#CardId").val(ret.Data.CardId.substring(6, 14));
            $("#SerialNum").val(ret.Data.SerialNum);
            $("#IssueDate").val(ret.Data.IssueDate);
            $("#Sex").val(ret.Data.Sex);
            var Path = Params.getProjectPathFormUrl();
            $('#qrcode').qrcode({ width: 80, height: 80, text: Path + '/html/score/querydetail.html?Id=' + ret.Data.Id + "&OLSchoolUserId" + ret.Data.OLSchoolUserId });

        }
        else {
            top.layer.msg(ret.Msg, { icon: 5 });
        }
    }

    function get_fail(ret) {
        top.layer.msg("服务器异常", { icon: 5 });
    }
});