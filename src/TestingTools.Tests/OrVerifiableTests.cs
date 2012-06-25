namespace TestingTools.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Core;
    using Extensions;

    /// <summary>
    /// Summary description for OrVerifiableTests
    /// </summary>
    [TestFixture]
    public class OrVerifiableTests
    {
        public OrVerifiableTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [Test]
        public void TestIfCanVerifyATrueOrAssertionOnInnerPredicament()
        {
            // Arrange
            object target = new object();

            // Act
            Verify.That(target)
                  .IsNull()
                  .Or(t => t.IsNotNull())
                  .Now();                                                     

            // Assert
            // Empty
        }

        [Test]
        public void TestIfCanVerifyAFailingOrAssertionWithInnerPredicament()
        {
            // Arrange
            object target = new object();
            Exception expected = null;

            // Act
            try
            {
                Verify.That(target)
                      .IsNull()
                      .Or(t => t.IsNull())
                      .Now();
            }
            catch (Exception ex)
            {
                expected = ex;   
            }

            // Assert
            Assert.IsNotNull(expected);
        }

        [Test]
        public void TestIfCanVerifyAValidOrAssertionWithOuterPredicament()
        {
            // Arrange
            object target = new object();
            Exception expected = null;

            // Act
            try
            {
                Verify.That(target)
                      .IsNotNull()
                      .Or(t => t.IsNull())
                      .Now();
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            // Assert
            Assert.IsNull(expected);
        }
    }
}
