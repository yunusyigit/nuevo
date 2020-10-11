// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var tbl;
var tblLog;
var ListArr = new Array;
var ListLogArr = new Array;
$(document).ready(function () {  


    // Data table yapısı oluşturmak için gerekli kodlar.
    tbl = $('#example').DataTable({
        pageLength: 10,
        data: ListArr,
        dom: 'Bfrtip',
        buttons: [
            'excel',
        ],
        columns: [
            { "data": "webName" },
            { "data": "url" },
            { "data": "time" },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    return '<button class="btn  btn-primary btn-sm" onclick="tableRowUpdate(' + o.id + ')">' + 'Update' + '</button> <button class="btn  btn-danger btn-sm" onclick="tableRowDelet(' + o.id + ')">' + 'Delete' + '</button>';
                }
            },
        ]
        
    });

    tblLog = $('#exampleLog').DataTable({
        pageLength: 10,
        data: ListLogArr,
        dom: 'Bfrtip',
        buttons: [
            'excel',
        ],
        columns: [
            { "data": "webName" },
            { "data": "webUrl" },
            { "data": "responseCode" }

        ]

    });
    //Sayfa yüklendiğinde login olan kişiye ait hata logları ve kayıtlı url listelerini getirmek için
    getAllUrlList();
    getAllUrlListLog();
    //urlChecks();
});

// login olan kullanıcı url listesini getiri.
function getAllUrlList() {    
    $.ajax({
        type: "GET",
        dataType: 'json',
        data: null,
        url: "/api/RestRequest/getAllUrlList",
        success: function (data) {
            ListArr = data;
            //data table temizler ve gelen data table atar
            tbl.clear();
            tbl.rows.add(ListArr);
            tbl.draw();
        },
        error: function (e) {

        },
        cache: false

    });
}
// login olan kullanıcı hata log listesini getiri.
function getAllUrlListLog() {
    $.ajax({
        type: "GET",
        dataType: 'json',
        data: null,
        url: "/api/RestRequest/getAllUrlListLog",
        success: function (data) {
            ListLogArr = data;
               //data table temizler ve gelen data table atar
            tblLog.clear();
            tblLog.rows.add(ListLogArr);
            tblLog.draw();
        },
        error: function (e) {

        },
        cache: false

    });
}


//Url güncellemek için açılan modaldaki inputlara seçili row bilgilerini atar
function tableRowUpdate(dataId) {
    var data = ListArr.filter(x => x.id === dataId);
    $('#url').val("" + data[0].url);
    $('#webName').val("" + data[0].webName);
    $('#time').val("" + data[0].time);
    $('#id').val("" + data[0].id);
    $('#updateModal').modal({ backdrop: 'static', keyboard: false })
    $("#updateModal").modal("show");    
}

//Url eklemek için moadal açar ve içindeki inputları temizler.
function tableRowAdd() {
    $('#url').val("");
    $('#webName').val("");
    $('#time').val("");
    $('#addModal').modal({ backdrop: 'static', keyboard: false })
    $("#addModal").modal("show");
}

//Serialize edilmiş formu url liste kaydeder.
function addURL() {
    var str = $("#formAdd").serialize();

    $.ajax({
        type: "POST",
        dataType: 'json',
        data: str,
        url: "/api/RestRequest/addURL",
        success: function (data) {
            getAllUrlList();
            $("#addModal").modal("hide");
        },
        error: function (e) {

        },
        cache: false

    });

}

//Serialize edilmiş formu günceller.
function updateURL() {
    var str = $("#formUpdate").serialize();

    $.ajax({
        type: "POST",
        dataType: 'json',
        data: str,
        url: "/api/RestRequest/updateURL",
        success: function (data) {
            getAllUrlList();
            $("#updateModal").modal("hide");
        },
        error: function (e) {

        },
        cache: false

    });

}

//Seçili url siler.
function tableRowDelet(idUrl) {
    $.ajax({
        type: "GET",
        dataType: 'json',
        data: { id: idUrl },
        url: "/api/RestRequest/deleteURL",
        success: function (data) {
            getAllUrlList();
        },
        error: function (e) {

        },
        cache: false

    });
}

//******************************Cronjob olarak güncellendi*********************************
//function urlChecks() {
//    $.ajax({
//        type: "GET",
//        dataType: 'json',
//        data: null,
//        url: "/api/RestRequest/urlChecks",
//        success: function (data) {

//        },
//        error: function (e) {

//        },
//        cache: false

//    });
//}
