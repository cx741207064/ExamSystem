var main;
var url = "/Handler/UserCenter.ashx?action=getMarkUserCertificateByName"
var name = Params.getCookieDis("name")

$.ajax({
    url: url,
    type: "post",
    data: JSON.stringify({ "name": name })
}).then(function (data) {
    main = new Vue({
        el: "#app",
        data() {
            return {
                form: {},
                certificateValue: "",
                certificates: data.Data,
            }
        },
        methods: {
            change: function () {
                var url = "/Handler/UserCenter.ashx?action=getStudentsByCertificateID"
                var da = { "certificateId": this.certificateValue }
                $.ajax({ url: url, type: "post", data: JSON.stringify(da) }).success(function (d) {
                    Vue2.tableData = d.Data
                })
            }
        }
    })
})

var Vue2 = new Vue({
    el: "#students",
    data: {
        tableData: []
    },
    methods: {
        IssueCertificate(index, row) {
            $.ajax({
                url: "/Handler/CertificateCenter.ashx?action=IssueCertificate",
                type: "post",
                data: {
                    "certificateId": main.certificateValue,
                    "studentId": row.studentId
                }
            }).then(function (d) {
                if (d.Code == 0) {
                    Vue2.openSuccess(d.Msg)
                }
                else {
                    Vue2.openError("颁发失败")
                }
            })
        },
        openSuccess(msg) {
            this.$message({
                message: msg,
                type: 'success'
            })
        },
        openError(msg) {
            this.$message.error(msg);
        },
    }
})
