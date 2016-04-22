/*
 * Borrow Accept Controller
 * @author Borentra 
 * 
 * Provides an implementation silo for handling making requests (free)
 */
(function ($) {
  var context = $("#dialogFreeItem");
  var form = context.find("form");

  if (!context.length) { return false; }


  form.on("submit", function (event) {
    var params = {
      itemIdentifier: form.find("#itemId").val()
      , Comment: form.find("#comment").val()
    };

    ServiceLocator.getService("getFreeRequest").invoke(params, function (err, response) {
      context.modal("hide");
    });

    return false;
  });

  $(".btn-free").on("click", function (event) {
    event.preventDefault();
    $("#dialogFreeItem").modal({});
    var itemId = $(event.currentTarget).data().id;
    form.find("#itemId").val(itemId);
  });
})(jQuery);