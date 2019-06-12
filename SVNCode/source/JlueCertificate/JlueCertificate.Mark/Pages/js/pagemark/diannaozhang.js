layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload', 'laydate'], function () {
    var layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common,
        laydate = layui.laydate;
    $(".site-doc-icon").on("click", "li", function () {
            layer.open({
              type: 1
              ,title: '选择' //不显示标题栏
              ,area: '400px;'
              ,shade: 0
              ,id: 'LAY_layuipro' //设定一个id，防止重复弹出
              ,btn: ['综合版会计实务', '电脑账', '报税']
              ,btnAlign: 'c'
              ,moveType: 1 //拖拽模式，0或者1
              ,content: $("#check-select")
              ,success: function(layero){
                 var url="http://jluepracticeautobookscore.kjcytk.com//Pages/Electronic/index.html?CourseId=16&identify=eb3ac3e163fe4138ac2b0c2e5a7c9520&token=" + getToken() + "&isexam=1";
                var btn = layero.find('.layui-layer-btn');
                btn.find('.layui-layer-btn1').attr({
                  href: url
                  ,target: '_blank'
                });
              }
            });
        //window.open("http://jluepracticeautobookscore.kjcytk.com//Pages/Electronic/index.html?CourseId=16&identify=eb3ac3e163fe4138ac2b0c2e5a7c9520&token=" + getToken() + "&isexam=1", "diannaozhang");
    })
    function getToken() {
        var d1 = new Date().getDate();
        var d2 = "EB3AC3E163FE4138AC2B0C2E5A7C9520";
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
        //if (ret.Code == "0") {
        //    var _data = ret.Data
        //    laypage.render({
        //        elem: 'laypage'
        //      , count: 100
        //      , limit: 156
        //      , layout: ['count', 'prev', 'page', 'next']
        //      , jump: function (obj,first) {
        //          console.log(obj)
        //          if (!first) {
        //              curnum = obj.curr;
        //              var _hangye = Number($("#hangye")[0].value);
        //              var url = "/Handler/UserCenter.ashx?action=hangyestudent&hangye=" + _hangye + "&page=" + curnum + "&limit=156";
        //              Params.Ajax(url, "get", "", getHangye_success, getHangye_fail);
        //          }
        //      }
        //    });
        //} else {
        //    layer.msg(ret.Msg);
        //}
    }
});