$("#limudTypes a").click(function (evt) {
    //evt.preventDefault();
    const path = "/Schedule/GetLimudForms/";
    const a = evt.currentTarget;
    //let limudString = $(a).text(); 
    let value = $(a).attr("action") /*+ "/"*/;
    let = route = path + value /*+ limudString*/;
    $("#pv").load(route);
});