using System;

namespace Calculate;

public class Program
{
    /// <summary>
    /// Delegate for writing a line of text. Defaults to Console.WriteLine.
    /// Exposing WriteLine as an Actionallows us to redirect output for testing.
    /// Rather than writing directly to the console, we can capture output in memory, 
    /// enabling automated verifiction.
    /// </summary>
    public Action<string?> WriteLine { get; init; }

    /// <summary>
    /// Delegate for reading a line of text. Defaults to Console.ReadLine.
    /// Using a Func for input lets us replace Console.ReadLine with a stub or mock sequence in tests.
    /// This removes the need for user input during execution.
    /// </summary>
    public Func<string?> ReadLine { get; init; }

    /// <summary>
    /// Default constructor.
    /// Initializes delegates to Console methods.
    /// This ensures the application works normally when run from the command line,
    /// but still allows the delegates to be overridden for testing.
    /// </summary>
    public Program()
    {
        WriteLine = Console.WriteLine;
        ReadLine = Console.ReadLine;
    }

    /// <summary>
    /// Constructor allowing custom delegates for input and output.
    /// This enables dependency injection at construction time.
    /// This pattern supports unit testing and reuse in other contexts
    /// where you may want to replace console input/output with alternative mechanisms.
    /// </summary>
    /// <param name="writeLine">Delegate for output; must not be null.</param>
    /// <param name="readLine">Delegate for input; must not be null.</param>
    public Program(Action<string?> writeLine, Func<string?> readLine)
    {
        WriteLine = writeLine ?? throw new ArgumentNullException(nameof(writeLine));
        ReadLine = readLine ?? throw new ArgumentNullException(nameof(readLine));
    }

    /// <summary>
    /// Runs the main calculator loop, reading expressions and displayng results.
    /// Encapsulating the interaction logic in a method makes it easier to test
    /// the logic directly without launching a separate process.
    /// It returns an integer so tests can assert exit codes or termination conditions.
    /// </summary>
    /// <returns>0 for normal exit, non-zero for abnormal termination.</returns>
    public int Run()
    {
        var Caluclator = new Calculator();

        while (true)
        {
            WriteLine("Enter an expression like \"3 + 4\" (or 'q' to quit):");
            var input = ReadLine();

            if (input is null)
                return 1; // Indicate abnormal termination due to null input

            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
                break;

            if (Calculator.TryCalculate(input, out var result))
                WriteLine($"{input} = {result}");
            else
                WriteLine("Invalid input. Please include spaces around the operator and use integers.");
        }

        return 0;
    }

    /// <summary>
    /// Application entry point.
    /// Keeps Main minimal by instantiating Program with default console delegates.
    /// This separation simplifies testing and aligns with the Single Responsibility Principle.
    /// </summary>
    public static void Main()
    {
        var program = new Program();
        program.Run();
    }
}
