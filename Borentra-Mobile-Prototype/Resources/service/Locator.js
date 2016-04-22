var ServiceLocator = {
    services: {},
    addService: function(service) {
        if (!service) return false;
        this.services[service.name] = service;
        return this;
    },
    getService: function(name) {
        return this.services[name];
    },
    removeService: function(name) {
        delete this.services[name];
    },
    removeServices: function() {
        this.services = {};
        return true;
    },
    getServices: function() {
        if (void 0 === typeof this.services) return false;
        return this.services;
    }
};

module.exports = ServiceLocator;