namespace Tests.Core
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Borentra.Core;

    [TestClass]
    public class ScheduledCoreCases
    {
        [TestMethod]
        public void Constructor()
        {
            new ScheduledTaskCore(TimeSpan.FromMinutes(1));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorPeriodZero()
        {
            new ScheduledTaskCore(TimeSpan.Zero);
        }
    }
}
