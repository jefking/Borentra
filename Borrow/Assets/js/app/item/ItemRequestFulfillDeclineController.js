(function ($) {
  var context = $("#dialogItemRequestFulfillDecline");
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
      identifier: form.find("#identifier").val()
      , comment: form.find("textarea").val()
    };

    ServiceLocator.getService("getItemRequestFulfillDecline").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-request-fulfill-decline").on("click", function (event) {
    event.preventDefault();
    $("#dialogItemRequestFulfillDecline").modal({});
    var identifier = $(event.currentTarget).data().identifier;
    form.find("#identifier").val(identifier);
    Initialize(identifier);

  });
})(jQuery);