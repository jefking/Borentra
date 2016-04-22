MetricConversionHelper = function() {
    return {
        cm2inches: function(cm) {
            function roundit(which) {
                return Math.round(100 * which) / 100;
            }
            function cmconvert(cm) {
                return {
                    feet: roundit(cm / 30.84),
                    inches: roundit(cm / 2.54)
                };
            }
            var data = cmconvert(+cm);
            var feet = Math.floor(data.feet);
            data.inches;
            var inchesLeft = Math.round(data.inches - 12 * feet);
            if (inchesLeft >= 12) {
                feet += 1;
                inchesLeft -= 12;
            }
            return {
                feet: feet,
                inches: inchesLeft
            };
        }
    };
};

module.exports = MetricConversionHelper;