/*
 * Borrow Accept Controller
 * @author Borentra 
 * 
 * Provides an implementation silo for handling accepting requests (free)
 */
(function ($) {
  var form = $("#quick-request-item-fulfill-form");

  form.on("submit", function (event) {
    var params = {
      ItemRequestIdentifier: form.find("#requestIdentifier").val()
      , Status: 0
      , Comment: form.find("#comment").val()
      , WillRent: form.find("#for-rent")[0].checked ? 1 : 0
      , WillShare: form.find("#for-share")[0].checked ? 1 : 0
      , WillTrade: form.find("#for-trade")[0].checked ? 1 : 0
      , WillGive: form.find("#for-free")[0].checked ? 1 : 0
    };

    ServiceLocator.getService("addItemRequestFulfill").invoke(params, function (err, response) {
      var context = $("#dialogItemRequestFulfill");
      context.modal("hide");
    });

    return false;
  });

  $("body .btn-dialog-item-request-fulfill").on("click", function (e) {
    var meta = $(e.currentTarget).data();
    e.preventDefault();

    GlobalRequestItemFulfillCommand(meta);
  });

})(jQuery);

// This is used as a way for key bindings to invoke the dialog
var GlobalRequestItemFulfillCommand = function () {
  var form = $("#quick-request-item-fulfill-form");
  if (arguments.length !== 0) {
    var meta = arguments[0];
    form.find("#requestIdentifier").val(meta.requestidentifier);
    if (meta.forshare == 0) {
      $("#fulfill-request-share").hide();
    }
    if (meta.fortrade == 0) {
      $("#fulfill-request-trade").hide();
    }
    if (meta.forrent == 0) {
      $("#fulfill-request-rent").hide();
    }
    if (meta.forfree == 0) {
      $("#fulfill-request-free").hide();
    }
  }

  form.find(".btn-primary").button("reset");

  $("#dialogItemRequestFulfill").modal({ backdrop: 'static' }).css({
    "width": 700,
    "margin-left": function () {
      return -($(this).width() / 2);
    }
  });
};