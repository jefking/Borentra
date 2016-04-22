namespace Borentra.Security
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Key
    /// </summary>
    public static class Key
    {
        #region Variables
        /// <summary>
        /// Hexadecimal Expression
        /// </summary>
        private const string HexadecimalExpression = "[a-fA-F0-9]";

        /// <summary>
        /// License Key Expression
        /// </summary>
        private const string LicenseKeyExpression = @"[\w]{5}-[\w]{5}-[\w]{5}-[\w]{5}";

        /// <summary>
        /// Alphabet
        /// </summary>
        private static readonly char[] alphabet = { 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        #endregion

        #region Methods
        /// <summary>
        /// Is Valid Key
        /// </summary>
        /// <param name="key">Key to validate</param>
        /// <param name="amplitude">Amplitude</param>
        /// <param name="verticalOffset">Vertical Offset</param>
        /// <param name="angularFrequency">Angular Frequency</param>
        /// <param name="phaseShift">Phase Shift</param>
        /// <returns>Is Valid</returns>
        public static bool IsValidKey(string key, int amplitude, int verticalOffset, int angularFrequency, int phaseShift)
        {
            key = key.TrimIfNotNull();

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("key");
            }
            if (23 != key.Length)
            {
                throw new ArgumentException("key not 23 characters");
            }
            if (!Regex.IsMatch(key, LicenseKeyExpression))
            {
                throw new ArgumentException("key does not match regex statement");
            }
            if (amplitude == verticalOffset && verticalOffset == angularFrequency && angularFrequency == phaseShift)
            {
                throw new ArgumentException("Amplitude, Vertical Offset, Angular Frequency and Phase Shift cannot all be the same value.");
            }
            if (amplitude == 0 || verticalOffset == 0 || angularFrequency == 0 || phaseShift == 0)
            {
                throw new ArgumentException("Amplitude, Vertical Offset, Angular Frequency and Phase Shift cannot be 0.");
            }

            var trimmed = Trim(key.Substring(0, 11));
            var calculated = Calculate(Trim(key.Substring(12)), amplitude, verticalOffset, angularFrequency, phaseShift);
            
            return trimmed == calculated;
        }

        /// <summary>
        /// Create Key
        /// </summary>
        /// <param name="amplitude">Amplitude</param>
        /// <param name="verticalOffset">Vertical Offset</param>
        /// <param name="angularFrequency">Angular Frequency</param>
        /// <param name="phaseShift">Phase Shift</param>
        /// <returns>Key</returns>
        public static string CreateKey(int amplitude, int verticalOffset, int angularFrequency, int phaseShift)
        {
            if (amplitude == verticalOffset && verticalOffset == angularFrequency && angularFrequency == phaseShift)
            {
                throw new ArgumentException("Amplitude, Vertical Offset, Angular Frequency and Phase Shift cannot all be the same value.");
            }
            if (amplitude == 0 || verticalOffset == 0 || angularFrequency == 0 || phaseShift == 0)
            {
                throw new ArgumentException("Amplitude, Vertical Offset, Angular Frequency and Phase Shift cannot be 0.");
            }

            var random = new Random();
            long x = random.Next();
            var y = Calculate(x, amplitude, verticalOffset, angularFrequency, phaseShift);
            var paddedY = Pad(string.Format("{0:x}", y)).Insert(5, "-");
            var paddedX = Pad(string.Format("{0:x}", x)).Insert(5, "-");
            return string.Format("{0}-{1}", paddedY, paddedX);
        }

        /// <summary>
        /// Trim String
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Trimmed Text</returns>
        private static long Trim(string text)
        {
            text = text.TrimIfNotNull();
            var sb = new StringBuilder();
            for (var i = 0; i < text.Length; ++i)
            {
                if (Regex.IsMatch(text.Substring(i, 1), HexadecimalExpression))
                {
                    sb.Append(text.Substring(i, 1));
                }
            }

            return long.Parse(sb.ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Calculate unique value
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="amplitude">Amplitude</param>
        /// <param name="verticalOffset">Vertical Offset</param>
        /// <param name="angularFrequency">Angular Frequency</param>
        /// <param name="phaseShift">Phase Shift</param>
        /// <returns>Calculated Value</returns>
        private static long Calculate(long x, int amplitude, int verticalOffset, int angularFrequency, int phaseShift)
        {
            var sin = Math.Sin((angularFrequency * x) - phaseShift);
            var degrees = Degrees((amplitude * sin) + verticalOffset);
            return degrees < 0 ? degrees * -1 : degrees;
        }

        /// <summary>
        /// Determines degrees from number
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>Degrees</returns>
        private static long Degrees(double number)
        {
            return (long)(number * 180 / Math.PI);
        }

        /// <summary>
        /// Pad Text
        /// </summary>
        /// <param name="text">Text to pad</param>
        /// <returns>Padded text</returns>
        private static string Pad(string text)
        {
            text = text.TrimIfNotNull();

            var random = new Random();
            int index;
            while (10 > text.Length)
            {
                index = random.Next(text.Length);
                text = text.Insert(index, alphabet[random.Next(alphabet.Length - 1)].ToString());
            }

            return text;
        }
        #endregion
    }
}
