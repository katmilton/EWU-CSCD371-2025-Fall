using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericsHomework.Tests;

[TestClass]
public class VennDiagramTests
{
    [TestMethod]
    public void Intersection_ReturnsOnlyCommonItems_Success()
    {
        var vd = new VennDiagram<string>();
        var a = vd.AddCircle("A");
        var b = vd.AddCircle("B");
        a.Add("x"); a.Add("y");
        b.Add("y"); b.Add("z");

        var intersection = vd.Intersection("A", "B").ToList();
        Assert.AreEqual<int>(1, intersection.Count);
        Assert.AreEqual<string>("y", intersection[0]);
    }

    [TestMethod]
    public void Union_ReturnsAllUniqueItems_Success()
    {
    }

    [TestMethod]
    public void Difference_ReturnsOnlyLeftMinusRightItems_Success()
    {
    }
}
