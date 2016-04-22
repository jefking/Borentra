var _ = require("underscore");
var fs = require("fs");
var request = require("request");
var items = require("../data/books/books.json");
var mkdirp = require("mkdirp");
var http = require('http-get');

exports.download = function(req, res) {

	return res.jsonp({
		success: true
	});

	var download = function(uri, filename) {
		request.head(uri, function(err, res, body) {

			request(uri).pipe(fs.createWriteStream('data/books/images/' + filename));
		});
	};
	var count = 0;
	_.each(items, function(item) {

		if (item.Image != 'NULL') {

			var parts = item.Image.split("/");

			var filename = parts[parts.length - 1];
			var set = parts[parts.length - 2];

			count++;

			mkdirp('data/books/images', 0777, function(err) {
				if (err) {
					console.error(err);
				}

				console.log(set + "/" + filename);

				download(item.Image, filename);

			});
		}
	});

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
			Name: item.Name,
			Image: item.Image,
			Query: "book"
		};
		return newItem;
	});

	res.jsonp(json);

};
