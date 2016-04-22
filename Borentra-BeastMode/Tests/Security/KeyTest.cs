namespace Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Borentra.Security;

    /// <summary>
    /// Key Test
    /// </summary>
    [TestClass]
    public class KeyTest
    {
        #region Error Cases
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NullKey()
        {
            Key.IsValidKey(null, 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyKey()
        {
            Key.IsValidKey(string.Empty, 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhiteSpaceKey()
        {
            Key.IsValidKey("  ", 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShortKey()
        {
            Key.IsValidKey("asdasdasd", 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsValidKeyAllValuesTheSame()
        {
            var random = new Random();
            var number = random.Next();
            Key.IsValidKey("50we5-4aeb5-pes44-c36id", number, number, number, number);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsValidKeyAllValuesZero()
        {
            var number = 0;
            Key.IsValidKey("50we5-4aeb5-pes44-c36id", number, number, number, number);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateKeyAllValuesTheSame()
        {
            var random = new Random();
            var number = random.Next();
            Key.CreateKey(number, number, number, number);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateKeyAllValuesZero()
        {
            var number = 0;
            Key.CreateKey(number, number, number, number);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LongKey()
        {
            Key.IsValidKey("jaoaskdqwopdnasdmasdadfasd932asldkasdajsdbnasu8dajdasdn", 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRegexSpecialChar()
        {
            Key.IsValidKey("si8wjs6sh&hsjtlwisajehd", 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidRegexNoDash()
        {
            Key.IsValidKey("si8wjs6shghsjtlwisajehd", 0, 0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumbersAreEqualValidation()
        {
            var random = new Random();
            int number = random.Next();
            Key.IsValidKey("50we5-4aeb5-pes44-c36id", number, number, number, number);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NumbersAreEqualCreate()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int number = random.Next();
            Key.CreateKey(number, number, number, number);
        }
        #endregion

        #region Valid Cases
        /// <summary>
        /// Normal Key
        /// </summary>
        [TestMethod]
        public void ValidValues()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int amplitude = random.Next();
            int verticalOffset = random.Next();
            int angularFrequency = random.Next();
            int phaseShift = random.Next();
            string tokenKey = Key.CreateKey(amplitude, verticalOffset, angularFrequency, phaseShift);
            Assert.IsNotNull(tokenKey, "Token Key should not be null");
            Assert.AreEqual<int>(23, tokenKey.Length, "Token Key should be 23 charcters");
            bool isValid = Key.IsValidKey(tokenKey, amplitude, verticalOffset, angularFrequency, phaseShift);
            Assert.IsTrue(isValid, "Key should be valid");
        }

        /// <summary>
        /// Hard Coded Valid Case
        /// </summary>
        [TestMethod]
        public void HardCodedValidCase()
        {
            int amplitude = 1016658862;
            int verticalOffset = 1146651392;
            int angularFrequency = 1582882305;
            int phaseShift = 2070800605;
            bool isValid = Key.IsValidKey("17ccf-92b13-g4eo5-ara70", amplitude, verticalOffset, angularFrequency, phaseShift);
            Assert.IsTrue(isValid, "Key should be valid");
        }

        /// <summary>
        /// Hard Coded Invalid Case
        /// </summary>
        [TestMethod]
        public void HardCodedInValidKey()
        {
            int amplitude = 1016658862;
            int verticalOffset = 1146651392;
            int angularFrequency = 1582882305;
            int phaseShift = 2070800605;
            Assert.IsFalse(Key.IsValidKey("17ccf-92b13-g4eo5-ara71", amplitude, verticalOffset, angularFrequency, phaseShift));
        }

        /// <summary>
        /// Hard Coded Invalid Case
        /// </summary>
        [TestMethod]
        public void HardCodedInValidCaseKey()
        {
            int amplitude = 678967;
            int verticalOffset = 12312;
            int angularFrequency = 231234;
            int phaseShift = 2345;
            Assert.IsFalse(Key.IsValidKey("17ccf-92b13-g4eo5-ara70", amplitude, verticalOffset, angularFrequency, phaseShift));
        }

        /// <summary>
        /// Hard Coded Invalid Case
        /// </summary>
        [TestMethod]
        public void HardCodedInValidCaseAmplitude()
        {
            var random = new Random();
            int number = random.Next();
            int amplitude = 1016658862 + number;
            int verticalOffset = 1146651392;
            int angularFrequency = 1582882305;
            int phaseShift = 2070800605;
            Assert.IsFalse(Key.IsValidKey("17ccf-92b13-g4eo5-ara70", amplitude, verticalOffset, angularFrequency, phaseShift));
        }

        /// <summary>
        /// Hard Coded Invalid Case
        /// </summary>
        [TestMethod]
        public void HardCodedInValidCaseVerticalOffset()
        {
            var random = new Random();
            int number = random.Next();
            int amplitude = 1016658862;
            int verticalOffset = 1146651392 + number;
            int angularFrequency = 1582882305;
            int phaseShift = 2070800605;
            Assert.IsFalse(Key.IsValidKey("17ccf-92b13-g4eo5-ara70", amplitude, verticalOffset, angularFrequency, phaseShift));
        }

        /// <summary>
        /// Hard Coded Invalid Case
        /// </summary>
        [TestMethod]
        public void HardCodedInValidCaseAngularFrequency()
        {
            var random = new Random();
            int number = random.Next();
            int amplitude = 1016658862;
            int verticalOffset = 1146651392;
            int angularFrequency = 1582882305 + number;
            int phaseShift = 2070800605;
            Assert.IsFalse(Key.IsValidKey("17ccf-92b13-g4eo5-ara70", amplitude, verticalOffset, angularFrequency, phaseShift));
        }

        /// <summary>
        /// Hard Coded Invalid Case
        /// </summary>
        [TestMethod]
        public void HardCodedInValidCasePhaseShift()
        {
            var random = new Random();
            int number = random.Next();
            int amplitude = 1016658862;
            int verticalOffset = 1146651392;
            int angularFrequency = 1582882305;
            int phaseShift = 2070800605 + number;
            Assert.IsFalse(Key.IsValidKey("17ccf-92b13-g4eo5-ara70", amplitude, verticalOffset, angularFrequency, phaseShift));
        }
        #endregion
    }
}