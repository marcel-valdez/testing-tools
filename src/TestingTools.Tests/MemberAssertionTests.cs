namespace TestingTools.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Core;
    using Extensions;

    [TestClass]
    public class MemberAssertionTests
    {
        [TestMethod]
        public void TestIfCanVerifyATrueAssertionOfAMemberFunc()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;

            // Act
            Verify.That(parent)
                  .Member(() => parent.Child)
                         (child =>
                            child.IsNotNull())
                  .Now();

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestIfCanVerifyATrueAssertionOfAMemberOfAnObject()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;

            // Act
            Verify.That(parent)
                  .Member(parent.Child)
                         (child =>
                            child.IsNotNull())
                  .Now();

            // Assert
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void TestIfCanVerifyATrueAssertionOfAMemberGetter()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;

            // Act
            Verify.That(parent)
                  .Member(p => p.Child)
                         (child =>
                            child.IsNotNull())
                  .Now();

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests if can verify A failing assertion of A member func of an object.
        /// </summary>
        [TestMethod]
        public void TestIfCanVerifyAFailingAssertionOfAMemberFuncOfAnObject()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;
            Exception expected = null;

            // Act
            try
            {
                Verify.That(parent)
                      .Member(() => parent.Child)
                             (child =>
                                child.IsNull())
                      .Now();
            }
            catch (Exception ex)
            {
                expected = ex;   
            }

            // Assert
            Assert.IsNotNull(expected);
        }

        /// <summary>
        /// Tests if can verify A failing assertion of A member of an object.
        /// </summary>
        [TestMethod]
        public void TestIfCanVerifyAFailingAssertionOfAMemberOfAnObject()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;
            Exception expected = null;

            // Act
            try
            {
                Verify.That(parent)
                      .Member(parent.Child)
                             (child =>
                                child.IsNull())
                      .Now();
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            // Assert
            Assert.IsNotNull(expected);
        }


        /// <summary>
        /// Tests if can verify A failing assertion of A member getter.
        /// </summary>
        [TestMethod]
        public void TestIfCanVerifyAFailingAssertionOfAMemberGetter()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;
            Exception expected = null;

            // Act
            try
            {
                Verify.That(parent)
                      .Member(p => p.Child)
                             (child =>
                                child.IsNull())
                      .Now();
            }
            catch (Exception ex)
            {
                expected = ex;
            }

            // Assert
            Assert.IsNotNull(expected);
        }

        /// <summary>
        /// Tests if can recursively do A true assertion.
        /// </summary>
        [TestMethod]
        public void TestIfCanRecursivelyDoATrueAssertion()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;
            member.Data = "test";

            // Act
            Verify.That(parent)
                  .Member(parent.Child)
                                (Child =>
                                 Child.IsNotNull()
                                      .And()
                                      .Member(c => c.Data)
                                                    (Data => 
                                                     Data.IsEqualTo("test")))
                  .Now();

            // Assert
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests if can recursively do A failing assertion.
        /// </summary>
        [TestMethod]
        public void TestIfCanRecursivelyDoAFailingAssertion()
        {
            // Arrange
            var parent = new Parent();
            var member = new Child();
            parent.Child = member;
            member.Data = "test";
            Exception expected = null;

            // Act
            try
            {
                Verify.That(parent)
                      .Member(parent.Child)
                             (child =>
                                 child.Member(
                                    c => c.Data)
                                          (data => data.IsEqualTo("wrong")))
                      .Now();
            }
            catch (Exception ex)
            {
                expected = ex;   
            }

            // Assert
            Assert.IsNotNull(expected);
        }
    }

    public class Parent
    {
        public virtual Child Child
        {
            get;
            set;
        }
    }

    public class Child
    {
        public virtual string Data
        {
            get;
            set;
        }
    }
}
