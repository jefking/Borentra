(function ($) {
  var context = $(".trade-management-component");
  if (!context.length) { return false; }


  var self = {
    TradeRequests: ko.observableArray([]),

    AcceptRequest: function (data, event) {
      event.preventDefault();
      var tradeIdentifier = $(event.currentTarget).data().id;

      $.post("/Api/Trade/Accept?tradeIdentifier=" + tradeIdentifier, function (data) {
        // TODO: hide the table row on success
        window.location.reload(true);
      });

      window.setTimeout(function () {
        window.location.reload(true);
      }, 2e3);
    },
    RejectRequest: function (data, event) {
      event.preventDefault();
      var tradeIdentifier = $(event.currentTarget).data().id;
     
      $.post("/Api/Trade/Reject?tradeIdentifier=" + tradeIdentifier, function (data) {
        // TODO: hide the table row on success

      });

      window.setTimeout(function () {
        window.location.reload(true);
      }, 2e3);
      
    },
    CancelRequest: function (data, event) {
      var id = $(event.currentTarget).data().id;

      if (window.confirm("Are you sure you would like to cancel this trade?")) {
        $.post("/Api/Trade/Delete?tradeIdentifier=" + id, function (data) {
          // TODO: hide the table row on success
          window.location.reload(true);
        });
      }
    }
  };

  ko.applyBindings(self, context[0]);

})(jQuery);