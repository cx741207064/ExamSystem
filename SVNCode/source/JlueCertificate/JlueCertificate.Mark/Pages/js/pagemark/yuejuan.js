layui.use(['layer', 'laypage', 'form', 'table', 'common', 'upload', 'laydate'], function () {
    var layer = layui.layer,
        form = layui.form,
        table = layui.table,
        upload = layui.upload,
        laypage = layui.laypage,
        common = layui.common,
        laydate = layui.laydate;

    pageInit()
    function pageInit() {
        var url = "/Handler/UserCenter.ashx?action=getMarkUserCertificateByName"
        var name = Params.getCookieDis("name")
        Params.Ajax(url, "post", { "name": name }, pageInitSuccessCallBack)
    }

    function pageInitSuccessCallBack(d) {
        Vue1 = new Vue({
            el: '#select-certificate',
            data: {
                selected: "",
                certificates: d.Data,
            },
            mounted: function () {
                form.render()
            },
            watch: {
                selected: function (oldValue, newValue) {
                }
            },
            computed: {
                change: function (event) {
                }
            }
        })
    }

    form.on("select(certificate)", function (data) {
        var url = "/Handler/UserCenter.ashx?action=getStudentsByCertificateID"
        var da = { "certificateId": data.value }
        var subs = JSON.parse($(data.elem).find("option:selected").attr("subs"))
        $.ajax({ url: url, type: "post", data: JSON.stringify(da) }).success(function (d) {
            certificateChangeSuccessCallBack(d, subs)
        })
    })

    function certificateChangeSuccessCallBack(d, subs) {
        Vue2.students = d.Data
        Vue2.seen = true
        Vue2.subjects = subs
    }

})

Vue.prototype.global =
    {
        baoshuihost: "http://47.97.29.32:8099",
        diannaozhanghost: "http://jluepracticeautobookscore.kjcytk.com",
        identifyhost: "http://114.55.38.113:8054",
        SubjectType:
        {
            "baoshui": "实操-报税",
            "diannaozhang": "实操-电脑账",
            "tiku": "题库"
        }
    }
var Vue1;
var Vue2 = new Vue({
    el: '#list-students',
    data: {
        seen: false,
        students: [],
        subjects: [],
    },
    mounted: function () {
        //form.render()
    },
    updated: function () {
        //form.render()
    },
    watch: {
    },
    methods: {
        stuClick: function (stu) {
            $(".site-doc-icon").unbind()
            var subjects = this.subjects
            var $this = this
            layer.confirm('请选择阅卷科目？',
                {
                    btn: [subjects[0].Name, subjects[1].Name, subjects[2].Name],
                    btn3: function (index, layero) {
                        $this.subject(subjects[2], stu)
                    },
                    area: ['auto']
                }, function (index, layero) {
                    $this.subject(subjects[0], stu)
                }, function (index) {
                    $this.subject(subjects[1], stu)
                })
        },
        subject: function (sub, stu) {
            var global = this.global
            var url
            if (sub.Category == global.SubjectType.tiku) {
                top.layer.msg("此科目自动评分，无需手动评分。", { icon: 1 });
            }
            else if (sub.Category == global.SubjectType.baoshui) {
                //userid添加后缀"_1"区分考试成绩记录与平时成绩记录
                url = global.baoshuihost + "/QuestionMainPingCe.aspx?userid=" + stu.OLSchoolUserId + "_1&username=" + stu.OLSchoolUserName + "&classid=" + stu.ClassId + "&courseid=" + sub.OLSchoolCourseId + "&sortid=" + sub.OLSchoolId + "&StudentTicketId=" + stu.StudentTicketId
                window.open(url, "报税阅卷")
            }
            else if (sub.Category == global.SubjectType.diannaozhang) {
                var url_identify = global.identifyhost + "/Member/GetMobileAndIdentify?classid=" + stu.ClassId + "&OLSchoolUserId=" + stu.OLSchoolUserId + "&OLSchoolId=" + sub.OLSchoolId
                $.ajax({ url: url_identify, type: "get", dataType: "json" }).success(function (ret) {
                    var Data = ret.Data
                    if (Data) {
                        var token = $.md5(new Date().getDate() + Data.Identify.toUpperCase())
                        url = global.diannaozhanghost + "/Pages/Electronic/index.html?CourseId=" + sub.OLAccCourseId + "&identify=" + Data.Identify + "&userid=" + stu.OLSchoolUserId + "&token=" + token + "&StudentTicketId=" + stu.StudentTicketId + "&OLSchoolId=" + sub.OLSchoolId + "&isexam=1"
                        window.open(url, "diannaozhang")
                    }
                })
            }
        }
    },
    computed: {
    }
})