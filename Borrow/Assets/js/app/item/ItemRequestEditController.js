(function ($) {
    var form = $("#quick-request-item-edit-form");

    form.on("submit", function (event) {
        var params = {
            Identifier: form.find("#request-identifier").val(),
            Title: form.find("#title").val(),
            Description: form.find("#description").val(),
            ForTrade: form.find("#for-trade")[0].checked ? 1 : 0,
            ForRent: form.find("#for-rent")[0].checked ? 1 : 0,
            ForFree: form.find("#for-free")[0].checked ? 1 : 0,
            ForShare: form.find("#for-share")[0].checked ? 1 : 0,
        };

        form.find(".btn-primary").button("loading");
        ServiceLocator.getService("addItemRequestSave").invoke(params, function (err, res) {

            form.find(".alert-success").removeClass("hide");
            window.setTimeout(function () {
                $("#dialogItemRequestEdit").modal("hide");
                window.location.href = "/wanted/" + res.Key;

            }, 2e3);

        });

        return false;
    });

    $("body .btn-dialog-item-request-edit").on("click", function (e) {
        var meta = $(e.currentTarget).data();
        e.preventDefault();

        GlobalRequestItemEditCommand(meta);
    });

})(jQuery);

// This is used as a way for key bindings to invoke the dialog
var GlobalRequestItemEditCommand = function () {
    var form = $("#quick-request-item-edit-form");
    if (arguments.length !== 0) {
        var meta = arguments[0];
        form.find("#request-identifier").val(meta.requestidentifier);
        form.find("#title").val(meta.title);
        form.find("#description").val(meta.description);
        form.find("#for-share")[0].checked = meta.forshare;
        form.find("#for-trade")[0].checked = meta.fortrade;
        form.find("#for-rent")[0].checked = meta.forrent;
        form.find("#for-free")[0].checked = meta.forfree;
    }

    form.find(".btn-primary").button("reset");

    $("#dialogItemRequestEdit").modal({ backdrop: 'static' }).css({
        "width": 700,
        "margin-left": function () {
            return -($(this).width() / 2);
        }
    });
};