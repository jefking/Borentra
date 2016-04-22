(function ($) {
    var context = $("#dialogRent");
    var form = context.find("form");
    if (!context.length) { return false; }

    $("#on").datepicker();
    $("#until").datepicker();

    form.on("submit", function (event) {
        var params = {
            ItemIdentifier: form.find("#itemId").val()
          , On: form.find("div[name='On'] input").val()
          , Until: form.find("div[name='Until'] input").val()
          , Comment: form.find("textarea").val()
        };

        ServiceLocator.getService("addRentRequest").invoke(params, function (err, response) {
            context.modal("hide");
        });

        return false;
    });

    $(".btn-rent").on("click", function (event) {
        event.preventDefault();
        $("#dialogRent").modal({});
        var itemId = $(event.currentTarget).data().id;
        form.find("#itemId").val(itemId);
    });
})(jQuery);