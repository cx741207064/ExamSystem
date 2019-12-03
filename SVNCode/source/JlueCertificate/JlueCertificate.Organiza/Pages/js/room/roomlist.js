layui.use(['layer', 'laypage','laydate', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
        layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
        laydate = layui.laydate,
        common = layui.common;
        editlayer = {};
        DetailedList = [];
        addRowlayer = {};
        restlist = [];
        deleteDetailedList = [];
    var curnum = 1;
    var limitcount = 10;
    getroomlistBykeyWord();//加载考场列表
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
        var addIndex=layer.open({
            type: 1
            , title: _title
            , area: ['750px', '430px']
            ,offset: '5px'
            , shade: 0
            , content: $("#notice1")
            , zIndex: layer.zIndex
        });
        table.render({
            elem: '#SeatTables',
            limit: 100,
            height: 360,
            text: { none: "暂无数据" },
            cols: [[
                { type: 'checkbox', fixed: 'left' },
                { field: 'SeatNumber', title: '座位号码', width: 150, align: 'center' }
            ]],
            page: false,
            data: restlist
        })
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
    function bangding_success(ret)
    {
        if (ret.Code == "0") {
            top.layer.msg("操作成功！");
            layer.close(addIndex);
        }
        else {
            top.layer.msg();
        }
    }
    function bangding_fail()
    {
        top.layer.msg("操作失败！");
    }
   
    $("#editfrom_save").on("click", function () {

        var _data = {
            Id: $("#editfrom_id").val(),
            ExamName: $("#editfrom_kdmc").val(),
            ExamPlace: $("#editfrom_kcdz").val(),
            CentreName: $("#editfrom_kcmc").val(),
            ExamNum: $("#editfrom_kch").val(),
            SeatNum: $("#editfrom_zws").val(),
            ResultReleaseTime: $("#editfrom_cjgbsj").val(),
            detal:DetailedList,
            del: deleteDetailedList,
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
        Params.Ajax("/Handler/ExamCenter.ashx?action=updateexamroom", "post", _data, editbangding_success, editbangding_fail);
    });
    function editbangding_success(ret) {
        if (ret.Code == "0") {
            DetailedList.length = 0;
            deleteDetailedList.length = 0;
            top.layer.msg("操作成功！");
            layer.close(editlayer);
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    function editbangding_fail() {
        top.layer.msg("操作失败！");
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
    
    $("#deleditrow").on("click", function () {
        let checkListValue = table.checkStatus('editfrom_SeatTables');
        for(var i=0;i<checkListValue.data.length;i++)
        {
            deleteDetailedList.push(checkListValue.data[i]);
            DetailedList = DetailedList.filter(function (item) {
                return item.Id != checkListValue.data[i].Id;

            });

        }
        table.render({
            elem: '#editfrom_SeatTables',
            height: 360,
            limit: 100,
            toolbar: 'default',
            text: { none: "暂无数据" },
            cols: [[
                { type: 'checkbox', fixed: 'left' },
                { field: 'Id', width: 80, title: '编码' },
                { field: 'SeatNumber', title: '座位号码', width: 150, align: 'center' }
            ]],
            page: false,
            data: DetailedList
        });
    })
    
    $("#addRow_btnsave").on("click", function () {
        var SeatNumberinfo = $("#edit_add_kdmc").val();
        for (var i = 0; i < DetailedList.length;i++)
        {
            if (SeatNumberinfo == DetailedList[i].SeatNumber)
            {
                top.layer.msg("座位号重复！");
                return;
            }
        }
        let info = { "Id": null, "ExamRoomId": null, "SeatNumber": SeatNumberinfo, "TicketId": "", "LAY_TABLE_INDEX": 25 };
        DetailedList.push(info);
        layer.close(addRowlayer);
        getroomlistBykeyWord();
        table.render({
            elem: '#editfrom_SeatTables',
            height: 360,
            limit: 100,
            toolbar: 'default',
            text: { none: "暂无数据" },
            cols: [[
                { type: 'checkbox', fixed: 'left' },
                { field: 'Id', width: 80, title: '编码' },
                { field: 'SeatNumber', title: '座位号码', width: 150, align: 'center' }
            ]],
            page: false,
            data: DetailedList
        });
    })
    $("#addeditrow").on("click", function () {
        addRowlayer = layer.open({
            type: 1
            , title: "添加行"
            , area: ['400px', '200px']
            , shade: 0
            , content: $("#addRownotice")
            , zIndex: layer.zIndex
        });
    })
    function getroomlistBykeyWord() {
        var roomname = $("#condes input[name='roomname']").val();
        var url = "/Handler/ExamCenter.ashx?action=getexamroom&name=" + roomname + "&page=" + curnum + "&limit=" + limitcount;
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
            var url = "/Handler/ExamCenter.ashx?action=getexamroombyid&id=" + data.id;
            Params.Ajax(url, "get", "",getroombyid,get_failback);
           
        }
    });
    function getroombyid(ret)
    {
        $("#editfrom_id").val(ret.Data.Id);
        $("#editfrom_kdmc").val(ret.Data.ExamName);
        $("#editfrom_kcmc").val(ret.Data.CentreName);
        $("#editfrom_kcdz").val(ret.Data.ExamPlace);
        $("#editfrom_kch").val(ret.Data.ExamNum);
        $("#editfrom_zws").val(ret.Data.SeatNum);
        $("#editfrom_cjgbsj").val(ret.Data.ResultReleaseTime);
        var _title = "修改考场";
        editlayer=layer.open({
            type: 1
            , title: _title
            , area: ['750px', '430px']
            ,offset: '10px'
            , shade: 0
            , content: $("#editfrom")
            , yes: function () {

            }
            , end: function () {

            }
            , zIndex: layer.zIndex
        });
        DetailedList = ret.Data.Detailed;
        table.render({
            elem: '#editfrom_SeatTables',
            height: 360,
            limit:100,
            toolbar: 'default',
            text: { none: "暂无数据" },
            cols: [[
                { type: 'checkbox', fixed: 'left' },
                { field: 'Id', width: 80, title: '编码' },
                { field: 'SeatNumber', title: '座位号码', width: 150, align: 'center' }
            ]],
            page: false,
            data: DetailedList
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