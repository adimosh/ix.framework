// <copyright file="ObservableReadOnlyCompositeList{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;

using IX.Observable.Adapters;

using JetBrains.Annotations;

namespace IX.Observable
{
    /// <summary>
    /// An observable, composite, thread-safe and read-only list made of multiple lists of the same rank.
    /// </summary>
    /// <typeparam name="T">The type of the list item.</typeparam>
    /// <seealso cref="IDisposable" />
    /// <seealso cref="Observable.ObservableReadOnlyCollectionBase{T}" />
    [PublicAPI]
    public class ObservableReadOnlyCompositeList<T> : ObservableReadOnlyCollectionBase<T>
    {
#pragma warning disable IDISP002 // Dispose member. - It is
#pragma warning disable IDISP006 // Implement IDisposable. - It is
        /// <summary>
        /// The thread synchronization locker.
        /// </summary>
        private ReaderWriterLockSlim locker;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableReadOnlyCompositeList{T}"/> class.
        /// </summary>
        public ObservableReadOnlyCompositeList()
            : base(new MultiListListAdapter<T>())
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableReadOnlyCompositeList{T}"/> class.
        /// </summary>
        /// <param name="context">The synchronization context to use, if any.</param>
        public ObservableReadOnlyCompositeList(SynchronizationContext context)
            : base(new MultiListListAdapter<T>(), context)
        {
            this.locker = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        /// <summary>
        /// Sets a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        public void SetList<TList>(TList list)
            where TList : class, IEnumerable<T>, INotifyCollectionChanged
        {
            using (this.WriteLock())
            {
                ((MultiListListAdapter<T>)this.InternalContainer).SetList(list);
            }

            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
        }

        /// <summary>
        /// Removes a list.
        /// </summary>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        public void RemoveList<TList>(TList list)
            where TList : class, IEnumerable<T>, INotifyCollectionChanged
        {
            using (this.WriteLock())
            {
                ((MultiListListAdapter<T>)this.InternalContainer).RemoveList(list);
            }

            this.RaiseCollectionReset();
            this.RaisePropertyChanged(nameof(this.Count));
            this.RaisePropertyChanged(Constants.ItemsName);
        }

        /// <summary>
        /// Disposes the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            Interlocked.Exchange(ref this.locker, null)?.Dispose();

            base.DisposeManagedContext();
        }

        /// <summary>
        /// Disposes the general context.
        /// </summary>
        protected override void DisposeGeneralContext()
        {
            Interlocked.Exchange(ref this.locker, null);

            base.DisposeGeneralContext();
        }
    }
}