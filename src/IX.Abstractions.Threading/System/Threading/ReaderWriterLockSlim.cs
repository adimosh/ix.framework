// <copyright file="ReaderWriterLockSlim.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using IX.StandardExtensions.ComponentModel;
using JetBrains.Annotations;

namespace IX.System.Threading
{
    /// <summary>
    /// A wrapper over <see cref="T:System.Threading.ReaderWriterLockSlim"/>, compatible with <see cref="IReaderWriterLock"/>.
    /// </summary>
    /// <seealso cref="IX.System.Threading.IReaderWriterLock" />
    [PublicAPI]
    public class ReaderWriterLockSlim : DisposableBase, IReaderWriterLock
    {
        private readonly bool lockerLocal;

#pragma warning disable IDISP002 // Dispose member. - It is disposed when neeed only
#pragma warning disable IDISP008 // Don't assign member with injected and created disposables. - We're not, actually
#pragma warning disable IDISP006 // Implement IDisposable. - It is
        private readonly global::System.Threading.ReaderWriterLockSlim locker;
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP008 // Don't assign member with injected and created disposables.
#pragma warning restore IDISP002 // Dispose member.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterLockSlim"/> class.
        /// </summary>
        public ReaderWriterLockSlim()
        {
            this.locker = new global::System.Threading.ReaderWriterLockSlim();
            this.lockerLocal = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterLockSlim"/> class.
        /// </summary>
        /// <param name="lockRecursionPolicy">The lock recursion policy.</param>
        public ReaderWriterLockSlim(global::System.Threading.LockRecursionPolicy lockRecursionPolicy)
        {
            this.locker = new global::System.Threading.ReaderWriterLockSlim(lockRecursionPolicy);
            this.lockerLocal = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderWriterLockSlim"/> class.
        /// </summary>
        /// <param name="locker">The existing locker.</param>
        public ReaderWriterLockSlim(global::System.Threading.ReaderWriterLockSlim locker)
        {
            this.locker = locker;
        }

        /// <summary>
        /// Gets a value indicating whether the current thread has a write lock held.
        /// </summary>
        /// <value><see langword="true"/> if the current thread has a write lock held; otherwise, <see langword="false"/>.</value>
        public bool IsWriteLockHeld => this.InvokeIfNotDisposed((lck) => lck.IsWriteLockHeld, this.locker);

        /// <summary>
        /// Gets a value indicating whether the current thread has an upgradeable lock held.
        /// </summary>
        /// <value><see langword="true"/> if the current thread has an upgradeable lock held; otherwise, <see langword="false"/>.</value>
        public bool IsUpgradeableReadLockHeld => this.InvokeIfNotDisposed((lck) => lck.IsUpgradeableReadLockHeld, this.locker);

        /// <summary>
        /// Gets a value indicating whether the current thread has a read lock held.
        /// </summary>
        /// <value><see langword="true"/> if the current thread has a read lock held; otherwise, <see langword="false"/>.</value>
        public bool IsReadLockHeld => this.InvokeIfNotDisposed((lck) => lck.IsReadLockHeld, this.locker);

        /// <summary>
        /// Performs an implicit conversion from <see cref="T:System.Threading.ReaderWriterLockSlim"/> to <see cref="ReaderWriterLockSlim"/>.
        /// </summary>
        /// <param name="lock">The locker.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ReaderWriterLockSlim(global::System.Threading.ReaderWriterLockSlim @lock) => new ReaderWriterLockSlim(@lock);

        /// <summary>
        /// Performs an implicit conversion from <see cref="ReaderWriterLockSlim"/> to <see cref="T:System.Threading.ReaderWriterLockSlim"/>.
        /// </summary>
        /// <param name="lock">The locker.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator global::System.Threading.ReaderWriterLockSlim(ReaderWriterLockSlim @lock) => @lock.locker;

        /// <summary>
        /// Enters a read lock.
        /// </summary>
        public void EnterReadLock() => this.InvokeIfNotDisposed((lck) => lck.EnterReadLock(), this.locker);

        /// <summary>
        /// Enters an upgradeable read lock.
        /// </summary>
        public void EnterUpgradeableReadLock() => this.InvokeIfNotDisposed((lck) => lck.EnterUpgradeableReadLock(), this.locker);

        /// <summary>
        /// Enters the write lock.
        /// </summary>
        public void EnterWriteLock() => this.InvokeIfNotDisposed((lck) => lck.EnterWriteLock(), this.locker);

        /// <summary>
        /// Exits a read lock.
        /// </summary>
        public void ExitReadLock() => this.InvokeIfNotDisposed((lck) => lck.ExitReadLock(), this.locker);

        /// <summary>
        /// Exits an upgradeable read lock.
        /// </summary>
        public void ExitUpgradeableReadLock() => this.InvokeIfNotDisposed((lck) => lck.ExitUpgradeableReadLock(), this.locker);

        /// <summary>
        /// Exits a write lock.
        /// </summary>
        public void ExitWriteLock() => this.InvokeIfNotDisposed((lck) => lck.ExitWriteLock(), this.locker);

        /// <summary>
        /// Tries to enter a read lock.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout, in milliseconds.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        public bool TryEnterReadLock(int millisecondsTimeout) => this.InvokeIfNotDisposed((lck, timeout) => lck.TryEnterReadLock(timeout), this.locker, millisecondsTimeout);

        /// <summary>
        /// Tries to enter a read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        public bool TryEnterReadLock(TimeSpan timeout) => this.InvokeIfNotDisposed((lck, timeoutInternal) => lck.TryEnterReadLock(timeoutInternal), this.locker, timeout);

        /// <summary>
        /// Tries to enter an upgradeable read lock.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout, in milliseconds.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        public bool TryEnterUpgradeableReadLock(int millisecondsTimeout) => this.InvokeIfNotDisposed((lck, timeout) => lck.TryEnterUpgradeableReadLock(timeout), this.locker, millisecondsTimeout);

        /// <summary>
        /// Tries to enter an upgradeable read lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        public bool TryEnterUpgradeableReadLock(TimeSpan timeout) => this.InvokeIfNotDisposed((lck, timeoutInternal) => lck.TryEnterUpgradeableReadLock(timeoutInternal), this.locker, timeout);

        /// <summary>
        /// Tries to enter a write lock.
        /// </summary>
        /// <param name="millisecondsTimeout">The timeout, in milliseconds.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        public bool TryEnterWriteLock(int millisecondsTimeout) => this.InvokeIfNotDisposed((lck, timeout) => lck.TryEnterWriteLock(timeout), this.locker, millisecondsTimeout);

        /// <summary>
        /// Tries to enter a write lock.
        /// </summary>
        /// <param name="timeout">The timeout.</param>
        /// <returns><see langword="true"/> if the lock has been acquired for the calling thread, <see langword="false"/> otherwise.</returns>
        public bool TryEnterWriteLock(TimeSpan timeout) => this.InvokeIfNotDisposed((lck, timeoutInternal) => lck.TryEnterWriteLock(timeoutInternal), this.locker, timeout);

        /// <summary>
        /// Disposes in the managed context.
        /// </summary>
        protected override void DisposeManagedContext()
        {
            base.DisposeManagedContext();

#pragma warning disable IDISP007 // Don't dispose injected. - We're not, the analyzer cannot tell though
            if (this.lockerLocal)
            {
                this.locker.Dispose();
            }
#pragma warning restore IDISP007 // Don't dispose injected.
        }
    }
}