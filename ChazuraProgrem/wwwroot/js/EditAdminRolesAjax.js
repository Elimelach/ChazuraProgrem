"use strict"

$(document).ready(() => {
    $("#selectUser").change(evt => {
        GetUserRoles(evt);
    });//end click event
   
})//end ready
const GetUserRoles = (evt) => {
    const path = "/admin/manager/geteditform/";
    const select = evt.currentTarget;
    const id = $(select).val();
    let route = path + id;
    $("#formEdit").load(route);
}