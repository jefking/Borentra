var neo4j = require("node-neo4j");
var _ = require("underscore");
//db = new neo4j("http://borentra:vPbeRvEwSIsj4OIohd29@borentra.sb01.stations.graphenedb.com:24789");
db = new neo4j("http://localhost:7474");
var items = require("../data/mtg/AllSets.json");

exports.import = function(req, res) {
  return res.json(true);
  
  var cards = require("../data/mtg/AllSets.json");
  _.map(cards, function(set) {
    console.log(set.name)

    var setNode, setCard;


    setNode = {
      physical_type: "set",
      name: set.name,
      code: set.code,
      releaseDate: set.releaseDate,
      border: set.border,
      type: set.type
    };

    db.insertNode(setNode, function(err, createdSetNode) {
      if (err) throw err;

      var createdSetId = createdSetNode.id;

      _.map(set.cards, function(card) {
        console.log(card);
        var setCard = {
          physical_type: "card",
          name: card.name,
          type: card.type,
          layout: card.layout,
          rarity: card.rarity,
          artist: card.artist,
          manaCost: card.manaCost,
          text: card.text,
          flavor: card.flavor,
          number: card.number,
          cmc: card.cmc,
          colors: card.colors,
          types: card.types,
          imageName: card.imageName
        };
        db.insertNode(setCard, function(err, createdSetCard) {
          if (err) throw err;

          var createdCardId = createdSetCard.id;

          db.insertRelationship(createdSetId, createdCardId, 'contains', {}, function(err, relationship) {
            if (err) throw err;

            // Output relationship properties.
            console.log(relationship.data);

            // Output relationship id.
            console.log(relationship.id);

            // Output relationship start_node_id.
            console.log(relationship.start_node_id);

            // Output relationship end_node_id.
            console.log(relationship.end_node_id);
          });

        });
      });
    });

  });
  res.json({
    Success: true
  })
};


exports.get2 = function(req, res) {

  var params = req.query;
  var limit = 5;
  if (params.limit) {
    limit = params.limit;
  }
  var data = {};

  var cards = items;

  var allCards = [];

  _.map(cards, function(set) {

    _.map(set.cards, function(card) {
      card.setName = set.name;
      allCards.push(card);

    });
  });  
 
  var shuffled = _.shuffle(allCards).slice(0, limit);
  
  var json = _.map(shuffled, function(card) {
    var newCard = {
      Name: card.name + " " + card.setName,
      Query: "mtg card magic the gathering"
    };
    return newCard;
  });     
  res.jsonp(json);
};


exports.clear = function(req, res) {
  db.cypherQuery("START n=node(*) MATCH n-[r?]-() WHERE ID(n) <> 0 DELETE n,r", function(err, result) {
    if (err) throw err;
    console.log(err);
    res.json({
      Cleared: true
    })
  });
};
