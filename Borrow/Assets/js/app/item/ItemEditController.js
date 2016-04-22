(function ($) {
  var form = $("#quick-edit-item-form");

  // Provides a getter for the current item being selected <code>invoke itemIdentifier()</code>
  var itemIdentifier = function () {
    return form.find("#itemId").val();
  };

  // Do not break submission without images
  form.on("submit", function (event) {
    var params = {
      Identifier: form.find("#itemId").val(),
      Title: form.find("#item-title").val(),
      Description: form.find("#item-description").val(),
      SharePrivacyLevel: form.find("#item-share-privacy").val(),
      TradePrivacyLevel: form.find("#item-trade-privacy").val(),
      RentPrivacyLevel: form.find("#item-rent-privacy").val(),
      FreePrivacyLevel: form.find("#item-free-privacy").val(),
      Price: form.find("#price").val(),
      PerUnit: form.find("#unit").val()
    };

    form.find(".btn-primary").button("loading");
    ServiceLocator.getService("addProduct").invoke(params, function (err, res) {
      console.log(res);
      var itemId = res.Identifier;
      form.find("#itemId").val(itemId);
      $("#dialogEditItem").modal("hide");
      window.location.reload(true);

    });

    return false;
  });

  $("body a.btn-dialog-edit-item").on("click", function (e) {
    var meta = $(e.currentTarget).data();
    e.preventDefault();

    GlobalEditItemCommand(meta);
  });

  var UpdatePhoto = function (params, callback) {

    ServiceLocator.getService("addProductPhoto").invoke(params, function (err, response) {
      callback(response);
    });
  };

  form.delegate(".btn-delete-photo", "click", function (e) {
    e.preventDefault();

    if (window.confirm("Are you sure you want to remove this photo?")) {
      var params = {
        Identifier: $(e.currentTarget).data().id,
        IsPrimary: false,
        Delete: true
      };

      UpdatePhoto(params, function (data) {

      });

      setTimeout(function () {

        $(e.currentTarget).parent().parent().fadeOut();
        form.find(".photos-count").text(form.find("li.item-photos:visible").length);
      }, 100);
    }

  });

  form.delegate(".btn-primary-photo", "click", function (e) {
    e.preventDefault();

    var params = {
      Identifier: $(e.currentTarget).data().id,
      IsPrimary: true,
      Delete: false
    };

    UpdatePhoto(params, function (data) {
      form.find("li.item-photo").removeClass("golden");
      $(e.currentTarget).parent().parent().addClass("golden");
    });
  });

})(jQuery);

var GlobalEditItemCommand = function () {
  var form = $("#quick-edit-item-form");
  if (arguments.length !== 0) {
    var meta = arguments[0];
    form.find("#itemId").val(meta.id);
    form.find("#item-title").val(meta.title);
    form.find("#item-description").val(meta.description);
    form.find("#item-share-privacy").val(meta.shareprivacylevel);
    form.find("#item-trade-privacy").val(meta.tradeprivacylevel);
    form.find("#item-rent-privacy").val(meta.rentprivacylevel);
    form.find("#item-free-privacy").val(meta.freeprivacylevel);
    form.find("#price").val(meta.price);

    ServiceLocator.getService("getProductPhotos").invoke({ itemIdentifier: meta.id }, function (err, response) {
      var photos = response;
      form.find(".photos-count").text(photos.length);
      var list = form.find("ul.thumbnails");
      list.empty();
      $(photos).each(function (index, photo) {
        var img = $("<img/>").attr({
          src: "//cdn.borentra.com" + photo.Thumbnail
        });
        img.css("cursor", "pointer");

        var element = $("<li/>");
        element.addClass("thumbnail item-photo");

        var menu = $("<div class='item-controls pull-right'></div>");
        menu.append("<a href='#' class='btn-primary-photo' data-id='" + photo.Identifier + "' title='Set As Primary Photo'><span class='glyphicon glyphicon-star'></span></a>");
        menu.append("<a href='#' class='btn-delete-photo' data-id='" + photo.Identifier + "' title='Remove Photo'><span class='glyphicon glyphicon-remove'></span></a>");

        $(menu).appendTo(element);
        $(img).appendTo(element);

        element.appendTo(list);
      });
    });

  }
  form.find(".btn-primary").button("reset");

  $("#dialogEditItem").modal({}).css({
    "width": 900,
    "margin-left": function () {
      return -($(this).width() / 2);
    }
  });
};