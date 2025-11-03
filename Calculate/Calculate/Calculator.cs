using System;
using System.Collections.Generic;

namespace Calculate;

public class Calculator
{
    
    public IReadOnlyDictionary<char, Func<int, int, int>> MathematicalOperations { get; } 
        = new Dictionary<char, Func<int, int, int>>
    {
        ['+'] = Add,
        ['-'] = Subtract,
        ['*'] = Multiply,
        ['/'] = Divide
    };

    public static int Add(int a, int b) => a + b;

    public static int Subtract(int a, int b) => a - b;

    public static int Multiply(int a, int b) => a * b;

    public static int Divide(int a, int b) => a / b;


    public bool TryCalculate(string expression, out int result)
    {
        result = 0;

        if (string.IsNullOrWhiteSpace(expression))
        {
            return false;
        }

        string[] splitExp = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (splitExp.Length != 3)
        {
            return false;
        }

        if (splitExp[1].Length != 1)
        {
            return false;
        }
        char op = splitExp[1][0];

        bool leftParsed = int.TryParse(splitExp[0], out int left);
        bool rightParsed = int.TryParse(splitExp[2], out int right);
        
        if (!leftParsed || !rightParsed)
        {
            return false;
        }

        if (op == '/' && right == 0)
        {
            return false;
        }

        if (!MathematicalOperations.TryGetValue(op, out var operation))
        {
            return false;
        }

        result = operation(left, right);
        return true;
    }
}
