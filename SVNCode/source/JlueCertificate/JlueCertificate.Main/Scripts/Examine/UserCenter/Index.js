
layui.use(['jquery','layer', 'form'], function () {
    var $ = layui.$,
        layer = layui.layer,
        form = layui.form;
    PageIni();
    $("#loaderT").on("click", ".btn-vf", function (e) {
        var _desc = this.value;
        var _nodeid = $(this).attr("nodeid");
        layer.alert('开始【' + _desc + '】考试？', { icon: 0 }, function (index) {
            if (_nodeid == "2" || _nodeid == "3") {
                window.open("exam.html?nodeid=" + _nodeid, _desc);
            }
            if (_nodeid == "1") {
                //window.open("http://192.168.2.43:8090/JLStudent/ChongCi/PaperActionCopy?username=jljyxc&password=123&ProvinceID=29&CourseSort=8&CourseID=2&PaperID=f0d49a4e9e054b018a05486547111568&Sort_Name=%E5%88%9D%E7%BA%A7%E4%BC%9A%E8%AE%A1%E5%AE%9E%E5%8A%A1", _desc);
                window.open("http://test.kjqx.com/JLStudent/ChongCi/PaperActionCopy?username=testzhc&password=123456&ProvinceID=29&CourseSort=8&CourseID=2&PaperID=d1ef903218e74bb49e2eabe8a524a780&Sort_Name=%E7%BB%BC%E5%90%88%E7%89%88%E4%BC%9A%E8%AE%A1%E5%AE%9E%E5%8A%A1", _desc);
            }
            layer.closeAll();
        });
        //var _data = {
        //    certifiid: $(this).attr("certifiid"),
        //    subjectid : $(this).attr("subjectid")
        //}
        //Params.Ajax("/Handler/UserCenter.ashx?action=subjectinfo", "post", _data, SubjectInfo_Success, PageIni_error)
    })
    function SubjectInfo_Success(ret) {
        ret = ret || JSON.parse(ret);
        if (ret.Code == 0) {
            if (Params.getCookieDis("examid").length > 0 && Params.getCookieDis("cardid").length > 0) {
                layer.alert('开始【' + ret.Data.subjectname + '】考试？', { icon: 0 }, function (index) {
                    debugger;
                    if (ret.Data.iswinopen) {
                        window.open(ret.Data.url, ret.Data.subjectname);
                    }
                    else {
                        window.open("exam.html?nodeid=" + _nodeid, ret.Data.subjectname);
                    }
                    //if (_nodeid == "2" || _nodeid == "3") {
                    //    window.open("exam.html?nodeid=" + _nodeid, _desc);
                    //}
                    //if (_nodeid == "1") {
                    //    window.open("http://192.168.2.43:8090/JLStudent/ChongCi/PaperActionCopy?username=jljyxc&password=123&ProvinceID=29&CourseSort=8&CourseID=2&PaperID=f0d49a4e9e054b018a05486547111568&Sort_Name=%E5%88%9D%E7%BA%A7%E4%BC%9A%E8%AE%A1%E5%AE%9E%E5%8A%A1", _desc);
                    //}
                    layer.closeAll();
                });
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
    function PageIni() {
        Params.Ajax("/Handler/UserCenter.ashx?action=userinfo", "get", "", PageIni_suncess, PageIni_error)
    }
    function PageIni_suncess(ret) {
        ret = ret || JSON.parse(ret);
        if (ret.Code == 0) {
            if ( Params.getCookieDis("examid").length > 0 && Params.getCookieDis("cardid").length > 0) {
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

    function ExamSubjectsInit(data){
        var startTime=new Date(data.certificateStartTime)
        var endTime=new Date(data.certificateEndTime)
        var dateIsValid=startTime<=new Date()&&endTime>=new Date()
        data.dateIsValid=dateIsValid
        data.subjects.forEach(function(e,i){
            e.index=NumberToChinese(e.index)
        })

        var Vue1=new Vue({
            el: '#subjects',
            data: data,
            mounted:function(){
            },
            updated:function(){
                form.render()
            },
            methods: {
                tips: function (event) {
                    var dom=event.target
                    if(dom.getAttribute("dateIsValid")){

                    }
                    else{
                        layer.tips("当前时间不在允许考试的时间范围内("+data.certificateStartTime+"至"+data.certificateEndTime+")",dom,{tips: 1,time:0,area:'auto',maxWidth:500})
                        dom.disabled="disabled"
                    }
                }
              }
        })
    }

})

var chnNumChar = ["零","一","二","三","四","五","六","七","八","九"];
var chnUnitSection = ["","万","亿","万亿","亿亿"];
var chnUnitChar = ["","十","百","千"];

function SectionToChinese(section){
    var strIns = '', chnStr = '';
    var unitPos = 0;
    var zero = true;
    while(section > 0){
        var v = section % 10;
        if(v === 0){
            if(!zero){
                zero = true;
                chnStr = chnNumChar[v] + chnStr;
            }
        }else{
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

function NumberToChinese(num){
      var unitPos = 0;
      var strIns = '', chnStr = '';
      var needZero = false;
      if(num === 0){
        return chnNumChar[0];
      }
      while(num > 0){
        var section = num % 10000;
        if(needZero){
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