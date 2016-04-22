(function() {
    function baseIndexOf(array, value, fromIndex) {
        var index = (fromIndex || 0) - 1, length = array ? array.length : 0;
        while (length > ++index) if (array[index] === value) return index;
        return -1;
    }
    function cacheIndexOf(cache, value) {
        var type = typeof value;
        cache = cache.cache;
        if ("boolean" == type || null == value) return cache[value] ? 0 : -1;
        "number" != type && "string" != type && (type = "object");
        var key = "number" == type ? value : keyPrefix + value;
        cache = (cache = cache[type]) && cache[key];
        return "object" == type ? cache && baseIndexOf(cache, value) > -1 ? 0 : -1 : cache ? 0 : -1;
    }
    function cachePush(value) {
        var cache = this.cache, type = typeof value;
        if ("boolean" == type || null == value) cache[value] = true; else {
            "number" != type && "string" != type && (type = "object");
            var key = "number" == type ? value : keyPrefix + value, typeCache = cache[type] || (cache[type] = {});
            "object" == type ? (typeCache[key] || (typeCache[key] = [])).push(value) : typeCache[key] = true;
        }
    }
    function charAtCallback(value) {
        return value.charCodeAt(0);
    }
    function compareAscending(a, b) {
        var ac = a.criteria, bc = b.criteria;
        if (ac !== bc) {
            if (ac > bc || "undefined" == typeof ac) return 1;
            if (bc > ac || "undefined" == typeof bc) return -1;
        }
        return a.index - b.index;
    }
    function createCache(array) {
        var index = -1, length = array.length, first = array[0], mid = array[0 | length / 2], last = array[length - 1];
        if (first && "object" == typeof first && mid && "object" == typeof mid && last && "object" == typeof last) return false;
        var cache = getObject();
        cache["false"] = cache["null"] = cache["true"] = cache["undefined"] = false;
        var result = getObject();
        result.array = array;
        result.cache = cache;
        result.push = cachePush;
        while (length > ++index) result.push(array[index]);
        return result;
    }
    function escapeStringChar(match) {
        return "\\" + stringEscapes[match];
    }
    function getArray() {
        return arrayPool.pop() || [];
    }
    function getObject() {
        return objectPool.pop() || {
            array: null,
            cache: null,
            criteria: null,
            "false": false,
            index: 0,
            "null": false,
            number: null,
            object: null,
            push: null,
            string: null,
            "true": false,
            undefined: false,
            value: null
        };
    }
    function noop() {}
    function releaseArray(array) {
        array.length = 0;
        maxPoolSize > arrayPool.length && arrayPool.push(array);
    }
    function releaseObject(object) {
        var cache = object.cache;
        cache && releaseObject(cache);
        object.array = object.cache = object.criteria = object.object = object.number = object.string = object.value = null;
        maxPoolSize > objectPool.length && objectPool.push(object);
    }
    function slice(array, start, end) {
        start || (start = 0);
        "undefined" == typeof end && (end = array ? array.length : 0);
        var index = -1, length = end - start || 0, result = Array(0 > length ? 0 : length);
        while (length > ++index) result[index] = array[start + index];
        return result;
    }
    function runInContext(context) {
        function lodash(value) {
            return value && "object" == typeof value && !isArray(value) && hasOwnProperty.call(value, "__wrapped__") ? value : new lodashWrapper(value);
        }
        function lodashWrapper(value, chainAll) {
            this.__chain__ = !!chainAll;
            this.__wrapped__ = value;
        }
        function baseClone(value, deep, callback, stackA, stackB) {
            if (callback) {
                var result = callback(value);
                if ("undefined" != typeof result) return result;
            }
            var isObj = isObject(value);
            if (!isObj) return value;
            var className = toString.call(value);
            if (!cloneableClasses[className]) return value;
            var ctor = ctorByClass[className];
            switch (className) {
              case boolClass:
              case dateClass:
                return new ctor(+value);

              case numberClass:
              case stringClass:
                return new ctor(value);

              case regexpClass:
                result = ctor(value.source, reFlags.exec(value));
                result.lastIndex = value.lastIndex;
                return result;
            }
            var isArr = isArray(value);
            if (deep) {
                var initedStack = !stackA;
                stackA || (stackA = getArray());
                stackB || (stackB = getArray());
                var length = stackA.length;
                while (length--) if (stackA[length] == value) return stackB[length];
                result = isArr ? ctor(value.length) : {};
            } else result = isArr ? slice(value) : assign({}, value);
            if (isArr) {
                hasOwnProperty.call(value, "index") && (result.index = value.index);
                hasOwnProperty.call(value, "input") && (result.input = value.input);
            }
            if (!deep) return result;
            stackA.push(value);
            stackB.push(result);
            (isArr ? forEach : forOwn)(value, function(objValue, key) {
                result[key] = baseClone(objValue, deep, callback, stackA, stackB);
            });
            if (initedStack) {
                releaseArray(stackA);
                releaseArray(stackB);
            }
            return result;
        }
        function baseCreateCallback(func, thisArg, argCount) {
            if ("function" != typeof func) return identity;
            if ("undefined" == typeof thisArg) return func;
            var bindData = func.__bindData__ || support.funcNames && !func.name;
            if ("undefined" == typeof bindData) {
                var source = reThis && fnToString.call(func);
                support.funcNames || !source || reFuncName.test(source) || (bindData = true);
                if (support.funcNames || !bindData) {
                    bindData = !support.funcDecomp || reThis.test(source);
                    setBindData(func, bindData);
                }
            }
            if (true !== bindData && bindData && 1 & bindData[1]) return func;
            switch (argCount) {
              case 1:
                return function(value) {
                    return func.call(thisArg, value);
                };

              case 2:
                return function(a, b) {
                    return func.call(thisArg, a, b);
                };

              case 3:
                return function(value, index, collection) {
                    return func.call(thisArg, value, index, collection);
                };

              case 4:
                return function(accumulator, value, index, collection) {
                    return func.call(thisArg, accumulator, value, index, collection);
                };
            }
            return bind(func, thisArg);
        }
        function baseFlatten(array, isShallow, isArgArrays, fromIndex) {
            var index = (fromIndex || 0) - 1, length = array ? array.length : 0, result = [];
            while (length > ++index) {
                var value = array[index];
                if (value && "object" == typeof value && "number" == typeof value.length && (isArray(value) || isArguments(value))) {
                    isShallow || (value = baseFlatten(value, isShallow, isArgArrays));
                    var valIndex = -1, valLength = value.length, resIndex = result.length;
                    result.length += valLength;
                    while (valLength > ++valIndex) result[resIndex++] = value[valIndex];
                } else isArgArrays || result.push(value);
            }
            return result;
        }
        function baseIsEqual(a, b, callback, isWhere, stackA, stackB) {
            if (callback) {
                var result = callback(a, b);
                if ("undefined" != typeof result) return !!result;
            }
            if (a === b) return 0 !== a || 1 / a == 1 / b;
            var type = typeof a, otherType = typeof b;
            if (!(a !== a || a && objectTypes[type] || b && objectTypes[otherType])) return false;
            if (null == a || null == b) return a === b;
            var className = toString.call(a), otherClass = toString.call(b);
            className == argsClass && (className = objectClass);
            otherClass == argsClass && (otherClass = objectClass);
            if (className != otherClass) return false;
            switch (className) {
              case boolClass:
              case dateClass:
                return +a == +b;

              case numberClass:
                return a != +a ? b != +b : 0 == a ? 1 / a == 1 / b : a == +b;

              case regexpClass:
              case stringClass:
                return a == String(b);
            }
            var isArr = className == arrayClass;
            if (!isArr) {
                if (hasOwnProperty.call(a, "__wrapped__ ") || hasOwnProperty.call(b, "__wrapped__")) return baseIsEqual(a.__wrapped__ || a, b.__wrapped__ || b, callback, isWhere, stackA, stackB);
                if (className != objectClass) return false;
                var ctorA = a.constructor, ctorB = b.constructor;
                if (ctorA != ctorB && !(isFunction(ctorA) && ctorA instanceof ctorA && isFunction(ctorB) && ctorB instanceof ctorB)) return false;
            }
            var initedStack = !stackA;
            stackA || (stackA = getArray());
            stackB || (stackB = getArray());
            var length = stackA.length;
            while (length--) if (stackA[length] == a) return stackB[length] == b;
            var size = 0;
            result = true;
            stackA.push(a);
            stackB.push(b);
            if (isArr) {
                length = a.length;
                size = b.length;
                result = size == a.length;
                if (!result && !isWhere) return result;
                while (size--) {
                    var index = length, value = b[size];
                    if (isWhere) {
                        while (index--) if (result = baseIsEqual(a[index], value, callback, isWhere, stackA, stackB)) break;
                    } else if (!(result = baseIsEqual(a[size], value, callback, isWhere, stackA, stackB))) break;
                }
                return result;
            }
            forIn(b, function(value, key, b) {
                if (hasOwnProperty.call(b, key)) {
                    size++;
                    return result = hasOwnProperty.call(a, key) && baseIsEqual(a[key], value, callback, isWhere, stackA, stackB);
                }
            });
            result && !isWhere && forIn(a, function(value, key, a) {
                if (hasOwnProperty.call(a, key)) return result = --size > -1;
            });
            if (initedStack) {
                releaseArray(stackA);
                releaseArray(stackB);
            }
            return result;
        }
        function baseMerge(object, source, callback, stackA, stackB) {
            (isArray(source) ? forEach : forOwn)(source, function(source, key) {
                var found, isArr, result = source, value = object[key];
                if (source && ((isArr = isArray(source)) || isPlainObject(source))) {
                    var stackLength = stackA.length;
                    while (stackLength--) if (found = stackA[stackLength] == source) {
                        value = stackB[stackLength];
                        break;
                    }
                    if (!found) {
                        var isShallow;
                        if (callback) {
                            result = callback(value, source);
                            (isShallow = "undefined" != typeof result) && (value = result);
                        }
                        isShallow || (value = isArr ? isArray(value) ? value : [] : isPlainObject(value) ? value : {});
                        stackA.push(source);
                        stackB.push(value);
                        isShallow || baseMerge(value, source, callback, stackA, stackB);
                    }
                } else {
                    if (callback) {
                        result = callback(value, source);
                        "undefined" == typeof result && (result = source);
                    }
                    "undefined" != typeof result && (value = result);
                }
                object[key] = value;
            });
        }
        function baseUniq(array, isSorted, callback) {
            var index = -1, indexOf = getIndexOf(), length = array ? array.length : 0, result = [];
            var isLarge = !isSorted && length >= largeArraySize && indexOf === baseIndexOf, seen = callback || isLarge ? getArray() : result;
            if (isLarge) {
                var cache = createCache(seen);
                if (cache) {
                    indexOf = cacheIndexOf;
                    seen = cache;
                } else {
                    isLarge = false;
                    seen = callback ? seen : (releaseArray(seen), result);
                }
            }
            while (length > ++index) {
                var value = array[index], computed = callback ? callback(value, index, array) : value;
                if (isSorted ? !index || seen[seen.length - 1] !== computed : 0 > indexOf(seen, computed)) {
                    (callback || isLarge) && seen.push(computed);
                    result.push(value);
                }
            }
            if (isLarge) {
                releaseArray(seen.array);
                releaseObject(seen);
            } else callback && releaseArray(seen);
            return result;
        }
        function createAggregator(setter) {
            return function(collection, callback, thisArg) {
                var result = {};
                callback = lodash.createCallback(callback, thisArg, 3);
                var index = -1, length = collection ? collection.length : 0;
                if ("number" == typeof length) while (length > ++index) {
                    var value = collection[index];
                    setter(result, value, callback(value, index, collection), collection);
                } else forOwn(collection, function(value, key, collection) {
                    setter(result, value, callback(value, key, collection), collection);
                });
                return result;
            };
        }
        function createBound(func, bitmask, partialArgs, partialRightArgs, thisArg, arity) {
            var isBind = 1 & bitmask, isBindKey = 2 & bitmask, isCurry = 4 & bitmask, isCurryBound = 8 & bitmask, isPartial = 16 & bitmask, isPartialRight = 32 & bitmask, key = func;
            if (!isBindKey && !isFunction(func)) throw new TypeError();
            if (isPartial && !partialArgs.length) {
                bitmask &= -17;
                isPartial = partialArgs = false;
            }
            if (isPartialRight && !partialRightArgs.length) {
                bitmask &= -33;
                isPartialRight = partialRightArgs = false;
            }
            var bindData = func && func.__bindData__;
            if (bindData) {
                !isBind || 1 & bindData[1] || (bindData[4] = thisArg);
                !isBind && 1 & bindData[1] && (bitmask |= 8);
                !isCurry || 4 & bindData[1] || (bindData[5] = arity);
                isPartial && push.apply(bindData[2] || (bindData[2] = []), partialArgs);
                isPartialRight && push.apply(bindData[3] || (bindData[3] = []), partialRightArgs);
                bindData[1] |= bitmask;
                return createBound.apply(null, bindData);
            }
            if (!isBind || isBindKey || isCurry || isPartialRight || !(support.fastBind || nativeBind && isPartial)) bound = function() {
                var args = arguments, thisBinding = isBind ? thisArg : this;
                if (isCurry || isPartial || isPartialRight) {
                    args = nativeSlice.call(args);
                    isPartial && unshift.apply(args, partialArgs);
                    isPartialRight && push.apply(args, partialRightArgs);
                    if (isCurry && arity > args.length) {
                        bitmask |= 16;
                        return createBound(func, isCurryBound ? bitmask : -4 & bitmask, args, null, thisArg, arity);
                    }
                }
                isBindKey && (func = thisBinding[key]);
                if (this instanceof bound) {
                    thisBinding = createObject(func.prototype);
                    var result = func.apply(thisBinding, args);
                    return isObject(result) ? result : thisBinding;
                }
                return func.apply(thisBinding, args);
            }; else {
                if (isPartial) {
                    var args = [ thisArg ];
                    push.apply(args, partialArgs);
                }
                var bound = isPartial ? nativeBind.apply(func, args) : nativeBind.call(func, thisArg);
            }
            setBindData(bound, nativeSlice.call(arguments));
            return bound;
        }
        function createObject(prototype) {
            return isObject(prototype) ? nativeCreate(prototype) : {};
        }
        function escapeHtmlChar(match) {
            return htmlEscapes[match];
        }
        function getIndexOf() {
            var result = (result = lodash.indexOf) === indexOf ? baseIndexOf : result;
            return result;
        }
        function shimIsPlainObject(value) {
            var ctor, result;
            if (!(value && toString.call(value) == objectClass && (ctor = value.constructor, 
            !isFunction(ctor) || ctor instanceof ctor))) return false;
            forIn(value, function(value, key) {
                result = key;
            });
            return "undefined" == typeof result || hasOwnProperty.call(value, result);
        }
        function unescapeHtmlChar(match) {
            return htmlUnescapes[match];
        }
        function isArguments(value) {
            return value && "object" == typeof value && "number" == typeof value.length && toString.call(value) == argsClass || false;
        }
        function clone(value, deep, callback, thisArg) {
            if ("boolean" != typeof deep && null != deep) {
                thisArg = callback;
                callback = deep;
                deep = false;
            }
            return baseClone(value, deep, "function" == typeof callback && baseCreateCallback(callback, thisArg, 1));
        }
        function cloneDeep(value, callback, thisArg) {
            return baseClone(value, true, "function" == typeof callback && baseCreateCallback(callback, thisArg, 1));
        }
        function findKey(object, callback, thisArg) {
            var result;
            callback = lodash.createCallback(callback, thisArg, 3);
            forOwn(object, function(value, key, object) {
                if (callback(value, key, object)) {
                    result = key;
                    return false;
                }
            });
            return result;
        }
        function findLastKey(object, callback, thisArg) {
            var result;
            callback = lodash.createCallback(callback, thisArg, 3);
            forOwnRight(object, function(value, key, object) {
                if (callback(value, key, object)) {
                    result = key;
                    return false;
                }
            });
            return result;
        }
        function forInRight(object, callback, thisArg) {
            var pairs = [];
            forIn(object, function(value, key) {
                pairs.push(key, value);
            });
            var length = pairs.length;
            callback = baseCreateCallback(callback, thisArg, 3);
            while (length--) if (false === callback(pairs[length--], pairs[length], object)) break;
            return object;
        }
        function forOwnRight(object, callback, thisArg) {
            var props = keys(object), length = props.length;
            callback = baseCreateCallback(callback, thisArg, 3);
            while (length--) {
                var key = props[length];
                if (false === callback(object[key], key, object)) break;
            }
            return object;
        }
        function functions(object) {
            var result = [];
            forIn(object, function(value, key) {
                isFunction(value) && result.push(key);
            });
            return result.sort();
        }
        function has(object, property) {
            return object ? hasOwnProperty.call(object, property) : false;
        }
        function invert(object) {
            var index = -1, props = keys(object), length = props.length, result = {};
            while (length > ++index) {
                var key = props[index];
                result[object[key]] = key;
            }
            return result;
        }
        function isBoolean(value) {
            return true === value || false === value || toString.call(value) == boolClass;
        }
        function isDate(value) {
            return value ? "object" == typeof value && toString.call(value) == dateClass : false;
        }
        function isElement(value) {
            return value ? 1 === value.nodeType : false;
        }
        function isEmpty(value) {
            var result = true;
            if (!value) return result;
            var className = toString.call(value), length = value.length;
            if (className == arrayClass || className == stringClass || className == argsClass || className == objectClass && "number" == typeof length && isFunction(value.splice)) return !length;
            forOwn(value, function() {
                return result = false;
            });
            return result;
        }
        function isEqual(a, b, callback, thisArg) {
            return baseIsEqual(a, b, "function" == typeof callback && baseCreateCallback(callback, thisArg, 2));
        }
        function isFinite(value) {
            return nativeIsFinite(value) && !nativeIsNaN(parseFloat(value));
        }
        function isFunction(value) {
            return "function" == typeof value;
        }
        function isObject(value) {
            return !!(value && objectTypes[typeof value]);
        }
        function isNaN(value) {
            return isNumber(value) && value != +value;
        }
        function isNull(value) {
            return null === value;
        }
        function isNumber(value) {
            return "number" == typeof value || toString.call(value) == numberClass;
        }
        function isRegExp(value) {
            return value ? "object" == typeof value && toString.call(value) == regexpClass : false;
        }
        function isString(value) {
            return "string" == typeof value || toString.call(value) == stringClass;
        }
        function isUndefined(value) {
            return "undefined" == typeof value;
        }
        function merge(object) {
            var args = arguments, length = 2;
            if (!isObject(object)) return object;
            "number" != typeof args[2] && (length = args.length);
            if (length > 3 && "function" == typeof args[length - 2]) var callback = baseCreateCallback(args[--length - 1], args[length--], 2); else length > 2 && "function" == typeof args[length - 1] && (callback = args[--length]);
            var sources = nativeSlice.call(arguments, 1, length), index = -1, stackA = getArray(), stackB = getArray();
            while (length > ++index) baseMerge(object, sources[index], callback, stackA, stackB);
            releaseArray(stackA);
            releaseArray(stackB);
            return object;
        }
        function omit(object, callback, thisArg) {
            var indexOf = getIndexOf(), isFunc = "function" == typeof callback, result = {};
            if (isFunc) callback = lodash.createCallback(callback, thisArg, 3); else var props = baseFlatten(arguments, true, false, 1);
            forIn(object, function(value, key, object) {
                (isFunc ? !callback(value, key, object) : 0 > indexOf(props, key)) && (result[key] = value);
            });
            return result;
        }
        function pairs(object) {
            var index = -1, props = keys(object), length = props.length, result = Array(length);
            while (length > ++index) {
                var key = props[index];
                result[index] = [ key, object[key] ];
            }
            return result;
        }
        function pick(object, callback, thisArg) {
            var result = {};
            if ("function" != typeof callback) {
                var index = -1, props = baseFlatten(arguments, true, false, 1), length = isObject(object) ? props.length : 0;
                while (length > ++index) {
                    var key = props[index];
                    key in object && (result[key] = object[key]);
                }
            } else {
                callback = lodash.createCallback(callback, thisArg, 3);
                forIn(object, function(value, key, object) {
                    callback(value, key, object) && (result[key] = value);
                });
            }
            return result;
        }
        function transform(object, callback, accumulator, thisArg) {
            var isArr = isArray(object);
            callback = baseCreateCallback(callback, thisArg, 4);
            if (null == accumulator) if (isArr) accumulator = []; else {
                var ctor = object && object.constructor, proto = ctor && ctor.prototype;
                accumulator = createObject(proto);
            }
            (isArr ? forEach : forOwn)(object, function(value, index, object) {
                return callback(accumulator, value, index, object);
            });
            return accumulator;
        }
        function values(object) {
            var index = -1, props = keys(object), length = props.length, result = Array(length);
            while (length > ++index) result[index] = object[props[index]];
            return result;
        }
        function at(collection) {
            var args = arguments, index = -1, props = baseFlatten(args, true, false, 1), length = args[2] && args[2][args[1]] === collection ? 1 : props.length, result = Array(length);
            while (length > ++index) result[index] = collection[props[index]];
            return result;
        }
        function contains(collection, target, fromIndex) {
            var index = -1, indexOf = getIndexOf(), length = collection ? collection.length : 0, result = false;
            fromIndex = (0 > fromIndex ? nativeMax(0, length + fromIndex) : fromIndex) || 0;
            isArray(collection) ? result = indexOf(collection, target, fromIndex) > -1 : "number" == typeof length ? result = (isString(collection) ? collection.indexOf(target, fromIndex) : indexOf(collection, target, fromIndex)) > -1 : forOwn(collection, function(value) {
                if (++index >= fromIndex) return !(result = value === target);
            });
            return result;
        }
        function every(collection, callback, thisArg) {
            var result = true;
            callback = lodash.createCallback(callback, thisArg, 3);
            var index = -1, length = collection ? collection.length : 0;
            if ("number" == typeof length) {
                while (length > ++index) if (!(result = !!callback(collection[index], index, collection))) break;
            } else forOwn(collection, function(value, index, collection) {
                return result = !!callback(value, index, collection);
            });
            return result;
        }
        function filter(collection, callback, thisArg) {
            var result = [];
            callback = lodash.createCallback(callback, thisArg, 3);
            var index = -1, length = collection ? collection.length : 0;
            if ("number" == typeof length) while (length > ++index) {
                var value = collection[index];
                callback(value, index, collection) && result.push(value);
            } else forOwn(collection, function(value, index, collection) {
                callback(value, index, collection) && result.push(value);
            });
            return result;
        }
        function find(collection, callback, thisArg) {
            callback = lodash.createCallback(callback, thisArg, 3);
            var index = -1, length = collection ? collection.length : 0;
            if ("number" != typeof length) {
                var result;
                forOwn(collection, function(value, index, collection) {
                    if (callback(value, index, collection)) {
                        result = value;
                        return false;
                    }
                });
                return result;
            }
            while (length > ++index) {
                var value = collection[index];
                if (callback(value, index, collection)) return value;
            }
        }
        function findLast(collection, callback, thisArg) {
            var result;
            callback = lodash.createCallback(callback, thisArg, 3);
            forEachRight(collection, function(value, index, collection) {
                if (callback(value, index, collection)) {
                    result = value;
                    return false;
                }
            });
            return result;
        }
        function forEach(collection, callback, thisArg) {
            var index = -1, length = collection ? collection.length : 0;
            callback = callback && "undefined" == typeof thisArg ? callback : baseCreateCallback(callback, thisArg, 3);
            if ("number" == typeof length) {
                while (length > ++index) if (false === callback(collection[index], index, collection)) break;
            } else forOwn(collection, callback);
            return collection;
        }
        function forEachRight(collection, callback, thisArg) {
            var length = collection ? collection.length : 0;
            callback = callback && "undefined" == typeof thisArg ? callback : baseCreateCallback(callback, thisArg, 3);
            if ("number" == typeof length) {
                while (length--) if (false === callback(collection[length], length, collection)) break;
            } else {
                var props = keys(collection);
                length = props.length;
                forOwn(collection, function(value, key, collection) {
                    key = props ? props[--length] : --length;
                    return callback(collection[key], key, collection);
                });
            }
            return collection;
        }
        function invoke(collection, methodName) {
            var args = nativeSlice.call(arguments, 2), index = -1, isFunc = "function" == typeof methodName, length = collection ? collection.length : 0, result = Array("number" == typeof length ? length : 0);
            forEach(collection, function(value) {
                result[++index] = (isFunc ? methodName : value[methodName]).apply(value, args);
            });
            return result;
        }
        function map(collection, callback, thisArg) {
            var index = -1, length = collection ? collection.length : 0;
            callback = lodash.createCallback(callback, thisArg, 3);
            if ("number" == typeof length) {
                var result = Array(length);
                while (length > ++index) result[index] = callback(collection[index], index, collection);
            } else {
                result = [];
                forOwn(collection, function(value, key, collection) {
                    result[++index] = callback(value, key, collection);
                });
            }
            return result;
        }
        function max(collection, callback, thisArg) {
            var computed = -1/0, result = computed;
            if (!callback && isArray(collection)) {
                var index = -1, length = collection.length;
                while (length > ++index) {
                    var value = collection[index];
                    value > result && (result = value);
                }
            } else {
                callback = !callback && isString(collection) ? charAtCallback : lodash.createCallback(callback, thisArg, 3);
                forEach(collection, function(value, index, collection) {
                    var current = callback(value, index, collection);
                    if (current > computed) {
                        computed = current;
                        result = value;
                    }
                });
            }
            return result;
        }
        function min(collection, callback, thisArg) {
            var computed = 1/0, result = computed;
            if (!callback && isArray(collection)) {
                var index = -1, length = collection.length;
                while (length > ++index) {
                    var value = collection[index];
                    result > value && (result = value);
                }
            } else {
                callback = !callback && isString(collection) ? charAtCallback : lodash.createCallback(callback, thisArg, 3);
                forEach(collection, function(value, index, collection) {
                    var current = callback(value, index, collection);
                    if (computed > current) {
                        computed = current;
                        result = value;
                    }
                });
            }
            return result;
        }
        function pluck(collection, property) {
            var index = -1, length = collection ? collection.length : 0;
            if ("number" == typeof length) {
                var result = Array(length);
                while (length > ++index) result[index] = collection[index][property];
            }
            return result || map(collection, property);
        }
        function reduce(collection, callback, accumulator, thisArg) {
            if (!collection) return accumulator;
            var noaccum = 3 > arguments.length;
            callback = baseCreateCallback(callback, thisArg, 4);
            var index = -1, length = collection.length;
            if ("number" == typeof length) {
                noaccum && (accumulator = collection[++index]);
                while (length > ++index) accumulator = callback(accumulator, collection[index], index, collection);
            } else forOwn(collection, function(value, index, collection) {
                accumulator = noaccum ? (noaccum = false, value) : callback(accumulator, value, index, collection);
            });
            return accumulator;
        }
        function reduceRight(collection, callback, accumulator, thisArg) {
            var noaccum = 3 > arguments.length;
            callback = baseCreateCallback(callback, thisArg, 4);
            forEachRight(collection, function(value, index, collection) {
                accumulator = noaccum ? (noaccum = false, value) : callback(accumulator, value, index, collection);
            });
            return accumulator;
        }
        function reject(collection, callback, thisArg) {
            callback = lodash.createCallback(callback, thisArg, 3);
            return filter(collection, function(value, index, collection) {
                return !callback(value, index, collection);
            });
        }
        function sample(collection, n, guard) {
            var length = collection ? collection.length : 0;
            "number" != typeof length && (collection = values(collection));
            if (null == n || guard) return collection ? collection[random(length - 1)] : undefined;
            var result = shuffle(collection);
            result.length = nativeMin(nativeMax(0, n), result.length);
            return result;
        }
        function shuffle(collection) {
            var index = -1, length = collection ? collection.length : 0, result = Array("number" == typeof length ? length : 0);
            forEach(collection, function(value) {
                var rand = random(++index);
                result[index] = result[rand];
                result[rand] = value;
            });
            return result;
        }
        function size(collection) {
            var length = collection ? collection.length : 0;
            return "number" == typeof length ? length : keys(collection).length;
        }
        function some(collection, callback, thisArg) {
            var result;
            callback = lodash.createCallback(callback, thisArg, 3);
            var index = -1, length = collection ? collection.length : 0;
            if ("number" == typeof length) {
                while (length > ++index) if (result = callback(collection[index], index, collection)) break;
            } else forOwn(collection, function(value, index, collection) {
                return !(result = callback(value, index, collection));
            });
            return !!result;
        }
        function sortBy(collection, callback, thisArg) {
            var index = -1, length = collection ? collection.length : 0, result = Array("number" == typeof length ? length : 0);
            callback = lodash.createCallback(callback, thisArg, 3);
            forEach(collection, function(value, key, collection) {
                var object = result[++index] = getObject();
                object.criteria = callback(value, key, collection);
                object.index = index;
                object.value = value;
            });
            length = result.length;
            result.sort(compareAscending);
            while (length--) {
                var object = result[length];
                result[length] = object.value;
                releaseObject(object);
            }
            return result;
        }
        function toArray(collection) {
            if (collection && "number" == typeof collection.length) return slice(collection);
            return values(collection);
        }
        function compact(array) {
            var index = -1, length = array ? array.length : 0, result = [];
            while (length > ++index) {
                var value = array[index];
                value && result.push(value);
            }
            return result;
        }
        function difference(array) {
            var index = -1, indexOf = getIndexOf(), length = array ? array.length : 0, seen = baseFlatten(arguments, true, true, 1), result = [];
            var isLarge = length >= largeArraySize && indexOf === baseIndexOf;
            if (isLarge) {
                var cache = createCache(seen);
                if (cache) {
                    indexOf = cacheIndexOf;
                    seen = cache;
                } else isLarge = false;
            }
            while (length > ++index) {
                var value = array[index];
                0 > indexOf(seen, value) && result.push(value);
            }
            isLarge && releaseObject(seen);
            return result;
        }
        function findIndex(array, callback, thisArg) {
            var index = -1, length = array ? array.length : 0;
            callback = lodash.createCallback(callback, thisArg, 3);
            while (length > ++index) if (callback(array[index], index, array)) return index;
            return -1;
        }
        function findLastIndex(array, callback, thisArg) {
            var length = array ? array.length : 0;
            callback = lodash.createCallback(callback, thisArg, 3);
            while (length--) if (callback(array[length], length, array)) return length;
            return -1;
        }
        function first(array, callback, thisArg) {
            var n = 0, length = array ? array.length : 0;
            if ("number" != typeof callback && null != callback) {
                var index = -1;
                callback = lodash.createCallback(callback, thisArg, 3);
                while (length > ++index && callback(array[index], index, array)) n++;
            } else {
                n = callback;
                if (null == n || thisArg) return array ? array[0] : undefined;
            }
            return slice(array, 0, nativeMin(nativeMax(0, n), length));
        }
        function flatten(array, isShallow, callback, thisArg) {
            if ("boolean" != typeof isShallow && null != isShallow) {
                thisArg = callback;
                callback = thisArg && thisArg[isShallow] === array ? null : isShallow;
                isShallow = false;
            }
            null != callback && (array = map(array, callback, thisArg));
            return baseFlatten(array, isShallow);
        }
        function indexOf(array, value, fromIndex) {
            if ("number" == typeof fromIndex) {
                var length = array ? array.length : 0;
                fromIndex = 0 > fromIndex ? nativeMax(0, length + fromIndex) : fromIndex || 0;
            } else if (fromIndex) {
                var index = sortedIndex(array, value);
                return array[index] === value ? index : -1;
            }
            return baseIndexOf(array, value, fromIndex);
        }
        function initial(array, callback, thisArg) {
            var n = 0, length = array ? array.length : 0;
            if ("number" != typeof callback && null != callback) {
                var index = length;
                callback = lodash.createCallback(callback, thisArg, 3);
                while (index-- && callback(array[index], index, array)) n++;
            } else n = null == callback || thisArg ? 1 : callback || n;
            return slice(array, 0, nativeMin(nativeMax(0, length - n), length));
        }
        function intersection(array) {
            var args = arguments, argsLength = args.length, argsIndex = -1, caches = getArray(), index = -1, indexOf = getIndexOf(), length = array ? array.length : 0, result = [], seen = getArray();
            while (argsLength > ++argsIndex) {
                var value = args[argsIndex];
                caches[argsIndex] = indexOf === baseIndexOf && (value ? value.length : 0) >= largeArraySize && createCache(argsIndex ? args[argsIndex] : seen);
            }
            outer: while (length > ++index) {
                var cache = caches[0];
                value = array[index];
                if (0 > (cache ? cacheIndexOf(cache, value) : indexOf(seen, value))) {
                    argsIndex = argsLength;
                    (cache || seen).push(value);
                    while (--argsIndex) {
                        cache = caches[argsIndex];
                        if (0 > (cache ? cacheIndexOf(cache, value) : indexOf(args[argsIndex], value))) continue outer;
                    }
                    result.push(value);
                }
            }
            while (argsLength--) {
                cache = caches[argsLength];
                cache && releaseObject(cache);
            }
            releaseArray(caches);
            releaseArray(seen);
            return result;
        }
        function last(array, callback, thisArg) {
            var n = 0, length = array ? array.length : 0;
            if ("number" != typeof callback && null != callback) {
                var index = length;
                callback = lodash.createCallback(callback, thisArg, 3);
                while (index-- && callback(array[index], index, array)) n++;
            } else {
                n = callback;
                if (null == n || thisArg) return array ? array[length - 1] : undefined;
            }
            return slice(array, nativeMax(0, length - n));
        }
        function lastIndexOf(array, value, fromIndex) {
            var index = array ? array.length : 0;
            "number" == typeof fromIndex && (index = (0 > fromIndex ? nativeMax(0, index + fromIndex) : nativeMin(fromIndex, index - 1)) + 1);
            while (index--) if (array[index] === value) return index;
            return -1;
        }
        function pull(array) {
            var args = arguments, argsIndex = 0, argsLength = args.length, length = array ? array.length : 0;
            while (argsLength > ++argsIndex) {
                var index = -1, value = args[argsIndex];
                while (length > ++index) if (array[index] === value) {
                    splice.call(array, index--, 1);
                    length--;
                }
            }
            return array;
        }
        function range(start, end, step) {
            start = +start || 0;
            step = "number" == typeof step ? step : +step || 1;
            if (null == end) {
                end = start;
                start = 0;
            }
            var index = -1, length = nativeMax(0, ceil((end - start) / (step || 1))), result = Array(length);
            while (length > ++index) {
                result[index] = start;
                start += step;
            }
            return result;
        }
        function remove(array, callback, thisArg) {
            var index = -1, length = array ? array.length : 0, result = [];
            callback = lodash.createCallback(callback, thisArg, 3);
            while (length > ++index) {
                var value = array[index];
                if (callback(value, index, array)) {
                    result.push(value);
                    splice.call(array, index--, 1);
                    length--;
                }
            }
            return result;
        }
        function rest(array, callback, thisArg) {
            if ("number" != typeof callback && null != callback) {
                var n = 0, index = -1, length = array ? array.length : 0;
                callback = lodash.createCallback(callback, thisArg, 3);
                while (length > ++index && callback(array[index], index, array)) n++;
            } else n = null == callback || thisArg ? 1 : nativeMax(0, callback);
            return slice(array, n);
        }
        function sortedIndex(array, value, callback, thisArg) {
            var low = 0, high = array ? array.length : low;
            callback = callback ? lodash.createCallback(callback, thisArg, 1) : identity;
            value = callback(value);
            while (high > low) {
                var mid = low + high >>> 1;
                value > callback(array[mid]) ? low = mid + 1 : high = mid;
            }
            return low;
        }
        function union() {
            return baseUniq(baseFlatten(arguments, true, true));
        }
        function uniq(array, isSorted, callback, thisArg) {
            if ("boolean" != typeof isSorted && null != isSorted) {
                thisArg = callback;
                callback = thisArg && thisArg[isSorted] === array ? null : isSorted;
                isSorted = false;
            }
            null != callback && (callback = lodash.createCallback(callback, thisArg, 3));
            return baseUniq(array, isSorted, callback);
        }
        function without(array) {
            return difference(array, nativeSlice.call(arguments, 1));
        }
        function zip() {
            var array = arguments.length > 1 ? arguments : arguments[0], index = -1, length = array ? max(pluck(array, "length")) : 0, result = Array(0 > length ? 0 : length);
            while (length > ++index) result[index] = pluck(array, index);
            return result;
        }
        function zipObject(keys, values) {
            var index = -1, length = keys ? keys.length : 0, result = {};
            while (length > ++index) {
                var key = keys[index];
                values ? result[key] = values[index] : key && (result[key[0]] = key[1]);
            }
            return result;
        }
        function after(n, func) {
            if (!isFunction(func)) throw new TypeError();
            return function() {
                if (1 > --n) return func.apply(this, arguments);
            };
        }
        function bind(func, thisArg) {
            return arguments.length > 2 ? createBound(func, 17, nativeSlice.call(arguments, 2), null, thisArg) : createBound(func, 1, null, null, thisArg);
        }
        function bindAll(object) {
            var funcs = arguments.length > 1 ? baseFlatten(arguments, true, false, 1) : functions(object), index = -1, length = funcs.length;
            while (length > ++index) {
                var key = funcs[index];
                object[key] = createBound(object[key], 1, null, null, object);
            }
            return object;
        }
        function bindKey(object, key) {
            return arguments.length > 2 ? createBound(key, 19, nativeSlice.call(arguments, 2), null, object) : createBound(key, 3, null, null, object);
        }
        function compose() {
            var funcs = arguments, length = funcs.length;
            while (length--) if (!isFunction(funcs[length])) throw new TypeError();
            return function() {
                var args = arguments, length = funcs.length;
                while (length--) args = [ funcs[length].apply(this, args) ];
                return args[0];
            };
        }
        function createCallback(func, thisArg, argCount) {
            var type = typeof func;
            if (null == func || "function" == type) return baseCreateCallback(func, thisArg, argCount);
            if ("object" != type) return function(object) {
                return object[func];
            };
            var props = keys(func), key = props[0], a = func[key];
            if (1 == props.length && a === a && !isObject(a)) return function(object) {
                var b = object[key];
                return a === b && (0 !== a || 1 / a == 1 / b);
            };
            return function(object) {
                var length = props.length, result = false;
                while (length--) if (!(result = baseIsEqual(object[props[length]], func[props[length]], null, true))) break;
                return result;
            };
        }
        function curry(func, arity) {
            arity = "number" == typeof arity ? arity : +arity || func.length;
            return createBound(func, 4, null, null, null, arity);
        }
        function debounce(func, wait, options) {
            var args, maxTimeoutId, result, stamp, thisArg, timeoutId, trailingCall, lastCalled = 0, maxWait = false, trailing = true;
            if (!isFunction(func)) throw new TypeError();
            wait = nativeMax(0, wait) || 0;
            if (true === options) {
                var leading = true;
                trailing = false;
            } else if (isObject(options)) {
                leading = options.leading;
                maxWait = "maxWait" in options && (nativeMax(wait, options.maxWait) || 0);
                trailing = "trailing" in options ? options.trailing : trailing;
            }
            var delayed = function() {
                var remaining = wait - (now() - stamp);
                if (0 >= remaining) {
                    maxTimeoutId && clearTimeout(maxTimeoutId);
                    var isCalled = trailingCall;
                    maxTimeoutId = timeoutId = trailingCall = undefined;
                    if (isCalled) {
                        lastCalled = now();
                        result = func.apply(thisArg, args);
                    }
                } else timeoutId = setTimeout(delayed, remaining);
            };
            var maxDelayed = function() {
                timeoutId && clearTimeout(timeoutId);
                maxTimeoutId = timeoutId = trailingCall = undefined;
                if (trailing || maxWait !== wait) {
                    lastCalled = now();
                    result = func.apply(thisArg, args);
                }
            };
            return function() {
                args = arguments;
                stamp = now();
                thisArg = this;
                trailingCall = trailing && (timeoutId || !leading);
                if (false === maxWait) var leadingCall = leading && !timeoutId; else {
                    maxTimeoutId || leading || (lastCalled = stamp);
                    var remaining = maxWait - (stamp - lastCalled);
                    if (0 >= remaining) {
                        maxTimeoutId && (maxTimeoutId = clearTimeout(maxTimeoutId));
                        lastCalled = stamp;
                        result = func.apply(thisArg, args);
                    } else maxTimeoutId || (maxTimeoutId = setTimeout(maxDelayed, remaining));
                }
                timeoutId || wait === maxWait || (timeoutId = setTimeout(delayed, wait));
                leadingCall && (result = func.apply(thisArg, args));
                return result;
            };
        }
        function defer(func) {
            if (!isFunction(func)) throw new TypeError();
            var args = nativeSlice.call(arguments, 1);
            return setTimeout(function() {
                func.apply(undefined, args);
            }, 1);
        }
        function delay(func, wait) {
            if (!isFunction(func)) throw new TypeError();
            var args = nativeSlice.call(arguments, 2);
            return setTimeout(function() {
                func.apply(undefined, args);
            }, wait);
        }
        function memoize(func, resolver) {
            if (!isFunction(func)) throw new TypeError();
            var memoized = function() {
                var cache = memoized.cache, key = resolver ? resolver.apply(this, arguments) : keyPrefix + arguments[0];
                return hasOwnProperty.call(cache, key) ? cache[key] : cache[key] = func.apply(this, arguments);
            };
            memoized.cache = {};
            return memoized;
        }
        function once(func) {
            var ran, result;
            if (!isFunction(func)) throw new TypeError();
            return function() {
                if (ran) return result;
                ran = true;
                result = func.apply(this, arguments);
                func = null;
                return result;
            };
        }
        function partial(func) {
            return createBound(func, 16, nativeSlice.call(arguments, 1));
        }
        function partialRight(func) {
            return createBound(func, 32, null, nativeSlice.call(arguments, 1));
        }
        function throttle(func, wait, options) {
            var leading = true, trailing = true;
            if (!isFunction(func)) throw new TypeError();
            if (false === options) leading = false; else if (isObject(options)) {
                leading = "leading" in options ? options.leading : leading;
                trailing = "trailing" in options ? options.trailing : trailing;
            }
            debounceOptions.leading = leading;
            debounceOptions.maxWait = wait;
            debounceOptions.trailing = trailing;
            var result = debounce(func, wait, debounceOptions);
            return result;
        }
        function wrap(value, wrapper) {
            if (!isFunction(wrapper)) throw new TypeError();
            return function() {
                var args = [ value ];
                push.apply(args, arguments);
                return wrapper.apply(this, args);
            };
        }
        function escape(string) {
            return null == string ? "" : String(string).replace(reUnescapedHtml, escapeHtmlChar);
        }
        function identity(value) {
            return value;
        }
        function mixin(object, source) {
            var ctor = object, isFunc = !source || isFunction(ctor);
            if (!source) {
                ctor = lodashWrapper;
                source = object;
                object = lodash;
            }
            forEach(functions(source), function(methodName) {
                var func = object[methodName] = source[methodName];
                isFunc && (ctor.prototype[methodName] = function() {
                    var value = this.__wrapped__, args = [ value ];
                    push.apply(args, arguments);
                    var result = func.apply(object, args);
                    if (value && "object" == typeof value && value === result) return this;
                    result = new ctor(result);
                    result.__chain__ = this.__chain__;
                    return result;
                });
            });
        }
        function noConflict() {
            context._ = oldDash;
            return this;
        }
        function random(min, max, floating) {
            var noMin = null == min, noMax = null == max;
            if (null == floating) if ("boolean" == typeof min && noMax) {
                floating = min;
                min = 1;
            } else if (!noMax && "boolean" == typeof max) {
                floating = max;
                noMax = true;
            }
            noMin && noMax && (max = 1);
            min = +min || 0;
            if (noMax) {
                max = min;
                min = 0;
            } else max = +max || 0;
            var rand = nativeRandom();
            return floating || min % 1 || max % 1 ? nativeMin(min + rand * (max - min + parseFloat("1e-" + ((rand + "").length - 1))), max) : min + floor(rand * (max - min + 1));
        }
        function result(object, property) {
            if (object) {
                var value = object[property];
                return isFunction(value) ? object[property]() : value;
            }
        }
        function template(text, data, options) {
            var settings = lodash.templateSettings;
            text || (text = "");
            options = defaults({}, options, settings);
            var imports = defaults({}, options.imports, settings.imports), importsKeys = keys(imports), importsValues = values(imports);
            var isEvaluating, index = 0, interpolate = options.interpolate || reNoMatch, source = "__p += '";
            var reDelimiters = RegExp((options.escape || reNoMatch).source + "|" + interpolate.source + "|" + (interpolate === reInterpolate ? reEsTemplate : reNoMatch).source + "|" + (options.evaluate || reNoMatch).source + "|$", "g");
            text.replace(reDelimiters, function(match, escapeValue, interpolateValue, esTemplateValue, evaluateValue, offset) {
                interpolateValue || (interpolateValue = esTemplateValue);
                source += text.slice(index, offset).replace(reUnescapedString, escapeStringChar);
                escapeValue && (source += "' +\n__e(" + escapeValue + ") +\n'");
                if (evaluateValue) {
                    isEvaluating = true;
                    source += "';\n" + evaluateValue + ";\n__p += '";
                }
                interpolateValue && (source += "' +\n((__t = (" + interpolateValue + ")) == null ? '' : __t) +\n'");
                index = offset + match.length;
                return match;
            });
            source += "';\n";
            var variable = options.variable, hasVariable = variable;
            if (!hasVariable) {
                variable = "obj";
                source = "with (" + variable + ") {\n" + source + "\n}\n";
            }
            source = (isEvaluating ? source.replace(reEmptyStringLeading, "") : source).replace(reEmptyStringMiddle, "$1").replace(reEmptyStringTrailing, "$1;");
            source = "function(" + variable + ") {\n" + (hasVariable ? "" : variable + " || (" + variable + " = {});\n") + "var __t, __p = '', __e = _.escape" + (isEvaluating ? ", __j = Array.prototype.join;\nfunction print() { __p += __j.call(arguments, '') }\n" : ";\n") + source + "return __p\n}";
            var sourceURL = "\n/*\n//# sourceURL=" + (options.sourceURL || "/lodash/template/source[" + templateCounter++ + "]") + "\n*/";
            try {
                var result = Function(importsKeys, "return " + source + sourceURL).apply(undefined, importsValues);
            } catch (e) {
                e.source = source;
                throw e;
            }
            if (data) return result(data);
            result.source = source;
            return result;
        }
        function times(n, callback, thisArg) {
            n = (n = +n) > -1 ? n : 0;
            var index = -1, result = Array(n);
            callback = baseCreateCallback(callback, thisArg, 1);
            while (n > ++index) result[index] = callback(index);
            return result;
        }
        function unescape(string) {
            return null == string ? "" : String(string).replace(reEscapedHtml, unescapeHtmlChar);
        }
        function uniqueId(prefix) {
            var id = ++idCounter;
            return String(null == prefix ? "" : prefix) + id;
        }
        function chain(value) {
            value = new lodashWrapper(value);
            value.__chain__ = true;
            return value;
        }
        function tap(value, interceptor) {
            interceptor(value);
            return value;
        }
        function wrapperChain() {
            this.__chain__ = true;
            return this;
        }
        function wrapperToString() {
            return String(this.__wrapped__);
        }
        function wrapperValueOf() {
            return this.__wrapped__;
        }
        context = context ? _.defaults(root.Object(), context, _.pick(root, contextProps)) : root;
        var Array = context.Array, Boolean = context.Boolean, Date = context.Date, Function = context.Function, Math = context.Math, Number = context.Number, Object = context.Object, RegExp = context.RegExp, String = context.String, TypeError = context.TypeError;
        var arrayRef = [];
        var objectProto = Object.prototype;
        var oldDash = context._;
        var reNative = RegExp("^" + String(objectProto.valueOf).replace(/[.*+?^${}()|[\]\\]/g, "\\$&").replace(/valueOf|for [^\]]+/g, ".+?") + "$");
        var ceil = Math.ceil, clearTimeout = context.clearTimeout, floor = Math.floor, fnToString = Function.prototype.toString, getPrototypeOf = reNative.test(getPrototypeOf = Object.getPrototypeOf) && getPrototypeOf, hasOwnProperty = objectProto.hasOwnProperty, now = reNative.test(now = Date.now) && now || function() {
            return +new Date();
        }, push = arrayRef.push, setImmediate = context.setImmediate, setTimeout = context.setTimeout, splice = arrayRef.splice, toString = objectProto.toString, unshift = arrayRef.unshift;
        var defineProperty = function() {
            try {
                var o = {}, func = reNative.test(func = Object.defineProperty) && func, result = func(o, o, o) && func;
            } catch (e) {}
            return result;
        }();
        var nativeBind = reNative.test(nativeBind = toString.bind) && nativeBind, nativeCreate = reNative.test(nativeCreate = Object.create) && nativeCreate, nativeIsArray = reNative.test(nativeIsArray = Array.isArray) && nativeIsArray, nativeIsFinite = context.isFinite, nativeIsNaN = context.isNaN, nativeKeys = reNative.test(nativeKeys = Object.keys) && nativeKeys, nativeMax = Math.max, nativeMin = Math.min, nativeParseInt = context.parseInt, nativeRandom = Math.random, nativeSlice = arrayRef.slice;
        var isIeOpera = reNative.test(context.attachEvent), isV8 = nativeBind && !/\n|true/.test(nativeBind + isIeOpera);
        var ctorByClass = {};
        ctorByClass[arrayClass] = Array;
        ctorByClass[boolClass] = Boolean;
        ctorByClass[dateClass] = Date;
        ctorByClass[funcClass] = Function;
        ctorByClass[objectClass] = Object;
        ctorByClass[numberClass] = Number;
        ctorByClass[regexpClass] = RegExp;
        ctorByClass[stringClass] = String;
        lodashWrapper.prototype = lodash.prototype;
        var support = lodash.support = {};
        support.fastBind = nativeBind && !isV8;
        support.funcDecomp = !reNative.test(context.WinRTError) && reThis.test(runInContext);
        support.funcNames = "string" == typeof Function.name;
        lodash.templateSettings = {
            escape: /<%-([\s\S]+?)%>/g,
            evaluate: /<%([\s\S]+?)%>/g,
            interpolate: reInterpolate,
            variable: "",
            imports: {
                _: lodash
            }
        };
        nativeCreate || (createObject = function(prototype) {
            if (isObject(prototype)) {
                noop.prototype = prototype;
                var result = new noop();
                noop.prototype = null;
            }
            return result || {};
        });
        var setBindData = defineProperty ? function(func, value) {
            descriptor.value = value;
            defineProperty(func, "__bindData__", descriptor);
        } : noop;
        var isArray = nativeIsArray || function(value) {
            return value && "object" == typeof value && "number" == typeof value.length && toString.call(value) == arrayClass || false;
        };
        var shimKeys = function(object) {
            var index, iterable = object, result = [];
            if (!iterable) return result;
            if (!objectTypes[typeof object]) return result;
            for (index in iterable) hasOwnProperty.call(iterable, index) && result.push(index);
            return result;
        };
        var keys = nativeKeys ? function(object) {
            if (!isObject(object)) return [];
            return nativeKeys(object);
        } : shimKeys;
        var htmlEscapes = {
            "&": "&amp;",
            "<": "&lt;",
            ">": "&gt;",
            '"': "&quot;",
            "'": "&#39;"
        };
        var htmlUnescapes = invert(htmlEscapes);
        var reEscapedHtml = RegExp("(" + keys(htmlUnescapes).join("|") + ")", "g"), reUnescapedHtml = RegExp("[" + keys(htmlEscapes).join("") + "]", "g");
        var assign = function(object, source, guard) {
            var index, iterable = object, result = iterable;
            if (!iterable) return result;
            var args = arguments, argsIndex = 0, argsLength = "number" == typeof guard ? 2 : args.length;
            if (argsLength > 3 && "function" == typeof args[argsLength - 2]) var callback = baseCreateCallback(args[--argsLength - 1], args[argsLength--], 2); else argsLength > 2 && "function" == typeof args[argsLength - 1] && (callback = args[--argsLength]);
            while (argsLength > ++argsIndex) {
                iterable = args[argsIndex];
                if (iterable && objectTypes[typeof iterable]) {
                    var ownIndex = -1, ownProps = objectTypes[typeof iterable] && keys(iterable), length = ownProps ? ownProps.length : 0;
                    while (length > ++ownIndex) {
                        index = ownProps[ownIndex];
                        result[index] = callback ? callback(result[index], iterable[index]) : iterable[index];
                    }
                }
            }
            return result;
        };
        var defaults = function(object, source, guard) {
            var index, iterable = object, result = iterable;
            if (!iterable) return result;
            var args = arguments, argsIndex = 0, argsLength = "number" == typeof guard ? 2 : args.length;
            while (argsLength > ++argsIndex) {
                iterable = args[argsIndex];
                if (iterable && objectTypes[typeof iterable]) {
                    var ownIndex = -1, ownProps = objectTypes[typeof iterable] && keys(iterable), length = ownProps ? ownProps.length : 0;
                    while (length > ++ownIndex) {
                        index = ownProps[ownIndex];
                        "undefined" == typeof result[index] && (result[index] = iterable[index]);
                    }
                }
            }
            return result;
        };
        var forIn = function(collection, callback, thisArg) {
            var index, iterable = collection, result = iterable;
            if (!iterable) return result;
            if (!objectTypes[typeof iterable]) return result;
            callback = callback && "undefined" == typeof thisArg ? callback : baseCreateCallback(callback, thisArg, 3);
            for (index in iterable) if (false === callback(iterable[index], index, collection)) return result;
            return result;
        };
        var forOwn = function(collection, callback, thisArg) {
            var index, iterable = collection, result = iterable;
            if (!iterable) return result;
            if (!objectTypes[typeof iterable]) return result;
            callback = callback && "undefined" == typeof thisArg ? callback : baseCreateCallback(callback, thisArg, 3);
            var ownIndex = -1, ownProps = objectTypes[typeof iterable] && keys(iterable), length = ownProps ? ownProps.length : 0;
            while (length > ++ownIndex) {
                index = ownProps[ownIndex];
                if (false === callback(iterable[index], index, collection)) return result;
            }
            return result;
        };
        var isPlainObject = function(value) {
            if (!(value && toString.call(value) == objectClass)) return false;
            var valueOf = value.valueOf, objProto = "function" == typeof valueOf && (objProto = getPrototypeOf(valueOf)) && getPrototypeOf(objProto);
            return objProto ? value == objProto || getPrototypeOf(value) == objProto : shimIsPlainObject(value);
        };
        var countBy = createAggregator(function(result, value, key) {
            hasOwnProperty.call(result, key) ? result[key]++ : result[key] = 1;
        });
        var groupBy = createAggregator(function(result, value, key) {
            (hasOwnProperty.call(result, key) ? result[key] : result[key] = []).push(value);
        });
        var indexBy = createAggregator(function(result, value, key) {
            result[key] = value;
        });
        var where = filter;
        isV8 && moduleExports && "function" == typeof setImmediate && (defer = function(func) {
            if (!isFunction(func)) throw new TypeError();
            return setImmediate.apply(context, arguments);
        });
        var parseInt = 8 == nativeParseInt(whitespace + "08") ? nativeParseInt : function(value, radix) {
            return nativeParseInt(isString(value) ? value.replace(reLeadingSpacesAndZeros, "") : value, radix || 0);
        };
        lodash.after = after;
        lodash.assign = assign;
        lodash.at = at;
        lodash.bind = bind;
        lodash.bindAll = bindAll;
        lodash.bindKey = bindKey;
        lodash.chain = chain;
        lodash.compact = compact;
        lodash.compose = compose;
        lodash.countBy = countBy;
        lodash.createCallback = createCallback;
        lodash.curry = curry;
        lodash.debounce = debounce;
        lodash.defaults = defaults;
        lodash.defer = defer;
        lodash.delay = delay;
        lodash.difference = difference;
        lodash.filter = filter;
        lodash.flatten = flatten;
        lodash.forEach = forEach;
        lodash.forEachRight = forEachRight;
        lodash.forIn = forIn;
        lodash.forInRight = forInRight;
        lodash.forOwn = forOwn;
        lodash.forOwnRight = forOwnRight;
        lodash.functions = functions;
        lodash.groupBy = groupBy;
        lodash.indexBy = indexBy;
        lodash.initial = initial;
        lodash.intersection = intersection;
        lodash.invert = invert;
        lodash.invoke = invoke;
        lodash.keys = keys;
        lodash.map = map;
        lodash.max = max;
        lodash.memoize = memoize;
        lodash.merge = merge;
        lodash.min = min;
        lodash.omit = omit;
        lodash.once = once;
        lodash.pairs = pairs;
        lodash.partial = partial;
        lodash.partialRight = partialRight;
        lodash.pick = pick;
        lodash.pluck = pluck;
        lodash.pull = pull;
        lodash.range = range;
        lodash.reject = reject;
        lodash.remove = remove;
        lodash.rest = rest;
        lodash.shuffle = shuffle;
        lodash.sortBy = sortBy;
        lodash.tap = tap;
        lodash.throttle = throttle;
        lodash.times = times;
        lodash.toArray = toArray;
        lodash.transform = transform;
        lodash.union = union;
        lodash.uniq = uniq;
        lodash.values = values;
        lodash.where = where;
        lodash.without = without;
        lodash.wrap = wrap;
        lodash.zip = zip;
        lodash.zipObject = zipObject;
        lodash.collect = map;
        lodash.drop = rest;
        lodash.each = forEach;
        lodash.eachRight = forEachRight;
        lodash.extend = assign;
        lodash.methods = functions;
        lodash.object = zipObject;
        lodash.select = filter;
        lodash.tail = rest;
        lodash.unique = uniq;
        lodash.unzip = zip;
        mixin(lodash);
        lodash.clone = clone;
        lodash.cloneDeep = cloneDeep;
        lodash.contains = contains;
        lodash.escape = escape;
        lodash.every = every;
        lodash.find = find;
        lodash.findIndex = findIndex;
        lodash.findKey = findKey;
        lodash.findLast = findLast;
        lodash.findLastIndex = findLastIndex;
        lodash.findLastKey = findLastKey;
        lodash.has = has;
        lodash.identity = identity;
        lodash.indexOf = indexOf;
        lodash.isArguments = isArguments;
        lodash.isArray = isArray;
        lodash.isBoolean = isBoolean;
        lodash.isDate = isDate;
        lodash.isElement = isElement;
        lodash.isEmpty = isEmpty;
        lodash.isEqual = isEqual;
        lodash.isFinite = isFinite;
        lodash.isFunction = isFunction;
        lodash.isNaN = isNaN;
        lodash.isNull = isNull;
        lodash.isNumber = isNumber;
        lodash.isObject = isObject;
        lodash.isPlainObject = isPlainObject;
        lodash.isRegExp = isRegExp;
        lodash.isString = isString;
        lodash.isUndefined = isUndefined;
        lodash.lastIndexOf = lastIndexOf;
        lodash.mixin = mixin;
        lodash.noConflict = noConflict;
        lodash.parseInt = parseInt;
        lodash.random = random;
        lodash.reduce = reduce;
        lodash.reduceRight = reduceRight;
        lodash.result = result;
        lodash.runInContext = runInContext;
        lodash.size = size;
        lodash.some = some;
        lodash.sortedIndex = sortedIndex;
        lodash.template = template;
        lodash.unescape = unescape;
        lodash.uniqueId = uniqueId;
        lodash.all = every;
        lodash.any = some;
        lodash.detect = find;
        lodash.findWhere = find;
        lodash.foldl = reduce;
        lodash.foldr = reduceRight;
        lodash.include = contains;
        lodash.inject = reduce;
        forOwn(lodash, function(func, methodName) {
            lodash.prototype[methodName] || (lodash.prototype[methodName] = function() {
                var args = [ this.__wrapped__ ], chainAll = this.__chain__;
                push.apply(args, arguments);
                var result = func.apply(lodash, args);
                return chainAll ? new lodashWrapper(result, chainAll) : result;
            });
        });
        lodash.first = first;
        lodash.last = last;
        lodash.sample = sample;
        lodash.take = first;
        lodash.head = first;
        forOwn(lodash, function(func, methodName) {
            var callbackable = "sample" !== methodName;
            lodash.prototype[methodName] || (lodash.prototype[methodName] = function(n, guard) {
                var chainAll = this.__chain__, result = func(this.__wrapped__, n, guard);
                return chainAll || null != n && (!guard || callbackable && "function" == typeof n) ? new lodashWrapper(result, chainAll) : result;
            });
        });
        lodash.VERSION = "2.2.1";
        lodash.prototype.chain = wrapperChain;
        lodash.prototype.toString = wrapperToString;
        lodash.prototype.value = wrapperValueOf;
        lodash.prototype.valueOf = wrapperValueOf;
        forEach([ "join", "pop", "shift" ], function(methodName) {
            var func = arrayRef[methodName];
            lodash.prototype[methodName] = function() {
                var chainAll = this.__chain__, result = func.apply(this.__wrapped__, arguments);
                return chainAll ? new lodashWrapper(result, chainAll) : result;
            };
        });
        forEach([ "push", "reverse", "sort", "unshift" ], function(methodName) {
            var func = arrayRef[methodName];
            lodash.prototype[methodName] = function() {
                func.apply(this.__wrapped__, arguments);
                return this;
            };
        });
        forEach([ "concat", "slice", "splice" ], function(methodName) {
            var func = arrayRef[methodName];
            lodash.prototype[methodName] = function() {
                return new lodashWrapper(func.apply(this.__wrapped__, arguments), this.__chain__);
            };
        });
        return lodash;
    }
    var undefined;
    var arrayPool = [], objectPool = [];
    var idCounter = 0;
    var keyPrefix = +new Date() + "";
    var largeArraySize = 75;
    var maxPoolSize = 40;
    var whitespace = " 	\f ﻿\n\r\u2028\u2029 ᠎             　";
    var reEmptyStringLeading = /\b__p \+= '';/g, reEmptyStringMiddle = /\b(__p \+=) '' \+/g, reEmptyStringTrailing = /(__e\(.*?\)|\b__t\)) \+\n'';/g;
    var reEsTemplate = /\$\{([^\\}]*(?:\\.[^\\}]*)*)\}/g;
    var reFlags = /\w*$/;
    var reFuncName = /^function[ \n\r\t]+\w/;
    var reInterpolate = /<%=([\s\S]+?)%>/g;
    var reLeadingSpacesAndZeros = RegExp("^[" + whitespace + "]*0+(?=.$)");
    var reNoMatch = /($^)/;
    var reThis = /\bthis\b/;
    var reUnescapedString = /['\n\r\t\u2028\u2029\\]/g;
    var contextProps = [ "Array", "Boolean", "Date", "Function", "Math", "Number", "Object", "RegExp", "String", "_", "attachEvent", "clearTimeout", "isFinite", "isNaN", "parseInt", "setImmediate", "setTimeout" ];
    var templateCounter = 0;
    var argsClass = "[object Arguments]", arrayClass = "[object Array]", boolClass = "[object Boolean]", dateClass = "[object Date]", funcClass = "[object Function]", numberClass = "[object Number]", objectClass = "[object Object]", regexpClass = "[object RegExp]", stringClass = "[object String]";
    var cloneableClasses = {};
    cloneableClasses[funcClass] = false;
    cloneableClasses[argsClass] = cloneableClasses[arrayClass] = cloneableClasses[boolClass] = cloneableClasses[dateClass] = cloneableClasses[numberClass] = cloneableClasses[objectClass] = cloneableClasses[regexpClass] = cloneableClasses[stringClass] = true;
    var debounceOptions = {
        leading: false,
        maxWait: 0,
        trailing: false
    };
    var descriptor = {
        configurable: false,
        enumerable: false,
        value: null,
        writable: false
    };
    var objectTypes = {
        "boolean": false,
        "function": true,
        object: true,
        number: false,
        string: false,
        undefined: false
    };
    var stringEscapes = {
        "\\": "\\",
        "'": "'",
        "\n": "n",
        "\r": "r",
        "	": "t",
        "\u2028": "u2028",
        "\u2029": "u2029"
    };
    var root = objectTypes[typeof window] && window || this;
    var freeExports = objectTypes[typeof exports] && exports && !exports.nodeType && exports;
    var freeModule = objectTypes[typeof module] && module && !module.nodeType && module;
    var moduleExports = freeModule && freeModule.exports === freeExports && freeExports;
    var freeGlobal = objectTypes[typeof global] && global;
    !freeGlobal || freeGlobal.global !== freeGlobal && freeGlobal.window !== freeGlobal || (root = freeGlobal);
    var _ = runInContext();
    if ("function" == typeof define && "object" == typeof define.amd && define.amd) {
        root._ = _;
        define(function() {
            return _;
        });
    } else freeExports && freeModule ? moduleExports ? (freeModule.exports = _)._ = _ : freeExports._ = _ : root._ = _;
}).call(this);