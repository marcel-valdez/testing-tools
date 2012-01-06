﻿namespace TestingTools.Extensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestingTools.Core;

    public static class BooleanExtensions
    {
        public static IVerifiable<bool> IsTrue(this IAssertion<bool> condition)
        {
            return  new Verifiable<bool>(condition, target => Assert.IsTrue(target));
        }

        public static IVerifiable<bool> IsTrue(this IAssertion<bool> condition, string message)
        {
            return  new Verifiable<bool>(condition, target => Assert.IsTrue(target, message));
        }

        public static IVerifiable<bool> IsFalse(this IAssertion<bool> condition)
        {
            return new Verifiable<bool>(condition, target => Assert.IsFalse(target));
        }

        public static IVerifiable<bool> IsFalse(this IAssertion<bool> condition, string message)
        {
            return new Verifiable<bool>(condition, target => Assert.IsFalse(target, message));
        }
    }

}