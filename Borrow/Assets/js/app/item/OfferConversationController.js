/*
 * Offer Conversation Controller
 * @author Borentra 
 * 
 * Provides an implementation silo for handling accepting requests (share)
 */
(function ($) {
  var context = $("#dialogOfferConversation");

  if (!context.length) {
    return false;
  }
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

  $(".btn-offer-conversation").on("click", function (event) {
    event.preventDefault();
    $("#dialogOfferConversation").modal({});

    var itemId = $(event.currentTarget).data().id;
    form.find("input[type=hidden]").val(itemId);
    Initialize(itemId);
  });
})(jQuery);