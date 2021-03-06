// <copyright file="AtomicEnumeratorUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using IX.StandardExtensions.Extensions;
using IX.StandardExtensions.Threading;
using Xunit;

namespace IX.UnitTests.IX.StandardExtensions
{
    /// <summary>
    /// Unit tests for the atomic enumerator.
    /// </summary>
    public class AtomicEnumeratorUnitTests
    {
        /// <summary>
        /// Unit test for the <see cref="AtomicEnumerator{TItem, TEnumerator}"/> correctness of enumeration.
        /// </summary>
        [Fact]
        public void AtomicEnumeratorCorrectEnumerationTest()
        {
            // ARRANGE
            var q = new List<int> { 1, 2, 3, 4, 5 };

            using (List<int>.Enumerator enumerator = q.GetEnumerator())
            {
                List<int> newList1 = new List<int>(5), newList2 = new List<int>(5);

                using (var ae = new AtomicEnumerator<int, List<int>.Enumerator>(enumerator, () => new ReadOnlySynchronizationLocker(null)))
                {
                    // ACT
                    while (ae.MoveNext())
                    {
                        newList1.Add(ae.Current);
                    }

                    ae.Reset();

                    while (ae.MoveNext())
                    {
                        newList2.Add(ae.Current);
                    }
                }

                // ASSERT
                Assert.True(q.SequenceEquals(newList1));
                Assert.True(q.SequenceEquals(newList2));
            }
        }
    }
}