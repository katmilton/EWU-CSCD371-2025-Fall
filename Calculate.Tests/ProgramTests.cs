using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Calculate;
using System.Security;


namespace Calculate.Tests;

[TestClass]
public sealed class ProgramTests
{
    [TestMethod]
    public void Properties_CanBeSetAtConstructionInvokedCorrectly_Success()
    {
        // Arrange
        var writes = new List<string?>();
        var inputs = new Queue<string?>(new[] { "hello", (string?)null });

        Action<string?> captureWrite = s => writes.Add(s);
        Func<string?> supplyRead = () => inputs.Count > 0 ? inputs.Dequeue() : null;

        // Act
        var program = new Program(captureWrite, supplyRead);

        program.WriteLine("first");
        var read1 = program.ReadLine();
        var read2 = program.ReadLine(); //will return null

        // Assert
        Assert.AreEqual<int>(1, writes.Count);
        Assert.AreEqual<string?>("first", writes[0]);
        Assert.AreEqual<string?>("hello", read1);
        Assert.AreEqual<string?>(null, read2);
    }

    [TestMethod]
    public void DefaultConstructor_WiresDelegatesToConsoleMethods_Success()
    {
        // Arrange
        var program = new Program();

        // Act
        var writeMethodType = program.WriteLine.Method.DeclaringType;
        var readMethodType = program.ReadLine.Method.DeclaringType;

        // Assert
        Assert.AreEqual<Type>(typeof(Console), writeMethodType);
        Assert.AreEqual<Type>(typeof(Console), readMethodType);
    }
}
