(function ($) {
  var context = $(".component-user-plate");

  if (!context.length) {
    return false;
  }

  var ViewModel = {
    Name: ko.observable("fetching..."),
    Picture: ko.observable("/assets/img/ui/thumbnail.png"),
    Key: ko.observable(),

    EditProfile: function (event, data) {
      GlobalProfileCommand();
      _gaq.push(['_trackEvent', 'Profile', 'Edit', 'Profile']);
    }
  };

  ko.applyBindings(ViewModel, context[0]);

  ServiceLocator.getService("getProfile").invoke({ id: null }, function (err, response) {
    ViewModel.Name(response.Name);
    ViewModel.Key(response.Key);
    ViewModel.Picture(response.Picture);
  });
})(jQuery);
