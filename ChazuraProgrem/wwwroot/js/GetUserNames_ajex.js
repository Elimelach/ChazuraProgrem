"use strict"

$(document).ready(() => {

    $("#searchInput").on( "input",evt => {
        let o = $("#searchInput");

        evt.preventDefault();
        let formTargeted = $("#searchForm");
        formTargeted = formTargeted[0];
        const url = formTargeted.action;
            let form = new FormData(formTargeted);
           
            fetch(url, {
                method: "post",
                body: form,
            }).then(response => response.json())
                .then(json => {
                    displaySelectOptions(JSON.parse(json));
                });
        });///end sumitEvent
   
});///end ready
const displaySelectOptions = (jsonObject) => {
    var divDisplay = $("#selectDisplay");
    var htmlOutput;
    if (jsonObject.length === 0) {
        htmlOutput = "no matches found";
    }
    
    else {
        htmlOutput = `<select id="selectUser2" class="form-select col-sm-4 form-control"size="${jsonObject.length}">`
        for (let user of jsonObject) {
            htmlOutput += `<option value="${user.Id}">${user.FirstName} ${user.LastName}</option>`;
        
        }
        htmlOutput += ` </select>`;
    }
    divDisplay = divDisplay[0];
    $(divDisplay).html(htmlOutput);
    $("#selectUser2").change(evt => {
        GetUserRoles(evt);
    });//end click event
}