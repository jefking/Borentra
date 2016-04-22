/*
 * Borrow Accept Controller
 * @author Borentra 
 * 
 * Provides an implementation silo for handling accepting requests (share)
 */
(function ($) {
  var context = $("#dialogBorrowAccept");
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

    ServiceLocator.getService("addBorrowAccept").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-borrow-accept").on("click", function (event) {
    event.preventDefault();
    $("#dialogBorrowAccept").modal({});

    var itemId = $(event.currentTarget).data().id;
    form.find("input[type=hidden]").val(itemId);
    Initialize(itemId);
  });
})(jQuery);