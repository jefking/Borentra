(function ($) {

  var context = $(".component.global-heat-map-component");

  if (!context.length) {
    return false;
  }

  var Process = function (arr) {
    var map = {};

    _.each(arr, function (it) {
      map[it.IsoCode] = it.Percentage;
    });
    return map;
  };

  ServiceLocator.getService("getGlobalHeatMapStats").invoke({}, function (err, data) {
    var map = new AdminMapView({
      data: Process(data), el: 'world_map_container'
    });

    map.render();

    $("#world_map_loading").hide();

  });

})(jQuery);
