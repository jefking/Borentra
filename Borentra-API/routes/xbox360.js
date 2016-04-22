var _ = require("underscore");



var games = require("../data/xbox360/games.json");


exports.get = function(req, res) {

  var params = req.query;
  var limit = 5;
  if (params.limit) {
    limit = params.limit;
  }
  var data = {};

  var shuffled = _.shuffle(games).slice(0, limit);
  var json = _.map(shuffled, function(game) {
    var newGame = {
      Name: game.GameName + " XBOX360",
      Query: "xbox360 game cover"
    };
    return newGame;
  });

  res.jsonp(json);

};
