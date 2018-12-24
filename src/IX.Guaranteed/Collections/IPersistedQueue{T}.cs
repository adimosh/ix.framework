// <copyright file="IPersistedQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.System.Collections.Generic;

namespace IX.Guaranteed.Collections
{
    /// <summary>
    /// A contract for a persisted queue.
    /// </summary>
    /// <typeparam name="T">The type of items in the queue.</typeparam>
    public interface IPersistedQueue<T> : IQueue<T>
    {
        /// <summary>
        /// Tries the load topmost item and execute an action on it, deleting the topmost object data if the operation is successful.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to send to the action.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the invoked action.</param>
        /// <returns>The number of items that have been dequeued.</returns>
        /// <remarks>
        /// <para>Warning! This method has the potential of overrunning its read/write lock timeouts. Please ensure that the <paramref name="predicate"/> method
        /// filters out items in a way that limits the amount of data passing through.</para>
        /// </remarks>
        int DequeueWhilePredicateWithAction<TState>(Func<TState, T, bool> predicate, Action<TState, IEnumerable<T>> actionToInvoke, TState state);

        /// <summary>
        /// De-queues an item from the queue, and executes the specified action on it.
        /// </summary>
        /// <typeparam name="TState">The type of the state object to pass to the action.</typeparam>
        /// <param name="actionToInvoke">The action to invoke.</param>
        /// <param name="state">The state object to pass to the action.</param>
        /// <returns><see langword="true"/> if the dequeuing is successful, and the action performed, <see langword="false"/> otherwise.</returns>
        bool DequeueWithAction<TState>(Action<TState, T> actionToInvoke, TState state);
    }
}