using Calculate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;


namespace Calculate.Tests;

[TestClass]
public class CalculatorTests
{

    [TestMethod]
    public void Calculator_Add_TwoPositiveIntegers_ReturnsSum()
    {
        // Arrange
        int a = 2;
        int b = 8;

        // Act
        int result = Calculator.Add(a, b);

        // Assert
        Assert.AreEqual<int>(10, result);
    }

    [TestMethod]
    public void Calculator_Subtract_NegativeResult_ReturnsDifference()
    {
        // Arrange
        int a = 5;
        int b = 9;

        // Act
        int result = Calculator.Subtract(a, b);

        // Assert
        Assert.AreEqual<int>(-4, result);
    }

    [TestMethod]
    public void Calculator_Multiply_WithZero_ReturnsZero()
    {
        // Arrange
        int a = 20;
        int b = 0;

        // Act
        int result = Calculator.Multiply(a, b);

        // Assert
        Assert.AreEqual<int>(0, result);
    }

    [TestMethod]
    public void Calculator_Divide_WithEvenDivision_ReturnsQuotient()
    {
        // Arrange
        int a = 10;
        int b = 5;

        // Act
        int result = Calculator.Divide(a, b);

        // Assert
        Assert.AreEqual<int>(3, result);
    }

    [TestMethod]
    [ExpectedException(typeof(DivideByZeroException))]
    public void Calculator_Divide_ByZero_ThrowsDivideByZeroException()
    {
        // Arrange
        int a = 12;
        int b = 0;

        // Act
        _ = Calculator.Divide(a, b);

        // Assert (handled by ExpectedException)
    }

    [TestMethod]
    public void Calculator_MathematicalOperations_HasFourOperators_MapsToExpectedMethods()
    {
        // Arrange
        Calculator calc = new Calculator();

        // Act
        IReadOnlyDictionary<char, Func<int, int, int>> map = calc.MathematicalOperations;

        // Assert
        Assert.AreEqual<int>(4, map.Count);
        Assert.AreEqual<bool>(true, map.ContainsKey('+'));
        Assert.AreEqual<bool>(true, map.ContainsKey('-'));
        Assert.AreEqual<bool>(true, map.ContainsKey('*'));
        Assert.AreEqual<bool>(true, map.ContainsKey('/'));
    }

    [TestMethod]
    public void Calculator_MathematicalOperations_InvokeAdd_PerformsAddition()
    {
        // Arrange
        Calculator calc = new Calculator();
        Func<int, int, int> add = calc.MathematicalOperations['+'];

        // Act
        int result = add(6, 14);

        // Assert
        Assert.AreEqual<int>(20, result);
    }

    [TestMethod]
    public void Calculator_MathematicalOperations_InvokeEachOperator_PerformsCorrectOperation()
    {
        // Arrange
        Calculator calc = new Calculator();
        IReadOnlyDictionary<char, Func<int, int, int>> map = calc.MathematicalOperations;

        // Act
        int sum = map['+'](2, 9);
        int diff = map['-'](15, 7);
        int prod = map['*'](2, 5);
        int quot = map['/'](21, 7);

        // Assert
        Assert.AreEqual<int>(11, sum);
        Assert.AreEqual<int>(8, diff);
        Assert.AreEqual<int>(10, prod);
        Assert.AreEqual<int>(3, quot);
    }


}