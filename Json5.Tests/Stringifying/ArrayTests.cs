using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Stringifying
{
    [TestClass]
    public class ArrayTests
    {
        [TestMethod]
        public void EmptyArraysTest()
        {
            var s = Json5.Stringify(new Json5Array());
            Assert.AreEqual("[]", s);
        }

        [TestMethod]
        public void ArrayValuesTest()
        {
            var s = Json5.Stringify(new Json5Array { 1 });
            Assert.AreEqual("[1]", s);
        }

        [TestMethod]
        public void MultipleArrayValuesTest()
        {
            var s = Json5.Stringify(new Json5Array { 1, 2 });
            Assert.AreEqual("[1,2]", s);
        }

        [TestMethod]
        public void NestedArraysTest()
        {
            var s = Json5.Stringify(new Json5Array { 1, new Json5Array { 2, 3 } });
            Assert.AreEqual("[1,[2,3]]", s);
        }

        [TestMethod]
        public void CircularArraysTest()
        {
            var a = new Json5Array();
            a["a"] = a;
            //Json5.Stringify(a);
            Assert.Fail("Not Implemented");
        }
    }
}
