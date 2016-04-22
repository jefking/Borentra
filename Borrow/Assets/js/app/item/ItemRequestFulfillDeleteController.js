(function ($) {
  var context = $("#dialogItemRequestFulfillDelete");
  var form = context.find("form");

  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("#identifier").val()
    };

    ServiceLocator.getService("getItemRequestFulfillDelete").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-request-fulfill-delete").on("click", function (event) {
    event.preventDefault();
    $("#dialogItemRequestFulfillDelete").modal({});
    var id = $(event.currentTarget).data().identifier;
    form.find("#identifier").val(id);
  });
})(jQuery);