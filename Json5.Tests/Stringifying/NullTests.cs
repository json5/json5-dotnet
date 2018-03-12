using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class NullTests
    {
        [TestMethod]
        public void NullTest()
        {
            var s = Json5.Stringify(Json5Value.Null);
            Assert.AreEqual("null", s);
        }
    }
}
