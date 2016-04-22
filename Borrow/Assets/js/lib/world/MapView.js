(function ($) {

  var AdminMapView = function (opts) {
    this.data = opts.data;
    this.el = opts.el;
    this.worldmap = window.worldmap;

    return this;
  };

  AdminMapView.prototype.render = function () {
    var paper, world,
      _this = this;
    paper = new Raphael(this.el, 1000, 750);
    paper.rect(0, 0, 1000, 750, 10).attr({
      stroke: 'none',
      fill: '#fff'
    });
    paper.setStart();

    _.each(_.keys(this.data), function (country_iso_code) {
      var path;
      path = paper.path(_this.worldmap.shapes[country_iso_code]).attr({
        stroke: "#fff",
        fill: _this.colorOfCountry(country_iso_code),
        "stroke-opacity": 0.25
      });
      return _this.bindHoverToCountry(paper, country_iso_code, path);
    });
    world = paper.setFinish();
    world.transform("t0, 80");
    return this.drawBars(paper);
  };

  AdminMapView.prototype.drawBars = function (paper) {
    var x;
    x = 840;
    return _.each(this.colorsRange().reverse(), function (range) {
      paper.rect(x, 500, 50, 25).attr({
        fill: range.color,
        stroke: 'none'
      });
      paper.text(x + 25, 490, "" + range.min + "-" + range.max).attr({
        'text-anchor': 'center'
      });
      return x = x - 10 - 50;
    });
  };

  AdminMapView.prototype.bindHoverToCountry = function (paper, country_iso_code, path) {
    var that;
    that = this;
    return path.hover(function (e) {
      var desc, name;
      name = that.worldmap.names[country_iso_code];
      desc = "" + that.data[country_iso_code] + " %";
      this.tipSet = that.drawCountryTooltip(paper, e.offsetX, e.offsetY, name, desc);
      this.c = this.c || this.attr("fill");
      return this.stop().animate({
        fill: "#C13A27"
      }, 500);
    }, function (e) {
      this.tipSet.remove();
      return this.stop().animate({
        fill: this.c
      }, 500);
    });
  };

  AdminMapView.prototype.drawCountryTooltip = function (paper, offsetX, offsetY, name, desc) {
    var rectHeight, rectRadius, rectWidth, strWidth, tipSet, x, y;
    tipSet = paper.set();
    strWidth = _.max([name.length, desc.length]);
    rectWidth = parseInt(strWidth * 220) / 33 + 10;
    rectWidth = _.max([rectWidth, 80]);
    if ((1000 - offsetX) < rectWidth) {
      x = 1000 - rectWidth;
    } else {
      x = offsetX;
    }
    y = offsetY;
    rectHeight = 48;
    rectRadius = 3;
    tipSet.push(paper.rect(x, y, rectWidth, rectHeight, rectRadius).attr({
      fill: '#fff',
      'fill-opacity': 0.7,
      stroke: '#D9DADA'
    }));
    tipSet.push(paper.text(x + 10, y + 15, name).attr({
      'text-anchor': 'start',
      'font-size': '13px'
    }));
    tipSet.push(paper.text(x + 10, y + 35, desc).attr({
      'text-anchor': 'start',
      'font-size': '13px'
    }));
    return tipSet;
  };

  AdminMapView.prototype.colorOfCountry = function (iso_code) {
    var country_name, value;
    country_name = this.worldmap.names[iso_code];
    value = this.data[iso_code];
    if (value != null) {
      return this.colorOfValue(value);
    } else {
      return '#FF';
    }
  };

  AdminMapView.prototype.colorOfValue = function (value) {
    var color;
    value = parseFloat(value);
    color = '#FF';
    _.each(this.colorsRange(), function (range) {
      if (range.min < value && value < range.max) {
        return color = range.color;
      }
    });
    return color;
  };

  AdminMapView.prototype.colorsRange = function () {
    return [{
        min: 0,
        max: 1,
        color: '#EEEEEE'
      }, {
        min: 1,
        max: 6,
        color: '#CCCCCC'
      }, {
        min: 6,
        max: 11,
        color: '#AAAAAA'
      }, {
        min: 11,
        max: 21,
        color: '#e06d61'
      }, {
        min: 21,
        max: 100,
        color: '#e55445'
      }
    ];
  };

  window.AdminMapView = AdminMapView;
})(jQuery);
