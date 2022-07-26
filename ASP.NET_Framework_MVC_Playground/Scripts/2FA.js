$("#2FA-Switch-Checked").click(function () {
    $("#DisableTwoFactorAuthentication").submit();
});

$("#2FA-Switch-Unchecked").click(function () {
    $("#EnableTwoFactorAuthentication").submit();
});