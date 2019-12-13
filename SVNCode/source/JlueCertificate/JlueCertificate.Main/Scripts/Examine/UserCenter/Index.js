var baoshuihost = "http://tybscppublish.kjcytk.com"
//var baoshuihost = "http://192.168.1.115:8067"
//var baoshuihost = "http://192.168.10.195:8022"
var diannaozhanghost = "https://ssl.jinglue.cn"
//var diannaozhanghost = "http://192.168.1.115:8068"

var diannaozhangapihost = "http://114.55.38.113:8054"
//var diannaozhangapihost = "http://localhost:8014"

layui.use(['jquery', 'layer', 'form'], function () {
    var $ = layui.$,
        layer = layui.layer,
        form = layui.form;
    PageIni();
    function PageIni() {
        Params.Ajax("/Handler/UserCenter.ashx?action=userinfo", "get", "", PageIni_success, PageIni_error)
    }
    function PageIni_success(ret) {
        
        ret = ret || JSON.parse(ret);
        if (ret.Code == 0) {
            if (Params.getCookieDis("examid").length > 0 && Params.getCookieDis("cardid").length > 0) {
                ExamSubjectsInit(ret.Data)
                setTimeout(function () {
                    $("#foot_span").html(new Date().getFullYear())
                    $("#loader").css("display", "none")
                }, 1000)
            }
            else {
                layer.alert('登陆失效，重新登录？', { icon: 0 }, function (index) {
                    Params.clrCookie();
                    layer.closeAll();
                    window.parent.location.href = "../Login.html";
                });
            }
        }
        else {
            layer.msg(ret.Msg, { icon: 1, time: 1000 });
            if (Params.getCookieDis("examid").length > 0 && Params.getCookieDis("cardid").length > 0) {
            }
            else {
                setTimeout(function () {
                    layer.alert('登陆失效，重新登录？', { icon: 0 }, function (index) {
                        Params.clrCookie();
                        window.parent.location.href = "../Login.html";
                    });
                }, 1500)
            }
        }
    }
    function PageIni_error(ret) {
        layer.msg("请求错误", { icon: 1, time: 1000 });
    }
    function getItemByCate(data,key)
    {
        for(var mm=0;mm<data.subjects.length;mm++)
        {
            if(data.subjects[mm].Category==key)
            {
                return data.subjects[mm];
            }
        }
    }
    function getItemByCate2(data, key) {
        var isfind=false;
        for (var mm = 0; mm < data.subjects.length; mm++) {
            if (data.subjects[mm].Category == key) {
                if (isfind)
                {
                    return data.subjects[mm];
                }
                isfind = true;
            }
        }
    }
    function ExamSubjectsInit(data) {
        var study_url = 'http://cwrcpj.kjcytk.com/Member/DefaultLogin?UserName=' + data.OLSchoolUserName + '&UserPass=' + data.OLSchoolPWD;
        $("#lamstuday").click(function () { window.open(study_url, "_blank") });
        if (data.certificateLevel.indexOf("一星C") != -1) {//
            $("#subjects").hide();
            $("#subjectsyixing1").show();
        }
        else
        {
            $("#subjects").show();
            $("#subjectsyixing1").hide();
           
        }
        var startTime = new Date(data.certificateStartTime)
        var endTime = new Date(data.certificateEndTime)
        var dateIsValid = startTime <= new Date() && endTime >= new Date()
        data.dateIsValid = dateIsValid
        if (dateIsValid) {
            $("#loaderT").show();
            $("#studayView").hide();
        }
        else
        {
            $("#studayView").show();
            $("#loaderT").hide();
        }
        var subjects=data.subjects
        for(var i=subjects.length-1;i>=0;i--){
            if(subjects[i].Category=="视频"){
                subjects.splice(i,1)
            }
        }
        data.subjects.forEach(function (e, i) {
            e.index = NumberToChinese(i + 1)
        })
        var url = "/json/SubjectType.json"
        $.ajax({ type: "get", url: url, dataType: "json" }).success(function (ret) {
            var zhbkjsw = getItemByCate(data, "题库");
            var zhbkjsw2 = getItemByCate2(data, "题库");
            var dnz=getItemByCate(data,"实操-电脑账");
            var bs = getItemByCate(data, "实操-报税");
            $("#zhbkjsw").click(function () { subjectTypeCallBack(data, zhbkjsw, ret) });
            $("#dnz").click(function () { subjectTypeCallBack(data, dnz, ret) });
            $("#bs").click(function () { subjectTypeCallBack(data, bs, ret) });
            $("#cnsx").click(function () { subjectTypeCallBack(data, zhbkjsw, ret) });
            $("#cngw").click(function () { subjectTypeCallBack(data, zhbkjsw2, ret) });

        })
    }
    //data:
    //item:
    //ret:
    function subjectTypeCallBack(data, item, ret) {
        var url
        layer.alert('开始【' + item.Name + '】考试？', { icon: 0 }, function (index) {
            layer.closeAll()

            if (item.Category == ret.baoshui) {
                //userid添加后缀"_1"区分考试成绩记录与平时成绩记录
                url = baoshuihost + "/QuestionMainExam.aspx?userid=" + data.OLSchoolUserId + "_1&username=" + data.OLSchoolUserName + "&classid=" + data.orgClassId + "&courseid=" + item.OLSchoolCourseId + "&sortid=" + item.OLSchoolId + "&StudentTicketId=" + data.StudentTicketId + "&ExamLength=" + item.ExamLength + "&Name=" + item.Name
                window.open(url, "_blank")
            }
            else if (item.Category == ret.diannaozhang) {
                var url2 = diannaozhangapihost + "/Member/GetMobileAndIdentify?classid=" + data.orgClassId + "&OLSchoolUserId=" + data.OLSchoolUserId + "&OLSchoolId=" + item.OLSchoolId
                $.ajax({ url: url2, type: "get", async: false }).done(function (re) {
                    if (re.Data) {
                        url = diannaozhanghost + "/DefaultIndex.aspx?SortId=" + item.OLSchoolId + "&CourseId=" + item.OLAccCourseId + "&mobile=" + re.Data.GdMobile + "&identify=" + re.Data.Identify + "&StudentTicketId=" + data.StudentTicketId + "&studentid=" + data.OLSchoolUserId + "&ExamLength=" + item.ExamLength
                        //window.open(url, "_blank")
                        var open = window.open("_blank");
                        open.location = url;
                    }
                })
            }
            else if (item.Category == ret.tiku) {
                url = data.orgPath + "/JLStudent/ChongCi/PaperActionCopy?username=" + data.OLSchoolUserName + "&password=" + data.OLSchoolPWD + "&name=" + data.studentName + "&CardId=" + data.CardId + "&ProvinceID=" + item.OLSchoolProvinceId + "&CourseSort=" + item.OLSchoolId + "&CourseID=" + item.OLSchoolCourseId + "&PaperID=" + item.OLPaperID + "&Sort_Name=" + item.OLSchoolName + "&Source=CGX" + "&ExamLength=" + item.ExamLength + "userid=" + data.OLSchoolUserId + "&StudentTicketId=" + data.StudentTicketId 
                //url = "http://localhost:8360" + "/JLStudent/ChongCi/PaperActionCopy?username=" + data.OLSchoolUserName + "&password=" + data.OLSchoolPWD + "&name=" + data.studentName + "&CardId=" + data.CardId + "&ProvinceID=" + item.OLSchoolProvinceId + "&CourseSort=" + item.OLSchoolId + "&CourseID=" + item.OLSchoolCourseId + "&PaperID=" + item.OLPaperID + "&Sort_Name=" + item.OLSchoolName + "&Source=CGX"
                window.open(url, "_blank")
            }
        })
    }
})

var chnNumChar = ["零", "一", "二", "三", "四", "五", "六", "七", "八", "九"];
var chnUnitSection = ["", "万", "亿", "万亿", "亿亿"];
var chnUnitChar = ["", "十", "百", "千"];

function SectionToChinese(section) {
    var strIns = '', chnStr = '';
    var unitPos = 0;
    var zero = true;
    while (section > 0) {
        var v = section % 10;
        if (v === 0) {
            if (!zero) {
                zero = true;
                chnStr = chnNumChar[v] + chnStr;
            }
        } else {
            zero = false;
            strIns = chnNumChar[v];
            strIns += chnUnitChar[unitPos];
            chnStr = strIns + chnStr;
        }
        unitPos++;
        section = Math.floor(section / 10);
    }
    return chnStr;
}

function NumberToChinese(num) {
    var unitPos = 0;
    var strIns = '', chnStr = '';
    var needZero = false;
    if (num === 0) {
        return chnNumChar[0];
    }
    while (num > 0) {
        var section = num % 10000;
        if (needZero) {
            chnStr = chnNumChar[0] + chnStr;
        }
        strIns = SectionToChinese(section);
        strIns += (section !== 0) ? chnUnitSection[unitPos] : chnUnitSection[0];
        chnStr = strIns + chnStr;
        needZero = (section < 1000) && (section > 0);
        num = Math.floor(num / 10000);
        unitPos++;
    }
    return chnStr;
}

$(".submitexam").on("click", function () {
    layer.confirm('确认提交?', { icon: 3, title: '提示' }, function (index) {
        Params.Ajax("/Handler/UserCenter.ashx?action=submitexam", "get", "", submitexam_success, submitexam_error)

        layer.close(index);
    });
});

function submitexam_success(ret) {
    ret = ret || JSON.parse(ret);
    if (ret.Code == 0) {

    }
    else {
        layer.msg("提交失败", { icon: 1, time: 1000 });
    }
}

function submitexam_error(ret) {
    layer.msg("请求错误", { icon: 1, time: 1000 });
}