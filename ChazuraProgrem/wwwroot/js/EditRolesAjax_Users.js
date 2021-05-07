$(document).ready(() => {
    $(".roleEdit").click(function (evt) {
        const path = "/admin/Users/GetRolesForm/";
        const a = evt.currentTarget;
        let value = $(a).val();
        let = route = path + value;
        $("#re").load(route);

    });
})