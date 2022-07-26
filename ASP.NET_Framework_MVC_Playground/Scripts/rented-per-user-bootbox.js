var submitButton = $("#SubmitButton");

submitButton.click(function (e) {
    if ($('#UserDropdown option:selected').val() == '') {
        e.preventDefault();
        e.stopPropagation();

        bootbox.alert({
            title: "Error",
            message: "Please choose a user!",
            backdrop: true,
        });
    }
});