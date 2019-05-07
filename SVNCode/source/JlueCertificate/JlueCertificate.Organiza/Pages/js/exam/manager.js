﻿layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;
    var curnum = 1;
    var limitcount = 10;
    getstudent();
    getcity();
    //普通图片上传
    var uploadInst = upload.render({
        elem: '#uploadimg1'
      , url: "/Handler/BaseData.ashx?action=uploadheader"
      , accept: "images"
      , method: "POST"
      , done: function (res, index, upload) {
          res = res || JSON.parse(res) ;
          if (res.Code == "0") {
              $("#uploadimg1_div").css("display", "none");
              $('#uploadimg1').css('background-image', 'url(' + res.Data + ')');
              $('#uploadimg1').attr('desc', res.Data);
          }
          else {
              $("#uploadimg1_div").css("display", "");
              $('#uploadimg1').css('background-image', "");
              $('#uploadimg1').attr('desc', "");
              top.layer.msg(res.Msg)
          }
      }
    });
    form.on('select(province)', function (data) {
        var _tags = data.elem.getElementsByTagName("option");
        $.each(_tags, function (i, m) {
            if (m.value  == data.value) {
                var _desc = $(m).attr("desc");
                if (_desc) {
                    var _descJson = JSON.parse(_desc);
                    if (_descJson) {
                        var _htmlc = '';
                        $.each(_descJson.citys, function (j, c) {
                            _htmlc += '<option value=\'' + c.id + '\' desc = \'' + JSON.stringify(c) + '\'>' + c.name + '</option>';
                            if (j == 0) {
                                var _htmlz = ''
                                $.each(c.zones, function (k, z) {
                                    _htmlz += '<option value=\'' + z.id + '\'>' + z.name + '</option>';
                                })
                                $("#zone").html(_htmlz);
                            }
                        })
                        $("#city").html(_htmlc);
                        form.render('select');
                    }
                }
            }
        })
    });
    form.on('select(post_province)', function (data) {
        var _tags = data.elem.getElementsByTagName("option");
        $.each(_tags, function (i, m) {
            if (m.value == data.value) {
                var _desc = $(m).attr("desc");
                if (_desc) {
                    var _descJson = JSON.parse(_desc);
                    if (_descJson) {
                        var _htmlc = '';
                        $.each(_descJson.citys, function (j, c) {
                            _htmlc += '<option value=\'' + c.id + '\' desc = \'' + JSON.stringify(c) + '\'>' + c.name + '</option>';
                            if (j == 0) {
                                var _htmlz = ''
                                $.each(c.zones, function (k, z) {
                                    _htmlz += '<option value=\'' + z.id + '\'>' + z.name + '</option>';
                                })
                                $("#post_zone").html(_htmlz);
                            }
                        })
                        $("#post_city").html(_htmlc);
                        form.render('select');
                    }
                }
            }
        })
    });
    form.on('select(city)', function (data) {
        var _tags = data.elem.getElementsByTagName("option");
        $.each(_tags, function (i, m) {
            if (m.value == data.value) {
                var _desc = $(m).attr("desc");
                if (_desc) {
                    var _descJson = JSON.parse(_desc);
                    if (_descJson) {
                        var _htmlc = '';
                        $.each(_descJson.zones, function (j, c) {
                            _htmlc += '<option value=\'' + c.id + '\' >' + c.name + '</option>';
                        })
                        $("#zone").html(_htmlc);
                        form.render('select');
                    }
                }
            }
        })
    });
    form.on('select(post_city)', function (data) {
        var _tags = data.elem.getElementsByTagName("option");
        $.each(_tags, function (i, m) {
            if (m.value == data.value) {
                var _desc = $(m).attr("desc");
                if (_desc) {
                    var _descJson = JSON.parse(_desc);
                    if (_descJson) {
                        var _htmlc = '';
                        $.each(_descJson.zones, function (j, c) {
                            _htmlc += '<option value=\'' + c.id + '\' >' + c.name + '</option>';
                        })
                        $("#post_zone").html(_htmlc);
                        form.render('select');
                    }
                }
            }
        })
    });
    $("#btnquery").on("click", function () {
        getstudent();
    })
    $("#btnadd").on("click", function () {
        $("#idnumber").val("");
        $("#name").val("")
        $("#cardid").val("")
        $("#telphone").val("")
        $("#address").val("")
        $("#post_address").val(""),
        $("#uploadimg1_div").css("display", "");
        $('#uploadimg1').css('background-image', "");
        $('#uploadimg1').attr('desc', "");
        if ($("#sex option")[0]) {
            $("#sex option")[0].selected = true;
        }
        if ($("#province option")[0]) {
            $("#province option")[0].selected = true;
            var _desc = $($("#province option")[0]).attr("desc");
            if (_desc) {
                var _descJson = JSON.parse(_desc);
                if (_descJson) {
                    var _htmlc = '';
                    $.each(_descJson.citys, function (j, c) {
                        _htmlc += '<option value=\'' + c.id + '\' desc = \'' + JSON.stringify(c) + '\'>' + c.name + '</option>';
                        if (j == 0) {
                            var _htmlz = ''
                            $.each(c.zones, function (k, z) {
                                _htmlz += '<option value=\'' + z.id + '\'>' + z.name + '</option>';
                            })
                            $("#zone").html(_htmlz);
                        }
                    })
                    $("#city").html(_htmlc);
                }
            }
        }
        
        if ($("#post_province option")[0]) {
            $("#post_province option")[0].selected = true;
            var _desc = $($("#post_province option")[0]).attr("desc");
            if (_desc) {
                var _descJson = JSON.parse(_desc);
                if (_descJson) {
                    var _htmlc = '';
                    $.each(_descJson.citys, function (j, c) {
                        _htmlc += '<option value=\'' + c.id + '\' desc = \'' + JSON.stringify(c) + '\'>' + c.name + '</option>';
                        if (j == 0) {
                            var _htmlz = ''
                            $.each(c.zones, function (k, z) {
                                _htmlz += '<option value=\'' + z.id + '\'>' + z.name + '</option>';
                            })
                            $("#post_zone").html(_htmlz);
                        }
                    })
                    $("#post_city").html(_htmlc);
                }
            }
        }
        form.render('select');
        Params.Ajax("/Handler/ExamCenter.ashx?action=getstudentid", "get", "", getstudentid_success, getstudent_fail);
    })
    $("#btnadd2").on("click", function () {
        var _data = {
            idnumber: $("#idnumber").val(),
            name: $("#name").val(),
            headerurl :$('#uploadimg1').attr('desc'),
            cardid: $("#cardid").val(),
            sex:  $("#sex")[0].value,
            telphone: $("#telphone").val(),
            provinceid: $("#province")[0].value,
            cityid:  $("#city")[0].value,
            zoneid: $("#zone")[0].value,
            postprovinceid: $("#post_province")[0].value,
            postcityid: $("#post_city")[0].value,
            postzoneid: $("#post_zone")[0].value,
            address: $("#address").val(),
            postaddress: $("#post_address").val(),
        }
        if (_data.name.length == 0) {
            top.layer.msg("姓名不能为空");
            return 
        }
        if (_data.cardid.length == 0) {
            top.layer.msg("省份证号不能为空");
            return 
        }
        if (_data.telphone.length == 0) {
            top.layer.msg("手机号不能为空");
            return 
        }
        var _type = Number($("#btnadd2").attr("type"))
        if (_type == 1) {
            Params.Ajax("/Handler/ExamCenter.ashx?action=addstudent", "post", _data, addstudent_success, getstudent_fail);
        }
        else if (_type == 2) {
            Params.Ajax("/Handler/ExamCenter.ashx?action=updatestudent", "post", _data, addstudent_success, getstudent_fail);
        }
    })
    function addstudent(data) {
        var url = "/Handler/ExamCenter.ashx?action=addstudent";
        Params.Ajax(url, "post", data, getstudent_success, getstudent_fail);
    }
    function addstudent_success(ret) {
        if (ret.Code == 0) {
            var _type = Number($("#btnadd2").attr("type"))
            if (_type == 1) {
                top.layer.msg("新增成功");
            }
            else if (_type == 2) {
                top.layer.msg("修改成功");
            }
            getstudent();
            setTimeout(function () {
                layer.closeAll();
            },1000)
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    function getstudentid_success(ret) {
        if (ret.Code == "0") {
            $("#idnumber").val(ret.Data)
            $("#signup").val(Params.getDate())
            $("#btnadd2").attr("type", "1")
            $("#btnadd2 cite").html("新增")
            var _title = "学员登记";
            layer.open({
                type: 1
                , title: _title
                , area: ['830px', '710px']
                , shade: 0
                , content: $("#notice1")
                , yes: function () {

                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        } else {
            top.layer.msg(ret.Msg);
        }
    }
    function getstudent() {
        curnum = 1;
        limitcount = 10;
        var name = $("#condes input[name='name']").val();
        var cardid = $("#condes input[name='cardid']").val();
        var url = "/Handler/ExamCenter.ashx?action=getstudent&name=" + escape(name) + "&cardid=" + escape(cardid);
        Params.Ajax(url, "get", "", getstudent_success, getstudent_fail);
    }
    function getstudent_success(ret) {
        if (ret.Code == "0") {
            var _data = ret.Data
            table.render({
                elem: '#userTables',
                loading: true,
                text: { none: "暂无数据" },
                cols: [[
                    { field: 'id',  title: '序号', align: 'center' },
                    { field: 'idnumber',  title: '学籍号', align: 'center' },
                    { field: 'name', title: '姓名', align: 'center' },
                    { field: 'cardid',  title: '身份证号', align: 'left' },
                    { field: 'telphone', title: '电话', align: 'left' },
                    { field: 'sex',  title: '性别', align: 'left' },
                    { field: 'createtime', title: '报名时间', align: 'left' },
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
                                var cardid = $("#condes input[name='cardid']").val();
                                var url = "/Handler/ExamCenter.ashx?action=getstudent&name=" + escape(name) + "&cardid=" + escape(cardid) + "&page=" + curnum + "&limit=" + limitcount;
                                Params.Ajax(url, "get", "", getstudent_success, getstudent_fail);
                            }
                        }
                    })
                }
            });
        } else {
            top.layer.msg(ret.Msg);
        }
    }
    function getstudent_fail(ret) {
        top.layer.msg("服务器异常")
    }
    function getcity() {
        var url = "/Handler/BaseData.ashx?action=getcity";
        Params.Ajax(url, "get", "", getcity_success, getstudent_fail);
    }
    function getcity_success(ret) {
        if (ret.Code == "0") {
            var _htmlp = '';
            $.each(ret.Data, function (i, p) {
                _htmlp += '<option value=\'' + p.id + '\' desc = \'' + JSON.stringify(p) + '\'>' + p.name + '</option>';
                if (i == 0) {
                    var _htmlc = '';
                    $.each(p.citys, function (j, c) {
                        _htmlc += '<option value=\'' + c.id + '\' desc = \'' + JSON.stringify(c) + '\'>' + c.name + '</option>';
                        if (j == 0) {
                            var _htmlz = ''
                            $.each(c.zones, function (k, z) {
                                _htmlz += '<option value=\'' + z.id + '\'>' + z.name + '</option>';
                            })
                            $("#zone").html(_htmlz);
                            $("#post_zone").html(_htmlz);
                        }
                    })
                    $("#city").html(_htmlc);
                    $("#post_city").html(_htmlc);
                }
            })
            $("#province").html(_htmlp);
            $("#post_province").html(_htmlp);
            form.render('select');
        }
    }
    function deletestudent_success(ret) {
        if (ret.Code == 0) {
            top.layer.msg("删除成功");
            getstudent();
            setTimeout(function () {
                layer.closeAll();
            }, 1000)
        }
        else {
            top.layer.msg(ret.Msg);
        }
    }
    //监听工具条
    table.on('tool(userTables)', function (obj) {
        var data = obj.data;
        if (obj.event === 'edit') {
            $("#btnadd2").attr("type", "2")
            $("#btnadd2 cite").html("修改")
            $("#idnumber").val(data.idnumber);
            $("#name").val(data.name);
            if (data.headerurl.length > 0) {
                $("#uploadimg1_div").css("display", "none");
                $('#uploadimg1').css('background-image', 'url(' + data.headerurl + ')');
                $('#uploadimg1').attr('desc', data.headerurl);
            }
            else{
                $("#uploadimg1_div").css("display", "");
                $('#uploadimg1').css('background-image', "");
                $('#uploadimg1').attr('desc', "");
            }
            $("#cardid").val(data.cardid);
            $("#telphone").val(data.telphone);
            $("#signup").val(data.createtime);
            $("#address").val(data.address);
            $("#post_address").val(data.postaddress);
            var _sex = "1";
            if (data.sex == "女") {
                _sex = "2";
            }
            $.each($("#sex option"),function(i,n){
                if (n.value == _sex) {
                    n.selected = true;
                }
            })
            $.each($("#province option"), function (i, m) {
                if (m.value == data.provinceid) {
                    m.selected = true;
                    var _desc = $(m).attr("desc");
                    if (_desc) {
                        var _descJson = JSON.parse(_desc);
                        if (_descJson) {
                            var _htmlc = '';
                            $.each(_descJson.citys, function (j, c) {
                                _htmlc += '<option value=\'' + c.id + '\' desc = \'' + JSON.stringify(c) + '\'>' + c.name + '</option>';
                            })
                            $("#city").html(_htmlc);
                            $.each($("#city option"), function (j, c) {
                                if (c.value == data.cityid) {
                                    c.selected = true;
                                    var _descz = $(c).attr("desc");
                                    if (_descz) {
                                        var _desczJson = JSON.parse(_descz);
                                        if (_desczJson) {
                                            var _htmlz = '';
                                            $.each(_desczJson.zones, function (j, z) {
                                                _htmlz += '<option value=\'' + z.id + '\' >' + z.name + '</option>';
                                            })
                                            $("#zone").html(_htmlz);
                                            $.each($("#zone option"), function (k, z) {
                                                if (z.value == data.zoneid) {
                                                    z.selected = true;
                                                }
                                            })
                                        }
                                    }
                                }
                            })
                        }
                    }
                }
            })

            $.each($("#post_province option"), function (i, m) {
                if (m.value == data.postprovinceid) {
                    m.selected = true;
                    var _desc = $(m).attr("desc");
                    if (_desc) {
                        var _descJson = JSON.parse(_desc);
                        if (_descJson) {
                            var _htmlc = '';
                            $.each(_descJson.citys, function (j, c) {
                                _htmlc += '<option value=\'' + c.id + '\' desc = \'' + JSON.stringify(c) + '\'>' + c.name + '</option>';
                            })
                            $("#post_city").html(_htmlc);
                            $.each($("#post_city option"), function (j, c) {
                                if (c.value == data.postcityid) {
                                    c.selected = true;
                                    var _descz = $(c).attr("desc");
                                    if (_descz) {
                                        var _desczJson = JSON.parse(_descz);
                                        if (_desczJson) {
                                            var _htmlz = '';
                                            $.each(_desczJson.zones, function (j, z) {
                                                _htmlz += '<option value=\'' + z.id + '\' >' + z.name + '</option>';
                                            })
                                            $("#post_zone").html(_htmlz);
                                            $.each($("#post_zone option"), function (k, z) {
                                                if (z.value == data.postzoneid) {
                                                    z.selected = true;
                                                }
                                            })
                                        }
                                    }
                                }
                            })
                        }
                    }
                }
            })

            form.render('select');
            var _title = "学员修改";
            layer.open({
                type: 1
                , title: _title
                , area: ['830px', '710px']
                , shade: 0
                , content: $("#notice1")
                , yes: function () {

                }
                , end: function () {

                }
                , zIndex: layer.zIndex
            });
        } else if (obj.event === 'shouquan') {
            layer.alert('授权行：<br>' + JSON.stringify(data))
        } else if (obj.event === 'disable') {
            layer.alert('禁用行：<br>' + JSON.stringify(data))
        } else if (obj.event === 'del') {
            layer.confirm('真的删除行么', function (index) {
                Params.Ajax("/Handler/ExamCenter.ashx?action=deletestudent", "post", data, deletestudent_success, getstudent_fail);
            });
        }
    });
});