using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericsHomework.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void NewNode_HasSelfLoopInNext()
        {
            var n = new Node<int>(42);
            Assert.IsNotNull(n.Next);
            Assert.AreSame(n, n.Next);
        }

        [TestMethod]
        public void ToString_DelegatesToValueToString_ForValueTypes()
        {
            var n = new Node<int>(123);
            Assert.AreEqual<string>("123", n.ToString());
        }

        [TestMethod]
        public void ToString_HandlesNullableReferenceValue()
        {
            var n = new Node<string?>(null);
            Assert.AreEqual<string>(string.Empty, n.ToString());
        }

        [TestMethod]
        public void Next_HasPrivateSetter()
        {
            var n = new Node<int>(1);
            Assert.AreSame(n, n.Next);
        }
    }
}
