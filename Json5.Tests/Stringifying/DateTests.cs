using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class DateTests
    {
        [TestMethod]
        public void DatesTest()
        {
            var s = Json5.Stringify(new DateTime(2016, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            Assert.AreEqual("'2016-01-01T00:00:00.000Z'", s);
        }
    }
}
