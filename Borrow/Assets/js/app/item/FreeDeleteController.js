(function ($) {
  var context = $("#dialogFreeCancel");
  var form = context.find("form");

  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("#identifier").val()
    };

    ServiceLocator.getService("getFreeCancel").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-free-delete").on("click", function (event) {
    event.preventDefault();
    $("#dialogFreeCancel").modal({});
    var id = $(event.currentTarget).data().identifier;
    form.find("#identifier").val(id);
  });
})(jQuery);