
layui.use(['jquery', 'layer'], function () {
    Params.clrCookie();
    var $ = layui.$,
        layer = layui.layer,
        form = layui.form;

    $("#jq_submit").on("click", function (e) {
        if ($("#examid").val().length <= 0) {
            layer.tips('请输入准考证号', $('#examid'), {
                tips: [3, '#FF5722']
            });
            return;
        }
        if ($("#cardid").val().length <= 0) {
            layer.tips('请输入身份证号', $('#cardid'), {
                tips: [3, '#FF5722']
            });
            return;
        }
        if ($("#vcode").val().length <= 0) {
            layer.tips('请输入验证码', $('#vcode'), {
                tips: [3, '#FF5722']
            });
            return;
        }
        Params.Login("examid=" + $("#examid").val() + "&cardid=" + $("#cardid").val() + "&vcode=" + $("#vcode").val(), login_success, login_error)
    })
    $("#jq_submit2").on("click", function (e) {
        window.parent.location.reload();
    })
    function VerfyCode() {
        $("#captchaImage")[0].src = $("#captchaImage")[0].src + '?';
    }
    function login_success(ret) {
        ret = ret || JSON.parse(ret);
        if (ret.Code == "0") {
            layer.msg('登录成功', { icon: 1, time: 1000 });
            setTimeout(function () {
                window.parent.location.href = "UserCenter/Index.html";
            },2000)
        }
        else {
            layer.msg(ret.Msg, { icon: 1, time: 1000 });
            VerfyCode();
        }
    }
    function login_error(ret) {
        layer.msg("服务异常", { icon: 1, time: 1000 });
        VerfyCode();
    }
});

