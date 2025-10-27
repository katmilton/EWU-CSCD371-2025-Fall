using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsHomework.Tests;

[TestClass]
public class NodeTests_ICollection
{
    [TestMethod]
    public void ICollectionCount_SingleAndMulti_Success()
    {
        var n = new Node<int>(1);
        Assert.AreEqual<int>(1, ((ICollection<int>)n).Count);

        n.Append(2);
        n.Append(3);
        Assert.AreEqual<int>(3, ((ICollection<int>)n).Count);
    }

    [TestMethod]
    public void ICollection_AddIntoEmptyAfterClear_Success()
    {
        var n = new Node<string>("first");
        n.Clear();
        ((ICollection<string>)n).Add("x");

        Assert.AreEqual<int>(1, ((ICollection<string>)n).Count);
        Assert.IsTrue(n.Exists("x"));
        Assert.AreSame(n, n.Next);
    }

    [TestMethod]
    public void ICollection_RemoveHeadOnlyItemAndEmpty_Success()
    {
        var n = new Node<int>(42);
        bool removed = ((ICollection<int>)n).Remove(42);

        Assert.IsTrue(removed);
        Assert.AreEqual<int>(0, ((ICollection<int>)n).Count);
        Assert.IsFalse(n.Exists(42));
        Assert.AreSame(n, n.Next);
    }

    [TestMethod]
    public void ICollection_RemoveHeadInMultiListShiftsHead_Success()
    {
        var n = new Node<string>("a");
        n.Append("b");
        n.Append("c");

        bool removed = ((ICollection<string>)n).Remove("a");
        Assert.IsTrue(removed);

        Assert.AreEqual<int>(2, ((ICollection<string>)n).Count);
        Assert.IsFalse(n.Exists("a"));
        Assert.IsTrue(n.Exists("b"));
        Assert.IsTrue(n.Exists("c"));
        Assert.AreSame(n, n.Next.Next);
    }

    [TestMethod]
    public void ICollection_RemoveMiddleNode_Success()
    {
        var n = new Node<int>(1);
        n.Append(2);
        n.Append(3);

        bool removed = ((ICollection<int>)n).Remove(2);
        Assert.IsTrue(removed);
        Assert.AreEqual<int>(2, ((ICollection<int>)n).Count);
        Assert.IsFalse(n.Exists(2));
        Assert.IsTrue(n.Exists(1));
        Assert.IsTrue(n.Exists(3));
    }

    [TestMethod]
    public void ICollection_CopyToWritesSequentially_Success()
    {
        var n = new Node<int>(5);
        n.Append(6);
        n.Append(7);

        var arr = new int[5];
        ((ICollection<int>)n).CopyTo(arr, 1);

        CollectionAssert.AreEqual(new[] { 0, 5, 6, 7, 0 }, arr);
    }

    [TestMethod]
    public void ICollection_EnumeratesAllItemsOnce_Success()
    {
        var n = new Node<string>("x");
        n.Append("y");
        n.Append("z");

        var items = ((IEnumerable<string>)n).ToList();
        CollectionAssert.AreEquivalent(new[] { "x", "y", "z" }, items);
        Assert.AreEqual<int>(3, items.Count);
    }

    [TestMethod]
    public void ICollection_IsReadOnlyIsFalse_Success()
    {
        var n = new Node<int>(1);
        Assert.IsFalse(((ICollection<int>)n).IsReadOnly);
    }

}
