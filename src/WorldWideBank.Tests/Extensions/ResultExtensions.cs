using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorldWideBank.Domain;

namespace WorldWideBank.Tests.Extensions
{
    public static class ResultExtensions
    {
        /// <summary>
        /// Helper method to assert that a <see cref="Result"/> did not error.
        /// If it did, then show the errors joined by <see cref="Environment.NewLine"/> in the test result.
        /// </summary>
        public static T AssertOk<T>(this Result<T> result)
        {
            Assert.IsTrue(result.IsOk, string.Join(Environment.NewLine, result.Errors));
            return result.Value;
        }
    }
}