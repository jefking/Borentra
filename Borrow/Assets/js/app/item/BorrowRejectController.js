(function ($) {
  var context = $("#dialogBorrowReject");
  var form = context.find("form");


  var Initialize = function (id) {
    var source = form.find("script").html();
    var container = form.find("section");
    ServiceLocator.getService("getItemActionComments").invoke({ id: id }, function (err, response) {
      var template = Handlebars.compile(source);
      var content = template({ comments: response });
      container.html(content);
    });
  };

  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("input[type=hidden]").val()
      , Comment: form.find("textarea").val()
    };

    ServiceLocator.getService("addBorrowReject").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-borrow-reject").on("click", function (event) {
    event.preventDefault();
    $("#dialogBorrowReject").modal({});
    var itemId = $(event.currentTarget).data().id;
    form.find("input[type=hidden]").val(itemId);
    Initialize(itemId);

  });
})(jQuery);