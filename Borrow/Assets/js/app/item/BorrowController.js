(function ($) {
  var context = $("#dialogBorrowItem");
  var form = context.find("form");
  if (!context.length) { return false; }

  $("#borrow-on").datepicker();
  $("#borrow-until").datepicker();

  form.on("submit", function (event) {
    var params = {
      ItemIdentifier: form.find("input[type=hidden]").val()
      , On: form.find("div[name='borrowOn'] input").val()
      , Until: form.find("div[name='borrowUntil'] input").val()
      , Comment: form.find("textarea").val()
    };

    ServiceLocator.getService("addBorrowRequest").invoke(params, function (err, response) {
      context.modal("hide");
    });

    return false;
  });

  $(".btn-borrow").on("click", function (event) {
    event.preventDefault();
    $("#dialogBorrowItem").modal({});
    var itemId = $(event.currentTarget).data().id;
    form.find("input[type=hidden]").val(itemId);
  });
})(jQuery);