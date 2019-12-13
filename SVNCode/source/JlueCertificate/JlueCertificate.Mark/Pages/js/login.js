layui.use(['jquery','common','layer','form','larryMenu'],function(){
    var $ = layui.$,
        layer = layui.layer,
        form = layui.form,
        common = layui.common;
    // 页面上下文菜单
    var larryMenu = layui.larryMenu();

    var mar_top = ($(document).height()-$('#larry_login').height())/2.5;
    $('#larry_login').css({ 'margin-top': mar_top });
    //common.larryCmsSuccess('账号：张三 密码：111  输入正确后直接登录!', '后台帐号登录提示', 20);
    var placeholder = '';
    $("#larry_form input[type='text'],#larry_form input[type='password']").on('focus',function(){
          placeholder = $(this).attr('placeholder');
          $(this).attr('placeholder','');
    });
    $("#larry_form input[type='text'],#larry_form input[type='password']").on('blur',function(){
          $(this).attr('placeholder',placeholder);
    });
    common.larryCmsLoadJq('../../common/plus/jquery.supersized.min.js', function() {
        $.supersized({
            // 功能
            slide_interval: 3000,
            transition: 1,
            transition_speed: 1000,
            performance: 1,
            // 大小和位置
            min_width: 0,
            min_height: 0,
            vertical_center: 1,
            horizontal_center: 1,
            fit_always: 0,
            fit_portrait: 1,
            fit_landscape: 0,
            // 组件
            slide_links: 'blank',
            slides: [{
                image: '../pages/images/login/1.jpg'
            }, {
                image: '../pages/images/login/2.jpg'
            }, {
                image: '../pages/images/login/3.jpg'
            }]
        });
    });
    Params.clrCookie();

    form.on('submit(submit)', function (data) {
        Params.Login("uid=" + data.field.user_name + "&pwd=" + $.md5(data.field.password) + "&vcode=" + data.field.code, login_success, login_fail);
        return false;
    });
    function login_success(ret) {
        if (ret.Code == 0) {
            layer.msg('登录成功', { icon: 1, time: 1000 });
            setTimeout(function () {
                window.location.href = 'index.html';
            }, 1000);
        }
        else {
            layer.msg(ret.Msg, { icon: 1, time: 1000 });
            VerfyCode();
        }
    }

    function login_fail(ret) {

        layer.tips('服务异常', $('#password'), {
            tips: [3, '#FF5722']
        });
        VerfyCode();
    }
    function VerfyCode() {
        $("#codeimage")[0].src = $("#codeimage")[0].src + '?';
    }
    // 右键菜单控制
    var larrycmsMenuData = [
        [{
            text: "刷新页面",
            func: function() {
                top.document.location.reload();
            }
        }, {
            text: "检查元素(F12)",
            func: function() {
                common.larryCmsError('抱歉！暂不支持此功能，可访问精略官网交流', common.larryCore.tit);
            }
        }],
        [{
            text: "访问精略官网",
            func: function() {
                window.open('http://www.jinglve.com');
            }
        }]
    ];
    larryMenu.ContentMenu(larrycmsMenuData,{
         name: "html" 
    },$('html'));
});
    