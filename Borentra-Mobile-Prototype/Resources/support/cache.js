(function() {
    var CONFIG = {
        DISABLE: false,
        CACHE_EXPIRATION_INTERVAL: 60,
        EXPIRE_ON_GET: false
    };
    Ti.App.Cache = function() {
        var init_cache, expire_cache, current_timestamp, get, put, del;
        init_cache = function(cache_expiration_interval) {
            var db = Titanium.Database.open("cache");
            db.execute("CREATE TABLE IF NOT EXISTS cache (key TEXT UNIQUE, value TEXT, expiration INTEGER)");
            db.close();
            Ti.API.info("[CACHE] INITIALIZED");
            if (!CONFIG || CONFIG && !CONFIG.EXPIRE_ON_GET) {
                setInterval(expire_cache, 1e3 * cache_expiration_interval);
                Ti.API.info("[CACHE] Will expire objects each " + cache_expiration_interval + " seconds");
            }
        };
        expire_cache = function() {
            var db = Titanium.Database.open("cache");
            var timestamp = current_timestamp();
            var count = 0;
            var rs = db.execute("SELECT COUNT(*) FROM cache WHERE expiration <= ?", timestamp);
            while (rs.isValidRow()) {
                count = rs.field(0);
                rs.next();
            }
            rs.close();
            db.execute("DELETE FROM cache WHERE expiration <= ?", timestamp);
            db.close();
            Ti.API.debug("[CACHE] EXPIRATION: [" + count + "] object(s) expired");
        };
        current_timestamp = function() {
            var value = Math.floor(new Date().getTime() / 1e3);
            Ti.API.debug("[CACHE] current_timestamp=" + value);
            return value;
        };
        get = function(key) {
            var db = Titanium.Database.open("cache");
            if (CONFIG && CONFIG.EXPIRE_ON_GET) {
                Ti.API.debug('[CACHE] EXPIRE_ON_GET is set to "true"');
                expire_cache();
            }
            var rs = db.execute("SELECT value FROM cache WHERE key = ?", key);
            var result = null;
            if (rs.isValidRow()) {
                Ti.API.info("[CACHE] HIT, key[" + key + "]");
                result = JSON.parse(rs.fieldByName("value"));
            } else Ti.API.info("[CACHE] MISS, key[" + key + "]");
            rs.close();
            db.close();
            return result;
        };
        put = function(key, value, expiration_seconds) {
            expiration_seconds || (expiration_seconds = 300);
            var expires_in = current_timestamp() + expiration_seconds;
            var db = Titanium.Database.open("cache");
            Ti.API.info("[CACHE] PUT: time=" + current_timestamp() + ", expires_in=" + expires_in);
            var query = "INSERT OR REPLACE INTO cache (key, value, expiration) VALUES (?, ?, ?);";
            db.execute(query, key, JSON.stringify(value), expires_in);
            db.close();
        };
        del = function(key) {
            var db = Titanium.Database.open("cache");
            db.execute("DELETE FROM cache WHERE key = ?", key);
            db.close();
            Ti.API.info("[CACHE] DELETED key[" + key + "]");
        };
        return function() {
            if (CONFIG && CONFIG.DISABLE) return {
                get: function() {},
                put: function() {},
                del: function() {}
            };
            var cache_expiration_interval = 30;
            CONFIG && CONFIG.CACHE_EXPIRATION_INTERVAL && (cache_expiration_interval = CONFIG.CACHE_EXPIRATION_INTERVAL);
            init_cache(cache_expiration_interval);
            return {
                get: get,
                put: put,
                del: del
            };
        }();
    }(CONFIG);
})();