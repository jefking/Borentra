(function ($) {
  var o = $({});
  $.subscribe = o.on.bind(o);
  $.unsubscribe = o.off.bind(o);
  $.publish = o.trigger.bind(o);
}(jQuery));