var _ = require("support/underscore");

ServiceResponse = function(params) {
    var response = {
        code: 500,
        data: []
    };
    return _.extend({}, response, params);
};

module.exports = ServiceResponse;