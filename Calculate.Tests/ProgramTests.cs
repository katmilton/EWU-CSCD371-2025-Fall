using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Calculate;
using System.Security;


namespace Calculate.Tests;

[TestClass]
public class ProgramTests
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

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_ThrowsWhenWriteLineIsNull_Success()
    {
        _ = new Program(null!, () => "input");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_ThrowsWhenReadLineIsNull_Success()
    {
        _ = new Program(s => {}, null!);
    }

    [TestMethod]
    public void Run_ReturnsNonZeroWhenEndOfInputOccurs_Success()
    {
        // Arrange
        var linesWritten = new List<string?>();
        var inputs = new Queue<string?>(new string?[] { null });

        var program = new Program(linesWritten.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(1, exitCode);
    }

    [TestMethod]
    public void Run_QuitsWhenUserEntersQ_Success()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "q" });

        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(0, exitCode);
        Assert.IsTrue(outputs.Any(o => o!.Contains("Enter an expression", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void Run_QuitUppercaseQ_Success()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "Q" });
        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(0, exitCode);
        Assert.IsTrue(outputs.Any(o => o!.Contains("Enter an expression", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void Run_CalculatesValidExpression_Success()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "3 + 4", "q" });
        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act

        var exitCode = program.Run();
        // Assert
        Assert.AreEqual<int>(0, exitCode);
        Assert.IsTrue(outputs.Any(o => o!.Contains("3 + 4 = 7", StringComparison.Ordinal)));
    }

    [TestMethod]
    public void Run_HandlesInvalidInput_Success()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "3+4", "q" });
        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(0, exitCode);
        Assert.IsTrue(outputs.Any(o => o!.Contains("Invalid input", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void Run_UnknownOperatorInput_HandlesGracefully()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "5 ^ 2", "q" });
        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(0, exitCode);
        Assert.IsTrue(outputs.Any(o => o!.Contains("Invalid input", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void Run_DivisionByZeroInput_HandlesGracefully()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "10 / 0", "q" });
        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(0, exitCode);
        Assert.IsTrue(outputs.Any(o => o!.Contains("Invalid input", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void Run_ShowsPromptEachIteration_Success()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "1 + 1", "2 * 2", "q" });
        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(0, exitCode);
        int promptCount = outputs.Count(o => o != null && o!.Contains("Enter an expression", StringComparison.OrdinalIgnoreCase));
        Assert.AreEqual<int>(3, promptCount);
    }

    [TestMethod]
    public void Run_EmptyLineTreatedAsInvalidInput_Success()
    {
        // Arrange
        var outputs = new List<string?>();
        var inputs = new Queue<string?>(new[] { "", "q" });
        var program = new Program(outputs.Add, () => inputs.Dequeue());

        // Act
        var exitCode = program.Run();

        // Assert
        Assert.AreEqual<int>(0, exitCode);
        Assert.IsTrue(outputs.Any(o => o!.Contains("Invalid input", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void Main_EntryPointReadsAndRunsUntilQuit_Success()
    {
        // Arrange
        var originalIn = Console.In;
        var originalOut = Console.Out;
        try
        {
            Console.SetIn(new StringReader("q" + Environment.NewLine));
            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Program.Main();

            // Assert
            var stdout = sw.ToString();
            StringAssert.Contains(stdout, "Enter an expression");
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }
}
