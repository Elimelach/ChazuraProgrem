"use strict"

$(document).ready(() => {
    $(".btnDelete").click((evt) => {
        let btn = evt.currentTarget;
        let id = $(btn).val();
        $("#deleteId").val(id);
        $("#overlayer").show();
    });//end click1
    $("#btnCancel").click(() => {
        $("#overlayer").hide();
    });//end click2
});//end ready