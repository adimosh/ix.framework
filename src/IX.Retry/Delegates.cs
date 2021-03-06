// <copyright file="Delegates.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IX.Retry
{
    /// <summary>
    ///     A delegate that, when invoked, should determine whether or not a retry should take place.
    /// </summary>
    /// <param name="retriedTimes">The number of times previously retried.</param>
    /// <param name="retryingSince">The date and time (in UTC) when retrying started.</param>
    /// <param name="exceptions">The exceptions that have been thrown until now.</param>
    /// <param name="options">The retry options.</param>
    /// <returns><see langword="true" /> whether retrying should occur, <see langword="false" /> otherwise.</returns>
    [PublicAPI]
    public delegate bool RetryConditionDelegate(
        int retriedTimes,
        DateTime retryingSince,
        IEnumerable<Exception> exceptions,
        RetryOptions options);

    /// <summary>
    ///     A delegate that, when invoked, should tell how much to wait between retry attempts.
    /// </summary>
    /// <param name="retriedTimes">The number of times previously retried.</param>
    /// <param name="retryingSince">The date and time (in UTC) when retrying started.</param>
    /// <param name="options">The retry options.</param>
    /// <returns>How much to wait, as a time span.</returns>
    [PublicAPI]
    public delegate TimeSpan RetryWaitDelegate(
        int retriedTimes,
        DateTime retryingSince,
        RetryOptions options);
}