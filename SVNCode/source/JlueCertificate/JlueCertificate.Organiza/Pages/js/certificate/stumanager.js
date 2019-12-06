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
    var layerIndex = '';
    var examinee = ''
    var signstate = true;
    var roomid =""
    var seatid =""
    var w = ""
    getCertificate();
    getExamRoomList()
    $("#btnquery").on("click", function () {
        getCertificate();
    })

    function getCertificate() {
        var studentid = $("#condes input[name='studentid']").val();
        var unsignupurl = "/Handler/ExamCenter.ashx?action=getstudentcertifi&studentid=" + escape(studentid);
        Params.Ajax(unsignupurl, "get", "", getCertificate_success, get_fail);
    }
    function getCertificate_success(ret) {

        if (ret.Code == "0") {
            if (ret.Msg && ret.Msg.length > 0) {
                top.layer.msg(ret.Msg);
            }
            //初始化学员网校userid
            var _data = ret.Data.unsignup;
            table.render({
                elem: '#Tables_certificate1',
                height: ["200px"],
                limit: 100,
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers',title: '序号',width:60 },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'CategoryName', title: '证书类别',width: 180, align: 'center' },
                    { field: 'ExamSubject', title: '考核级次',width: 120, align: 'center' },
                    { field: 'StartTime', title: '本期考试开始时间',width: 160, align: 'center' },
                    { field: 'EndTime', title: '本期考试结束时间',width: 160, align: 'center' },
                    {  title: '常用操作', align: 'center', toolbar: '#userbar_certificate1', fixed: "right" }
                ]],
                data: _data,
                page: false
            });

            var _data = ret.Data.signup
            table.render({
                elem: '#Tables_certificate2',
                loading: true,
                height: ["200px"],
                limit: 100,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers',title: '序号',width: 60 },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'TicketNum', title: '准考证号',width: 190, align: 'center' },
                    { field: 'CategoryName', title: '证书类别',width: 180, align: 'center' },
                    { field: 'ExamSubject', title: '考核级次',width: 110, align: 'center' },
                    { field: 'StartTime', title: '本期考试开始时间',width: 150, align: 'center' },
                    { field: 'EndTime', title: '本期考试结束时间',width: 150, align: 'center' },
                    { title: '常用操作', align: 'center', toolbar: '#userbar_certificate2', fixed: "right" }
                ]],
                data: _data,
                page: false
            });
            var _data = ret.Data.hold
            table.render({
                elem: '#Tables_certificate3',
                height: ["200px"],
                loading: true,
                limit: 100,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers',title: '序号' },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'SerialNum', title: '证书编号',width:200, align: 'center' },
                    { field: 'CategoryName', title: '证书类别',width: 180, align: 'center' },
                    { field: 'ExamSubject', title: '考核级次',width: 110, align: 'center' },
                    { field: 'IssueDate', title: '获取日期',width: 180, align: 'center' },
                    {
                        field: 'CertState', title: '发放状态',width: 120, align: 'center',
                        templet: '<div>已发放</div>'
                    },
                    {  title: '常用操作', align: 'center', toolbar: '#userbar_certificate3', fixed: "right" }
                ]],
                data: _data,
                page: false
            });
        } else {
            top.layer.msg(ret.Msg);
        }
    }
    function get_fail(ret) {
        top.layer.msg("服务器异常", { icon: 5 });
    }
    //监听考场号下拉框
    form.on('select(testareanum)', function(data){
        console.log(data.value); //得到被选中的值
        var examRoomId = data.value
        roomid = data.value
        $("#setnum").html("")
        getSeatList(examRoomId)
    }); 
    form.on('select(setnum)',function(data){
        seatid = data.value
    })
    //监听工具条
    table.on('tool(Tables_certificate1)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            var _title = "报考证书";
            layer.open({
                type: 1
                , title: _title
                , area: ['830px', '430px']
                ,offset: '10px'
                , shade: 0
                , content: $("#notice1")
                , btn: ['确认报考']
                , success: function (layero, index) {
                    $("#CategoryName").val(data.CategoryName);
                    $("#ExamSubject").val(data.ExamSubject);
                    getSubjects(data.Subject);
                }
                , yes: function () {
                    signup(data.CertifiId);
                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        }
    });

    function signup(certificateid) {
        if (signstate) {
            var _data = {
                studentid: $("#condes input[name='studentid']").val(),
                certificateid: certificateid
            };
            signstate = false;
            var url = "/Handler/ExamCenter.ashx?action=signup";
            Params.Ajax(url, "post", _data, signup_success, signup_fail);
        }
        else {
            top.layer.msg("报考中请勿重复操作！", { icon: 5 });
        }
    }

    function signup_success(ret) {
        if (ret.Code == 0) {
            top.layer.msg("报考成功", { icon: 1 });
            getCertificate();
            setTimeout(function () {
                layer.closeAll();
            }, 1500);
        }
        else {
            top.layer.msg(ret.Msg, { icon: 5 });
        }
        signstate = true;
    }

    function signup_fail() {
        top.layer.msg("报名失败", { icon: 5 });
        signstate = true;
    }

    function getSubjects(_data) {
        table.render({
            elem: '#Tables_subjects',
            height: "185px",
            loading: true,
            text: { none: "暂无数据" },
            cols: [[
                { type: 'numbers', title: '序号',width: 60 },
                { field: 'Name', width: 300, title: '课程名称', align: 'center' },
                // { field: 'Price', width: 150, title: '购买价格', align: 'center' },
                { field: 'Category', title: '类型', align: 'center' }
            ]],
            data: _data,
            page: false,
            done: function (res, curr, count) {
            }
        });
    }
    
    table.on('tool(Tables_certificate2)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {     
            examinee = data.TicketNum
            var isconnectUrl = '/Handler/ExamCenter.ashx?action=isbountseat&TicketNum=' + examinee
            Params.Ajax(isconnectUrl, "get","", checkexamseat_success, get_fail);
            // layer.open({
            //     type: 1
            //     , title: "绑定考场座位号"
            //     , area: ['630px', '350px']
            //     , shade: 0
            //     , content: $("#notice2")
            //     , btn: ['确认绑定']
            //     , success: function (layero, index) {
            //         layerIndex = index;
            //         console.log(layerIndex)
            //         form.render('select');
            //     }
            //     , yes: function () {
            //         if(roomid =="" || seatid ==""){
            //             top.layer.msg("请先选择考场号或座位号")
            //         } else {
            //             //w = window.open();
            //             var params = {
            //                 examroomid: roomid,
            //                 seatnumber: seatid,
            //                 ticketid: examinee
            //             }
            //             Params.Ajax("/Handler/ExamCenter.ashx?action=updateexamseat", "post", params, saveexamseat_success, get_fail);
            //         }
            //     }
            //     , end: function () {
    
            //     }
            //     , zIndex: layer.zIndex
            // });
            //window.open('ticketprint.html?TicketNum=' + escape(examinee));
        }
        if (obj.event === 'del') {
            layer.confirm('确定取消报名么', function (index) {
                Params.Ajax("/Handler/ExamCenter.ashx?action=cancel", "post", data, cancel_success, cancel_fail);
            });
        }
    });
    //点击绑定座位号弹窗里的确认绑定按钮
    // $(".conform-btn").click(function(){
    //     if(roomid =="" || seatid ==""){
    //         top.layer.msg("请先选择考场号或座位号")
    //     } else {
    //         //w = window.open();
    //         var params = {
    //             examroomid: roomid,
    //             seatnumber: seatid,
    //             ticketid: examinee
    //         }
    //         Params.Ajax("/Handler/ExamCenter.ashx?action=updateexamseat", "post", params, saveexamseat_success, get_fail);
    //     }
    // })
    function cancel_success(ret) {
        if (ret.Code == 0) {
            top.layer.msg("取消成功", { icon: 1 });
            getCertificate();
            setTimeout(function () {
                layer.closeAll();
            }, 1000)
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    function cancel_fail(ret) {
        top.layer.msg("取消失败", { icon: 5 });
    }
    table.on('tool(Tables_certificate3)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            window.open('certifiprint.html?SerialNum=' + escape(data.SerialNum));
        }
    });

    //获取考场列表
    function getExamRoomList() {
        var unsignupurl = "/Handler/ExamCenter.ashx?action=getexaminfo";
        Params.Ajax(unsignupurl, "get", "", getExamRoomList_success, get_fail);
    }
    //获取座位号列表
    function getSeatList(id) {
        var unsignupurl = "/Handler/ExamCenter.ashx?action=getexamseatinfo&ExamRoomId=" + id;
        Params.Ajax(unsignupurl, "get", "", getSeatList_success, get_fail);
    }
    function getSeatList_success(ret){
        console.log(ret)
        if(ret.Code == "0"){
            var data = ret.Data
            console.log(data)
            var html = '<option value=""></option>'
            for(var i=0;i<data.length;i++){
                html += '<option value="'+ data[i].SeatNumber + '">'+ data[i].SeatNumber  +  '</option>'
            }
            $("#setnum").append(html)
            form.render('select');
        }
    }
    function getExamRoomList_success(ret){
        console.log(ret)
        if(ret.Code == "0"){
            var data = ret.Data
            var html = ''
            for(var i = 0;i<data.length;i++){
               html += '<option value="'+ data[i].id + '">'+ data[i].ExamNum  +  '</option>' 
            }
            $("#testareanum").append(html)
            form.render('select');
        } else {
            top.layer.msg(ret.Msg)
        }
    }
    function get_fail(ret) {
        top.layer.msg("服务器异常", { icon: 5 });
    }
    function saveexamseat_success(ret){
        if(ret.Code == "0"){
            top.layer.msg("保存成功",function(){
                layer.close(layerIndex)
                //w.location.href = 'ticketprint.html?TicketNum=' + escape(examinee)
                layer.open({
                    type: 2,
                    content: 'ticketprint.html?TicketNum=' + escape(examinee),
                    area: ['1000px', '550px'],
                    offset: '2px'

                })
                //window.open('ticketprint.html?TicketNum=' + escape(examinee));
            })
        } else {
            top.layer.msg(ret.Msg,function(){
                layer.close(layerIndex)
                //window.open('ticketprint.html?TicketNum=' + escape(examinee));
                //w.location.href = 'ticketprint.html?TicketNum=' + escape(examinee)
            })
        }
    }
    //检查准考证是否绑定座位
    function checkexamseat_success(ret) {
        if(ret.Code == "0"){
            layer.open({
                type: 2,
                content: 'ticketprint.html?TicketNum=' + escape(examinee),
                area: ['1000px', '550px'],
                offset: '2px'

            })
        } else if (ret.Code == "-1"){
            layer.open({
                type: 1
                , title: "绑定考场座位号"
                , area: ['630px', '350px']
                , shade: 0
                , content: $("#notice2")
                , btn: ['确认绑定']
                , success: function (layero, index) {
                    layerIndex = index;
                    console.log(layerIndex)
                    form.render('select');
                }
                , yes: function () {
                    if(roomid =="" || seatid ==""){
                        top.layer.msg("请先选择考场号或座位号")
                    } else {
                        //w = window.open();
                        var params = {
                            examroomid: roomid,
                            seatnumber: seatid,
                            ticketid: examinee
                        }
                        Params.Ajax("/Handler/ExamCenter.ashx?action=updateexamseat", "post", params, saveexamseat_success, get_fail);
                    }
                }
                , end: function () {
    
                }
                , zIndex: layer.zIndex
            });
        } else {
            top.layer.msg(ret.Msg)
        }
    }
});