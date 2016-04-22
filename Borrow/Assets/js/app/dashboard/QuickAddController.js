(function ($) {


  var context = $(".component.quick-add-component");

  if (!context.length) {
    return false;
  }

  var Theme = context.data('theme');

  var Result = function (data) {

    this.Name = ko.observable(data.Name);
    this.Image = ko.observable(data.Image);
    this.ImageUrl = ko.observable(data.ImageUrl);
    this.ThumbnailUrl = ko.observable();
    this.Query = ko.observable(data.Query);
    return this;
  };

  var self = {
    Results: ko.observableArray(),
    UILoading: ko.observable(false),
    UIMessages: ko.observableArray(),
    UIErrors: ko.observableArray(),

    FadeOut: function (data, event) {

      var mediaObj = event.currentTarget.parentNode.parentNode.parentNode.parentNode;
      $(mediaObj).fadeOut('fast', function () {
        self.Results.remove(data);

      });
    },
    Fetch: function (limit, callback, append) {
        var fetchTheme = "getSuggestedBooksProducts";

      switch (Theme) {
        case "magicthegathering": fetchTheme = "getSuggestedMagicProducts"; break;
        case "gaming":
          fetchTheme = function () {
            var choices = ["getSuggestedXbox360Products", "getSuggestedPs3Products"];
            var chosen = _.random(0, choices.length - 1);

            return choices[chosen];
          }();
          break;
        case "xbox360": fetchTheme = "getSuggestedXbox360Products"; break;
        case "ps3": fetchTheme = "getSuggestedPs3Products"; break;
        //case "clothing": fetchTheme = "getSuggestedHalloweenProducts"; break;
        //case "halloween": fetchTheme = "getSuggestedHalloweenProducts"; break;
        case "pokemon": fetchTheme = "getSuggestedPokemonProducts"; break;
        case "books": fetchTheme = "getSuggestedBooksProducts"; break;
        case "tools": fetchTheme = "getSuggestedToolsProducts"; break;
        default:
            fetchTheme = "getSuggestedToolsProducts";

        }

      var url = ServiceLocator.getService(fetchTheme).getURI();
      $.getJSON(url, {
        limit: limit,
        format: "json"
      }).done(function (data) {

        callback(data, append);
      });

    },
    ExecuteSkip: function (data, event) {
      event.preventDefault();
      self.FadeOut(data, event);

      self.Fetch(1, self.Render, true);

    },
    ExecuteAddItem: function (data, event) {
      self.FadeOut(data, event);
      var params = {
        Title: data.Name(),
        Description: "#" + data.Query(),
        TradePrivacyLevel: 1,
        RentPrivacyLevel: 0,
        FreePrivacyLevel: 0,
        SharePrivacyLevel: 1
      };

      var imageUrl = data.ImageUrl();

      ServiceLocator.getService("addProduct").invoke(params, function (err, created) {
        var id = created.Identifier;
        ServiceLocator.getService("addProductPhotoUrl").invoke({ ItemIdentifier: id, Url: imageUrl }, function (err, response) {
        });
        self.Fetch(1, self.Render, true);
        $.publish("borentra.dashboard.NewsFeed.Refresh");
      });

    },

    ExecuteAddRequest: function (data, event) {
      self.FadeOut(data, event);

      var params = {
        Title: data.Name(),
        Description: "#" + data.Query(),
        ForTrade: true,
        ForRent: true,
        ForFree: true,
        ForShare: true
      };

      var imageUrl = data.ImageUrl();

      ServiceLocator.getService("addItemRequestSave").invoke(params, function (err, created) {
        var id = created.Identifier;
        ServiceLocator.getService("addItemRequestPhotoUrl").invoke({ ItemRequestIdentifier: id, Url: imageUrl }, function (err, response) {
        });
        self.Fetch(1, self.Render, true);
        $.publish("borentra.dashboard.NewsFeed.Refresh");
      });
    },

    Render: function (results, append) {
      results = _.map(results, function (it) {

        it = new Result(it);
        if (it.Image() != null) {
          it.ThumbnailUrl(it.Image());
          it.ImageUrl(it.Image());
        } else {
          ServiceLocator.getService("getPublicImageSearch").invoke({
            s: it.Name() + " " + it.Query(),
            take: 1
          }, function (err, photos) {
            var photo = photos[0];

            photo = _.extend({ ThumbnailUrl: "/assets/img/ui/thumbnail.png", Url: "/assets/img/ui/thumbnail.png" }, photo);
            it.ThumbnailUrl(photo.ThumbnailUrl);
            it.ImageUrl(photo.Url);
          });
        }


        return it;
      });

      if (typeof append === 'undefined') {
        self.Results(results);
      } else {
        self.Results.push(results[0]);
      }

    },
    Initialize: function () {
      self.Fetch(5, self.Render);

    }
  };
  ko.applyBindings(self, context[0]);


  self.Initialize();

})(jQuery);