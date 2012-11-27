This project is a simple library to wrap the NUnit (extensible to MSTest) assertion libraries in order to provide an expressive API for declaring unit-test assertions.
  
This tool is meant to be used in the Arrange/Act/Assert pattern, like in the following example:
`````csharp
[Test]
public void TestCalculatorSum()
{
	// Arrange
	Calculator target = new Calculator();
	int expected = 4;
	int result;
	
	// Act
	result = target.Sum(2, 2);
	
	// Assert
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
	
	// Assert
	Verify.That(result)
		  .IsLessThan(5)
		  .And()
		  .IsGreaterThan(3)
		  .Now();
}
`````