layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;
   
    $("#btnquery").on("click", function () {
        getscorequery();
    })

    function getscorequery() {
        var name = $("#condes input[name='name']").val();
        var cardid = $("#condes input[name='cardid']").val();
        var url = "/Handler/ExamCenter.ashx?action=getstudent&name=" + escape(name) + "&cardid=" + escape(cardid);
        Params.Ajax(url, "get", "", getstudent_success, getstudent_fail);
    }
    function getstudent_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data
            table.render({
                elem: '#userTables',
                height: "488px",
                loading: true,
                text: "暂无数据",
                cols: [[
                    { field: 'id', width: 100, title: '序号', align: 'center' },
                    { field: 'idnumber', width: 300, title: '学籍号', align: 'center' },
                    { field: 'name', width: 300, title: '姓名', align: 'center' },
                    { field: 'cardid', width: 300, title: '身份证号', align: 'left' },
                    { field: 'telphone', width: 200, title: '电话', align: 'left' },
                    { field: 'sex', width: 100, title: '性别', align: 'left' },
                    { field: 'createtime', width: 300, title: '报名时间', align: 'left' },
                    { width: 200, title: '常用操作', align: 'center', toolbar: '#userbar', fixed: "right" }
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
    function getstudent_fail(ret) {
        layer.msg("服务器异常")
    }
    
});