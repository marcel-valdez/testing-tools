﻿namespace TestingTools.Tests
{
    using TestingTools.Core;
    using NUnit.Framework;
    using System;
    
    
    /// <summary>
    ///This is a test class for VerifiableTest and is intended
    ///to contain all VerifiableTest Unit Tests
    ///</summary>
    [TestFixture]
    public class VerifiableTest
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
        public void TestIfItRecursivelyCallsCommand()
        {
            // Arrange
            var root = new Root<object>()
            {
                Target = "root"
            };

            var verifiable = new Verifiable<object>(root, _ =>
            {
            });

            // Act
            verifiable.Command("test");


            // Assert
            Assert.IsTrue(root.CommandCalled);
            Assert.AreEqual(root.CommandObject, "test");
            Assert.IsFalse(root.TargetGetCalled);
        }

        [Test]
        public void TestIfItDoesntTryToCommandANonCommandable()
        {
            // Arrange
            var root = new Assertion<object>(new object());
            var verifiable = new Verifiable<object>(root, _ =>
            {
            });

            // Act
            Exception ex = null;
            try
            {
                verifiable.Command("test");
            }
            catch (Exception x)
            {

                ex = x;
            }

            // Assert
            Assert.IsNull(ex);
        }

        class Root<T> : IAssertion<T>, ICommandable
        {
            #region ICommandable Members
            public bool CommandCalled = false;
            public object CommandObject = null;
            private T mTarget;
            public bool TargetGetCalled = false;

            public void Command(object command)
            {
                this.CommandCalled = true;
                this.CommandObject = command;
            }

            #endregion

            #region IAssertion<T> Members

            public T Target
            {
                get
                {
                    this.TargetGetCalled = true;
                    return mTarget;
                }

                set
                {
                    mTarget = value;
                }
            }

            #endregion
        }

    }
}
