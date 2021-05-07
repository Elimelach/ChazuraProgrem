"use strict"


const sendAjax = evt => {
    evt.preventDefault();
    const url = evt.currentTarget.href;
    const xhr = new XMLHttpRequest();
    xhr.responseType = "json";
    xhr.onreadystatechange=() => {
        if (xhr.readyState == 4 && xhr.status == 200) {
            console.log(xhr.response);
            let user = JSON.parse(xhr.response);
            $("#ajDisp").html(user.FirstName);
        }
    };
    xhr.onerror=(e) => {
        console.log(e.message);
    };
    xhr.open("GET", url);
    xhr.send();
}
const fetchAjax = (evt) => {
    evt.preventDefault();
    const url = evt.currentTarget.href;
    fetch(url).then(response => response.json())
        .then(json => {
            console.log(json);
            let user = JSON.parse(json);
            $("#ajDisp").html(user.FirstName);
        }).catch(e => console.log(e.message));
}
$("#tryAjax").click(fetchAjax);