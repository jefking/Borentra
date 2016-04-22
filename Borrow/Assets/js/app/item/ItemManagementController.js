(function ($) {

  var context = $(".item-manager-component");

  if (!context.length) {
    return false;
  }

  var self = {
    UILoading: ko.observable(false),
    UIErrors: ko.observableArray(),
    UIMessages: ko.observableArray(),

    Items: ko.observableArray(),


    Initialize: function () {
      ServiceLocator.getService("getProducts").invoke({ user: window.currentUserId }, function (err, items) {
        console.log(items);
      });
    }

  };
  ko.applyBindings(self, context[0]);

  self.Initialize();

})(jQuery);