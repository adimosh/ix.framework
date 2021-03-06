// <copyright file="RetryUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IX.Retry;
using JetBrains.Annotations;
using Xunit;
using Xunit.Abstractions;

namespace IX.UnitTests.IX.Retry
{
    /// <summary>
    /// Unit tests for the retry system.
    /// </summary>
    public class RetryUnitTests
    {
        private readonly ITestOutputHelper output;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryUnitTests"/> class.
        /// </summary>
        /// <param name="output">The output.</param>
        public RetryUnitTests(ITestOutputHelper output)
        {
            this.output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Generates the failure the test data.
        /// </summary>
        /// <returns>A set of objects that represent the data.</returns>
        [UsedImplicitly]
        public static IEnumerable<object[]> FailureTestData() => new[]
        {
            new object[]
            {
                global::IX.Retry.Retry.Once(),
                new List<Exception>
                {
                    new InvalidOperationException(),
                },
                new Func<ReadOnlyCollection<Exception>, bool>(
                    exes =>
                        exes.Count == 1 && exes[0] is InvalidOperationException),
            },
            new object[]
            {
                global::IX.Retry.Retry.Twice(),
                new List<Exception>
                {
                    new InvalidOperationException(),
                    new FormatException(),
                },
                new Func<ReadOnlyCollection<Exception>, bool>(
                    exes =>
                        exes.Count == 2 && exes[0] is InvalidOperationException
                        && exes[1] is FormatException),
            },
            new object[]
            {
                global::IX.Retry.Retry.ThreeTimes(),
                new List<Exception>
                {
                    new InvalidOperationException(),
                    new FormatException(),
                },
                new Func<ReadOnlyCollection<Exception>, bool>(
                    exes =>
                        exes.Count == 3 && exes[0].GetType() == typeof(InvalidOperationException)
                        && exes[1].GetType() == typeof(FormatException)
                        && exes[2].GetType() == typeof(InvalidOperationException)),
            },
        };

        /// <summary>
        /// Tests failure and retry with specific options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="exceptions">The exceptions.</param>
        /// <param name="exceptionEvaluator">The exception evaluator.</param>
        [Theory(DisplayName = "Retry on failure")]
        [MemberData(nameof(FailureTestData))]
        public void Failure(RetryOptions options, List<Exception> exceptions, Func<ReadOnlyCollection<Exception>, bool> exceptionEvaluator)
        {
            this.output.WriteLine("Beginning test run...");

            // Arrange
            var times = 0;
            void Act() => TestableMethodContainer.AlwaysFails(exceptions, ref times);

            options.ThrowException();

            ReadOnlyCollection<Exception> exes = null;

            var hasThrown = true;

            // Act
            try
            {
                global::IX.Retry.Retry.Now(Act, options);

                this.output.WriteLine("Retrying has completed successfully, even though an exception was expected.");

                hasThrown = false;
            }
            catch (AggregateException ex)
            {
                this.output.WriteLine("AggregateException has been caught.");

                if (ex.InnerExceptions != null && ex.InnerExceptions.Count == 1 && ex.InnerExceptions[0] is AggregateException exception)
                {
                    ex = exception;
                }

                if (ex.InnerExceptions != null)
                {
                    this.output.WriteLine($"Inner exceptions: {ex.InnerExceptions.Count}.");
                    this.output.WriteLine(
                        string.Join(
                            ", ",
                            ex.InnerExceptions?.Select(p => p.GetType().Name)));
                }

                exes = ex.InnerExceptions;
            }
            catch (Exception ex)
            {
#pragma warning disable EPC12 // Suspicious exception handling: only Message property is observed in exception block. - This is an analyzer error
                this.output.WriteLine($"Exception has been caught: {ex.GetType()}, with message {ex.Message}.");
#pragma warning restore EPC12 // Suspicious exception handling: only Message property is observed in exception block.
                throw;
            }

            if (exes == null)
            {
                exes = new ReadOnlyCollection<Exception>(new Exception[0]);
            }

            // Assert
            Assert.True(hasThrown);
            Assert.True(exceptionEvaluator(exes));
        }

        /// <summary>
        /// Checks whether there is a retry on success.
        /// </summary>
        [Fact(DisplayName = "No retry on a success")]
        public void Success()
        {
            // Arrange
            var i = 0;

            // Act
            global::IX.Retry.Retry.Now(
                () => i++,
                (options) => options.FiveTimes());

            // Assert
            Assert.True(i == 1);
        }
    }
}