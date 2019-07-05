var baoshuihost = "http://47.97.29.32:8099"
var diannaozhanghost = "https://ssl.jinglue.cn"
var diannaozhangapihost = "http://114.55.38.113:8054"

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
                    $("#loaderT").css("display", "block")
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

    function ExamSubjectsInit(data) {
        var startTime = new Date(data.certificateStartTime)
        var endTime = new Date(data.certificateEndTime)
        var dateIsValid = startTime <= new Date() && endTime >= new Date()
        data.dateIsValid = dateIsValid
        data.subjects.forEach(function (e, i) {
            e.index = NumberToChinese(i+1)
        })

        var Vue1 = new Vue({
            el: '#subjects',
            data: data,
            mounted: function () {
            },
            updated: function () {
                form.render()
            },
            methods: {
                tips: function (item, event) {
                    var dom = event.target
                    var $this = this
                    if (dom.getAttribute("dateIsValid")) {
                        var url = "/json/SubjectType.json"
                        $.ajax({ type: "get", url: url, dataType: "json" }).success(function (ret) {
                            subjectTypeCallBack($this.$data, item, ret)
                        })
                    }
                    else {
                        layer.tips("当前时间不在允许考试的时间范围内(" + data.certificateStartTime + "至" + data.certificateEndTime + ")", dom, { tips: 1, time: 0, area: 'auto', maxWidth: 500 })
                        $("#loaderT").unbind()
                    }
                }
            }
        })
    }

    function subjectTypeCallBack(data, item, ret) {
        var url
        layer.alert('开始【' + item.Name + '】考试？', { icon: 0 }, function (index) {
            layer.closeAll()

            if (item.Category == ret.baoshui) {
                url = baoshuihost + "/QuestionMain.aspx?userid=" + data.OLSchoolUserId + "&username=" + data.OLSchoolUserName + "&classid=" + data.orgClassId + "&courseid=" + item.OLSchoolCourseId + "&sortid=" + item.OLSchoolId
                window.open(url, "_blank")
            }
            else if (item.Category == ret.diannaozhang) {
                var url2 = diannaozhangapihost + "/Member/GetMobileAndIdentify?classid=" + data.orgClassId + "&OLSchoolUserId=" + data.OLSchoolUserId + "&OLSchoolId=" + item.OLSchoolId
                $.ajax({ url: url2, type: "get" }).done(function (re) {
                    if (re.Data) {
                        url = diannaozhanghost + "/DefaultIndex.aspx?SortId=" + item.OLSchoolId + "&CourseId=" + item.OLAccCourseId + "&mobile=" + re.Data.GdMobile + "&identify=" + re.Data.Identify
                        window.open(url, "_blank")
                    }
                })
            }
            else if (item.Category == ret.tiku) {
                url = data.orgPath + "/JLStudent/ChongCi/PaperActionCopy?username=" + data.OLSchoolUserName + "&password=" + data.OLSchoolPWD + "&ProvinceID=" + item.OLSchoolProvinceId + "&CourseSort=" + item.OLSchoolId + "&CourseID=" + item.OLSchoolCourseId + "&PaperID=" + item.OLPaperID + "&Sort_Name=" + item.OLSchoolName
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