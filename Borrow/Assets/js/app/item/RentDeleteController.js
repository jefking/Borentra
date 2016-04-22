(function ($) {
  var context = $("#dialogRentDelete");
  var form = context.find("form");

  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("input[type=hidden]").val()
    };

    ServiceLocator.getService("addRentDelete").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-rent-delete").on("click", function (event) {
    event.preventDefault();
    $("#dialogRentDelete").modal({});
    var itemId = $(event.currentTarget).data().id;
    form.find("input[type=hidden]").val(itemId);
  });
})(jQuery);