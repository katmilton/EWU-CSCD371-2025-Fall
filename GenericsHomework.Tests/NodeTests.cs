namespace GenericsHomework.Tests
{
    public class NodeTests
    {
        [Fact]
        public void NewNode_HasSelfLoopInNext()
        {
            var n = new Node<int>(42);
            Assert.NotNull(n.Next);
            Assert.Same(n, n.Next);
        }

        [Fact]
        public void ToString_DelegatesToValueToString_ForValueTypes()
        {
            var n = new Node<int>(123);
            Assert.Equal("123", n.ToString());
        }

        [Fact]
        public void ToString_HandlesNullableReferenceValue()
        {
            var n = new Node<string?>(null);
            Assert.Equal(string.Empty, n.ToString());
        }

        [Fact]
        public void Next_HasPrivateSetter()
        {
            var n = new Node<int>(1);
            Assert.Same(n, n.Next);
        }
    }
}
