(function ($) {
  var form = $("#quick-request-item-form");

  ResetForm = function () {

    form.find("#item-description").val("");
    form.find("#for-free").attr("checked", true);
    form.find("#for-share").attr("checked", true);
    form.find("#for-trade").attr("checked", false);
    form.find("#for-rent").attr("checked", false);
    form.find(".progress .bar").css({ width: "0%" });

    setTimeout(function () {
      form.find("#item-title").focus();
    }, 500);
  };

  form.on("submit", function (event) {
    var params = {
      Title: form.find("#item-title").val(),
      Description: form.find("#item-description").val(),
      ForTrade: form.find("#for-trade")[0].checked ? 1 : 0,
      ForRent: form.find("#for-rent")[0].checked ? 1 : 0,
      ForFree: form.find("#for-free")[0].checked ? 1 : 0,
      ForShare: form.find("#for-share")[0].checked ? 1 : 0,
    };

    form.find(".btn-primary").button("loading");
    ServiceLocator.getService("addItemRequestSave").invoke(params, function (err, res) {

      form.find(".alert-success").removeClass("hide");
      window.setTimeout(function () {
        ResetForm();
        $("#dialogItemRequest").modal("hide");
        window.location.href = "/wanted/" + res.Key + '?new=true';

      }, 2e3);

    });

    return false;
  });

  $("body .btn-dialog-request-item").on("click", function (e) {

    e.preventDefault();
    GlobalRequestItemCommand();
  });

  $("body a.btn-dialog-delete-item-request").on("click", function (e) {
      var meta = $(e.currentTarget).data();
      e.preventDefault();

      if (window.confirm("Are you sure you want to remove this request?")) {

          var listItem = $(e.currentTarget).parents("div.request-row")[0];

          var params = { "Identifier": meta.id, "Delete": meta['delete'] };

          $(listItem).fadeOut("fast", function () {
              $(listItem).remove();
          });
          // Async request
          ServiceLocator.getService("addItemRequestSave").invoke(params, function (err, res) {
          });

      }
  });
  $("body a.btn-dialog-delete-item-request2").on("click", function (e) {
      var meta = $(e.currentTarget).data();
      e.preventDefault();

      if (window.confirm("Are you sure you want to remove this request?")) {
          var form = $("#quick-request-item-edit-form");
          var listItem = $(e.currentTarget).parents("div.request-row")[0];

          var params = { "Identifier": form.find("#request-identifier").val(), "Delete": 1 };

          $(listItem).fadeOut("fast", function () {
              $(listItem).remove();
          });
          // Async request
          ServiceLocator.getService("addItemRequestSave").invoke(params, function (err, res) {
          });

          window.location = '/';
      }
  });
})(jQuery);

// This is used as a way for key bindings to invoke the dialog
var GlobalRequestItemCommand = function () {
  var form = $("#quick-request-item-form");

  ResetForm();

  $("#dialogItemRequest").modal({ backdrop: 'static' }).css({
    "width": 700,
    "margin-left": function () {
      return -($(this).width() / 2);
    }
  });

};