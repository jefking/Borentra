var _ = require("underscore");



var items = require("../data/tools/tools.json");


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
      Name: item.Name + " Tool",
      Query: "tool powertool"
    };
    return newItem;
  });

  res.jsonp(json);

};
