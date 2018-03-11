using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class NullTests
    {
        [TestMethod]
        public void NullTest()
        {
            var v = Json5.Parse("null");
            Assert.AreEqual(Json5Type.Null, v.Type);
        }
    }
}
