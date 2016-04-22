(function ($) {
  var context = $("#dialogFreeAccept");
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
      Identifier: form.find("#identifier").val()
      , Comment: form.find("#comment").val()
    };

    ServiceLocator.getService("getFreeAccept").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-free-accept").on("click", function (event) {
    event.preventDefault();
    $("#dialogFreeAccept").modal({});

    var id = $(event.currentTarget).data().identifier;
    form.find("#identifier").val(id);
    Initialize(id);
  });
})(jQuery);