using System;
using System.Collections.Generic;

namespace Calculate;

public class Calculator
{
    
    public IReadOnlyDictionary<char, Func<int, int, int>> MathematicalOperations 
        => Calculator<int>.MathematicalOperations;

    public static int Add(int a, int b) => Calculator<int>.Add(a,b);

    public static int Subtract(int a, int b) => Calculator<int>.Subtract(a,b);

    public static int Multiply(int a, int b) => Calculator<int>.Multiply(a,b);

    public static int Divide(int a, int b) => Calculator<int>.Divide(a,b);


    public bool TryCalculate(string expression, out int result)
        => Calculator<int>.TryCalculate(expression, out result);
}
