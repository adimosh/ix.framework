﻿// <copyright file="ConcurrentObservableQueue{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using IX.Observable.DebugAide;
using IX.System.Threading;
using GlobalThreading = System.Threading;

namespace IX.Observable
{
    /// <summary>
    /// A queue that broadcasts its changes.
    /// </summary>
    /// <typeparam name="T">The type of items in the queue.</typeparam>
    [DebuggerDisplay("ConcurrentObservableQueue, Count = {Count}")]
    [DebuggerTypeProxy(typeof(QueueDebugView<>))]
    [CollectionDataContract(Namespace = Constants.DataContractNamespace, Name = "Observable{0}Queue", ItemName = "Item")]
    public class ConcurrentObservableQueue<T> : ObservableQueue<T>
    {
        private ReaderWriterLockSlim locker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableQueue{T}"/> class.
        /// </summary>
        public ConcurrentObservableQueue()
            : base()
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="collection">A collection of items to copy from.</param>
        public ConcurrentObservableQueue(IEnumerable<T> collection)
            : base(collection)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="capacity">The initial capacity of the queue.</param>
        public ConcurrentObservableQueue(int capacity)
            : base(capacity)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        public ConcurrentObservableQueue(global::System.Threading.SynchronizationContext context)
            : base(context)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="collection">A collection of items to copy from.</param>
        public ConcurrentObservableQueue(global::System.Threading.SynchronizationContext context, IEnumerable<T> collection)
            : base(context, collection)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrentObservableQueue{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context top use when posting observable messages.</param>
        /// <param name="capacity">The initial capacity of the queue.</param>
        public ConcurrentObservableQueue(GlobalThreading.SynchronizationContext context, int capacity)
            : base(context, capacity)
        {
            this.locker = new ReaderWriterLockSlim(GlobalThreading.LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Gets a synchronization lock item to be used when trying to synchronize read/write operations between threads.
        /// </summary>
        protected override IReaderWriterLock SynchronizationLock => this.locker;

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            this.locker.Dispose();

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Disposes the general context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            this.locker = null;

            base.DisposeGeneralContext();
        }
    }
}