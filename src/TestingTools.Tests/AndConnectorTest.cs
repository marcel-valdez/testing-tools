namespace TestingTools.Tests
{
  using System;
  using Fasterflect;
  using Moq;
  using NUnit.Framework;
  using TestingTools.Core;

  /// <summary>
  ///This is a test class for AndConnectorTest and is intended
  ///to contain all AndConnectorTest Unit Tests
  ///</summary>
  [TestFixture]
  public class AndConnectorTest
  {

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get;
      set;
    }

    [Test]
    public void TestIfConstructorSetupsFields()
    {
      // Arrange
      object verified = new Object();
      IVerifiable<string> mock = new Mock<IVerifiable<string>>().Object;

      // Act
      var target = new AndConnector<object, string>(mock, verified);

      // Assert
      Assert.AreEqual(target.GetFieldValue("mLeftPredicament"), mock);
      Assert.AreEqual(target.GetFieldValue("mTarget"), verified);
    }

    [Test]
    public void TestIfTargetIsCorrectlyConnected()
    {
      // Arrange
      object verified = new Object();
      IVerifiable<string> mock = new Mock<IVerifiable<string>>().Object;

      // Act
      var target = new AndConnector<object, string>(mock, verified);

      // Assert
      Assert.AreEqual(target.Target, verified);
    }

    [Test]
    public void TestIfTestIfNowCallsLeftPredicament()
    {
      // Arrange
      var verifiable = new Mock<IVerifiable<string>>();
      verifiable.Setup(l => l.Now())
                .Verifiable("Se debió mandar llamar Now() en el predicamento");
      var mock = verifiable.Object;
      var target = new AndConnector<object, string>(mock, new object());

      // Act
      (target as IVerifiable<object>).Now();

      // Assert
      verifiable.Verify();
    }
  }
}
