using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class NumberTests
    {
        [TestMethod]
        public void NumbersTest()
        {
            var s = Json5.Stringify(-1.2);
            Assert.AreEqual("-1.2", s);
        }

        [TestMethod]
        public void NonFiniteNumbersTest()
        {
            var s = Json5.Stringify(new Json5Array { double.PositiveInfinity, double.NegativeInfinity, double.NaN });
            Assert.AreEqual("[Infinity,-Infinity,NaN]", s);
        }
    }
}
