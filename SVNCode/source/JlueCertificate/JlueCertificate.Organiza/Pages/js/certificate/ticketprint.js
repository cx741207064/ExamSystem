layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;

    getcertifiprint();
    getcertifiprintInfo();
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
    function getcertifiprintInfo(){
        var printInfoUrl = "/Handler/ExamCenter.ashx?action=getticketprintinfo&TicketNum=" + escape(Params.getParamsFormUrl("TicketNum"));
        //var printInfoUrl = "/Handler/ExamCenter.ashx?action=getticketprintinfo&TicketNum=ZKH180510165121233";
        Params.Ajax(printInfoUrl, "get", "", getcertifiprintInfo_success, get_fail);
    }
    function getcertifiprint_success(ret) {
        if (ret.Code == "0") {
            if (ret.Msg && ret.Msg.length > 0) {
                top.layer.msg(ret.Msg, { icon: 5 });
            }
            console.log(ret.Data)
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
    function getcertifiprintInfo_success(ret){
        if (ret.Code == "0") {
            if (ret.Msg && ret.Msg.length > 0) {
                top.layer.msg(ret.Msg, { icon: 5 });
            }
            console.log(ret.Data)
            var yearMonth = ret.Data.StartTime.slice(0,10)
            var starttime = ret.Data.StartTime.slice(11,16)
            var endtime = ret.Data.EndTime.slice(11,16)
            var examtime = starttime + "-" + endtime
            var openday = ret.Data.ResultReleaseTime.slice(0,10)
            console.log(yearMonth)
            console.log(starttime)
            console.log(endtime)
            console.log(examtime)
            $("#ExamSchool").val(ret.Data.ExamName);
            $("#ExamAddress").val(ret.Data.ExamPlace);
            $("#ExamLevel").val(ret.Data.ExamSubject);
            $("#CentreName").val(ret.Data.CentreName);
            $("#ExamSubject").val(ret.Data.CategoryName);
            $("#ExamDate").val(yearMonth);
            $("#ExamTime").val(examtime);
            $("#ExamHallNum").val(ret.Data.ExamNum);
            $("#SeatNum").val(ret.Data.SeatNumber);
            $("#PublishDate").val(openday)
        }
        else {
            top.layer.msg(ret.Msg, { icon: 5 });
        }
    }
    function get_fail(ret) {
        top.layer.msg("服务器异常", { icon: 5 });
    }
});