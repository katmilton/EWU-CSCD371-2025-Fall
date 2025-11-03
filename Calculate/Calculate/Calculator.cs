using System;
using System.Collections.Generic;

namespace Calculate;

public class Calculator
{
    
    public IReadOnlyDictionary<char, Func<int, int, int>> MathematicalOperations { get; } 
        = new Dictionary<char, Func<int, int, int>>
    {
        { '+', Add },
        { '-', Subtract },
        { '*', Multiple },
        { '/', Divide }
    };

    public static int Add(int a, int b) => a + b;

    public static int Subtract(int a, int b) => a - b;

    public static int Multiple(int a, int b) => a * b;

    public static int Divide(int a, int b) => a / b;


    /// <summary>
    /// Using "TryParse" pattern to attempt to calculate the result of a mathematical expression.
    /// Valid calculation expression include such strings as "3 + 4" or "10 / 2" etc.
    /// If there is no whitespace around the operator or if the operands are not integers, you can assume the calculation is invalid and return false.
    /// Use string.Split(), pattern matching, logical and operators to parse the string in their entirety.
    /// Index into the MathematicalOperations method using the operator parsed during pattern matching to find the corresponding implementation and invoke it.
    /// </summary>
    public static bool TryCalculate(string expression, out int result)
    {
        // Placeholder so Program.cs compiles and tests can run.
        // Please replace entirely.
        result = default;
        return false;
    }
}
