layui.use('layer', function () { //独立版的layer无需执行这一句
    var $ = layui.jquery, layer = layui.layer; //独立版的layer无需执行这一句
    $('#detail').click(function () {
        var html = $('.qgks-top-list ul').html()
        layer.alert(html)
    })
    function beforeloadResult() {
        console.log("关闭页面中...")
    }
    window.onbeforeunload = function () {
        setTimeout(function () {
            setTimeout(beforeloadResult, 50)
        }, 50);
        return '确认离开网页？';
    }

    $('#ifContents').height(window.innerHeight - 102);
    $('#body_cool').height(window.innerHeight - 155);
    $('#top_ico  input').click(function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
            $('.qgks-top').css('top', '0px');
            $('#ifcont').css('paddingTop', '62px');
            $('#ifContents').height(window.innerHeight - 102);
        } else {
            $(this).addClass('active');
            $('.qgks-top').css('top', '-62px');
            $('#ifcont').css('paddingTop', '0px');
            $('#ifContents').height(window.innerHeight - 40);
        }
    })
    $(document).ready(function (e) {
        var iframe = document.getElementById("ifContents");
        if (iframe.attachEvent) {
            iframe.attachEvent("onload", function () {
                refreshEaxm();
            });
        } else {
            iframe.onload = function () {
                refreshEaxm();
            };
        }
    })
    window.nodeid = 0;
    refreshUserInfo();
    $("#examsub").on("click", function () {
        layer.open({
            type: 1
                , offset: "auto"
                , id: 'layerDemo1'
                , content: '<div style="padding: 20px 100px;">确定交卷？</div>'
                , btn: '确定交卷'
                , btnAlign: 'c' //按钮居中
                , shade: 0.3
                , closeBtn: true
                , yes: function () {
                    layer.closeAll();
                    Params.Ajax("/Handler/UserCenter.ashx?action=userinfo", "get", "", examsub_success, ajax_fail);
                }
                , cancel: function () {
                }
        });
    })
    function examsub() {
        layer.open({
            type: 1
                , offset: "auto"
                , id: 'layerDemo1'
                , content: '<div style="padding: 20px 100px;">时间到，请准备交卷</div>'
                , btn: '确定交卷'
                , btnAlign: 'c' //按钮居中
                , shade: 0.3
                , closeBtn: true
                , yes: function () {
                    layer.closeAll();
                    Params.Ajax("/Handler/UserCenter.ashx?action=userinfo" , "get", "", examsub_success, ajax_fail);
                }
                , cancel: function () {
                    window.clearInterval(window.refreshTimerHandler);
                    window.refreshTimerHandler = setInterval(function () {
                        refreshTimer();
                    }, 1000);
                }
        });
    }
    function refreshUserInfo() {
        Params.Ajax("/Handler/UserCenter.ashx?action=userinfo&nodeid=" + Params.getParamsFormUrl("nodeid"), "get", "", refreshUserInfo_success, ajax_fail);
    }
    function refreshUserInfo_success(ret) {
        if (ret.Code == "0") {
            window.nodeid = Params.getParamsFormUrl("nodeid");
            if (window.nodeid == "2") {
                $("#ifContents")[0].src = "https://ssl.jinglue.cn/DefaultIndex.aspx?SortId=524&CourseId=22&mobile=10905098704&identify=b6958fd846174f2aaa10dadfd374147e";
            }
            if (window.nodeid == "3") {
                //$("#ifContents")[0].src = "http://192.168.1.72:8049/main.aspx?userid=xxx&username=jljyxq&classid=9&courseid=10&sortid=500&IsFree=0&type=0"

                $("#ifContents")[0].src = "http://47.97.29.32:8099/QuestionMain.aspx?userid=testzhc&username=testzhc&classid=9&courseid=11&sortid=600";
                //$("#ifContents")[0].src = "http://192.168.1.72:8011/portal/index.aspx?userid=bbc4630ef2dc469d805467162cdeda6c&username=%C3%A6%C2%B5%C2%8B%C3%A8%C2%AF%C2%95%C3%A4%C2%BA%C2%BA%C3%A5%C2%91%C2%98&classid=9&courseid=10&sortid=500&questionId=338&userquestionId=3967&CompanyId=6&rand=1527126740057";
            }
        }
        else {
            layer.alert(ret.Msg);
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
    function refreshEaxm() {
        Params.Ajax("/Handler/UserCenter.ashx?action=userinfo", "get", "", refreshEaxm_success, ajax_fail);
    }
    function refreshEaxm_success(ret) {
        if (ret.Code == "0") {
            $("#examid").html(Params.getCookieDis("examid"));
            $("#cardid").html(Params.getCookieDis("cardid"));
            if (window.nodeid == "1") {
                $("#titlediv").html('<li>人才评价考试平台</li><li>综合版会计实务</li>');
            }
            if (window.nodeid == "2") {
                document.title = "电脑账考试-商业";
                $("#titlediv").html('<li>人才评价考试平台</li><li>电脑账</li>');
            }
            if (window.nodeid == "3") {
                document.title = "报税考试-国税";
                $("#titlediv").html('<li>人才评价考试平台</li><li>报税</li>');
            }
            window.eaxmTimer = 7200;
            var min = parseInt(window.eaxmTimer / 60);
            var sec = parseInt(window.eaxmTimer % 60);
            if (min <= 0) {
                min = 0;
            }
            if (sec <= 0) {
                sec = 0;
            }
            $("#min").html(min);
            $("#sec").html(sec);
            setTimeout(function () {
                window.refreshTimerHandler = setInterval(function () {
                    refreshTimer();
                }, 1000);
            }, 2000)
        }
        else {
            layer.alert(ret.Msg);
        }
    }
    function examsub_success(ret) {
        if (ret.Code == "0") {
            layer.open({
                type: 1
                , offset: "auto"
                , id: 'layerDemo2'
                , content: '<div style="padding: 20px 100px;">交卷成功</div>'
                , btn: '确定'
                , btnAlign: 'c' //按钮居中
                , shade: 0 //不显示遮罩
                , yes: function () {
                    setTimeout(CloseWebPage, 200);
                    layer.closeAll();
                }
            });
        }
        else {
            layer.alert(ret.Msg);
        }
    }
    function CloseWebPage() {
        var opened = window.open('about:blank', '_self');
        opened.opener = null;
        opened.close();
        //window.onbeforeunload = null;
        //if (navigator.userAgent.indexOf("MSIE") > 0) {
        //    if (navigator.userAgent.indexOf("MSIE 6.0") > 0) {
        //        window.opener = null; window.close();
        //    }
        //    else {
        //        window.open('', '_top'); window.top.close();
        //    }
        //}
        //else if (navigator.userAgent.indexOf("Firefox") > 0 || navigator.userAgent.indexOf("Chrome")  > 0) {
        //    window.location.href = 'about:blank ';
        //    //window.history.go(-2);     
        //}
        //else {
        //    window.opener = null;
        //    window.open('', '_self');
        //    window.close();
        //    window.location.href = 'about:blank ';
        //}
    }
    function refreshTimer() {
        if (window.eaxmTimer <= 0) {
            window.clearInterval(window.refreshTimerHandler);
            examsub();
        }
        window.eaxmTimer--;
        if (window.eaxmTime <= 0) {
            window.eaxmTime = 0;
        }
        var min = parseInt(window.eaxmTimer / 60);
        var sec = parseInt(window.eaxmTimer % 60);
        if (min <= 0) {
            min = 0;
        }
        if (sec <= 0) {
            sec = 0;
        }
        $("#min").html(min);
        $("#sec").html(sec);
    }
    function ajax_fail() {
        layer.msg("服务器异常")
    }
})

