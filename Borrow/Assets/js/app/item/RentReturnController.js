(function ($) {
  var context = $("#dialogRentReturn");
  var form = context.find("form");
  if (!context.length) { return false; }

  var Initialize = function (id) {
    var source = form.find("script").html();
    var container = form.find("section");

    ServiceLocator.getService("getItemActionComments").invoke({ id: id }, function (err, response) {
      var template = Handlebars.compile(source);

      var content = template({ comments: response });
      console.log(content);
      container.html(content);
    });
  };

  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("input[type=hidden]").val()
      , Comment: form.find("textarea").val()
    };

    execute(params);

    return false;
  });

  $(".btn-rent-returned").on("click", function (event) {
    event.preventDefault();
    var itemId = $(event.currentTarget).data().id;

    if (isMobile) {
      if (window.confirm("Are you sure?")) {
        execute({
          Identifier: itemId
        });
      }

    } else {
      $("#dialogRentReturn").modal({});
      form.find("input[type=hidden]").val(itemId);
      Initialize(itemId);
    }

  });

  var execute = function (params) {
    ServiceLocator.getService("addRentReturn").invoke(params, function (err, response) {
      context.modal("hide");
      window.location.reload(true);
    });
  };
})(jQuery);