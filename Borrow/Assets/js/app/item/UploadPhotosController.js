(function ($) {
    var context = $(".upload-photos");

    if (!context) {
        return false;
    }

    var progressBar = context.find(".progress .bar");
    context.fileupload({
        dataType: "json",
        dropZone: context,
        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);

            progressBar.css(
                "width",
                progress + "%"
            );
        },
        start: function (e) {
        },
        fail: function (e, data) {
        },
        always: function (e, data) {

        },
        add: function (e, data) {
            context.find(".upload-form-ui").hide();
            context.find(".progress .bar").removeClass("hide");

            context.find(".alert").removeClass("hide");
            data.submit();
        },
        stop: function (e) {
            window.location.reload(true);
        }
    });

    context.bind("fileuploadsubmit", function (e, data) {
        var input = context.find("input[type=hidden]");
        data.formData = { Identifier: input.val() };
    });
})(jQuery);