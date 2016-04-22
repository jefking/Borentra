(function ($) {
  var context = $("#dialogProfileMessage");

  if (!context.length) return false;

  var form = context.find("form");
  var user = null;

  var self = {
    UIMessages: ko.observableArray([]),
    ToUserIdentifier: ko.observable(),
    Body: ko.observable(),
    Send: function (data, event) {
      var params = {
        ToUserIdentifier: self.ToUserIdentifier(),
        Body: self.Body()
      };

      ServiceLocator.getService("addConversationMessage").invoke(params, function (err, res) {
        self.UIMessages([{ message: "Successfully sent!" }]);
        window.setTimeout(function () {
          context.modal('hide');
        }, 1e3);
      });

    }
  };

  $(".btn-profile-message").on("click", function (event) {
    event.preventDefault();
    var data = $(event.currentTarget).data();
    if (typeof data.user == 'undefined') {
      console.error("Unable to set user identifier");
    }
    self.ToUserIdentifier(data.user);
    self.UIMessages([]);
    self.Body("");
    context.modal({});
  });

  ko.applyBindings(self, context[0]);
})(jQuery);