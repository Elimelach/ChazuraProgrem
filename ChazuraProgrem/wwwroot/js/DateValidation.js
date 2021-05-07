jQuery.validator.addMethod("mindate", function (value, element) {
	if (value === '') return false;

	var dateToCheck = new Date(value);
	if (dateToCheck === "Invalid Date") return false;

	let minDate = new Date(1583, 01, 01);
	let maxDate = new Date(2239, 09, 29)


	return (dateToCheck <= maxDate && dateToCheck >= minDate);
});

jQuery.validator.unobtrusive.adapters.addBool("mindate");
