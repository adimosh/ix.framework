// <copyright file="Retry.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using IX.Retry.Contexts;
using IX.StandardExtensions.Contracts;
using JetBrains.Annotations;

namespace IX.Retry
{
    /// <summary>
    ///     A class for containing retry operations.
    /// </summary>
    [PublicAPI]
    public static partial class Retry
    {
        /// <summary>
        ///     Retry now, with a default set of options.
        /// </summary>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        public static void Now(
            [CanBeNull] Action action,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            Run(
                action,
                new RetryOptions(),
                cancellationToken);
        }

        /// <summary>
        ///     Retry now, asynchronously, with an optional cancellation token.
        /// </summary>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync(
            [CanBeNull] Action action,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            return RunAsync(
                action,
                new RetryOptions(),
                cancellationToken);
        }

        /// <summary>
        ///     Retry now, with specified options.
        /// </summary>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        public static void Now(
            [CanBeNull] Action action,
            [CanBeNull] RetryOptions options,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            Run(
                action,
                options,
                cancellationToken);
        }

        /// <summary>
        ///     Retry now, asynchronously, with specified options and an optional cancellation token.
        /// </summary>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static Task NowAsync(
            [CanBeNull] Action action,
            [CanBeNull] RetryOptions options,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            return RunAsync(
                action,
                options,
                cancellationToken);
        }

        /// <summary>
        ///     Retry now, with a method to build up options.
        /// </summary>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        public static void Now(
            [CanBeNull] Action action,
            [CanBeNull] Action<RetryOptions> optionsSetter,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));
            Contract.RequiresNotNull(
                in optionsSetter,
                nameof(optionsSetter));

            var options = new RetryOptions();
            optionsSetter(options);

            Run(
                action,
                options,
                cancellationToken);
        }

        /// <summary>
        ///     Retry now, asynchronously, with a method to build up options and an optional cancellation token.
        /// </summary>
        /// <param name="action">The action to try and retry.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task" /> that can be awaited on.</returns>
        public static async Task NowAsync(
            [CanBeNull] Action action,
            [CanBeNull] Action<RetryOptions> optionsSetter,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));
            Contract.RequiresNotNull(
                in optionsSetter,
                nameof(optionsSetter));

            var options = new RetryOptions();
            optionsSetter(options);

            await RunAsync(
                action,
                options,
                cancellationToken).ConfigureAwait(false);
        }
#pragma warning disable HAA0301 // Closure Allocation Source
#pragma warning disable HAA0302 // Display class allocation to capture closure - These are expected to happen here
#pragma warning disable HAA0603 // Delegate allocation from a method group
        /// <summary>
        ///     Retry an action later.
        /// </summary>
        /// <param name="action">The action to retry.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        public static Action Later(
            [CanBeNull] Action action,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));

            return () => Run(
                action,
                new RetryOptions(),
                cancellationToken);
        }

        /// <summary>
        ///     Retry an action later, with retry options.
        /// </summary>
        /// <param name="action">The action to retry.</param>
        /// <param name="options">The retry options.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        public static Action Later(
            [CanBeNull] Action action,
            [CanBeNull] RetryOptions options,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));
            Contract.RequiresNotNull(
                in options,
                nameof(options));

            return () => Run(
                action,
                options,
                cancellationToken);
        }

        /// <summary>
        ///     Retry an action later, with a method to build up options.
        /// </summary>
        /// <param name="action">The action to retry.</param>
        /// <param name="optionsSetter">A method to build up options on the fly.</param>
        /// <param name="cancellationToken">The current operation's cancellation token.</param>
        /// <returns>An <see cref="System.Action" /> that can be executed whenever required.</returns>
        public static Action Later(
            [CanBeNull] Action action,
            [CanBeNull] Action<RetryOptions> optionsSetter,
            CancellationToken cancellationToken = default)
        {
            Contract.RequiresNotNull(
                in action,
                nameof(action));
            Contract.RequiresNotNull(
                in optionsSetter,
                nameof(optionsSetter));

            var options = new RetryOptions();
            optionsSetter(options);

            return () => Run(
                action,
                options,
                cancellationToken);
        }
#pragma warning restore HAA0603 // Delegate allocation from a method group
#pragma warning restore HAA0302 // Display class allocation to capture closure
#pragma warning restore HAA0301 // Closure Allocation Source

        /// <summary>
        ///     Retry for a number of times.
        /// </summary>
        /// <param name="times">The number of times to retry. Has to be greater than 0.</param>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions Times(int times)
        {
            Contract.RequiresPositive(
                in times,
                nameof(times));

            return new RetryOptions
            {
                Type = RetryType.Times,
                RetryTimes = times,
            };
        }

        /// <summary>
        ///     Retry once.
        /// </summary>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions Once() => new RetryOptions
        {
            Type = RetryType.Times,
            RetryTimes = 1,
        };

        /// <summary>
        ///     Retry twice.
        /// </summary>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions Twice() => new RetryOptions
        {
            Type = RetryType.Times,
            RetryTimes = 2,
        };

        /// <summary>
        ///     Retry three times.
        /// </summary>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions ThreeTimes() => new RetryOptions
        {
            Type = RetryType.Times,
            RetryTimes = 3,
        };

        /// <summary>
        ///     Retry five times.
        /// </summary>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions FiveTimes() => new RetryOptions
        {
            Type = RetryType.Times,
            RetryTimes = 5,
        };

        /// <summary>
        ///     Retries for a specific time span.
        /// </summary>
        /// <param name="timeSpan">How long to retry, as a time span.</param>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions For(TimeSpan timeSpan)
        {
            Contract.RequiresPositive(
                in timeSpan,
                nameof(timeSpan));

            return new RetryOptions
            {
                Type = RetryType.For,
                RetryFor = timeSpan,
            };
        }

        /// <summary>
        ///     Retries for a specific number of milliseconds.
        /// </summary>
        /// <param name="milliseconds">How long to retry, in milliseconds.</param>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions For(int milliseconds)
        {
            Contract.RequiresNonNegative(
                in milliseconds,
                nameof(milliseconds));

            return new RetryOptions
            {
                Type = RetryType.For,
                RetryFor = TimeSpan.FromMilliseconds(milliseconds),
            };
        }

        /// <summary>
        ///     Retries until a specific condition is reached.
        /// </summary>
        /// <param name="conditionMethod">The condition method to evaluate.</param>
        /// <returns>The configured retry options.</returns>
        /// <remarks>
        ///     <para>
        ///         Retrying happens while the <paramref name="conditionMethod" /> method, when executed, returns
        ///         <see langword="true" />.
        ///     </para>
        ///     <para>On first instance that the method return is <see langword="false" />, retrying stops.</para>
        /// </remarks>
        public static RetryOptions Until([CanBeNull] RetryConditionDelegate conditionMethod)
        {
            Contract.RequiresNotNull(
                in conditionMethod,
                nameof(conditionMethod));

            return new RetryOptions
            {
                Type = RetryType.Until,
                RetryUntil = conditionMethod,
            };
        }

        /// <summary>
        ///     Configures an exception that, when thrown by the code being retried, prompts a retry.
        /// </summary>
        /// <typeparam name="T">The exception type to configure.</typeparam>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions OnException<T>()
            where T : Exception
        {
            var options = new RetryOptions();
#pragma warning disable HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
            options.RetryOnExceptions.Add(
                new Tuple<Type, Func<Exception, bool>>(
                    typeof(T),
                    null));
#pragma warning restore HAA0303 // Lambda or anonymous method in a generic method allocates a delegate instance
            return options;
        }

        /// <summary>
        ///     Configures an exception that, when thrown by the code being retried, prompts a retry, if the method to test for it
        ///     allows it.
        /// </summary>
        /// <typeparam name="T">The exception type to configure.</typeparam>
        /// <param name="testExceptionFunc">The method to test the exceptions with.</param>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions OnException<T>([CanBeNull] Func<Exception, bool> testExceptionFunc)
            where T : Exception
        {
            Contract.RequiresNotNull(
                in testExceptionFunc,
                nameof(testExceptionFunc));

            var options = new RetryOptions();
            options.RetryOnExceptions.Add(
                new Tuple<Type, Func<Exception, bool>>(
                    typeof(T),
                    testExceptionFunc));
            return options;
        }

        /// <summary>
        ///     Configures retry options to throw an exception at the end of the retrying process, if it was unsuccessful.
        /// </summary>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions ThrowException() => new RetryOptions
        {
            ThrowExceptionOnLastRetry = true,
        };

        /// <summary>
        ///     Waiting time between retries.
        /// </summary>
        /// <param name="milliseconds">The number of milliseconds to wait for.</param>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions WaitFor(int milliseconds)
        {
            Contract.RequiresPositive(
                in milliseconds,
                nameof(milliseconds));

            return new RetryOptions
            {
                WaitBetweenRetriesType = WaitType.For,
                WaitForDuration = TimeSpan.FromMilliseconds(milliseconds),
            };
        }

        /// <summary>
        ///     Waiting time between retries.
        /// </summary>
        /// <param name="timeSpan">The time span to wait between retries.</param>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions WaitFor(TimeSpan timeSpan)
        {
            Contract.RequiresPositive(
                in timeSpan,
                nameof(timeSpan));

            return new RetryOptions
            {
                WaitBetweenRetriesType = WaitType.For,
                WaitForDuration = timeSpan,
            };
        }

        /// <summary>
        ///     Waits a time span that is configured by a given delegate.
        /// </summary>
        /// <param name="waitMethod">The waiting delegate to give the time span.</param>
        /// <returns>The configured retry options.</returns>
        public static RetryOptions WaitUntil([CanBeNull] RetryWaitDelegate waitMethod)
        {
            Contract.RequiresNotNull(
                in waitMethod,
                nameof(waitMethod));

            return new RetryOptions
            {
                WaitBetweenRetriesType = WaitType.Until,
                WaitUntilDelegate = waitMethod,
            };
        }

        private static async Task RunAsync(
            [CanBeNull] Action action,
            [CanBeNull] RetryOptions options,
            CancellationToken cancellationToken)
        {
            Contract.RequiresNotNullPrivate(
                in action,
                nameof(action));
            Contract.RequiresNotNullPrivate(
                in options,
                nameof(options));
            cancellationToken.ThrowIfCancellationRequested();

            using (var context = new ActionRetryContext(
                action,
                options))
            {
                await context.BeginRetryProcessAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private static void Run(
            [CanBeNull] Action action,
            [CanBeNull] RetryOptions options,
            CancellationToken cancellationToken)
        {
            Contract.RequiresNotNullPrivate(
                in action,
                nameof(action));
            Contract.RequiresNotNullPrivate(
                in options,
                nameof(options));
            cancellationToken.ThrowIfCancellationRequested();

            using (var context = new ActionRetryContext(
                action,
                options))
            {
                context.BeginRetryProcess(cancellationToken);
            }
        }
    }
}