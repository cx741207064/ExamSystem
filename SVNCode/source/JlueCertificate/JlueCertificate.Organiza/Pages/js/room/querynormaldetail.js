var wangxiaohost = "http://jluerawx.kjcytk.com"
var Vue1
var app = new Vue({
  el: "#jindu",
  data: {
    videoreat: "",
    lookvideo: "",
    allvideo: ""
  }
})

layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload', 'element'], function () {
  var $ = layui.$,
    layer = layui.layer,
    form = layui.form,
    table = layui.table,
    upload = layui.upload,
    laypage = layui.laypage,
    common = layui.common,
    element = layui.element;

  var url = "/Handler/ScoreSearch.ashx?action=getSubjectsByTicket&id=" + Params.getParamsFormUrl("id")
  Params.Ajax(url, "get", "", pageInitSuccessCallBack)
  function pageInitSuccessCallBack(ret) {
    table.render({
      elem: '#Tables_subjectcore'
      , cols: [[ //标题栏
        { field: 'subjectName', title: '课程名', minWidth: 200 }
        , { field: 'subjectType', title: '课程类别', width: 150 }
        , { field: 'score', title: '得分', width: 80 }
        , { width: 200, title: '操作', align: 'center', toolbar: '#userbar', fixed: "right" }
      ]]
      , data: ret.Data
      , even: false
    })

    table.on('tool(Tables_subjectcore)', function (obj) {
      var data = obj.data;
      if (data.subjectType === '视频') {
        var _title = "查看成绩";
        var info =
        {
          "AOMid": data.AOMid,
          "PublicMark": true,
          "Sort_Id": data.Sort_Id,
          "studentid": Params.getParamsFormUrl("OLSchoolUserId")
        }
        Params.Get(wangxiaohost + '/api/VideoJinDu/Check', info, setdetailAjax)
      }
      else if (data.subjectType === "题库") {
        var url = "http://114.55.38.113:8054/TiKu/Paper/CoursePaperList"
        var da = {
          "classid": 9,
          "userid": "3a30108ff834499db688866d305ac1c7",
          "sortid": 8
        }

        var da1 = {
          "classid": data.classId,
          "userid": Params.getParamsFormUrl("OLSchoolUserId"),
          "sortid": data.Sort_Id
        }

        $.when($.ajax({ type: "get", url: url, data: da }), $.ajax({ type: "get", url: url, data: da1 })).done(function (ret, ret1) {
          setTikuData(ret1[0])
        })

      }
    })

    function setTikuData(ret) {
      Vue1.Data = ret.Data

      layer.open({
        type: 1,
        shade: 0,
        moveOut: true,
        title: '分数详情', //不显示标题
        area: ['750px', '390px'], //宽高
        content: $('#exam-papers'), //捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响
        cancel: function () {
        }, success: function (layero, index) {
        },
      })

    }

    var setdetailAjax = function (ret) {
      if (ret.code == "0") {
        if (ret.code == 0) {
          app.lookvideo = ret.data.count
          app.allvideo = ret.data.sum
          app.videoreat = ret.data.persent
          for (var m = 0; m < ret.data.listAll.length; m++) {
            if (ret.data.listAll[m].creatiDateTime) {
              ret.data.listAll[m].creatiDateTimenew = ret.data.listAll[m].creatiDateTime.split('T')[0]
            } else {
              ret.data.listAll[m].creatiDateTimenew = ""
            }
          }
          table.render({
            elem: '#test2',
            url: '',
            page: { //支持传入 laypage 组件的所有参数（某些参数除外，如：jump/elem） - 详见文档
              layout: ['limit', 'count', 'prev', 'page', 'next', 'skip'] //自定义分页布局
            },
            cols: [
              [{
                field: 'sortName',
                width: 200,
                title: '视频标题',
                sort: true
              }, {
                field: 'userName',
                width: 100,
                title: '客户账号'
              }, {
                field: 'aoMid',
                width: 100,
                title: '课程编号',
                sort: true
              }, {
                field: 'creatiDateTimenew',
                width: 140,
                title: '观看时间'
              }, {
                field: 'seconds',
                width: 100,
                title: '观看时长'
              }, {
                field: 'sumSeconds',
                title: '总时长',
                width: 100
              }, {
                field: 'isEnd',
                width: 100,
                templet: '#statusTpl1',
                title: '是否看完',
              }]
            ],
            data: ret.data.listAll
          });
          //layer.close(layerindex);
        } else {
          //layer.close(layerindex);
          layer.msg(ret.msg, {
            icon: 2,
            time: 1000
          });
        }
        editMember(ret);
      } else {
        layer.msg(ret.msg, {
          icon: 2,
          time: 1000
        });
      }
    }

    function editMember(ret) {
      layer.open({
        type: 1,
        shade: 0,
        shadeClose: false,
        resize: false,
        move: true,
        title: '进度详情', //不显示标题
        area: ['850px', '490px'], //宽高
        content: $('.layer_notice'), //捕获的元素，注意：最好该指定的元素要存放在body最外层，否则可能被其它的相对元素所影响
        cancel: function () {
          //layer.msg('捕获就是从页面已经存在的元素上，包裹layer的结构', {time: 5000, icon:6});
        }, success: function (layero, index) {
          var layerIndex2 = index; //獲取當前窗口的索引
          var layerInitWidth = $("#layui-layer" + layerIndex2).width(); //獲取layer的寬度
          var layerInitHeight = $("#layui-layer" + layerIndex2).height(); //獲取layer的高度
          resizeLayer(layerIndex2, layerInitWidth, layerInitHeight);
        },
      });
    }

    function resizeLayer(layerIndex2, layerInitWidth, layerInitHeight) {
      var docWidth = $(window).width();
      var docHeight = $(window).height();
      console.log(docHeight)
      var minWidth = layerInitWidth > docWidth ? docWidth : layerInitWidth;
      var minHeight = layerInitHeight > docHeight ? docHeight : layerInitHeight;
      console.log(minHeight)
      layer.style(layerIndex2, {
        top: 10,
        width: minWidth,
        height: minHeight - 10
      });
    }
  }

  element.on('tab(exam-papers)', function (data) {
    console.log(this); //当前Tab标题所在的原始DOM元素
    console.log(data.index); //得到当前Tab的所在下标
    console.log(data.elem); //得到当前的Tab大容器
  });

  Vue1 = new Vue({
    el: "#exam-papers",
    data: {
      Data: [],
    },
    updated: function () {
      element.render("collapse")
      element.tabChange("exam-papers", "章节练习")
    }
  })

})