"use strict"
$(document).ready(() => {

    $(".compForm").each((_ind, formTargeted) => {
        formTargeted.addEventListener("submit", (evt) => {

            evt.preventDefault();
            const url = formTargeted.action;
            let form = new FormData(formTargeted);
            //    for (var i = 0; i < formTargeted.elements.length; i++) {
            //        form.append(formTargeted[i].name, formTargeted[i].value);
            //}
            fetch(url, {
                //credentials: "include",
                method: "post",
                body: form,
                //headers: new headers()
            }).then(response => response.json())
                .then(json => {
                    displayCompletedReults(JSON.parse(json), formTargeted);
                });
        });///end sumitEvent
    });//end each
});///end ready
const displayCompletedReults = (jsonObject, target) => {
    $(target).find(".btnComp").text(jsonObject.BtnText);
    $(target).find(".completed").val(jsonObject.CompVal);
    $(target).find(".compId").val(jsonObject.CompId);
    //$(target.parentElement).find(".compPText").text(jsonObject.CompText);
    let myClass;
    if (jsonObject.CompVal) {
        myClass = "comp";
    }
    else {
        myClass = "";
    }
    const o = $(target.parentElement);
    $(o).removeClass("comp").addClass(myClass);

}