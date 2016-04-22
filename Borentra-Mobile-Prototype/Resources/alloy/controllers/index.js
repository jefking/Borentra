function Controller() {
    require("alloy/controllers/BaseController").apply(this, Array.prototype.slice.call(arguments));
    this.__controllerPath = "index";
    arguments[0] ? arguments[0]["__parentSymbol"] : null;
    arguments[0] ? arguments[0]["$model"] : null;
    arguments[0] ? arguments[0]["__itemTemplate"] : null;
    var $ = this;
    var exports = {};
    $.__views.index = Ti.UI.createWindow({
        backgroundColor: "#fff",
        id: "index"
    });
    $.__views.index && $.addTopLevelView($.__views.index);
    exports.destroy = function() {};
    _.extend($, $.__views);
    var fb = require("facebook");
    fb.appid = "315854161864272";
    fb.permissions = [ "email", "user_about_me", "user_location" ];
    fb.forceDialogAuth = true;
    fb.addEventListener("login", function(e) {
        if (e.success) {
            $.index.close();
            var home = Alloy.createController("home").getView();
            home.open();
        } else e.error ? Ti.API.error("Authentication error") : e.cancelled && Ti.API.error("Cancelled login");
    });
    fb.authorize();
    $.index.open();
    _.extend($, exports);
}

var Alloy = require("alloy"), Backbone = Alloy.Backbone, _ = Alloy._;

module.exports = Controller;