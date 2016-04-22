/*
 * Service
 *
 * @author Jaime Bueza
 *
 * Represents a service call
 */
var _ = require("support/underscore")._;

Service = function Constructor(name, uri, options) {
  if ( typeof options == 'undefined')
    options = {};

  var defaults = {
    method : options.method || function() {
      var type = "get";
      if (name.match(/^get/i)) {
        type = "get";
      } else if (name.match(/^add|del|update/i)) {
        type = "post";
      }
      return type;

    }(),
    jsonp : false,
    wrapped : false,
    template : false,
    autoEncodeUrl : true,
    timeout : 45e3,
    async : true,
    contentType : "application/json; charset=utf-8"
  };
  this.name = name;
  this.uri = uri;
  this.options = _.extend({}, defaults, options);
};

Service.prototype.invoke = function(params, callback, scope) {
  if (!callback) {

    return false;
  }
  var self = this, data;
  var options = self.getOptions() || {};
  var method = options.method;
  var uri = self.getURI();
  var responseType = options.responseType || 'JSON';

  if (options.template) {
    uri = self.parse(uri, params);
    if (method == 'get') {
      params = null;
    }
  }

  var xhr = Ti.Network.createHTTPClient({
    autoEncodeUrl : options.autoEncodeUrl,
    async : options.async
  });

  if (method == 'get') {
    uri = uri + "?" + self.serialize(params);
  }

  Ti.API.info(method + " - XHR URL: " + uri);
  // URL
  xhr.open(options.method.toUpperCase(), uri);

  // Request header
  xhr.setRequestHeader('Content-Type', options.contentType);

  // Errors
  xhr.setTimeout(options.timeout);
  xhr.onerror = function() {
    Ti.API.error("XHR onerror [" + this.status + "]: " + this.responseText);
    return callback.call(null, this, this.responseText);

  };

  // Success
  xhr.onload = function() {
    var res = {};
    if (typeof this.responseText === 'string') {
      res = JSON.parse(this.responseText);
    }
    if (this.status < 400) {
      Ti.API.info('XHR onload');
      return callback.call(null, null, res);
    } else {
      Ti.API.error("XHR onload [" + this.status + "]: " + this.responseText);
      return callback.call(null, this, res);
    }
  };

  xhr.setRequestHeader('Content-Type', options.responseType || 'application/x-www-form-urlencoded');

  if (method != "get") {
    xhr.send(params);
  } else {
    xhr.send();
  }
};

Service.prototype.serialize = function(obj) {

  var str = [];
  for (var p in obj)
  str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
  return str.join("&");
};

/*
 *  Returns the current service's name
 */
Service.prototype.getName = function() {
  return this.name;
};

/*
 * Returns the current service's URI
 */
Service.prototype.getURI = function() {
  return this.uri;
};

/*
 * Returns the optional parameters of the current service
 */
Service.prototype.getOptions = function() {
  return this.options;
};

/*
 * Sets or Gets an option from a particular Service
 */
Service.prototype.option = function() {
  if (!arguments.length)
    return false;
  if (arguments.length > 2)
    return false;
  if ('string' != typeof arguments[0])
    return false;

  if (arguments.length == 2) {
    this.options[arguments[0]] = arguments[1];
    return this;
  } else if (arguments.length == 1) {
    return this.options[arguments[0]];
  }
};

/*
 * Returns an HTML fragment from {} templating
 * @param context {String} Accepts any length string with mustaches ({myKey})
 * @param params {Object} A JSON object that is ran against the content string that has mustaches
 */
Service.prototype.parse = function(content, params) {
  if (arguments.length != 2)
    return false;
  if ('string' != typeof content)
    return false;
  if ('object' != typeof params)
    return false;
  $.each(params, function(key, value) {
    content = content.split("{" + key + "}").join(value);
  });
  return content;
};

module.exports = Service;
