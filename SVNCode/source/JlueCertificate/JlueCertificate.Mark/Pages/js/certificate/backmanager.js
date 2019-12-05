layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload', 'laydate'], function () {
    var $ = layui.$,
        layer = layui.layer,
        form = layui.form,
        table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
        common = layui.common,
        laydate = layui.laydate;
    //时间控件
    laydate.render({
        elem: '#StartTime',
        type: 'datetime',
    });
    laydate.render({
        elem: '#EndTime',
        type: 'datetime',
    });

    var curnum = 1;
    var limitcount = 10;
    var layerIndex = ""
    getCertificate();
    getallSubject();
    $("#btnquery").on("click", function () {
        getCertificate();
    })
    $("#btnadd").on("click", function () {
        var _title = "添加证书";
        layer.open({
            type: 1
            , title: _title
            , area: ['830px', '430px']
            , offset: "10px"
            , shade: 0
            , content: $("#notice1")
            , btn: ['确认添加']
            , success: function (layero, index) {
                layerIndex = index;
                console.log(layerIndex)
                $("#Id").val("");
                $("#CategoryName").val("");
                $("#ExamSubject").val("");
                $("#StartTime").val("");
                $("#EndTime").val("");
                $("#Describe").val("");
                $("#NormalResult").val("20");
                $("#ExamResult").val("80");
                $("#div_addsub").hide();
                getSubjectsByCertId();
            }
            , yes: function () {
                var startTime = $("#StartTime").val()
                var endTime = $("#EndTime").val()
                var startTimes = new Date(startTime).getTime();
                var endTimes = new Date(endTime).getTime();
                if(endTimes > startTimes){
                    handelCertificate("add");
                } else {
                    top.layer.msg("证书结束时间要大于开始时间") 
                }
            
            }
            , end: function () {

            }
            , zIndex: layer.zIndex
        });
    })

    $("#btn_addsubject").on("click", function () {
        var _title = "添加课程";
        layer.open({
            type: 1
            , title: _title
            , area: ['830px', '360px']
            , shade: 0
            , content: $("#notice2")
            , btn: ['确认添加']
            , success: function (layero, index) {
                layerIndex = index;
                console.log(layerIndex)
                $("#Id_sub").val("");
                $("#NormalResult_Sub").val("20");
                $("#ExamResult_Sub").val("80");
                $("#IsNeedExam").val("0");
                $("#ExamLength").val("");
                if ($("#SubjectId option")[0]) {
                    $("#SubjectId option")[0].selected = true;
                }
                form.render('select');
            }
            , yes: function () {
                handelsubject("add");
            }
            , end: function () {

            }
            , zIndex: layer.zIndex
        });
    })

    function getallSubject() {
        var url = "/Handler/UserCenter.ashx?action=getallsubjectnopage";
        Params.Ajax(url, "get", "", getallSubject_success, null);
    }
    function getallSubject_success(ret) {
        if (ret.Code == "0") {
            var _htmlp = '';
            $.each(ret.Data, function (i, p) {
                _htmlp += '<option desc=\'' + JSON.stringify(p) + '\' value=\'' + p.ID + '\'>' + p.Name + '</option>';
            })
            $("#SubjectId").html(_htmlp);
            form.render('select');
        }
    }

    function getSubjectsByCertId() {
        var _certid = $("#Id").val();
        var url = "/Handler/UserCenter.ashx?action=getsubjectsbycertid&certid=" + escape(_certid);
        Params.Ajax(url, "get", "", getSubjectsByCertId_success, get_fail);
    }

    function getSubjectsByCertId_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data;
            table.render({
                elem: '#Tables_subjects',
                limit: 1000,
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    //{ field: 'ID', title: '序号', align: 'center' },
                    {type: 'numbers',title: '序号',align: 'center'},
                    { field: 'Name', title: '课程名称',width: 300, align: 'center' },
                    { field: 'Category', title: '类型',width: 140, align: 'center' },
                    { field: 'ExamLength', title: '考试时长',width: 100, align: 'center' },
                    {
                        field: 'NormalResult', title: '平时成绩比例',width: 160, align: 'center',
                        templet: function (d) {
                            return d.NormalResult + '%';
                        }
                    },
                    {
                        field: 'ExamResult', title: '考试成绩比例',width: 160, align: 'center',
                        templet: function (d) {
                            return d.ExamResult + '%';
                        }
                    },
                    { width: 150, title: '常用操作', align: 'center', toolbar: '#userbar_sub', fixed: "right" }
                ]],
                data: _data,
                page: false,
                done: function (res, curr, count) {
                    laypage.render({
                        elem: 'laypage_subjects'
                        , count: ret.Stamp
                        , curr: curnum
                        , limit: limitcount
                        , layout: ['prev', 'page', 'next', 'skip', 'count', 'limit']
                        , jump: function (obj, first) {
                            if (!first) {
                                curnum = obj.curr;
                                limitcount = obj.limit;
                                var _certid = $("#Id").val();
                                var url = "/Handler/UserCenter.ashx?action=getsubjectsbycertid&certid=" + escape(_certid) + "&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getCertificate_success, get_fail);
                            }
                        }
                    })
                }
            });
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }

    function handelCertificate(type) {
        var _normalresult = Number($("#NormalResult").val().replace("%", ""));
        var _examresult = Number($("#ExamResult").val().replace("%", ""));
        if (isNaN(_normalresult) || isNaN(_examresult)) {
            top.layer.msg("成绩比例为0-100之间")
            return
        }
        if (_normalresult < 0 || _examresult < 0 || _normalresult > 100 || _examresult > 100) {
            top.layer.msg("成绩比例为0-100之间")
            return
        }
        if (_normalresult + _examresult != 100) {
            top.layer.msg("平时成绩和考试成绩总和必须为100")
            return
        }
        if (type == "edit") {
            var _normalsubresultsum = 0;
            var _examsubresultsum = 0;

            $.each($("#subjects td[data-field='NormalResult']"), function (i, m) {
                if ($(m).text().indexOf("%") > -1) {
                    _normalsubresultsum += Number($(m).text().replace("%", ""));
                }
            });
            $.each($("#subjects td[data-field='ExamResult']"), function (i, m) {
                if ($(m).text().indexOf("%") > -1) {
                    _examsubresultsum += Number($(m).text().replace("%", ""));
                }
            });
            //if (_normalsubresultsum != 100) {
            //    top.layer.msg("所有课程平时成绩总和必须为100", { icon: 2 });
            //    return;
            //}
            //if (_examsubresultsum != 100) {
            //    top.layer.msg("所有课程考试成绩总和必须为100", { icon: 2 });
            //    return;
            //}
        }
        var _data = {
            Id: $("#Id").val(),
            CategoryName: $("#CategoryName").val(),
            ExamSubject: $("#ExamSubject").val(),
            StartTime: $("#StartTime").val(),
            EndTime: $("#EndTime").val(),
            Describe: $("#Describe").val(),
            ExamResult: _examresult,
            NormalResult: _normalresult,
        };
        if (_data.CategoryName.length == 0) {
            top.layer.msg("证书类别不能为空", { icon: 2 });
            return
        }
        if (_data.ExamSubject.length == 0) {
            top.layer.msg("科目名称不能为空", { icon: 2 });
            return
        }
        if (_data.StartTime.length == 0) {
            top.layer.msg("本期考试开始时间不能为空", { icon: 2 });
            return
        }
        if (_data.EndTime.length == 0) {
            top.layer.msg("本期考试结束时间不能为空", { icon: 2 });
            return
        }
        var url = "/Handler/UserCenter.ashx?action=addcertificate";
        if (type == "edit") {
            url = "/Handler/UserCenter.ashx?action=updatecertificate";
        }
        Params.Ajax(url, "post", _data, handelCertificate_success, handelCertificate_fail);
    }
    function handelCertificate_success(ret) {
        if (ret.Code == 0) {

            if (ret.Data == 1) {
                top.layer.msg("该证书不能删除", { icon: 0 });
            }else  {
                top.layer.msg("操作成功", { icon: 1 });
                getCertificate();
                setTimeout(function () {
                    layer.closeAll();
                }, 1500)
            }

          
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    function handelCertificate_fail() {
        top.layer.msg("操作失败", { icon: 5 });
    }

    function handelsubject(type) {
        var _normalresult = Number($("#NormalResult_Sub").val().replace("%", ""));
        var _examresult = Number($("#ExamResult_Sub").val().replace("%", ""));
        var _ExamLength = $("#ExamLength").val();
        if (isNaN(_normalresult) || isNaN(_examresult)) {
            layer.msg("成绩比例为0-100之间")
            return
        }
        if (_normalresult < 0 || _examresult < 0 || _normalresult > 100 || _examresult > 100) {
            layer.msg("成绩比例为0-100之间")
            return
        }
        if (isNaN(_ExamLength) || _ExamLength == "") {
            top.layer.msg("考试时长不能为空")
            return
        }
        var _data = {
            ID: $("#Id_sub").val(),
            ExamResult: _examresult,
            NormalResult: _normalresult,
            IsNeedExam: $("#IsNeedExam")[0].value,
            ExamLength: $("#ExamLength").val(),
            SubjectId: $("#SubjectId")[0].value,
            CertificateId: $("#Id").val()
        };
        if (_data.ExamLength.length == 0) {
            _data.ExamLength = "0";
            return
        }
        var url = "/Handler/UserCenter.ashx?action=addcertifisubject";
        if (type == "edit") {
            url = "/Handler/UserCenter.ashx?action=updatecertifisubject";
        }
        Params.Ajax(url, "post", _data, handelsubject_success, handelsubject_fail);
    }
    function handelsubject_success(ret) {
        if (ret.Code == 0) {
            if (ret.Data == "课程不能删除！") {
                top.layer.msg("该课程不能删除", { icon: 0 });
            } else  {
                top.layer.msg("操作成功", { icon: 1 });
                getSubjectsByCertId();
                setTimeout(function () {
                layer.close(layerIndex);
                }, 1500)
            }
            
        } else {
            top.layer.msg(ret.Msg, { icon: 5 });
        }
    }
    function handelsubject_fail() {
        top.layer.msg("操作失败", { icon: 5 });
    }
    function getCertificate() {
        curnum = 1;
        limitcount = 10;
        var name = $("#condes input[name='name']").val();
        var url = "/Handler/UserCenter.ashx?action=getallcertificate&name=" + escape(name);
        Params.Ajax(url, "get", "", getCertificate_success, get_fail);
    }
    function getCertificate_success(ret) {
        if (ret.Code == "0") {
            layer.close(layerIndex)
            var _data = ret.Data
            table.render({
                elem: '#certificateTables',
                // height: "390px",
                limit: ret.Stamp,
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { type: 'numbers',title: '序号' },
                    //{ field: 'Id', width: 100, title: '序号', align: 'center' },
                    { field: 'CategoryName', title: '证书类别',width: 200, align: 'center' },
                    { field: 'ExamSubject', title: '考核级次',width: 100, align: 'center' },
                    { field: 'StartTime', title: '本期考试开始时间',width: 200, align: 'center' },
                    { field: 'EndTime', title: '本期考试结束时间',width: 200, align: 'center' },
                    {
                        field: 'NormalResult', title: '平时成绩',width: 100, align: 'center',
                        templet: function (d) {
                            return d.NormalResult + '%';
                        }
                    },
                    {
                        field: 'ExamResult', title: '考试成绩',width: 100, align: 'center',
                        templet: function (d) {
                            return d.ExamResult + '%';
                        }
                    },
                    { width: 200, title: '常用操作', align: 'center', toolbar: '#userbar', fixed: "right" }
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
                                var name = $("#condes input[name='name']").val();
                                var url = "/Handler/UserCenter.ashx?action=getunsignupcertificate&name=" + escape(name) + "&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getCertificate_success, get_fail);
                            }
                        }
                    })
                }
            });
        } else {
            top.layer.msg(ret.Msg);
        }
    }
    function get_fail(ret) {
        top.layer.msg("服务器异常")
    }
    //监听工具条
    table.on('tool(certificateTables)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            var _title = "修改证书";
            layer.open({
                type: 1
                , title: _title
                , area: ['830px', '430px']
                , offset: "10px"
                , shade: 0
                , content: $("#notice1")
                , btn: ['确认修改']
                , success: function (layero, index) {
                    $("#Id").val(data.Id);
                    $("#CertId").val(data.Id);
                    $("#CategoryName").val(data.CategoryName);
                    $("#ExamSubject").val(data.ExamSubject);
                    $("#NormalResult").val(data.NormalResult);
                    $("#ExamResult").val(data.ExamResult);
                    $("#StartTime").val(data.StartTime);
                    $("#EndTime").val(data.EndTime);
                    $("#Describe").val(data.Describe);
                    getSubjectsByCertId();
                    $("#div_addsub").show();
                }
                , yes: function () {
                    var startTime = $("#StartTime").val()
                    var endTime = $("#EndTime").val()
                    var startTimes = new Date(startTime).getTime();
                    var endTimes = new Date(endTime).getTime();
                    if(endTimes > startTimes){
                        handelCertificate("edit");
                    } else {
                        top.layer.msg("证书结束时间要大于开始时间") 
                    } 
                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        } else if (obj.event === 'del') {
           // data.CertificateId = $("#CertId").val();
            layer.confirm('注：确认删除' + data.CategoryName + '-' + data.ExamSubject + '?', function (index) {
                var url = "/Handler/UserCenter.ashx?action=delcertificate";
               
                Params.Ajax(url, "post", data, handelCertificate_success, handelCertificate_fail);
            });
        }
    });

    table.on('tool(Tables_subjects)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            var _title = "修改课程";
            layer.open({
                type: 1
                , title: _title
                , area: ['830px', '360px']
                , offset: '5px'
                , shade: 0
                , content: $("#notice2")
                , btn: ['确认修改']
                , success: function (layero, index) {
                    layerIndex = index
                    $("#Id_sub").val(data.ID);
                    $("#NormalResult_Sub").val(data.NormalResult);
                    $("#ExamResult_Sub").val(data.ExamResult);
                    $("#SubjectId").val(data.SubjectId);
                    $("#IsNeedExam").val(data.IsNeedExam);
                    $("#ExamLength").val(data.ExamLength);
                    form.render('select');
                }
                , yes: function () {
                    handelsubject("edit");
                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        } else if (obj.event === 'del') {
            data.CertificateId = $("#CertId").val();
            top.layer.confirm('注：确认删除' + data.Name + '-' + data.Category + '?', function (index) {
                var url = "/Handler/UserCenter.ashx?action=delcertifisubject";
                
                Params.Ajax(url, "post", data, handelsubject_success, handelsubject_fail);
            });
        }
    });
});