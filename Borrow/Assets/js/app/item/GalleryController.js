(function ($) {
  var context = $(".component.item-image-gallery-component");
  if (!context.length) {
    return false;
  }

  var self = {
    Photos: ko.observableArray(),
    Show: function (data, event) {
      event.preventDefault();
      var big = $(".item-detail-big-image");
      var image = '//cdn.borentra.com' + data.Large;

      big.attr("src", image);
      big.addClass("img-responsive img-rounded");

      return false;

    }
  };
  ServiceLocator.getService("getProductPhotos").invoke({ itemIdentifier: context.data().id }, function (err, photos) {
    self.Photos(photos);
  });

  ko.applyBindings(self, context[0]);
})(jQuery);

(function ($) {
    var context = $(".component.request-image-gallery-component");
    if (!context.length) {
        return false;
    }

    var self = {
        Photos: ko.observableArray(),
        Show: function (data, event) {
            event.preventDefault();
            var big = $(".item-detail-big-image");
            var image = '//cdn.borentra.com' + data.Large;

            big.attr("src", image);
            big.addClass("img-responsive img-rounded");

            return false;

        }
    };
    ServiceLocator.getService("getItemRequestPhotos").invoke({ itemIdentifier: context.data().id }, function (err, photos) {
        self.Photos(photos);
    });

    ko.applyBindings(self, context[0]);
})(jQuery);