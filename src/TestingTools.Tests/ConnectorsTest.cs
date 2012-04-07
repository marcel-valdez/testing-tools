namespace TestingTools.Tests
{
    using TestingTools.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TestingTools.Core;
    using Moq;
    using Fasterflect;
    using Helpers;

    /// <summary>
    ///This is a test class for ConnectorsTest and is intended
    ///to contain all ConnectorsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConnectorsTest
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

        [TestMethod]
        public void TestIfAndCanConnectAItsAssertable()
        {
            // Arrange
            var parentMock = new Mock<Parent>();
            var childMock = new Mock<Child>();
            var mockAssertion = new Mock<IAssertion<Parent>>();
            mockAssertion.SetupGet(m => m.Target).Returns(parentMock.Object);
            parentMock.SetupGet(p => p.Child).Returns(childMock.Object);
            var parentAssertion = new StubVerifiable<Parent>(
                                    new Verifiable<Parent>(
                                        mockAssertion.Object,
                                        _ =>
                                        {
                                        }));

            bool rootMemberAssertionCalled = false;
            // Act
            parentAssertion
                .And(
                    Its<Parent>
                    .Member(p => p.Child)
                    .IsEqualTo(childMock.Object))
                    .And()
                    .ItsTrueThat(
                    _ =>
                    {
                        rootMemberAssertionCalled = true;
                        return true;
                    })
                    .And()

                .Now();

            // Assert
            Assert.IsTrue(rootMemberAssertionCalled);
            Assert.IsTrue(parentAssertion.NowCalled);
            Assert.IsTrue(parentAssertion.TargetGetCalled);
            mockAssertion.Verify(m => m.Target, Times.AtLeastOnce());
            parentMock.VerifyGet(p => p.Child, Times.AtLeastOnce());
        }

        /// <summary>
        /// Tests if And(Target) can connect a predicament.
        /// </summary>
        [TestMethod]
        public void TestIfAndCanConnectAPredicament()
        {
            // Arrange
            var mock = new Mock<IVerifiable<string>>().Object;
            object connected = new object();

            // Act
            var result = mock.And(connected);

            // Assert
            Assert.IsInstanceOfType(result, typeof(AndConnector<object, string>));
            Assert.AreEqual(result.GetFieldValue("mLeftPredicament"), mock);
            Assert.AreEqual(result.Target, connected);
        }

        /// <summary>
        /// Tests if Now() chains the Now() calls and redirects the Target.
        /// </summary>
        [TestMethod]
        public void TestIfAnd_NowChainsTheNowCallsAndRedirectsTheTarget()
        {
            // Arrange
            var mock = new Mock<IVerifiable<string>>();
            var mocked = mock.Object;
            StubVerifiable<object> right = null;

            // Act
            right = new StubVerifiable<object>(mocked
                .And(new object())
                .IsNotNull());
            right.Now();

            // Assert
            mock.Verify(m => m.Now(), Times.Once());
            mock.VerifyGet(m => m.Target, Times.Never());
            Assert.IsTrue(right.NowCalled);
        }

        /// <summary>
        /// Tests if a And() then Now() calls connects to two valid predicaments.
        /// </summary>
        [TestMethod]
        public void TestIfAnd_NowConnectsToTwoValidPredicaments()
        {
            // Arrange
            bool actionCalled = false;
            object target = new object();
            object received_parameter = null;
            var verifiable = new MockVerifiable<object>(
                                new Verifiable<object>(
                                    new Assertion<object>(target),
                                    p =>
                                    {
                                        actionCalled = true;
                                        received_parameter = p;
                                    }));
            StubVerifiable<string> right = null;

            // Act
            right = new StubVerifiable<string>(verifiable.And("data")
                      .IsEqualTo("data"));
            right.Now();

            // Assert
            Assert.IsTrue(verifiable.NowCalled, "No se mandó llamar Now()");
            Assert.IsTrue(actionCalled, "No se ejctuó la acción sobre el Target");
            Assert.AreEqual(target, received_parameter);
            Assert.IsFalse(verifiable.TargetGetCalled, "No se debe pedir el Target del Verifiable en la conexión");
            Assert.IsTrue(right.NowCalled);
        }



        /// <summary>
        /// Tests if And() - Now() connects an invalid right predicament.
        /// </summary>
        [TestMethod]
        public void TestIfAnd_NowConnectsAnInvalidRightPredicament()
        {
            // Arrange
            bool actionCalled = false;
            object target = new object();
            object received_parameter = null;
            var verifiable = new MockVerifiable<object>(
                                new Verifiable<object>(
                                    new Assertion<object>(target),
                                        p =>
                                        {
                                            actionCalled = true;
                                            received_parameter = p;
                                        }));
            StubVerifiable<string> right = null;

            // Act
            Exception x = null;
            try
            {
                right = new StubVerifiable<string>(
                    verifiable.And("data")
                              .IsEqualTo("bad"));
                right.Now();
            }
            catch (Exception ex)
            {
                x = ex;
            }

            // Assert
            Assert.IsNotNull(x);
            Assert.IsTrue(verifiable.NowCalled, "Se mandó llamar Now()");
            Assert.IsTrue(actionCalled, "Se ejecutó la acción sobre el Target");
            Assert.IsTrue(right.NowCalled);
        }

        /// <summary>
        /// Tests if and_ now connects an invalid left predicament.
        /// </summary>
        [TestMethod]
        public void TestIfAnd_NowConnectsAnInvalidLeftPredicament()
        {
            // Arrange
            object target = new object();
            var left = new MockVerifiable<object>(
                                new Verifiable<object>(
                                    new Assertion<object>(target),
                                    p =>
                                    {
                                        throw new Exception();
                                    }));
            StubVerifiable<string> right = null;
            bool rightPredicamentCalled = false;
            // Act
            Exception ex = null;

            try
            {
                right = new StubVerifiable<string>(
                           left.And("data")
                               .ItsTrueThat(_ => rightPredicamentCalled = true));
                right.Now();
            }
            catch (Exception e)
            {
                ex = e;
            }

            // Assert
            Assert.IsTrue(left.NowCalled, "No se mandó llamar Now()");
            Assert.IsFalse(rightPredicamentCalled, "No se debió mandar llamar el segundo Target_get(), pues ya debió fallar el primero");
            Assert.IsNotNull(ex);
        }
    }
}