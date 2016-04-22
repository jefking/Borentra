(function ($) {

  var context = $(".component-conversations");

  if (!context.length) { return false; }

  var MyUserId = context.attr("user");
  var viewTitle = "Messages";
  var MessagePoller;

  var Carousel = context.find(".carousel").carousel({
    interval: false
  });
  Carousel.bind("slide", function () {
    ViewModel.isLoading(true);
  });
  Carousel.bind("slid", function () {
    ViewModel.isLoading(false);
  });



  window.addEventListener('focus', function () {
    document.title = viewTitle;

    ServiceLocator.getService("addConversationMessage").invoke({
      Identifier: ViewModel.Thread.Identifier(),
      Read: true
    }, $.noop);
  });

  context.find(".typeahead").typeahead({
    header: "<div class='page-header'><h4><span class='glyphicon glyphicon-user'></span> Members</h4></div>",
    name: 'profiles',
    remote: { "url": ServiceLocator.getService("searchProfiles").getURI() + '?s=%QUERY', cache: false },
    valueKey: 'Name',
    template: [
      '<div class="media-object">',
        '<a class="pull-left" href="#">',
        '<img class="media-object" src="{{Picture}}?width=40&height=40" alt="{{Name}}">',
        '</a>',
        '<div class="media-body">',
          '<h5 class="media-heading">{{Name}}</h5>',
          '<p class="text-muted">{{Location}}</p>',
        '</div>',
      '</div>'
    ].join(''),
    engine: Hogan,
    limit: 5
  }).on("typeahead:selected", function (event, selected, source) {

    ViewModel.Compose.ToIdentifier(selected.Identifier);
  });

  var ViewModel = {
    uiMessages: ko.observableArray(),
    uiErrors: ko.observableArray(),
    conversations: ko.observableArray([]),
    view: ko.observable("inbox"),
    viewInbox: ko.observableArray(),
    viewSent: ko.observableArray(),

    Thread: {
      Identifier: ko.observable(),
      Messages: ko.observableArray(),
      ToIdentifier: ko.observable(),
      ToName: ko.observable(),
      FromIdentifier: ko.observable(),
      FromName: ko.observable()
    },
    Compose: {
      ToIdentifier: ko.observable(),
      ToName: ko.observable(),
      FromName: ko.observable(),
      Body: ko.observable()
    },

    sendInput: ko.observable(),
    isLoading: ko.observable(false),

    isCompose: ko.observable(false),

    startConversation: function (data, event) {
      var value = ViewModel.isCompose();

      ViewModel.isCompose(!value);

    },
    searchProfiles: function (data, event) {

      var value = event.currentTarget.value;
      ViewModel.isLoading(true);
      SearchProfiles({ s: value }, function (profiles) {
        ViewModel.isLoading(false);
        ViewModel.profiles(_.first(profiles, 5));
      });
    },
    selectProfile: function (data, event) {
      var dialog = $("#dialogProfileMessage");

      dialog.data("Identifier", data.Identifier);
      dialog.data("Name", data.Name);
      dialog.data("Picture", data.Picture);
      dialog.modal({});

    },
    refresh: function (data, event) {
      _gaq.push(['_trackEvent', 'Conversation', 'Message', 'Refresh']);
      Initialize();
      Carousel.carousel(1);
    },
    compose: function (data, event) {
      _gaq.push(['_trackEvent', 'Conversation', 'Message', 'Compose']);
      ViewModel.Compose.ToIdentifier(null);
      ViewModel.Compose.Body(null);
      ViewModel.Compose.ToName(null);
      ViewModel.Compose.FromName(null);
      Carousel.carousel(0);
      context.find("#toUser").focus();
    },
    composeSend: function (data, event) {
      if (ViewModel.Compose.ToIdentifier() == null || ViewModel.Compose.Body() == null) {
        ViewModel.uiErrors.push({ message: "Invalid message. Please fill out the body and a valid user." });

        window.setTimeout(function () {
          ViewModel.uiErrors([]);
        }, 5e3);
        return false;
      }

      _gaq.push(['_trackEvent', 'Conversation', 'Message', 'Send']);
      ViewModel.isLoading(true);

      var params = {
        ToUserIdentifier: ViewModel.Compose.ToIdentifier(),
        Body: ViewModel.Compose.Body(),
        ParentConversationIdentifier: ""
      };

      _gaq.push(['_trackEvent', 'Conversation', 'Message', 'Sent']);

      ViewModel.Compose.ToName(null);
      $("input.typeahead").val("");
      SendMessage(params, function (message) {
        ViewModel.uiMessages([{ message: "Your message was successfully sent!" }]);
        ViewModel.Compose.ToIdentifier(null);
        window.setTimeout(function () {
          ViewModel.uiMessages([]);
        }, 5e3);
        Carousel.carousel(1);
      });

    },
    send: function (data, event) {
      _gaq.push(['_trackEvent', 'Conversation', 'Message', 'Reply']);
      var value = ViewModel.sendInput();

      ViewModel.isLoading(true);
      ViewModel.sendInput("");

      var current = MyUserId;

      var id;

      if (current == ViewModel.Thread.ToIdentifier()) {
        id = ViewModel.Thread.FromIdentifier();
      } else {
        id = ViewModel.Thread.ToIdentifier();
      }



      var params = {
        Body: value,
        ToUserIdentifier: id,
        ParentConversationIdentifier: ViewModel.Thread.Identifier()
      };

      _gaq.push(['_trackEvent', 'Conversation', 'Message', 'Sent']);

      SendMessage(params, function (message) {
        onMessageSent(message);

      });
    },

    selectAll: function (data, event) {
      var val = event.target.checked;
      _.map(ViewModel.conversations(), function (it) {
        return it.Selected(val);
      });

      return true;
    },
    back: function (data, event) {
      _gaq.push(['_trackEvent', 'Conversation', 'Message', 'Back']);

      Carousel.carousel(1);
      ViewModel.refresh();
      window.clearInterval(MessagePoller);
    },
    viewMessage: function (data, event) {
      ViewModel.isLoading(true);

      data.Read(true);
      ViewModel.Thread.Identifier(data.Identifier);
      ViewModel.Thread.ToIdentifier(data.ToUserIdentifier);
      ViewModel.Thread.ToName(data.ToDisplayName);
      ViewModel.Thread.FromIdentifier(data.FromUserIdentifier);
      ViewModel.Thread.FromName(data.FromDisplayName);

      Carousel.carousel(2);

      MessagePoller = window.setInterval(function () {
        SearchMessages({
          "identifier": ViewModel.Thread.Identifier()
        }, RenderMessages);
      }, 5e3);

      SearchMessages({
        "identifier": ViewModel.Thread.Identifier()
      }, RenderMessages);

      ServiceLocator.getService("addConversationMessage").invoke({
        Identifier: ViewModel.Thread.Identifier(),
        Read: true
      }, $.noop);
    }
  };

  ko.applyBindings(ViewModel, context[0]);

  var onMessageSent = function (message) {

    message.On = moment.utc(message.On).fromNow();
    ViewModel.Thread.Messages.push(message);
    ViewModel.isLoading(false);
    var listMessages = context.find(".messages-list")[0];
    listMessages.scrollTop = listMessages.scrollHeight;
  };

  var Initialize = function () {
    ViewModel.conversations([]);
    ViewModel.viewInbox([]);
    ViewModel.isLoading(true);
    ViewModel.uiErrors([]);
    ViewModel.uiMessages([]);
    ServiceLocator.getService("getConversationMessages").invoke({}, function (err, messages) {

      var inbox = [];

      messages = _.map(messages, function (it) {
        it.Read = ko.observable(it.Read);
        it.On = moment.utc(it.On).fromNow()
        it.Selected = ko.observable(false);
        return it;
      });

      _.filter(messages, function (message) {
        inbox.push(message);
        return message;
      });

      ViewModel.viewInbox(inbox);
      ViewModel.conversations(inbox);

      ViewModel.isLoading(false);

    });
  };

  var RenderMessages = function (messages) {

    var unread = _.filter(messages, function (msg) {
      return msg.Read == false;
    });

    if (unread.length > 0) {
      document.title = viewTitle + " (" + unread.length + ")";
    }

    ViewModel.Thread.Messages([]);
    ViewModel.Thread.Messages(messages);
    var listMessages = context.find(".messages-list")[0];
    listMessages.scrollTop = listMessages.scrollHeight;

  };

  var SearchMessages = function (params, callback) {
    ServiceLocator.getService("getConversationMessages").invoke(params, function (err, messages) {
      messages = _.map(messages, function (it) {
        it.On = moment.utc(it.On).fromNow();
        return it;
      });
      callback(messages);
    });
  };

  var SearchProfiles = function (params, callback) {
    ServiceLocator.getService("searchProfiles").invoke(params, function (err, response) {
      callback(response);
    });
  };

  var SendMessage = function (params, callback) {
    ServiceLocator.getService("addConversationMessage").invoke(params, function (err, response) {
      callback(response);
    });
  };


  ViewModel.view.subscribe(function (val) {
    if (ViewModel.view() == "inbox") {
      ViewModel.conversations(ViewModel.viewInbox());

    } else {
      ViewModel.conversations(ViewModel.viewSent());
    }
  });
  Initialize();

})(jQuery);