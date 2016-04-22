(function ($) {
  var form = $("#quick-new-lend-item-form");

  ResetForm = function () {

    form.find("#item-description").val("");
    form.find("#item-share-privacy").val("1");
    form.find("#item-trade-privacy").val("0");
    form.find("#item-rent-privacy").val("0");
    form.find("#item-free-privacy").val("0");
    form.find(".progress .bar").css({ width: "0%" });

    setTimeout(function () {
      form.find("#item-title").focus();
    }, 500);

  };

  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("#itemId").val(),
      Title: form.find("#item-title").val(),
      Description: form.find("#item-description").val(),
      SharePrivacyLevel: form.find("#item-share-privacy").val(),
      TradePrivacyLevel: form.find("#item-trade-privacy").val(),
      RentPrivacyLevel: form.find("#item-rent-privacy").val(),
      FreePrivacyLevel: form.find("#item-free-privacy").val()
    };

    form.find(".btn-primary").button("loading");
    ServiceLocator.getService("addProduct").invoke(params, function (err, res) {

      var itemId = res.Identifier;

      form.find(".alert-success").removeClass("hide");
      window.setTimeout(function () {
        ResetForm();
        $("#dialogNewLendItem").modal("hide");
        window.location.href = "/offer/" + res.Key + '?new=true';

      }, 2e3);

    });

    return false;
  });

  $("body a.btn-dialog-new-lend-item").on("click", function (e) {
    if (isMobile) {
      window.location.href = "/offer/add";
    } else {
      e.preventDefault();
      GlobalNewLendItemCommand();
    }
 
  });
})(jQuery);

// This is used as a way for key bindings to invoke the dialog
var GlobalNewLendItemCommand = function () {
  var form = $("#quick-new-lend-item-form");

  ResetForm();

  $("#dialogNewLendItem").modal({ backdrop: 'static' }).css({
    "width": 700,
    "margin-left": function () {
      return -($(this).width() / 2);
    }
  });
}