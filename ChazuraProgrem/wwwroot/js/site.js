

$(document).ready(() => {
    $(".spin-at-Submit").submit(function (event) {

        var isValid = $('.spin-at-Submit').valid();
        if (isValid) {
            // Animate loader
            $("#overlay").show();
        }
    });
});




      
       
