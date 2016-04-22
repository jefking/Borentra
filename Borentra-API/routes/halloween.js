var _ = require("underscore");
var items = require("../data/halloween/costumes.json");

function capitaliseFirstLetter(string) {
  return string.charAt(0).toUpperCase() + string.slice(1);
}

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
      Name: capitaliseFirstLetter(item.Name) + " Costume",
      Query: "halloween costume"
    };
    return newItem;
  });

  res.jsonp(json);

};
