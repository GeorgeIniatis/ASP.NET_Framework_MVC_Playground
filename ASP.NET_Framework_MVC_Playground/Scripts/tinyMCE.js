function tinyTextArea(text) {
    tinymce.init({
        selector: 'textarea#TinyTextArea',
        plugins: 'image autolink lists media table wordcount',
        toolbar: 'image autolink lists media table wordcount',
        toolbar_mode: 'floating',
        onchange_callback: "loadContent",
        setup: function (editor) {
            editor.on("init", function () {
                this.setContent(text);
            });
            editor.on("change", function () {
                $("#TinyMCE").val(this.getContent());
            });
            var max = 30;
            editor.on('submit', function (event) {
                var numWords = tinymce.activeEditor.plugins.wordcount.body.getWordCount();
                if (numWords > max) {
                    alert("Maximum " + max + " words allowed.");
                    event.preventDefault();
                    return false;
                }
            });
        },
    });
};