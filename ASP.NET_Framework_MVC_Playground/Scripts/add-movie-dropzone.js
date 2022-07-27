// For IE bug thing
jQuery.browser = {};
(function () {
    jQuery.browser.msie = false;
    jQuery.browser.version = 0;
    if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
        jQuery.browser.msie = true;
        jQuery.browser.version = RegExp.$1;
    }
})();

Dropzone.options.imageDropzone = { // camelized version of the `id`
    paramName: "Image", // The name that will be used to transfer the file
    maxFilesize: 1, // MB
    dictDefaultMessage: "",
    maxFiles: 1,
    autoProcessQueue: false,
    addRemoveLinks: true,
    url: "null",
    createImageThumbnails: false,
    acceptedFiles: 'image/*',
    accept: function (file, done) {

        $('#isFileUploaded').val("true");
        file.previewElement.classList.add("dz-success");
        file.previewElement.classList.add("dz-complete");
        done();

        $('#images').hide();
        $('#CropButton').hide();

        var reader = new FileReader();
        reader.onload = function (e) {
            // Unhide rows
            $('#images').show();
            $('#CropButton').show();

            $('#UploadedImage').attr("src", e.target.result);
            $('#UploadedImage').Jcrop({
                onChange: SetCoordinates,
                onSelect: SetCoordinates,
            });
        }
        reader.readAsDataURL($(this)[0].files[0]);
    },
    init: function () {
        var submitButton = $("#SubmitButton");
        wrapperThis = this;

        submitButton.click(function (e) {
            if ($('#isFileUploaded').val() == '') {
                e.preventDefault();
                e.stopPropagation();

                var locale = $('#cultureChosen').val().substring(0, 2);

                bootbox.alert({
                    locale: locale,
                    title: resources.Error,
                    message: resources.PleaseUploadAMoviePoster,
                    backdrop: true
                });
            }
        });

        wrapperThis.on("removedfile", file => {
            $('#isFileUploaded').val('');

            // Rehide rows and stuff
            $('#images').hide();
            $('#CropButton').hide();

            $('#UploadedImage').attr("src", null);
            $('#UploadedImage').data('Jcrop').destroy()
        });

        wrapperThis.on("error", file => {

            var locale = $('#cultureChosen').val().substring(0, 2);

            bootbox.alert({
                locale: locale,
                title: resources.Error,
                message: resources.PleaseUploadASmallerImage,
                backdrop: true
            });
            $('#isFileUploaded').val('');
            wrapperThis.removeFile(file);
        });
    },
};

// Jcrop stuff
$(function () {
    $('#CropButton').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        // Unhide rows
        $('#CroppedImageTitle').show();

        var x1 = $('#imgX1').val();
        var y1 = $('#imgY1').val();
        var width = $('#imgWidth').val();
        var height = $('#imgHeight').val();
        var canvas = $("#Canvas")[0];
        var context = canvas.getContext('2d');
        var img = new Image();
        img.onload = function () {
            canvas.height = height;
            canvas.width = width;
            context.drawImage(img, x1, y1, width, height, 0, 0, width, height);
            $('#imgCropped').val(canvas.toDataURL());
        };
        img.src = $('#UploadedImage').attr("src");
    });
});

function SetCoordinates(c) {
    $('#imgX1').val(c.x);
    $('#imgY1').val(c.y);
    $('#imgWidth').val(c.w);
    $('#imgHeight').val(c.h);
    $('#btnCrop').show();
};

function SuccessBootbox(viewBagStatus, addAnotherLink, backToListLink) {

    var locale = $('#cultureChosen').val().substring(0, 2);

    bootbox.confirm({
        locale: locale,
        title: resources.Success,
        message: viewBagStatus,
        buttons: {
            confirm: {
                label: resources.AddAnother,
            },
            cancel: {
                label: resources.BackToList,
            }
        },
        callback: function (result) {
            if (result) {
                location.replace(addAnotherLink);
            } else {
                location.replace(backToListLink);
            }
        }
    });
};

function FailureBootbox(viewBagStatus, tryAgainLink) {

    var locale = $('#cultureChosen').val().substring(0, 2);

    bootbox.alert({
        locale: locale,
        title: resources.Error,
        message: viewBagStatus,
        backdrop: true,
        callback: function (result) {
            if (result) {
                location.replace(tryAgainLink);
            }
        }
    });
};



    
