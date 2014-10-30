## What is NVerify?
This project is a simple library to wrap the NUnit (extensible to MSTest) assertion libraries
in order to provide an expressive API for declaring unit-test assertions.

## Why another .Net fluent assertion library?
When I made this library, there was only 1 other option, which I did not like,
because it lacked flexible assertions for IEnumerable objects and methods.
So I created this one, it has grown a bit through time. It still needs more tests, but is quite complete and can be
used in any assertion scenario.

## Features
- It provides fluent assertions over:
    + Strings
    + Numbers
    + Arrays
    + Enumerables
    + Objects
    + Action<T,T1,T2...T15>
    + Func<T1,T1,T2...T15>
- It can be used to define to check for thrown Exceptions.
- It includes some functions like: Get file from **current** directory
  + If you have ever tried to read a data file from the current testing directory, you understand what I mean.
- You can assert over the Properties of an object under test, not just the object itself.

## How to get it
This tool is provided as a NuGet package, search for it using the name: [Testing Tools] (https://nuget.org/packages/TestingTools/)

You can also install it from the command line: `c:\project\dir\path> nuget install NVerify`

`TODO: Add example usage of every feature.`


## Usage
This tool is meant to be used in the **Arrange/Act/Assert** pattern, like in the following example:
`````csharp
using NVerify;
using NUnit.Framework;

[Fixture]
public class CalculatorTest
{
  [Test]
  public void TestCalculatorSum()
  {
    // Arrange
    Calculator target = new Calculator();
    int expected = 4;
    int result;

    // Act
    result = target.Sum(2, 2);

    // Assert: Should Pass
    Verify.That(result)
          .IsEqualTo(expected);
          .Now();
  }

  [Test]
  public void TestCalculatorSum2()
  {
    // Arrange
    Calculator target = new Calculator();
    int result;

    // Act
    result = target.Sum(2, 2);

    // Assert: Should Pass
    Verify.That(result)
          .IsLessThan(5)
          .And()
          .IsGreaterThan(3)
          .Now();
  }

  [Test]
  public void TestThrowsException()
  {
    // Arrange
    var target = new Calculator();
    int result;

    // Act
    var act = () => target.Sum(Int.MaxValue, 1);

    // Assert: Should Pass
    Verify.That(act).ThrowsException().Now();
  }

  [Test]
  public void TestArrayElements()
  {
    // Arrange
    var array = new int[] { 2, 3, 4 };

    // Assert: Should Pass
    Verify.That(array).DoesContain(2)
          .Now();

    Verify.That(array).DoesNotContain(1)
          .Now();
  }

  [Test]
  public void TestConsoleOutput()
  {
    // Arrange
    string expected = "Hello World!";

    // Act
    Action act = () => Console.Write("@Hello World!");

    // Assert: Should Pass
    Verify.That(act).WritesToConsole(expected)
          .Now();
  }

  [Test]
  public void TestExactConsoleOutput()
  {
    // Arrange
    string notExpected = "Hello World!";

    // Act
    Action act = () => Console.Write("@Hello World!");

    // Assert: Should Pass
    Verify.That(act)
          .DoesntWriteExactlyToConsole(notExpected)
          .Now();
  }
}
`````
