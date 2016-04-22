(function ($) {
    var context = $(".admin-reports-item-growth");

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
          vals.push([Date.UTC(+date[2], +date[0] - 1, +date[1]), val]);
      }

      return vals;
  })();

  var offers = (function () {
      var vals = [];
      var spans = graph.find(".graph-data > span.offer");
      for (var i = 0; i < spans.length; i++) {
          var key = $(spans[i]).find("span.key")[0].innerText;
          var val = +$(spans[i]).find("span.value")[0].innerText;

          var date = key.split("/");
          vals.push([Date.UTC(+date[2], +date[0] - 1, +date[1]), val]);
      }

      return vals;
  })();

  var requests = (function () {
      var vals = [];
      var spans = graph.find(".graph-data > span.request");
      for (var i = 0; i < spans.length; i++) {
          var key = $(spans[i]).find("span.key")[0].innerText;
          var val = +$(spans[i]).find("span.value")[0].innerText;

          var date = key.split("/");
          vals.push([Date.UTC(+date[2], +date[0] - 1, +date[1]), val]);
      }

      return vals;
  })();

  values = values.sort();
  offers = offers.sort();
  requests = requests.sort();
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
        text: 'Things'
      },
      min: 0
    },
    tooltip: {
      formatter: function () {
        return '<b>' + this.series.name + '</b><br/>' +
        Highcharts.dateFormat('%e. %b', this.x) + ': ' + this.y + ' offers';
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
        name: 'Offers',
        data: offers
    }, {
        name: 'Requests',
        data: requests
    }]
  });

})(jQuery);

