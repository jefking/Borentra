var _ = require("support/underscore")._;

Service = function(name, uri, options) {
    "undefined" == typeof options && (options = {});
    var defaults = {
        method: options.method || function() {
            var type = "get";
            name.match(/^get/i) ? type = "get" : name.match(/^add|del|update/i) && (type = "post");
            return type;
        }(),
        jsonp: false,
        wrapped: false,
        template: false,
        autoEncodeUrl: true,
        timeout: 45e3,
        async: true,
        contentType: "application/json; charset=utf-8"
    };
    this.name = name;
    this.uri = uri;
    this.options = _.extend({}, defaults, options);
};

Service.prototype.invoke = function(params, callback) {
    if (!callback) return false;
    var self = this;
    var options = self.getOptions() || {};
    var method = options.method;
    var uri = self.getURI();
    options.responseType || "JSON";
    if (options.template) {
        uri = self.parse(uri, params);
        "get" == method && (params = null);
    }
    var xhr = Ti.Network.createHTTPClient({
        autoEncodeUrl: options.autoEncodeUrl,
        async: options.async
    });
    "get" == method && (uri = uri + "?" + self.serialize(params));
    Ti.API.info(method + " - XHR URL: " + uri);
    xhr.open(options.method.toUpperCase(), uri);
    xhr.setRequestHeader("Content-Type", options.contentType);
    xhr.setTimeout(options.timeout);
    xhr.onerror = function() {
        Ti.API.error("XHR onerror [" + this.status + "]: " + this.responseText);
        return callback.call(null, this, this.responseText);
    };
    xhr.onload = function() {
        var res = {};
        "string" == typeof this.responseText && (res = JSON.parse(this.responseText));
        if (400 > this.status) {
            Ti.API.info("XHR onload");
            return callback.call(null, null, res);
        }
        Ti.API.error("XHR onload [" + this.status + "]: " + this.responseText);
        return callback.call(null, this, res);
    };
    xhr.setRequestHeader("Content-Type", options.responseType || "application/x-www-form-urlencoded");
    "get" != method ? xhr.send(params) : xhr.send();
};

Service.prototype.serialize = function(obj) {
    var str = [];
    for (var p in obj) str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
    return str.join("&");
};

Service.prototype.getName = function() {
    return this.name;
};

Service.prototype.getURI = function() {
    return this.uri;
};

Service.prototype.getOptions = function() {
    return this.options;
};

Service.prototype.option = function() {
    if (!arguments.length) return false;
    if (arguments.length > 2) return false;
    if ("string" != typeof arguments[0]) return false;
    if (2 == arguments.length) {
        this.options[arguments[0]] = arguments[1];
        return this;
    }
    if (1 == arguments.length) return this.options[arguments[0]];
};

Service.prototype.parse = function(content, params) {
    if (2 != arguments.length) return false;
    if ("string" != typeof content) return false;
    if ("object" != typeof params) return false;
    $.each(params, function(key, value) {
        content = content.split("{" + key + "}").join(value);
    });
    return content;
};

module.exports = Service;