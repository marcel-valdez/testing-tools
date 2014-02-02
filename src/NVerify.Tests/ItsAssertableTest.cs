namespace NVerify.Tests
{
  using Fasterflect;
  using Moq;
  using NUnit.Framework;
  using NVerify.Core;

  /// <summary>
  ///This is a test class for ItsAssertableTest and is intended
  ///to contain all ItsAssertableTest Unit Tests
  ///</summary>
  [TestFixture]
  public class ItsAssertableTest
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

    /// <summary>
    /// Tests if on target get it uses the func on the parent field.
    /// </summary>
    [Test]
    public void TestIfOnTargetGetItUsesTheFuncOnTheParentField()
    {
      // Arrange
      var parentMock = new Mock<Parent>();
      var target = new ItsAssertable<Parent, Child>(p => p.Child);
      target.SetFieldValue("mParent", parentMock.Object);

      // Act
      var result = target.Target;

      // Assert
      parentMock.VerifyGet(m => m.Child, Times.Once());
    }

    /// <summary>
    /// Tests if it sets parent field on command when it matches the parent type.
    /// </summary>
    [Test]
    public void TestIfItSetsParentFieldOnCommandWhenItMatchesTheParentType()
    {
      // Arrange
      var parentMock = new Mock<Parent>();
      var target = new ItsAssertable<Parent, Child>(p => p.Child);

      // Act
      target.Command(parentMock.Object);

      // Assert
      Assert.AreEqual(parentMock.Object, target.GetFieldValue("mParent"));
    }

    /// <summary>
    /// Tests if it ignores the command when it doesnt match the parent.
    /// </summary>
    [Test]
    public void TestIfIgnoresTheCommandWhenItDoesntMatchTheParentType()
    {
      // Arrange
      var wrongMock = new Mock<Child>();
      var target = new ItsAssertable<Parent, Child>(p => p.Child);

      // Act
      target.Command(wrongMock.Object);

      // Assert
      Assert.IsNull(target.GetFieldValue("mParent"));
    }
  }
}
