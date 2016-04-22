(function ($) {

  var context = $(".news-feed-component");

  if (!context.length) { return false; }

  window.addEventListener('focus', function () {
    //ServiceLocator.getService("getSearchActivity").invoke({}, Render);
  });


  $.subscribe("borentra.dashboard.NewsFeed.Refresh", function (event, message) {
    if (typeof message == 'undefined' || message.loading != false) {
      ViewModel.isLoading(true);

    }
    ServiceLocator.getService("getSearchActivity").invoke({}, function (err, response) {
      Render(err, response);
      ViewModel.isLoading(false);
    });
  });


  var Activity = function (data) {

    this.CommentCount = ko.observable(data.CommentCount);
    this.FavoriteCount = ko.observable(data.FavoriteCount);
    this.Identifier = ko.observable(data.Identifier);
    this.ModifiedOn = moment.utc(data.ModifiedOn).fromNow();

    this.ReferenceIdentifier = ko.observable(data.ReferenceIdentifier);
    this.ReferenceKey = ko.observable(data.ReferenceKey);
    this.Text = ko.observable(data.Text);
    this.Type = ko.observable(data.Type);
    this.UserContext = ko.observable(data.UserContext);
    this.UserDisplayName = ko.observable(data.UserDisplayName);
    this.UserFacebookId = ko.observable(data.UserFacebookId);
    this.UserIdentifier = ko.observable(data.UserIdentifier);
    this.UserKey = ko.observable(data.UserKey);
    this.UserPicture = ko.observable(data.UserPicture);
    this.Comments = ko.observableArray([]);
    this.CallerFavorited = ko.observable(data.CallerFavorited);
    this.ImageThumbnail = ko.observable(data.ImageThumbnail);

    var self = this;

    if (this.CommentCount() > 0) {
      var commentId = this.Identifier();
      ServiceLocator.getService("getSearchComment").invoke({
        "referenceId": commentId
      }, function (err, comments) {

        comments = _.map(comments, function (comment) {
          comment = new ActivityComment(comment);
          return comment;
        });
        self.Comments(comments);
      });
    }

    return this;
  };

  var globalCount = 0;

  var ActivityComment = function (data) {
    this.Identifier = ko.observable(data.Identifier);
    this.Comment = ko.observable(data.Comment);
    this.OwnerKey = ko.observable(data.OwnerKey);
    this.OwnerPicture = ko.observable(data.OwnerPicture);
    this.OwnerName = ko.observable(data.OwnerName);
    this.CreatedOn = moment.utc(data.CreatedOn).fromNow();
    this.IsMine = ko.observable(data.IsMine);

    this.Delete = function (data, event) {
      var params = {
        Identifier: data.Identifier(),
        Delete: true
      }
      $("#" + data.Identifier()).fadeOut("fast");

      ServiceLocator.getService("addSaveComment").invoke(params, function (err, res) {
        $.publish("borentra.dashboard.NewsFeed.Refresh", { loading: false });
      });
    };
    return this;
  };

  var ViewModel = {
    items: ko.observableArray([]),
    isLoading: ko.observable(false),

    Favorite: function (data, event) {

      var target = event.currentTarget;

      var heart = $(target).find(".glyphicon");
      heart.addClass("animated pulse active");

      var label = $(target).find(".btn-label");
      label.addClass("active");
      label.text("Liked");

      window.setTimeout(function () {
        heart.removeClass("animated pulse");
      }, 1e3);

      ServiceLocator.getService("addSaveFavorite").invoke({
        ReferenceIdentifier: data.Identifier()
      }, function (err, response) {

        _.each(ViewModel.items(), function (item) {
          if (item.Identifier() == data.Identifier()) {
            item.FavoriteCount(response.length);
          }
        });

      });
    },
    Comment: function (data, event) {
      var target = event.currentTarget;
      var id = data.Identifier();
      var form = $("#comment-form-" + id);
      form.find("input").focus();

    },
    SaveComment: function (form) {
      var input = $(form).find("input[type=text]");
      var text = input.val();
      var activityId = $(form).attr("id").split("comment-form-")[1];
      input.val("");
      input.attr("disabled", true);

      ServiceLocator.getService("addSaveComment").invoke({
        Comment: text,
        ReferenceIdentifier: activityId
      }, function (err, comments) {

        _.each(ViewModel.items(), function (item) {
          if (item.Identifier() == activityId) {
            item.Comments([]);
            var data = _.map(comments, function (comment) {
              comment = new ActivityComment(comment);
              return comment;
            });

            item.Comments(data);

          }
        });

        var last = $("#" + activityId).find(".activity-comments li:last");
        last.hide();
        last.fadeIn('slow');
        input.attr("disabled", false);
      });
    }
  };

  ko.applyBindings(ViewModel, context[0]);

  var Initialize = function () {
    ViewModel.items([]);
    ViewModel.isLoading(true);
    ServiceLocator.getService("getSearchActivity").invoke({}, Render);
  };

  var RenderNewsFeedItem = function (id) {

    console.log(id);
  };
  var Render = function (err, items) {

    var specificActivityId = context.find("#activityId").val();

    if (specificActivityId.length > 0) {
      var newItems = [];
      _.each(items, function (it) {
        if (it.Identifier == specificActivityId) {
          it = new Activity(it);
          newItems.push(it);
          return it;

        }
      });

      items = newItems;
    } else {
      items = _.map(items, function (it) {
        it = new Activity(it);
        return it;
      });
    }

    ViewModel.items(items);
    ViewModel.isLoading(false);
    _gaq.push(['_trackEvent', 'Dashboard', 'News Feed', 'Refresh']);
  };

  Initialize();
})(jQuery);
