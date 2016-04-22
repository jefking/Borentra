MetricConversionHelper = function Constructor() {
  return {
    cm2inches : function(cm) {

      function roundit(which) {
        return Math.round(which * 100) / 100;
      }

      function cmconvert(cm) {
        return {
          feet : roundit(cm / 30.84),
          inches : roundit(cm / 2.54)
        };
      }

      function feetAndInches(decimal) {
        var realFeet = decimal;
        var feet = Math.floor(realFeet);
        var inches = Math.round((realFeet - feet) * 12);
        return feet + "'" + inches + '"';
      }

      var data = cmconvert(+cm);
      var feet = Math.floor(data.feet);
      var inches = data.inches;
      var inchesLeft = Math.round((data.inches) - (feet * 12));

      if (inchesLeft >= 12) {
        feet = feet + 1;
        inchesLeft = inchesLeft - 12;
      }

      return {
        feet : feet,
        inches : inchesLeft
      };
    }
  };
};

module.exports = MetricConversionHelper;

