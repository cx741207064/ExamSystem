layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload'], function () {
    var $ = layui.$,
		layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common;
    //展示已知数据
    if (Params.getParamsFormUrl("OLSchoolUserId") == "683ff0c9521846f6a8d0b88bf16750c4") {
        table.render({
            elem: '#Tables_subjectcore'
          , cols: [[ //标题栏
             { field: 'username', title: '课程名', width: 200 }
            , { field: 'email', title: '课程类别', minWidth: 150 }
            , { field: 'sex', title: '完成度', minWidth: 160 }
            , { field: 'city', title: '得分', width: 80 }
          ]]
          , data: [{
              "username": "综合版会计实务教学视频"
            , "email": "视频"
            , "sex": "90%"
            , "city": "90"
          }, {
              "username": "商业账实训系统"
            , "email": "实训"
            , "sex": "80%"
            , "city": "80"
          }, {
              "username": "模拟报税系统"
            , "email": "实训"
            , "sex": "90%"
            , "city": "90"
          }, {
              "username": "综合版会计实务题库"
            , "email": "题库"
             , "sex": "70%"
            , "city": "70"
          }]
          , even: true
        });
    }
    else {
        table.render({
            elem: '#Tables_subjectcore'
          , cols: [[ //标题栏
             { field: 'username', title: '课程名', width: 200 }
            , { field: 'email', title: '课程类别', minWidth: 150 }
            , { field: 'sex', title: '完成度', minWidth: 160 }
            , { field: 'city', title: '得分', width: 80 }
          ]]
          , data: [{
              "username": "综合版会计实务教学视频"
            , "email": "视频"
            , "sex": "95%"
            , "city": "90"
          }, {
              "username": "商业账实训系统"
            , "email": "实训"
            , "sex": "80%"
            , "city": "80"
          }, {
              "username": "模拟报税系统"
            , "email": "实训"
            , "sex": "90%"
            , "city": "90"
          }, {
              "username": "综合版会计实务题库"
            , "email": "题库"
             , "sex": "70%"
            , "city": "70"
          }]
          , even: true
        });
    }
});