namespace TestingTools.Tests
{
    using TestingTools.Core;
    using System;
    using Moq;
    using Fasterflect;
    using NUnit.Framework;
    
    /// <summary>
    ///This is a test class for ItsAssertableTest and is intended
    ///to contain all ItsAssertableTest Unit Tests
    ///</summary>
    [TestFixture]
    public class ItsAssertableTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


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
