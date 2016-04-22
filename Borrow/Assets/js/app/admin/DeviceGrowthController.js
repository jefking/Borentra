(function ($) {
  var context = $(".admin-reports-device-growth");
  if (!context.length) {
    return false;
  }

  var graph = context.find(".graph");

  var values = (function () {
    var vals = [];
    var spans = graph.find(".graph-data > span.record");
    for (var i = 0; i < spans.length; i++) {
      var key = $(spans[i]).find("span.key")[0].innerText;
      var val = +$(spans[i]).find("span.value")[0].innerText;
      var date = key.split("/");
     vals.push([Date.UTC(+date[2], + date[0] - 1, + date[1]), val]);
    }

    return vals;
  })();
  var windows = (function () {
      var vals = [];
      var spans = graph.find(".graph-data > span.windows");
      for (var i = 0; i < spans.length; i++) {
          var key = $(spans[i]).find("span.key")[0].innerText;
          var val = +$(spans[i]).find("span.value")[0].innerText;

          var date = key.split("/");
          vals.push([Date.UTC(+date[2], +date[0] - 1, +date[1]), val]);
      }

      return vals;
  })();
  var apples = (function () {
      var vals = [];
      var spans = graph.find(".graph-data > span.apple");
      for (var i = 0; i < spans.length; i++) {
          var key = $(spans[i]).find("span.key")[0].innerText;
          var val = +$(spans[i]).find("span.value")[0].innerText;

          var date = key.split("/");
          vals.push([Date.UTC(+date[2], +date[0] - 1, +date[1]), val]);
      }

      return vals;
  })();
  var unknowns = (function () {
      var vals = [];
      var spans = graph.find(".graph-data > span.unknown");
      for (var i = 0; i < spans.length; i++) {
          var key = $(spans[i]).find("span.key")[0].innerText;
          var val = +$(spans[i]).find("span.value")[0].innerText;

          var date = key.split("/");
          vals.push([Date.UTC(+date[2], +date[0] - 1, +date[1]), val]);
      }

      return vals;
  })();

  values = values.sort();
  graph.find(".graph-visualization").highcharts({
    chart: {
      type: 'spline'
    },
    title: {
      text: 'Growth'
    },
    subtitle: {
      text: "Day Over Day"
    },
    xAxis: {
      type: 'datetime',
      dateTimeLabelFormats: { // don't display the dummy year
        month: '%e. %b',
        year: '%b'
      }
    },
    yAxis: {
      title: {
        text: 'Devices'
      },
      min: 0
    },
    tooltip: {
      formatter: function () {
        return '<b>' + this.series.name + '</b><br/>' +
        Highcharts.dateFormat('%e. %b', this.x) + ': ' + this.y + ' devices';
      }
    },
    plotOptions: {
      line: {
        dataLabels: {
          enabled: true
        },
        enableMouseTracking: false
      }
    },
    series: [{
        name: 'Total',
        data: values
    }, {
        name: 'Windows',
        data: windows
    }, {
        name: 'Apple',
        data: apples
    }, {
        name: 'Unknown',
        data: unknowns
    }]
  });

})(jQuery);