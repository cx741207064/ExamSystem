layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload', 'laydate'], function () {
    var layer = layui.layer,
		form = layui.form,
		table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
		common = layui.common,
        laydate = layui.laydate;

    pageInit()
    function pageInit(){
        var url="/Handler/UserCenter.ashx?action=getMarkUserCertificateByName"
        var name=Params.getCookieDis("name")
        Params.Ajax(url,"post",{"name":name},pageInitSuccessCallBack)
    }

    function pageInitSuccessCallBack(d){
        Vue1=new Vue({
            el: '#select-certificate',
            data: {
                selected:"",
                certificates: d.Data,
            },
            mounted:function(){
                form.render()
            },
            watch:{
                selected:function(oldValue,newValue){
                }
            },
            computed: {
                change: function (event) {
                }
              }
        })
    }

    form.on("select(certificate)",function(data){
        var url="/Handler/UserCenter.ashx?action=getStudentsByCertificateID"
        var data = {"certificateId":data.value}
        Params.Ajax(url,"post",data,certificateChangeSuccessCallBack)
    })

    function certificateChangeSuccessCallBack(d){
        Vue2.students=d.Data
        Vue2.seen=true
    }

})

var Vue1;

var Vue2=new Vue({
    el: '#list-students',
    data: {
        seen:false,
        students: [],
    },
    mounted:function(){
        //form.render()
    },
    updated:function(){
        //form.render()
    },
    watch:{
    },
    computed: {
        change:function(){
        }
      }
})
