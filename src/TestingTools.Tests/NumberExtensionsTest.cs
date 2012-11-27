// -----------------------------------------------------------------------
// <copyright file="NumberExtensionsTest.cs" company="">
// Copyright 2012 Marcel Valdez
// </copyright>
// -----------------------------------------------------------------------
namespace TestingTools.Tests
{
    using NUnit.Framework;
    using TestingTools.Extensions;
    using TestingTools.Core;
    using System;

    /// <summary>
    /// Has all the tests for the number extensions.
    /// </summary>
    public class NumberExtensionsTest
    {        
        // IsLess
        /// <summary>
        /// Tests if it can check less than.
        /// </summary>
        /// <param name="targetNumber">The target number.</param>
        /// <param name="comparedNumber">The compared number.</param>
        /// <param name="fail">if set to <c>true</c> it should [fail].</param>
        [TestCase(-1, 0, false)]
        [TestCase(0, 1, false)]
        // IsEqual
        [TestCase(0, 0, true)]
        // IsGreater                
        [TestCase(1, 0, true)]
        public void TestIfItCanCheckLessThan(int targetNumber, int comparedNumber, bool fail)
        {
            // Arrange            
            IAssertion<int> target = Verify.That(targetNumber);

            // Act
            IVerifiable<int> actual = NumberExtensions.IsLessThan(target, comparedNumber);

            // Assert
            AssertNumberComparison(fail, actual);
        }

        /// <summary>
        /// Tests if it can check less than or equal.
        /// </summary>
        /// <param name="targetNumber">The target number.</param>
        /// <param name="comparedNumber">The compared number.</param>
        /// <param name="fail">if set to <c>true</c> it should [fail].</param>        
        // IsLess
        [TestCase(-1, 0, false)]
        [TestCase(0, 1, false)]
        // IsEqual
        [TestCase(0, 0, false)]
        // IsGreater                
        [TestCase(1, 0, true)]
        public void TestIfItCanCheckLessThanOrEqual(int targetNumber, int comparedNumber, bool fail)
        {
            // Arrange            
            IAssertion<int> target = Verify.That(targetNumber);

            // Act
            IVerifiable<int> actual = NumberExtensions.IsLessThanOrEqual(target, comparedNumber);

            // Assert
            AssertNumberComparison(fail, actual);
        }
        
        /// <summary>
        /// Tests if it can check greater than.
        /// </summary>
        /// <param name="targetNumber">The target number.</param>
        /// <param name="comparedNumber">The compared number.</param>
        /// <param name="fail">if set to <c>true</c> it should [fail].</param>        
        // IsLess
        [TestCase(-1, 0, true)]
        [TestCase(0, 1, true)]
        // IsEqual
        [TestCase(0, 0, true)]
        // IsGreater                
        [TestCase(1, 0, false)]
        public void TestIfItCanCheckGreaterThan(int targetNumber, int comparedNumber, bool fail)
        {
            // Arrange            
            IAssertion<int> target = Verify.That(targetNumber);

            // Act
            IVerifiable<int> actual = NumberExtensions.IsGreaterThan(target, comparedNumber);

            AssertNumberComparison(fail, actual);
        }

        // IsLess
        /// <summary>
        /// Tests if it can check greater than or equal.
        /// </summary>
        /// <param name="targetNumber">The target number.</param>
        /// <param name="comparedNumber">The compared number.</param>
        /// <param name="shouldFail">if set to <c>true</c> it should [fail].</param>
        [TestCase(-1, 0, true)]
        [TestCase(0, 1, true)]
        // IsEqual
        [TestCase(0, 0, false)]
        // IsGreater                
        [TestCase(1, 0, false)]
        public void TestIfItCanCheckGreaterThanOrEqual(int targetNumber, int comparedNumber, bool shouldFail)
        {
            // Arrange            
            IAssertion<int> target = Verify.That(targetNumber);

            // Act
            IVerifiable<int> actual = NumberExtensions.IsGreaterThanOrEqual(target, comparedNumber);

            // Assert
            AssertNumberComparison(shouldFail, actual);
        }

        /// <summary>
        /// Asserts that the number comparison throws (or not) an AssertionException.
        /// </summary>
        /// <param name="shouldThrow">if set to <c>true</c> if it should throw an AssertionException.</param>
        /// <param name="verification">The verification to assert for an exception.</param>
        private static void AssertNumberComparison(bool shouldThrow, IVerifiable<int> verification)
        {
            // Assert
            if (shouldThrow)
            {
                Assert.Throws<AssertionException>(() => verification.Now());
            }
            else
            {
                Assert.DoesNotThrow(() => verification.Now());
            }
        }
    }
}
