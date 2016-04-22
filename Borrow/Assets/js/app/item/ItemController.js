(function ($) {
  var form = $("#quick-add-item-form");

  // Provides a getter for the current item being selected <code>invoke itemIdentifier()</code>
  var itemIdentifier = function () {
    return form.find("#itemId").val();
  };

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
        $("#dialogAddItem").modal("hide");
        window.location.href = "/offer/" + res.Key + '?new=true';

      }, 2e3);

    });

    return false;
  });

  $("body a.btn-dialog-delete-item").on("click", function (e) {
      e.preventDefault();


      if (window.confirm("Are you sure you would like to delete this item?")) {

          var meta = $(e.currentTarget).data();

          var params = { "Identifier": meta.id, "Delete": meta['delete'] };


          var listItem = $(e.currentTarget).parents("div.item-row")[0];

          $(listItem).fadeOut("fast", function () {
              $(listItem).remove();
          });

          ServiceLocator.getService("addProduct").invoke(params, function (err, res) {
          });

      }
  });

  $("body a.btn-dialog-delete-item2").on("click", function (e) {
      e.preventDefault();
      if (window.confirm("Are you sure you would like to delete this item?")) {
          var form = $("#quick-edit-item-form");

          var meta = $(e.currentTarget).data();
          console.log(form.find("#itemId").val());
          var params = { "Identifier": form.find("#itemId").val(), "Delete": 1 };

          var listItem = $(e.currentTarget).parents("div.item-row")[0];

          $(listItem).fadeOut("fast", function () {
              $(listItem).remove();
          });

          ServiceLocator.getService("addProduct").invoke(params, function (err, res) {
          });

          window.location = '/';
      }
  });
})(jQuery);


// This is used as a way for key bindings to invoke the dialog
var GlobalAddItemCommand = function () {
  var form = $("#quick-add-item-form");

  ResetForm();

  $("#dialogAddItem").modal({ backdrop: 'static' }).css({
    "width": 700,
    "margin-left": function () {
      return -($(this).width() / 2);
    }
  });
}