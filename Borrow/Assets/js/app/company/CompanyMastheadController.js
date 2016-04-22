(function ($) {

  var context = $(".company-header");

  if (!context.length) {
    return false;
  }

  context.find("h3").dotdotdot({
    watch: "window"
  });
})(jQuery);