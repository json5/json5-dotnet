using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class SpaceTests
    {
        [TestMethod]
        public void NullSpaceTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 });
            Assert.AreEqual("[1]", s);
        }

        [TestMethod]
        public void ZeroSpaceTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 }, 0);
            Assert.AreEqual("[1]", s);
        }

        [TestMethod]
        public void EmptyStringSpaceTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 }, "");
            Assert.AreEqual("[1]", s);
        }

        [TestMethod]
        public void NumberSpaceTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 }, 2);
            Assert.AreEqual("[\n  1,\n]", s);
        }

        [TestMethod]
        public void MaxNumberSpaceTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 }, 11);
            Assert.AreEqual("[\n          1,\n]", s);
        }

        [TestMethod]
        public void StringSpaceTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 }, "\t");
            Assert.AreEqual("[\n\t1,\n]", s);
        }

        [TestMethod]
        public void MaxStringSpaceTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 }, "           ");
            Assert.AreEqual("[\n          1,\n]", s);
        }

        [TestMethod]
        public void ArraysTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 }, 2);
            Assert.AreEqual("[\n  1,\n]", s);
        }

        [TestMethod]
        public void NestedArraysTest()
        {
            var s = Json5.Stringify(new Json5Array { 1, new Json5Array { 2 }, 3 }, 2);
            Assert.AreEqual("[\n  1,\n  [\n    2,\n  ],\n  3,\n]", s);
        }

        [TestMethod]
        public void ObjectsTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a", 1 } }, 2);
            Assert.AreEqual("{\n  a: 1,\n}", s);
        }

        [TestMethod]
        public void NestedObjectsTest()
        {
            var s = Json5.Stringify(new Json5Object { { "a", new Json5Object { { "b", 2 } } } }, 2);
            Assert.AreEqual("{\n  a: {\n    b: 2,\n  },\n}", s);
        }
    }
}
