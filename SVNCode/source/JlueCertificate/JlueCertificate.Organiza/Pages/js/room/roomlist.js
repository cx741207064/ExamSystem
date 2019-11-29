layui.use(['layer', 'laypage','laydate', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
        layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
        laydate = layui.laydate,
        common = layui.common;
    restlist = [{ "SeatNumber": "1" }, { "SeatNumber": "2" }, { "SeatNumber": "3" }];
    var curnum = 1;
    var limitcount = 10;
    $("#btnquery").on("click", function () {
        getroomlistBykeyWord();
    });
    laydate.render({elem: '#cjgbsj',format: 'yyyy-MM-dd'});
    $("#btnadd").on("click", function () {
        $("#kdmc").val("");
        $("#kcmc").val("");
        $("#kcdz").val("");
        $("#kch").val("");
        $("#zws").val("");
        $("#cjgbsj").val("");
        var _title = "添加考场";
        layer.open({
            type: 1
            , title: _title
            , area: ['750px', '650px']
            , shade: 0
            , content: $("#notice1")
            , yes: function () {

            }
            , end: function () {

            }
            , zIndex: layer.zIndex
        });
        table.render({
            elem: '#SeatTables',
            height:360,
            toolbar: 'default',
            text: { none: "暂无数据" },
            cols: [[
                { type: 'checkbox', fixed: 'left' },
                { field: 'SeatNumber', title: '座位号码', width: 150, align: 'center' }
            ]],
            page: false,
            data: restlist
        });
    })
    $("#btnsave").on("click",function(){

        var _data = {
            ExamName: $("#kdmc").val(),
            ExamPlace: $("#kcdz").val(),
            CentreName: $("#kcmc").val(),
            ExamNum: $("#kch").val(),
            SeatNum: $("#zws").val(),
            ResultReleaseTime: $("#cjgbsj").val(),
            detal:restlist
        }
        if (_data.ExamName.length == 0) {
            top.layer.msg("考点名称不能为空！");
            return
        }
        if (_data.ExamPlace.length == 0) {
            top.layer.msg("考场地址不能为空！");
            return
        }
        if (_data.CentreName.length == 0) {
            top.layer.msg("考场名称不能为空！");
            return
        }
        if (_data.ExamNum.length == 0) {
            top.layer.msg("考场号不能为空！");
            return
        }
        if (_data.SeatNum.length == 0) {
            top.layer.msg("座位数不能为空！");
            return
        }
        if (_data.ResultReleaseTime.length == 0) {
            top.layer.msg("成绩公布时间不能为空！");
            return
        }
        Params.Ajax("/Handler/ExamCenter.ashx?action=addexamroom", "post", _data, bangding_success, bangding_fail);
    });
    function bangding_success()
    {
    }
    function bangding_fail()
    {
    }
    $("#btnquery").on("click", function () {
        getroomlistBykeyWord();
    });
    $("#addrows").on("click", function () {
        let zws = $("#zws").val();
        restlist.length = 0;
        for (let i = 0; i <= zws; i++) {
            let info = { "SeatNumber":i };
            restlist.push(info);
        }
        table.render({
            elem: '#SeatTables',
            limit:100,
            height:360,
            text: { none: "暂无数据" },
            cols: [[
                { type: 'checkbox', fixed: 'left' },
                { field: 'SeatNumber', title: '座位号码', width: 150, align: 'center' }
            ]],
            page: false,
            data: restlist
        })
    })
    $("#addrow").on("click", function () {

    })
    $("#delrow").on("click", function () {
  
    })
    function getroomlistBykeyWord() {
        var roomname = $("#condes input[name='roomname']").val();
        var url = "/Handler/ExamCenter.ashx?action=getexamroom&name=" + roomname+ "&page=1&limit=" + limitcount;
        Params.Ajax(url, "get", "", getroomlistback, get_failback);
    }
    function get_failback(ret)
    {
        layer.msg("服务器异常");
    }
    function getroomlistback(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data;
            table.render({
                elem: '#userTables',
                loading: true,
                limit:100,
                text: { none: "暂无数据" },
                cols: [[
                    { field: 'id', width: 60, title: '编码', align: 'center' },
                    { field: 'ExamName', title: '考点名称',width: 150, align: 'center' },
                    { field: 'CentreName', title: '考场名称', width: 200, align: 'center' },
                    { field: 'ExamPlace', title: '考场地址', width: 200, align: 'center' },
                    { field: 'ExamNum', title: '考场号',width: 100, align: 'center' },
                    { field: 'SeatNum', title: '座位数', width: 250, align: 'center' },
                    { field: 'ResultReleaseTime', title: '成绩公布时间', width: 250, align: 'center' },
                    { field: 'createtime', title: '建立时间', width: 250, align: 'center' },
                    { width: 180, title: '常用操作', align: 'center', toolbar: '#bar', fixed:'right'}
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
                                var roomname = $("#condes input[name='roomname']").val();
                                roomname=escape(roomname);
                                var data={"name":roomname,"page":curnum,"limit":limitcount};
                                var url = "/Handler/ExamCenter.ashx?action=getexamroom&name=" + escape(roomname)+"&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getroomlistback, get_failback);
                            }
                        }
                    });
                }
            });
        } else {
            layer.msg(ret.Msg);
        }
    }
    table.on('tool(userTables)', function(obj){ //注：tool 是工具条事件名，test 是 table 原始容器的属性 lay-filter="对应的值"
        var data = obj.data ,
            layEvent = obj.event; //获得 lay-event 对应的值
        if(layEvent === 'detail'){
            layer.msg('查看操作');
        } else if(layEvent === 'del'){
            layer.confirm('是否删除？', function(index){
                var url = "/Handler/ExamCenter.ashx?action=deleteexamroom";
                Params.Ajax(url, "post", data,delroomback,get_failback);
                
                layer.close(index);
                //向服务端发送删除指令
            });
        } else if(layEvent === 'edit'){
            var url = "/Handler/ExamCenter.ashx?action=deleteexamroom";
            Params.Ajax(url, "post", data,getroombyid,get_failback);
           
        }
    });
    function getroombyid(ret)
    {
        
        $("#editfrom_kdmc").val("");
        $("#editfrom_kcmc").val("");
        $("#editfrom_kcdz").val("");
        $("#editfrom_kch").val("");
        $("#editfrom_zws").val("");
        $("#editfrom_cjgbsj").val("");
        var _title = "添加考场";
        layer.open({
            type: 1
            , title: _title
            , area: ['750px', '650px']
            , shade: 0
            , content: $("#notice1")
            , yes: function () {

            }
            , end: function () {

            }
            , zIndex: layer.zIndex
        });
        table.render({
            elem: '#SeatTables',
            height:360,
            toolbar: 'default',
            text: { none: "暂无数据" },
            cols: [[
                { type: 'checkbox', fixed: 'left' },
                { field: 'SeatNumber', title: '座位号码', width: 150, align: 'center' }
            ]],
            page: false,
            data: restlist
        });
    }
    function delroomback(ret)
    {
        layer.msg(ret.Msg);
        getroomlistBykeyWord();
    }
    function getscoredetail(_ticketid, _OLSchoolUserId) {
        var url = "/Handler/ScoreSearch.ashx?action=getscoredetail&OLSchoolUserId=" + escape(_OLSchoolUserId) + "&ticketid=" + escape(_ticketid);
        Params.Ajax(url, "get", "", getscoredetail_success, get_fail);
    }
});