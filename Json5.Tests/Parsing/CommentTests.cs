using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Json5.Tests.Parsing
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void SingleLineCommentsTest()
        {
            var v = Json5.Parse("{//comment\n}");
            var o = (Json5Object)v;
            Assert.AreEqual(0, o.Count);
        }

        [TestMethod]
        public void SingleLineCommentsAtEofTest()
        {
            var v = Json5.Parse("{}//comment");
            var o = (Json5Object)v;
            Assert.AreEqual(0, o.Count);
        }

        [TestMethod]
        public void MultiLineComments()
        {
            var v = Json5.Parse("{/*comment\n** */}");
            var o = (Json5Object)v;
            Assert.AreEqual(0, o.Count);
        }
    }
}
