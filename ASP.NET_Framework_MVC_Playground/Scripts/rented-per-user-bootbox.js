var submitButton = $("#SubmitButton");

submitButton.click(function (e) {
    if ($('#UserDropdown option:selected').val() == '') {
        e.preventDefault();
        e.stopPropagation();

        var locale = $('#cultureChosen').val().substring(0, 2);

        bootbox.alert({
            locale: locale,
            title: "Error",
            message: "Please choose a user!",
            backdrop: true,
        });
    }
});