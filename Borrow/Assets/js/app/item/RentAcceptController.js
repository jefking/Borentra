(function ($) {
  var context = $("#dialogRentAccept");
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
      , Comment: form.find("textarea").val()
    };

    ServiceLocator.getService("addRentAccept").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });

    return false;
  });

  $(".btn-rent-accept").on("click", function (event) {
    event.preventDefault();
    $("#dialogRentAccept").modal({});
    var id = $(event.currentTarget).data().id;
    form.find("#identifier").val(id);
    Initialize(id);
  });
})(jQuery);