namespace TestingTools.Tests
{
    using TestingTools.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using TestingTools.Core;
    using System.Linq;
    using Moq;

    /// <summary>
    ///This is a test class for CollectionExtensionsTest and is intended
    ///to contain all CollectionExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CollectionExtensionsTest
    {
        [TestMethod()]
        public void TestIfTrueForAllFailsWhenNoItemsInCollection()
        {
            // Arrange
            IAssertion<IEnumerable<bool>> values = MakeAssertion(0);
            Exception x = null;

            // Act
            x = IsTrueForAllActHelper(values);
            
            // Assert
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void TestIfTrueForAllFailsWhenSizeIsOneAndItsFalse()
        {
            // Arrange
            IAssertion<IEnumerable<bool>> values = MakeAssertion(1, 0);
            Exception x = null;

            // Act
            x = IsTrueForAllActHelper(values);

            // Assert
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void TestIfTrueForAllIsPassesWhenSizeIsOneAndItsTrue()
        {
            // Arrange
            IAssertion<IEnumerable<bool>> values = MakeAssertion(1);
            Exception x = null;

            // Act
            x = IsTrueForAllActHelper(values);

            // Assert
            Assert.IsNull(x);
        }

        [TestMethod]
        public void TestIfTrueForAllPassesWhenSizeIsMoreThanOneAndAllAreTrue()
        {
            // Arrange
            IAssertion<IEnumerable<bool>> values = MakeAssertion(2);
            Exception x = null;

            // Act
            x = IsTrueForAllActHelper(values);

            // Assert
            Assert.IsNull(x);
        }

        [TestMethod]
        public void TestIfTrueForAllFailsWhenSizeIsMoreThanOneAndFirstIsFalse()
        {
            // Arrange
            IAssertion<IEnumerable<bool>> values = MakeAssertion(2, 0);
            Exception x = null;

            // Act
            x = IsTrueForAllActHelper(values);

            // Assert
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void TestIfTrueForAllFailsWhenSizeIsMoreThanOneAndLastIsFalse()
        {
            // Arrange
            IAssertion<IEnumerable<bool>> values = MakeAssertion(2, 1);
            Exception x = null;

            // Act
            x = IsTrueForAllActHelper(values);

            // Assert
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void TestIfTrueForAllFailsWhenSizeIsMoreThanTwoAndMiddleIsFalse()
        {
            // Arrange
            IAssertion<IEnumerable<bool>> values = MakeAssertion(3, 1);
            Exception x = null;

            // Act
            x = IsTrueForAllActHelper(values);

            // Assert
            Assert.IsNotNull(x);
        }


        [TestMethod()]
        public void TestIfIsTrueForAnyFailsWhenCollectionIsEmpty()
        {
            // Arrange
            var assertion = MakeAssertion(0);

            // Act
            Exception x = IsTrueForAnyActHelper(assertion);

            // Assert
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void TestIfIsTrueForAnyFailsWhenSizeIsOneAndItsFalse()
        {
            // Arrange
            var assertion = MakeAssertion(1, 0);

            // Act
            Exception x = IsTrueForAnyActHelper(assertion);

            // Assert
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void TestIfIsTrueForAnyFailsWhenSizeIsOneAndItsTrue()
        {
            // Arrange
            var assertion = MakeAssertion(1);

            // Act
            Exception x = IsTrueForAnyActHelper(assertion);

            // Assert
            Assert.IsNull(x);
        }

        [TestMethod]
        public void TestIfIsTrueForAnyFailsWhenSizeIsMoreThanOneAndAllAreFalse()
        {
            // Arrange
            var assertion = MakeAssertion(2, 0, 1);

            // Act
            Exception x = IsTrueForAnyActHelper(assertion);

            // Assert
            Assert.IsNotNull(x);
        }

        [TestMethod]
        public void TestIfIsTrueForAnyPassesWhenSizeIsMoreThanOneAndOnlyFirstIsTrue()
        {
            // Arrange
            var assertion = MakeAssertion(2, 1);

            // Act
            Exception x = IsTrueForAnyActHelper(assertion);

            // Assert
            Assert.IsNull(x);
        }


        [TestMethod]
        public void TestIfIsTrueForAnyPassesWhenSizeIsMoreThanOneAndOnlyLastIsTrue()
        {
            // Arrange
            var assertion = MakeAssertion(2, 0);

            // Act
            Exception x = IsTrueForAnyActHelper(assertion);

            // Assert
            Assert.IsNull(x);
        }

        [TestMethod]
        public void TestIfIfIsTrueForAnyPassesWhenSizeIsMoreThanTwoAndOnlyMiddleIsTrue()
        {
            // Arrange
            var assertion = MakeAssertion(3, 0, 2);

            // Act
            Exception x = IsTrueForAnyActHelper(assertion);

            // Assert
            Assert.IsNull(x);
        }

        private static Exception IsTrueForAllActHelper(IAssertion<IEnumerable<bool>> values)
        {
            return CollectionActHelper(values, CollectionExtensions.IsTrueForAll(values, v => v));
        }

        private static Exception IsTrueForAnyActHelper(IAssertion<IEnumerable<bool>> values)
        {
            return CollectionActHelper(values, CollectionExtensions.IsTrueForAny(values, v => v));
        }

        private static Exception CollectionActHelper(IAssertion<IEnumerable<bool>> values, IVerifiable<IEnumerable<bool>> action)
        {
            Exception x = null;
            try
            {
                action.Now();
            }
            catch (Exception e)
            {
                x = e;
            }

            return x;
        }

        private static IAssertion<IEnumerable<bool>> MakeAssertion(int listSize, params int[] falseIndexes)
        {
            var target = Enumerable.Range(0, listSize)
                                  .Select(n => !falseIndexes.Contains(n));

            var mock = new Mock<IAssertion<IEnumerable<bool>>>();
            mock.SetupGet(obj => obj.Target)
                .Returns(target);

            return mock.Object;
        }
    }
}
