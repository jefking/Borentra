var _ = require("underscore");
var fs = require("fs");
var request = require("request");
var items = require("../data/pokemon/cards.json");
var mkdirp = require("mkdirp");
var http = require('http-get');

exports.downloadImages = function(req, res) { 
  return false;
  var download = function(uri, set, filename) {
      request.head(uri, function(err, res, body) {

        request(uri).pipe(fs.createWriteStream('data/pokemon/scans/' + set + '/' + filename));
      });
      };
  items = items.slice(6400, 6600);
  var count = 0;
  _.each(items, function(item) {

    if (item.Image != 'NULL') {

      var parts = item.Image.split("/");

      var filename = parts[parts.length - 1];
      var set = parts[parts.length - 2];

      count++;

      mkdirp('data/pokemon/scans/' + set, 0777, function(err) {
        if (err) {
          console.error(err);
        }

        console.log(set + "/" + filename);

        download(item.Image, set, filename);

      });
    }
  });

  console.log(count);


  res.json({
    success: true
  });
};

exports.downloadThumbnails = function(req, res) {
  res.json({
    success: true
  });
};
exports.get = function(req, res) {

  var params = req.query;
  var limit = 5;
  if (params.limit) {
    limit = params.limit;
  }
  var data = {};

  var shuffled = _.shuffle(items).slice(0, limit);
  var json = _.map(shuffled, function(item) {
    var newItem = {
      Name: item.Name + " Pokemon Card",
      Image: item.Image,
      Thumbnail: item.Thumbnail,
      Query: "pokemon #card"
    };
    return newItem;
  });

  res.jsonp(json);

};
