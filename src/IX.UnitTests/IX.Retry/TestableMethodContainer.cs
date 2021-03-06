// <copyright file="TestableMethodContainer.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;

namespace IX.UnitTests.IX.Retry
{
    internal static class TestableMethodContainer
    {
        public static void AlwaysFails(List<Exception> exceptions, ref int times)
        {
            if (times < 0)
            {
                throw new ArgumentException(nameof(times));
            }

            if (exceptions == null)
            {
                throw new ArgumentNullException(nameof(exceptions));
            }

            if (exceptions.Count == 0)
            {
                throw new ArgumentException(nameof(exceptions));
            }

            var index = times;
            while (index >= exceptions.Count)
            {
                index -= exceptions.Count;
            }

            times++;

            throw exceptions[index];
        }
    }
}