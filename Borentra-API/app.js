/**
 * Module dependencies.
 */

var express = require('express');
var routes = require('./routes');
var magic = require('./routes/magic');
var xbox360 = require('./routes/xbox360');
var halloween = require('./routes/halloween');
var pokemon = require('./routes/pokemon');
var ps3 = require('./routes/ps3');  
var tools = require('./routes/tools');  
var books = require('./routes/books');  

var http = require('http');

var path = require('path');

var app = express();

// all environments
app.set('port', process.env.PORT || 3000);
app.set('views', __dirname + '/views');
app.set('view engine', 'ejs');
app.use(express.favicon());
app.use(express.logger('dev'));
app.use(express.bodyParser());
app.use(express.methodOverride());
app.use(express.cookieParser('meowmeow'));
app.use(express.session());
app.use(app.router);
app.use(express.static(path.join(__dirname, 'public')));

// development only
if ('development' == app.get('env')) {
  app.use(express.errorHandler());
}
app.get('/', routes.index);
app.get('/mtg/get', magic.get2);
app.get('/mtg/import', magic.import);
app.get('/mtg/clear', magic.clear);
app.get('/xbox360/get', xbox360.get);
app.get('/ps3/get', ps3.get);
app.get('/halloween/get', halloween.get);

app.get('/pokemon/downloadImages', pokemon.downloadImages);
app.get('/pokemon/downloadThumbnails', pokemon.downloadThumbnails);

app.get('/pokemon/get', pokemon.get);
app.get('/tools/get', tools.get);    

app.get('/books/download', books.download);    
app.get('/books/get', books.get);    

http.createServer(app).listen(app.get('port'), function() {
  console.log('Express server listening on port ' + app.get('port'));
});
