/**
 * Created by luo on 2016/9/9.
 */
var Params = {
    Ajax: function (url, type, data, success_callback, callback_error) {
        $.ajax({
            type: type,
            url: url,
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
            data: escape(JSON.stringify(data)),
            success: function (ret) {
                if (success_callback && typeof success_callback == "function") {
                    success_callback(ret)
                }
            },
            error: function (ret) {
                if (callback_error && typeof callback_error == "function") {
                    callback_error(ret)
                }
            }
        });
    },
    Login: function (data, success_callback, callback_error) {
        $.ajax({
            type: "get",
            url: "/Handler/Login.ashx",
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", escape(data));
            },
            success: function (ret) {
                if (success_callback && typeof success_callback == "function") {
                    success_callback(ret)
                }
            },
            error: function (ret) {
                if (callback_error && typeof callback_error == "function") {
                    callback_error(ret)
                }
            }
        });
    },
    setCookie: function (key, value, expiresDays) {
        var date = new Date();
        date.setTime(date.getTime() + expiresDays * 24 * 3600 * 1000);
        document.cookie = key + "=" + encodeURI(value) + "; expires=" + date.toGMTString() + ";Path=/";
    },
    getCookie: function (key) {
        var result = "";
        var strCookie = document.cookie;
        var arrCookie = strCookie.split("; ");
        for (var i = 0; i < arrCookie.length; i++) {
            var _cookie =arrCookie[i];
            var arr = _cookie.split("=");
            if (arr[0] == key) {
                if (_cookie.length > key.length + 1) {
                    result = decodeURI(_cookie.substring(key.length + 1))
                }
            }
        }
        return result;
    },
    getCookieDis: function (key) {
        var result = "";
        switch (key) {
            case "examid": result = this.getCookie("75D7E5E68D37A9D970C30CBFF9BBEB76"); break;
            case "cardid": result = this.getCookie("9CBADF809B610EEF45748B4DB31D68D1"); break;
            case "userid": result = this.getCookie("95633772267E796366D03622ACE232F4"); break;
            default:

        }
        return result
    },
    clrCookie: function () {
        Params.setCookie("75D7E5E68D37A9D970C30CBFF9BBEB76", "", -1);
        Params.setCookie("9CBADF809B610EEF45748B4DB31D68D1", "", -1);
    },
    getParamsFormUrl: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        var result = "";
        if (r != null) {
            result = decodeURI(r[2]);
            //return unescape(r[2]);
        }
        return result;
    },
    getProjectPathFormUrl: function () {
        var strFullPath = window.document.location.href;
        var strPath = window.document.location.pathname;
        var pos = strFullPath.indexOf(strPath);
        var prePath = strFullPath.substring(0, pos);
        var num = strPath.substr(1).indexOf('/');
        var postPath;
        if (num == -1) {
            postPath = strPath.substring(0, 5);
        } else {
            postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
        }
        return prePath + postPath;
    },
    getLastguid: function () {
        var _uuid = "";
        try {
            if (window.localStorage) {
                var storage = window.localStorage;
                _uuid = storage.getItem("UUID");
                if (_uuid && _uuid.length > 0) {
                    return _uuid;
                }
            }
        } catch (e) {
            this.Remark = e.message;
        }
        _uuid = this.uuid(16);
        try {
            if (window.localStorage && _uuid && _uuid.length > 0) {
                var storage = window.localStorage;
                storage.setItem("UUID", _uuid);
            }
        } catch (e) {
        }
        return _uuid;
    },
    uuid: function (radix) {
        var len = 36;
        var chars = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'.split('');
        var uuid = [], i;
        try {

            radix = radix || chars.length;

            uuid[8] = uuid[13] = uuid[18] = uuid[23] = '-';
            uuid[14] = '4';
            if (len) {
                // Compact form
                for (i = 0; i < len; i++) {
                    if (!uuid[i]) {
                        uuid[i] = chars[0 | Math.random() * radix];
                    }
                };
            } else {
                // rfc4122, version 4 form
                var r;

                // rfc4122 requires these characters
                //uuid[8] = uuid[13] = uuid[18] = uuid[23] = '-';
                //uuid[14] = '4';

                // Fill in random data.  At i==19 set the high bits of clock sequence as
                // per rfc4122, sec. 4.1.5
                for (i = 0; i < 36; i++) {
                    if (!uuid[i]) {
                        r = 0 | Math.random() * 16;
                        uuid[i] = chars[(i == 19) ? (r & 0x3) | 0x8 : r];
                    }
                }
            }

        } catch (e) {

        }
        return uuid.join('');
    },
    getDate: function () {
        var d = new Date();
        var curr_date = d.getDate();
        var curr_month = d.getMonth() + 1;
        var curr_year = d.getFullYear();
        String(curr_month).length < 2 ? (curr_month = "0" + curr_month) : curr_month;
        String(curr_date).length < 2 ? (curr_date = "0" + curr_date) : curr_date;
        var yyyyMMdd = curr_year + "-" + curr_month + "-" + curr_date;
        return yyyyMMdd;
    },
    Get: function (url, data, success_callback, callback_error) {
        $.ajax({
            type: "get",
            url: url,
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
            data: data,
            success: function (ret) {
                if (success_callback && typeof success_callback == "function") {
                    success_callback(ret)
                }
            },
            error: function (ret) {
                if (callback_error && typeof callback_error == "function") {
                    callback_error(ret)
                }
            }
        });
    }
}