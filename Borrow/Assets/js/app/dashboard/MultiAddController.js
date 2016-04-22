(function ($) {

  var context = $(".multi-add-component");

  if (!context.length) {
    return false;
  }

  var nav = context.find(".nav-tabs");

  var self = {
    UILoading: ko.observable(false),
    UIErrors: ko.observableArray(),
    UIMessages: ko.observableArray(),

    ResetForms: function () {
      self.Status.Reset();
      self.Item.Reset();
      self.Request.Reset();
    },
    Status: {
      Text: ko.observable(),
      Reset: function () {
        self.Status.Text("");
        context.find(".status-input").focus();
      }
    },

    Request: {
      Title: ko.observable(),
      Description: ko.observable(),
      ForTrade: ko.observable(true),
      ForRent: ko.observable(true),
      ForShare: ko.observable(true),
      ForFree: ko.observable(true),
      HideDescription: ko.observable(true),
      HideOptions: ko.observable(true),
      HideControls: ko.observable(true),
      Reset: function () {
        self.Request.Title("");
        self.Request.Description("");
        self.PhotoSearchResults([]);
        context.find(".item-request-search").focus();
        if (window.location.href.match(/offers/gi)) {
          window.location.reload();
        }
      },
      Save: function (event, data) {
        if (!self.Request.Title()) {
          self.UIErrors([{ message: "Please enter a name for your offer" }]);
          return false;
        }

        var images = context.find("#multi-add-request .photo-results input[type=checkbox]:checked");

        self.UILoading(true);

        var params = {
          Title: self.Request.Title(),
          Description: self.Request.Description(),
          ForTrade: self.Request.ForTrade(),
          ForRent: self.Request.ForRent(),
          ForFree: self.Request.ForFree(),
          ForShare: self.Request.ForShare()
        };

        ServiceLocator.getService("addItemRequestSave").invoke(params, function (err, created) {
          var id = created.Identifier;

          _.map(images, function (photo) {
            ServiceLocator.getService("addItemRequestPhotoUrl").invoke({ ItemRequestIdentifier: id, Url: photo.value }, function (err, response) {
            });
          });

          self.UILoading(false);
          self.UIErrors([]);
          self.UIMessages([{
            message: "You added an offer!<br/><a href='/offer/" + created.Key + "'>View your offer</a>!"
          }]);

          $.publish("borentra.dashboard.NewsFeed.Refresh");
          window.setTimeout(function () {
            self.UIMessages([]);
            self.Request.Reset();
          }, 2e3);
        });
      }
    },

    Item: {

      Identifier: ko.observable(),
      IsValidUpload: ko.observable(false),
      IsUploading: ko.observable(false),
      UIMessages: ko.observableArray(),
      UIErrors: ko.observableArray(),
      Title: ko.observable(),
      Description: ko.observable(),
      ForTrade: ko.observable(false),
      ForRent: ko.observable(false),
      ForShare: ko.observable(true),
      ForFree: ko.observable(false),
      HideDescription: ko.observable(true),
      HideOptions: ko.observable(true),
      HideControls: ko.observable(true),
      PhotoSearchResults: ko.observableArray(),
      Reset: function () {
        self.Item.Title("");
        self.Item.Description("");
        self.Item.PhotoSearchResults([]);
        self.Item.UIMessages([]);
        self.Item.UIErrors([]);

        context.find(".item-search").focus();
        if (window.location.href.match(/offers/gi)) {
          window.location.reload();
        }
      },
      SaveItem: function (data, event) {
        if (!self.Item.Title()) {
          self.UIErrors([{ message: "Please enter a name for your offer" }]);
          return false;
        }

        var images = context.find("#multi-add-item .photo-results input[type=checkbox]:checked");

        self.UILoading(true);

        var params = {
          Title: self.Item.Title(),
          Description: self.Item.Description(),
          TradePrivacyLevel: self.Item.ForTrade() ? 1 : 0,
          RentPrivacyLevel: self.Item.ForRent() ? 1 : 0,
          FreePrivacyLevel: self.Item.ForFree() ? 1 : 0,
          SharePrivacyLevel: self.Item.ForShare() ? 1 : 0
        };

        ServiceLocator.getService("addProduct").invoke(params, function (err, created) {
          var id = created.Identifier;

          _.map(images, function (photo) {
            ServiceLocator.getService("addProductPhotoUrl").invoke({ ItemIdentifier: id, Url: photo.value }, function (err, response) {
            });
          });

          self.UILoading(false);
          self.Item.UIErrors([]);
          self.Item.UIMessages([{
            message: "You added an offer!<br/><a href='/offer/" + created.Key + "'>View your offer</a>!"
          }]);

          $.publish("borentra.dashboard.NewsFeed.Refresh");
          window.setTimeout(function () {
            self.Item.Reset();
          }, 2e3);
        });
      },
      FileUpload: function (e, data) {

        if (!self.Item.Title()) {
          self.Item.UIErrors([{ message: "Please enter a title for your offer" }]);
          return false;
        }

        self.UILoading(true);

        var params = {
          Identifier: self.Item.Identifier(),
          Title: self.Item.Title(),
          Description: self.Item.Description(),
          TradePrivacyLevel: self.Item.ForTrade() ? 1 : 0,
          RentPrivacyLevel: self.Item.ForRent() ? 1 : 0,
          FreePrivacyLevel: self.Item.ForFree() ? 1 : 0,
          SharePrivacyLevel: self.Item.ForShare() ? 1 : 0
        };

        if (!self.Item.IsUploading()) {

          ServiceLocator.getService("addProduct").invoke(params, function (err, created) {
            self.Item.IsUploading(true);
            data.submit();
          });
          data.formData = { ItemIdentifier: self.Item.Identifier() };
        }
      },
      SearchPhotos: function (data, event) {
        if (!self.Item.Title()) {
          self.Item.UIErrors([{ message: "Please enter a title for your offer" }]);
          return false;
        }
        self.UILoading(true);
        ServiceLocator.getService("getPublicImageSearch").invoke({
          s: self.Item.Title(),
          take: 6
        }, function (err, photos) {
          self.Item.PhotoSearchResults(photos);
          self.UILoading(false);
        });
      }
    },

    PhotoSearchResults: ko.observableArray([]),
    SearchPhotos: function (data, event) {
      if (!self.Request.Title()) {
        self.UIErrors([{ message: "Please enter a title for your offer" }]);
        return false;
      }
      self.UILoading(true);
      ServiceLocator.getService("getPublicImageSearch").invoke({
        s: self.Request.Title(),
        take: 6
      }, function (err, photos) {
        self.PhotoSearchResults(photos);
        self.UILoading(false);
      });
    },
    SaveStatusUpdate: function (data, event) {
      var params = {
        Status: self.Status.Text()
      };

      self.Status.Reset();
      ServiceLocator.getService("addProfile").invoke(params, function (err, res) {
        $.publish("borentra.dashboard.NewsFeed.Refresh");
      });
    }
  };


  self.Request.Title.subscribe(function (newValue) {
    if (newValue.length > 0) {
      self.Request.HideDescription(false);
      self.Request.HideOptions(false);
      self.Request.HideControls(false);
    } else {
      self.Request.Description("");
      self.Request.HideDescription(true);
      self.Request.HideOptions(true);
      self.Request.HideControls(true);
    }
  });

  self.Item.Title.subscribe(function (newValue) {
    if (newValue.length > 0) {
      self.Item.HideDescription(false);
      self.Item.HideOptions(false);
      self.Item.HideControls(false);
      self.Item.IsValidUpload(true);
    } else {
      self.Item.Description("");
      self.Item.HideDescription(true);
      self.Item.HideOptions(true);
      self.Item.HideControls(true);
      self.Item.IsValidUpload(false);
    }
  });

  // Photo Uploader

  var uploader = context.find(".add-item-form");

  uploader.fileupload({
    url: ServiceLocator.getService("addProductPhoto").getURI(),
    dataType: 'json',
    add: function (e, data) { },
    done: function (e, data) { },
    progressall: function (e, data) { }
  });

  uploader.bind("fileuploadsubmit", self.Item.FileUpload);

  ServiceLocator.getService("getProductGuid").invoke(null, function (err, response) {
    self.Item.Identifier(response.Identifier);
  });


  if (window.location.href.match("offers")) {
      nav.find("li:eq(1) a").tab('show');
    nav.find("li:eq(0), li:eq(2)").addClass("hidden");
  } else if (window.location.href.match("wanted")) {
    nav.find('li:eq(2) a').tab('show')
    nav.find("li:eq(0), li:eq(1)").addClass("hidden");
  } else {
      nav.find("li:eq(1) a").tab('show');

    nav.on('shown.bs.tab', function (e) {
      self.ResetForms();
    });
  }

  ko.applyBindings(self, context[0]);
})(jQuery);