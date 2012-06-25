namespace TestingTools.Tests
{
    using TestingTools.Core;
    using NUnit.Framework;
    using System;
    using Moq;
    using Fasterflect;
    
    
    /// <summary>
    ///This is a test class for AndConnectorTest and is intended
    ///to contain all AndConnectorTest Unit Tests
    ///</summary>
    [TestFixture]
    public class AndConnectorTest
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
