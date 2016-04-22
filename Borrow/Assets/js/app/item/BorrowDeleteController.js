(function ($) {
  var context = $("#dialogBorrowDelete");
  var form = context.find("form");

  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("input[type=hidden]").val()
    };

    ServiceLocator.getService("addBorrowDelete").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-borrow-delete").on("click", function (event) {
    event.preventDefault();
    $("#dialogBorrowDelete").modal({});
    var itemId = $(event.currentTarget).data().id;
    form.find("input[type=hidden]").val(itemId);
  });
})(jQuery);