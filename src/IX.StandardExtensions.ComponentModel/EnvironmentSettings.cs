// <copyright file="EnvironmentSettings.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System.Threading;

namespace IX.StandardExtensions.ComponentModel
{
    /// <summary>
    /// Global settings on how the synchronization context should work.
    /// </summary>
    public static class EnvironmentSettings
    {
        /// <summary>
        /// Gets or sets the backup synchronization context. the default is <see langword="null"/> (<see langword="Nothing"/> in Visual Basic).
        /// </summary>
        /// <value>The backup synchronization context.</value>
        /// <remarks>
        /// <para>Controls in the IX Framework will always attempt to use a synchronization context when posting component model messages, such as notifications of
        /// property changes or collection changes.</para>
        /// <para>The way in which synchronization contexts are chosen to be used is in this order:</para>
        /// <list>
        /// <item>The explicit synchronization context sent via the constructor.</item>
        /// <item>If the property <see cref="AlwaysSuppressCurrentSynchronizationContext"/> is set to <see langword="true"/>, then the synchronization context set at
        /// <see cref="BackupSynchronizationContext"/>.</item>
        /// <item>The synchronization context at <see cref="SynchronizationContext.Current"/>.</item>
        /// <item>If the property <see cref="AlwaysSuppressCurrentSynchronizationContext"/> is set to <see langword="false"/>, then the synchronization context set at
        /// <see cref="BackupSynchronizationContext"/></item>
        /// <item>The default synchronization context for the respective type of operation (such as the <see cref="T:ThreadPoolSynchronizationContext"/>), where
        /// applicable.</item>
        /// <item><see langword="null"/> (<see langword="Nothing"/> in Visual Basic).</item>
        /// </list>
        /// </remarks>
        public static SynchronizationContext BackupSynchronizationContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to always suppress the current synchronization context. The default is false.
        /// </summary>
        /// <value><see langword="true"/> in order to always suppress the current synchronization context; otherwise, <see langword="false"/>.</value>
        /// <remarks>
        /// <para>If this property is set, the <see cref="SynchronizationContext.Current"/> property will be ignored, and the synchronization context at
        /// <see cref="BackupSynchronizationContext"/> will always be used, if it is set.</para>
        /// </remarks>
        public static bool AlwaysSuppressCurrentSynchronizationContext { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether invocations should be performed on the calling thread instead of a synchronization context.
        /// </summary>
        /// <value><see langword="true"/> to invoke synchronously on current thread; otherwise, to use the synchronization context, <see langword="false"/>.</value>
        public static bool InvokeSynchronouslyOnCurrentThread { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to invoke synchronously on the synchronization context. The default is false.
        /// </summary>
        /// <value><see langword="true"/> if invocations should be done synchronously; otherwise, <see langword="false"/>.</value>
        public static bool InvokeSynchronously { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to send a notification of reset if there is an exception on collection change notification. The default is false.
        /// </summary>
        /// <value><see langword="true"/> if a reset on collection change notification exception should be sent; otherwise, <see langword="false"/>.</value>
        public static bool ResetOnCollectionChangeNotificationException { get; set; }

        /// <summary>
        /// Gets the currently usable synchronization context, according to the framework rules, except for the explicit synchronization context.
        /// </summary>
        /// <returns>The currently usable synchronization context, according to the framework rules.</returns>
        public static SynchronizationContext GetUsableSynchronizationContext()
        {
            if (AlwaysSuppressCurrentSynchronizationContext)
            {
                if (BackupSynchronizationContext != null)
                {
                    return BackupSynchronizationContext;
                }
            }

            SynchronizationContext currentSyncContext = SynchronizationContext.Current;

            if (currentSyncContext != null)
            {
                return currentSyncContext;
            }

            if (BackupSynchronizationContext != null)
            {
                return BackupSynchronizationContext;
            }

            return null;
        }
    }
}