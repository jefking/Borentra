(function ($) {
  var context = $(".component.edit-profile-component");

  if (!context.length) { return false; }

  var profile = context.data();

  context.find(".typeahead").typeahead({
    header: "<div class='page-header'><h4><span class='glyphicon glyphicon-globe'></span> Locations</h4></div>",
    name: 'profiles',
    remote: { "url": 'http://gd.geobytes.com/AutoCompleteCity?callback=?&q=%QUERY', cache: false },
    valueKey: 'Name',
    template: [
      '<div class="media-object">',
        '<div class="media-body">',
          '<h5 class="media-heading">{{Name}}</h5>',
        '</div>',
      '</div>'
    ].join(''),
    engine: Hogan,
    limit: 5
  }).on("typeahead:selected", function (event, selected, source) {
    self.Location(selected.Name);
  });

  var self = {

    UILoading: ko.observable(false),
    UIMessages: ko.observableArray([]),
    UIErrors: ko.observableArray([]),

    Name: ko.observable(profile.name),
    Email: ko.observable(profile.email),
    Location: ko.observable(profile.location),
    Latitude: ko.observable(profile.latitude),
    Longitude: ko.observable(profile.longitude),
    Status: ko.observable(profile.status),
    PrivacyLevel: ko.observable(profile.privacy),
    SearchRadius: ko.observable(profile.distance),

    // Used to build up the UI
    Distance: ko.observable(profile.distance / 1000),
    DistanceList: ko.observableArray(['100', '250', '500', '1000']),
    PrivacyList: ko.observableArray(['Public', 'Community']), //, 'Friends'

    RemoveProfile: function (data, event) {
      if (window.confirm("Are you sure you want to remove yourself from Borentra? We will miss you! :(")) {

        ServiceLocator.getService("deleteProfile").invoke(null, function (err, response) {
          window.location.href = "/";
        });
      }
    },
    Save: function (data, event) {
      self.UIErrors([]);
      self.UIMessages([]);
      if (!self.Name().length || !self.Email().length) {
        self.UIErrors([{
          message: "Please ensure that you have a display name and email address"
        }]);
        return false;
      }
      self.UILoading(true);

      $.getJSON("http://gd.geobytes.com/GetCityDetails?callback=?&fqcn=" + data.Location(), function (data) {

        self.Latitude(data.geobyteslatitude);
        self.Longitude(data.geobyteslongitude);

        var params = {
          Name: self.Name(),
          Email: self.Email(),
          Location: self.Location(),
          Latitude: self.Latitude(),
          Longitude: self.Longitude(),
          Status: self.Status(),
          PrivacyLevel: self.PrivacyLevel(),
          SearchRadius: self.Distance() * 1000
        };

        ServiceLocator.getService("addProfile").invoke(params, function (err, res) {
          self.UILoading(false);
          $(document).scrollTop(0);
          self.UIMessages([{
            message: "Successfully updated your distance!"
          }]);
        });
      });
    }
  };

  function renderPosition(position) {
    var urlJSON = 'http://ws.geonames.org/findNearbyPlaceNameJSON?&lat=' + position.coords.latitude + '&lng=' + position.coords.longitude;

    $.getJSON(urlJSON, function (json) {
      $.each(json.geonames, function (i, item) {
        self.Location(item.name + ', ' + item.adminName1 + ', ' + item.countryName);
      });

    });
  }

  function renderError() {
  }

  if (window.isManager) {
      self.DistanceList.push('100000');
  }
  ko.applyBindings(self, context[0]);
})(jQuery);