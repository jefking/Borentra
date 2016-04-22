(function ($) {
  var context = $(".component-trade-ui");
  if (!context.length) return false;


  var TargetUserIdentifier = context.data("user");
  var CurrentUserIdentifier = context.data("current");
  var SelectedItemIdentifier = context.data("item");

  var self = {
    UIMessages: ko.observableArray([]),
    UIErrors: ko.observableArray([]),
    itemsMine: ko.observableArray([]),
    itemsTheirs: ko.observableArray([]),
    selectedMine: ko.observableArray([]),
    selectedTheirs: ko.observableArray([]),

    isLoading: ko.observable(false),


    selectTheirItem: function (data, event) {

      data.Checked(!!data.Checked());
      return true;
    },
    selectMyItem: function (data, event) {
      data.Checked(!!data.Checked());
      return true;
    },
    request: function (data, event) {

      var mineChecked = _.filter(self.itemsMine(), function (item) {
        return item.Checked() == true;
      });
      var theirsChecked = _.filter(self.itemsTheirs(), function (item) {
        return item.Checked() == true;
      });

      if (mineChecked.length === 0 || theirsChecked.length === 0) {
        self.UIErrors([
           { message: "Please select some of your items and some of their items" }
        ]);

        return false;
      }

      var mine = _.pluck(mineChecked, "Identifier");
      var theirs = _.pluck(theirsChecked, "Identifier");

      var params = {
        ItemIdentifiers: _.union(mine, theirs)
      };

      ServiceLocator.getService("addTradeRequest").invoke(params, function (err, response) {
        window.location.href = "/dashboard/trades";
      });

    }
  };


  ko.applyBindings(self, context[0]);

  var Initialize = function () {
    self.isLoading(true);
    ServiceLocator.getService("getProducts").invoke({ user: TargetUserIdentifier, type: 3 }, function (err, items) {
      items = FixItemBinding(items);


      _.map(items, function (it) {
        if (it.Identifier == SelectedItemIdentifier) {
          it.Checked(true);
        }
        return it;
      });
      self.itemsTheirs(items);
    });

    ServiceLocator.getService("getProducts").invoke({ user: CurrentUserIdentifier, type: 3 }, function (err, items) {
      items = FixItemBinding(items);
      self.itemsMine(items);
    });
  };

  var FixItemBinding = function (items) {
    return _.map(items, function (item) {

      var cdn = "http://cdn.borentra.com";
      item.PrimaryImageThumbnail = cdn + item.PrimaryImageThumbnail;
      item.Checked = ko.observable(false);
      return item;
    });
  };


  Initialize();


})(jQuery);