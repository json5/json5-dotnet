using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void EmptyTest()
        {
            var v = Json5.Parse("[]");
            var a = (Json5Array)v;
            Assert.AreEqual(0, a.Count);
        }

        [TestMethod]
        public void SingleValueTest()
        {
            var v = Json5.Parse("[0]");
            var a = (Json5Array)v;
            Assert.AreEqual(1, a.Count);
            Assert.AreEqual(0D, (double)a[0]);
        }

        [TestMethod]
        public void MultipleValuesTest()
        {
            var v = Json5.Parse("[0, 'a', true]");
            var a = (Json5Array)v;
            Assert.AreEqual(3, a.Count);
            Assert.AreEqual(0D, (double)a[0]);
            Assert.AreEqual("a", (string)a[1]);
            Assert.IsTrue((bool)a[2]);
        }

        [TestMethod]
        public void TrailingCommaTest()
        {
            var v = Json5.Parse("[0, 1, ]");
            var a = (Json5Array)v;
            Assert.AreEqual(2, a.Count);
            Assert.AreEqual(0D, (double)a[0]);
            Assert.AreEqual(1D, (double)a[1]);
        }

        [TestMethod]
        public void NestedTest()
        {
            var v = Json5.Parse("[0, [1]]");
            var a = (Json5Array)v;
            Assert.AreEqual(2, a.Count);
            Assert.AreEqual(0D, (double)a[0]);
            Assert.AreEqual(1D, (double)a[1][0]);
        }
    }
}
